using DungeonGenerator.Kruskal;

namespace DungeonGeneratorTests.KruskalTests
{
    [TestClass]
    public class EdgeTest
    {
        [TestMethod]
        [DataRow(-1, 0)]
        [DataRow(0, -1)]
        public void Constructor_IndexOutOfRange_ThrowArgumentException(int index1, int index2)
        {
            Assert.ThrowsException<ArgumentException>(() => new Edge(index1, index2, 0));
        }

        public void Constructor_SameIndexes_ThrowArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Edge(0, 0, 0));
        }

        [TestMethod]
        public void ContainPoint_InEdge_ReturnsTrue()
        {
            var edge = new Edge(1, 0, 0);

            var result = edge.ContainPoint(1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ContainPoint_OutOfEdge_ReturnsFalse()
        {
            var edge = new Edge(1, 0, 0);

            var result = edge.ContainPoint(2);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AnotherPoint_FirstPoint_ReturnsSecond()
        {
            var edge = new Edge(1, 0, 0);

            var result = edge.AnotherPoint(1);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void AnotherPoint_SecondPoint_ReturnsFirst()
        {
            var edge = new Edge(1, 0, 0);

            var result = edge.AnotherPoint(0);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void AnotherPoint_PointOutOfRange_ThrowArgumentException()
        {
            var edge = new Edge(1, 0, 0);

            Assert.ThrowsException<ArgumentException>(() => edge.AnotherPoint(2));
        }
    }
}
