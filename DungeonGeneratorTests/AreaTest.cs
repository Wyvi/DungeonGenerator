using DungeonGenerator;

namespace DungeonGeneratorTests
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

        [TestMethod]
        public void AddCorridor_StraightXLineInToEmptyArea_ReturnsAreaWithoutLastCell()
        {
            var expected = new[]
            {
                new Vector2Int(0, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, 2),
            };

            var area = new Area();
            area.AddCorridor(new Vector2Int(0, 0), new Vector2Int(0, 3));
            var result = area.GetCells();

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AddCorridor_StraightYLineInToEmptyArea_ReturnsAreaWithoutLastCell()
        {
            var expected = new[]
            {
                new Vector2Int(0, 0),
                new Vector2Int(1, 0),
                new Vector2Int(2, 0),
            };

            var area = new Area();
            area.AddCorridor(new Vector2Int(0, 0), new Vector2Int(3, 0));
            var result = area.GetCells();

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AddCorridor_StraightLineInToFilledArea_ReturnsAreaWithCorridor()
        {
            var content = new Vector2Int(4, 4);
            var expected = new[]
            {
                content,
                new Vector2Int(0, 0),
                new Vector2Int(1, 0),
            };

            var area = new Area();
            area.Add(content);
            area.AddCorridor(new Vector2Int(0, 0), new Vector2Int(2, 0));
            var result = area.GetCells();

            CollectionAssert.AreEqual(expected, result);
        }



    }
}