using DungeonGenerator.Kruskal;

namespace DungeonGeneratorTests.KruskalTests
{
    [TestClass]
    public class SpanningTreeTest
    {
        [TestMethod]
        public void AddEdge_NotLoopingEdge_ReturnsGraphWithLargerSize()
        {
            var expected = 3;

            var graph = new CellGraph();
            graph.AddEdge(0, 1, 2);
            graph.AddEdge(0, 2, 0);

            var spanTree = SpanningTree.Create(graph);
            spanTree.AddEdge(new Edge(1, 3, 1));
            var result = spanTree.Edges().Count();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AddEdge_LoopingEdge_ReturnsGraphWithOriginalSize()
        {
            var expected = 2;

            var graph = new CellGraph();
            graph.AddEdge(0, 1, 2);
            graph.AddEdge(0, 2, 0);

            var spanTree = SpanningTree.Create(graph);
            spanTree.AddEdge(1, 2, 1);
            var result = spanTree.Edges().Count();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AddEdge_MultipleLoopingEdges_ReturnNumOfNotLoopingEdges()
        {
            var expected = 2;

            List<Edge> edges = new List<Edge>();
            edges.Add(new Edge(0, 1, 0));
            edges.Add(new Edge(0, 2, 0));
            edges.Add(new Edge(1, 2, 0));
            var tree = SpanningTree.Create(new CellGraph());
            tree.AddEdge(edges);

            var result = tree.Edges().Count;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void RemoveEdge_NonReplaceableEdge_ReturnSmallerNumOfEdges()
        {
            var expected = 1;

            List<Edge> edges = new List<Edge>();
            edges.Add(new Edge(0, 1, 0));
            edges.Add(new Edge(0, 2, 0));
            var tree = SpanningTree.Create(new CellGraph());
            tree.AddEdge(edges);
            tree.RemoveEdge(edges[0]);

            var result = tree.Edges().Count;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void RemoveEdge_ReplaceableEdge_ReturnSameNumOfEdges()
        {
            var expected = 2;

            List<Edge> edges = new List<Edge>();
            edges.Add(new Edge(0, 1, 0));
            edges.Add(new Edge(0, 2, 0));
            edges.Add(new Edge(1, 2, 0));
            var tree = SpanningTree.Create(new CellGraph());
            tree.AddEdge(edges);
            tree.RemoveEdge(edges[0]);

            var result = tree.Edges().Count;

            Assert.AreEqual(expected, result);
        }
    }
}
