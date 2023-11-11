using DungeonGenerator;
using DungeonGenerator.Structures;

namespace DungeonGeneratorTests.StructuresTests
{
    [TestClass]
    public class AreaTests
    {
        [TestMethod]
        public void Add_SingleCell_ReturnListWithSameCell()
        {
            var area = new Area();
            area.Add(new Vector2Int(0, 0));

            var cells = area.GetCells();

            CollectionAssert.AreEqual(new[] { new Vector2Int(0, 0) }, cells);
        }

        [TestMethod]
        public void Add_DuplicitCells_ReturnListWithSingleCell()
        {
            var area = new Area();
            area.Add(new Vector2Int(1, 0));
            area.Add(new Vector2Int(1, 0));

            var cells = area.GetCells();

            CollectionAssert.AreEqual(new[] { new Vector2Int(1, 0) }, cells);
        }


        [TestMethod]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(2, 4)]
        public void Count_CreateSquareArea_ReturnCountOfCells(int size, int expectedCount)
        {
            var area = new Area();
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    area.Add(new Vector2Int(x, y));
                }
            }

            var count = area.Count();

            Assert.AreEqual(expectedCount, count);
        }


    }
}