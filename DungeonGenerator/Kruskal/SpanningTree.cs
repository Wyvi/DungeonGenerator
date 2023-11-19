namespace DungeonGenerator.Kruskal
{
    public class SpanningTree : CellGraph
    {
        private List<Edge> unusedEdges = new List<Edge>();

        private SpanningTree() { }

        public static SpanningTree Create(CellGraph graph)
        {
            var tree = new SpanningTree();
            var graphCopy = graph.Copy();
            while (!graphCopy.IsEmpty())
            {
                var minEdge = graphCopy.PullOutMinimalEdge();
                tree.AddEdge(minEdge);
            }
            return tree;
        }


        public override void AddEdge(Edge edge)
        {
            if (!TryAddNotLooping(edge))
            {
                unusedEdges.Add(edge);
            }
        }


        public override void AddEdge(IEnumerable<Edge> edges)
        {
            edges.ToList().ForEach(e => AddEdge(e));
        }


        public void RemoveEdge(Edge edge)
        {
            edges.Remove(edge);
            foreach (var e in unusedEdges)
            {
                if (TryAddNotLooping(e))
                {
                    unusedEdges.Remove(e);
                    return;
                }
            }
        }


        private bool TryAddNotLooping(Edge edge)
        {
            if (!ExistPath(edge.FirstPointIndex, edge.SecondPointIndex))
            {
                edges.Add(edge);
                return true;
            }
            return false;
        }


        private bool ExistPath(int start, int end, List<Edge>? pathEdges = null)
        {
            if (pathEdges == null)
            {
                pathEdges = new List<Edge>();
            }

            var conectedEdges = NextEdges(start, pathEdges);
            if (conectedEdges.FirstOrDefault(e => e.ContainPoint(end)) != null)
            {
                return true;
            }
            pathEdges.AddRange(conectedEdges);

            var endPoints = conectedEdges.Select(e => e.AnotherPoint(start)).ToList();
            foreach (var point in endPoints)
            {
                if (ExistPath(point, end, pathEdges))
                {
                    return true;
                }
            }
            return false;
        }


        private List<Edge> EdgesWithPoint(int point)
        {
            return edges.Where(e => e.ContainPoint(point)).ToList();
        }


        private List<Edge> NextEdges(int point, List<Edge> pathEdges)
        {
            var edges = EdgesWithPoint(point);
            return edges.Where(e => !pathEdges.Contains(e)).ToList();
        }


    }
}
