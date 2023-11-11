using DungeonGenerator.Kruskal;

namespace DungeonGeneratorTests.KruskalTests
{
    [TestClass]
    public class CellGraphTest
    {
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
            graph.Add(0, 1, 2);
            graph.Add(0, 2, 0);
            graph.Add(1, 2, 1);

            var result = graph.PullOutMinimalEdge().ToString();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AddNotLoopingEdge_LoopingEdge_ReturnsGraphWithOriginalSize()
        {
            var expected = 2;

            var graph = new CellGraph();
            graph.Add(0, 1, 2);
            graph.Add(0, 2, 0);

            graph.AddNotLoopingEdge(new Edge(1, 2, 1));
            var result = graph.Edges().Count();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AddNotLoopingEdge_NotLoopingEdge_ReturnsGraphWithLargerSize()
        {
            var expected = 3;

            var graph = new CellGraph();
            graph.Add(0, 1, 2);
            graph.Add(0, 2, 0);

            graph.AddNotLoopingEdge(new Edge(1, 3, 1));
            var result = graph.Edges().Count();

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
            graph.Add(0, 1, 0);

            var result = graph.IsEmpty();

            Assert.IsFalse(result);
        }
    }
}
