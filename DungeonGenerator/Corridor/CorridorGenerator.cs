using DungeonGenerator.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator.Corridor
{
    public class CorridorGenerator
    {
        private DungeonSettings dungeonSettings;
        private Dictionary<int, Room> rooms = new Dictionary<int, Room>();
        private List<int> remainingRoomIndexes = new List<int>();
        private CellGraph graph = new CellGraph();


        public CorridorGenerator(List<Room> rooms, DungeonSettings dungeonSettings)
        {
            this.rooms = rooms.ToDictionary(i => rooms.IndexOf(i));
            remainingRoomIndexes = this.rooms.Keys.ToList();
            this.dungeonSettings = dungeonSettings;
        }

        public List<CorridorPart> Generate()
        {
            var corridors = new List<CorridorPart>();

            while (remainingRoomIndexes.Count > 0)
            {
                CreateEdges(ShiftRoomIndex());

            }
            var path = SpaningTree().Edges();
            foreach (var edge in path)
            {
                AddCorridor(edge,corridors);
            }
            return corridors;
        }

        private bool AddCorridor(Edge edge, List<CorridorPart> corridors)
        {
            var point1 = rooms[edge.FirstPointIndex].CenterNearCell;
            var point2 = rooms[edge.SecondPointIndex].CenterNearCell;

            var vertical = CorridorPart.CreateCorridor(true, point1, point2.Y - point1.Y,dungeonSettings);
            var horizontal = CorridorPart.CreateCorridor(false, point2, point1.X - point2.X,dungeonSettings);

            //kontrola kolize s ostatnimi koridory a mistnostmi
            
            if(vertical != null)
            {
                corridors.Add(vertical);
            }
            if (horizontal != null)
            {
                corridors.Add(horizontal);
            }

            return true;
        }

        //Kruskal's 
        private CellGraph SpaningTree()
        {
            var tree = new CellGraph();
            while (!graph.IsEmpty())
            {
                var minEdge = graph.PullOutMinimalEdge();
                tree.AddNotLoopingEdge(minEdge);
            }
            return tree;
        }

        private void CreateEdges(int roomIndex)
        {
            var point1 = rooms[roomIndex].CenterNearCell;
            foreach (var index in remainingRoomIndexes)
            {
                var point2 = rooms[index].CenterNearCell;
                var distance = Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
                graph.Add(new Edge(roomIndex, index, distance));
            }
        }

        private int ShiftRoomIndex()
        {
            var index = remainingRoomIndexes[0];
            remainingRoomIndexes.RemoveAt(0);
            return index;
        }



    }
}
