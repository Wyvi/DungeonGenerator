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
            SetRoomsWithoutEdges();
        }


        public List<CorridorPart> Generate()
        {
            var corridors = new List<CorridorPart>();
            CellGraph graph = new CellGraph();

            while (roomsWithoutEdges.Count > 0)
            {
                graph.AddRange(CreateEdges(ShiftRoomIndex()));
            }
            var path = SpaningTree(graph).Edges();
            foreach (var edge in path)
            {
                AddCorridor(edge, corridors);
            }
            return corridors;
        }


        private void SetRoomsWithoutEdges()
        {
            roomsWithoutEdges = this.rooms.Keys.ToList();
            
        }


        private bool AddCorridor(Edge edge, List<CorridorPart> corridors)
        {
            var point1 = rooms[edge.FirstPointIndex].CenterNearCell;
            var point2 = rooms[edge.SecondPointIndex].CenterNearCell;

            var vertical = CorridorPart
                .CreateCorridor(true, point1, point2.Y - point1.Y);
            var horizontal = CorridorPart
                .CreateCorridor(false, point2, point1.X - point2.X);

            if (vertical != null)
            {
                corridors.Add(vertical);
            }
            if (horizontal != null)
            {
                corridors.Add(horizontal);
            }

            return true;
        }


        private CellGraph SpaningTree(CellGraph graph)
        {
            var tree = new CellGraph();
            while (!graph.IsEmpty())
            {
                var minEdge = graph.PullOutMinimalEdge();
                tree.AddNotLoopingEdge(minEdge);
            }
            return tree;
        }

        private List<Edge> CreateEdges(int roomIndex)
        {
            List<Edge> edges = new List<Edge>();
            var point1 = rooms[roomIndex].CenterNearCell;
            foreach (var index in roomsWithoutEdges)
            {
                var point2 = rooms[index].CenterNearCell;
                var distance = Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
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
