using DungeonGenerator.Kruskal;

namespace DungeonGeneratorTests.KruskalTests
{
    [TestClass]
    public class CellGraphTest
    {
        [TestMethod]
        public void Copy_CopyGraph_ReturnGraphWithSameNumOfEdges()
        {
            var expected = 1;
            
            CellGraph graph = new CellGraph();
            graph.AddEdge(0, 1, 0);
            var result = graph.Copy();

            Assert.AreEqual(expected, result.Edges().Count);
        }

        [TestMethod]
        public void Copy_CopyGraph_ReturnNewObject()
        {
            CellGraph graph = new CellGraph();
            graph.AddEdge(0, 1, 0);
            var result = graph.Copy();

            Assert.AreNotEqual(graph, result);
        }

        [TestMethod]
        public void AddEdge_MultipleEdges_ReturnGreaterNumOfEdges()
        {
            List<Edge> edges = new List<Edge>();
            edges.Add(new Edge(0, 1, 0));
            edges.Add(new Edge(0, 2, 0));
            edges.Add(new Edge(1, 2, 0));
            var expected = edges.Count;

            var graph = new CellGraph();
            graph.AddEdge(edges);

            var result = graph.Edges().Count;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PullOutMinimalEdge_EmptyGraph_ReturnNull()
        {
            var graph = new CellGraph();

            var result = graph.PullOutMinimalEdge();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void PullOutMinimalEdge_FilledGraph_ReturnMinimalEdge()
        {
            var expected = new Edge(0, 2, 0).ToString();

            var graph = new CellGraph();
            graph.AddEdge(0, 1, 2);
            graph.AddEdge(0, 2, 0);
            graph.AddEdge(1, 2, 1);

            var result = graph.PullOutMinimalEdge().ToString();

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void IsEmpty_EmptyGraph_ReturnsTrue()
        {
            var graph = new CellGraph();

            var result = graph.IsEmpty();

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void IsEmpty_FullGraph_ReturnsFalse()
        {
            var graph = new CellGraph();
            graph.AddEdge(0, 1, 0);

            var result = graph.IsEmpty();

            Assert.IsFalse(result);
        }
    }
}
