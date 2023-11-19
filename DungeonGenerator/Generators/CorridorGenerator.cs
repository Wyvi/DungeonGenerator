using DungeonGenerator.Kruskal;
using DungeonGenerator.Structures;

namespace DungeonGenerator.Generators
{
    public class CorridorGenerator
    {
        private Dictionary<int, Room> rooms = new Dictionary<int, Room>();
        private List<int> roomsWithoutEdges = new List<int>();


        public CorridorGenerator(List<Room> rooms)
        {
            this.rooms = rooms.ToDictionary(i => rooms.IndexOf(i));
            roomsWithoutEdges = this.rooms.Keys.ToList();
        }


        public List<CorridorPart> Generate()
        {
            CellGraph graph = new CellGraph();

            while (roomsWithoutEdges.Count > 0)
            {
                graph.AddEdge(CreateEdges(ShiftRoomIndex()));
            }
            var spantree = SpanningTree.Create(graph);
            return FindCorridors(spantree);
        }


        private List<CorridorPart> FindCorridors(SpanningTree tree)
        {
            var path = tree.Edges();
            var corridors = new List<CorridorPart>();

            foreach (var edge in path)
            {
                var room1 = rooms[edge.FirstPointIndex];
                var room2 = rooms[edge.SecondPointIndex];
                if (!AddCorridor(room1, room2, corridors)
                    && !AddCorridor(room2, room1, corridors))
                {
                    tree.RemoveEdge(edge);
                    return FindCorridors(tree);
                }
            }
            return corridors;
        }


        private bool AddCorridor(Room room1, Room room2, List<CorridorPart> corridors)
        {
            var vertical = CorridorPart.Create(true, room1, room2);
            var horizontal = CorridorPart.Create(false, room2, room1);
            bool vertCollides = CollidesWithExisting(vertical, room1, room2, corridors);
            bool horColides = CollidesWithExisting(horizontal, room2, room1, corridors);

            if (vertCollides || horColides)
            {
                return false;
            }

            if (!vertical.IsWholeIn(room1))
            {
                corridors.Add(vertical);
            }
            if (!horizontal.IsWholeIn(room2))
            {
                corridors.Add(horizontal);
            }

            return true;
        }


        private bool CollidesWithExisting(CorridorPart part, Room start, Room end, List<CorridorPart> corridors)
        {
            if (!part.IsWholeIn(start))
            {
                return part.IsTouching(end)
                    || OverlapsRooms(part, start, end)
                    || part.IsTouching(corridors
                        .Where(c => c.IsVertical == part.IsVertical)
                        .Select(c => c as Rectangle).ToList());
            }
            return false;
        }


        private bool OverlapsRooms(CorridorPart part, Room room1, Room room2)
        {
            var roomOverlap = rooms.Select(r => r.Value)
                    .FirstOrDefault(room =>
                    room != room1 && room != room2
                    && room.IsOverlappingWithWall(part));

            return roomOverlap != null;
        }


        private List<Edge> CreateEdges(int roomIndex)
        {
            List<Edge> edges = new List<Edge>();
            var point1 = rooms[roomIndex].CenterNearCell;
            foreach (var index in roomsWithoutEdges)
            {
                var point2 = rooms[index].CenterNearCell;
                var distance = Math.Abs(point1.x - point2.x) + Math.Abs(point1.y - point2.y);
                edges.Add(new Edge(roomIndex, index, distance));
            }
            return edges;
        }


        private int ShiftRoomIndex()
        {
            var index = roomsWithoutEdges[0];
            roomsWithoutEdges.RemoveAt(0);
            return index;
        }

    }
}
