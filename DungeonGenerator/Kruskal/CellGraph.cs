using System.Collections.ObjectModel;

namespace DungeonGenerator.Kruskal
{
    public class CellGraph
    {
        protected List<Edge> edges = new List<Edge>();


        public CellGraph() { }


        public CellGraph Copy()
        {
            var copy = new CellGraph();
            copy.AddEdge(edges);
            return copy;
        }


        public virtual void AddEdge(int firstPointIndex, int secondPointIndex, float distance)
        {
            AddEdge(new Edge(firstPointIndex, secondPointIndex, distance));
        }


        public virtual void AddEdge(Edge edge)
        {
            edges.Add(edge);
        }


        public virtual void AddEdge(IEnumerable<Edge> edges)
        {
            this.edges.AddRange(edges);
        }


        public Edge PullOutMinimalEdge()
        {
            var minEdge = edges.Min();
            edges.Remove(minEdge);
            return minEdge;
        }


        public ReadOnlyCollection<Edge> Edges()
        {
            return edges.AsReadOnly();
        }


        public bool IsEmpty()
        {
            return edges.Count == 0;
        }


        public override string ToString()
        {
            var array = edges.Select(e => e.ToString()).ToArray();
            var str = string.Join("\n", array);
            return str;
        }

    }
}
