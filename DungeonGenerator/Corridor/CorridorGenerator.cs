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
        private Dictionary<int, Room> rooms = new Dictionary<int, Room>();
        private List<int> remainingRoomIndexes = new List<int>();
        private CellGraph graph = new CellGraph();

        public CorridorGenerator(List<Room> rooms)
        {
            this.rooms = rooms.ToDictionary(i => rooms.IndexOf(i));
            remainingRoomIndexes = this.rooms.Keys.ToList();
        }

        public Area Generate()
        {
            var corridorArea = new Area();

            while (remainingRoomIndexes.Count > 0)
            {
                CreateEdges(ShiftRoomIndex());

            }
            var path = SpaningTree().Edges();
            foreach (var edge in path)
            {
                var cell1 = rooms[edge.FirstPointIndex].CenterNearCell;
                var cell2 = rooms[edge.SecondPointIndex].CenterNearCell;
                corridorArea.AddCorridor(cell1, cell2);
            }
            return corridorArea;
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
                var distance = Vector2Int.Distance(point1, point2);
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
