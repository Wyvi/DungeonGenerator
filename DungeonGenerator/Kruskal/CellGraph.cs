using System.Collections.ObjectModel;

namespace DungeonGenerator.Kruskal
{
    public class CellGraph
    {
        private List<Edge> edges;

        public CellGraph()
        {
            edges = new List<Edge>();
        }


        public void Add(int firstPointIndex, int secondPointIndex, float distance)
        {
            Add(new Edge(firstPointIndex, secondPointIndex, distance));
        }


        public void Add(Edge edge)
        {
            edges.Add(edge);
        }


        public Edge PullOutMinimalEdge()
        {
            var minEdge = edges.Min();
            edges.Remove(minEdge);
            return minEdge;
        }


        public void AddNotLoopingEdge(Edge edge)
        {
            if (!ExistPath(edge.FirstPointIndex, edge.SecondPointIndex))
            {
                edges.Add(edge);
            }
        }


        public ReadOnlyCollection<Edge> Edges()
        {
            return edges.AsReadOnly();
        }


        public bool IsEmpty()
        {
            return edges.Count == 0;
        }


        private bool ExistPath(int start, int end, List<Edge>? usedEdges = null)
        {
            if (usedEdges == null)
            {
                usedEdges = new List<Edge>();
            }

            var conectedEdges = UnusedEdges(start, usedEdges);
            if (conectedEdges.FirstOrDefault(e => e.ContainPoint(end)) != null)
            {
                return true;
            }
            usedEdges.AddRange(conectedEdges);

            var endPoints = conectedEdges.Select(e => e.AnotherPoint(start)).ToList();
            foreach (var point in endPoints)
            {
                if (ExistPath(point, end, usedEdges))
                {
                    return true;
                }
            }
            return false;
        }


        private List<Edge> AllPointEdges(int point)
        {
            return edges.Where(e => e.ContainPoint(point)).ToList();
        }


        private List<Edge> UnusedEdges(int point, List<Edge> usedEdges)
        {
            var edges = AllPointEdges(point);
            return edges.Where(e => !usedEdges.Contains(e)).ToList();
        }


        public override string ToString()
        {
            var array = edges.Select(e => e.ToString()).ToArray();
            var str = string.Join("\n", array);
            return str;
        }

    }
}
