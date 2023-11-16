using DungeonGenerator;
using DungeonGenerator.Structures;

namespace DungeonGeneratorTests.StructuresTests
{
    [TestClass]
    public class LevelTest
    {
        [TestMethod]
        [DataRow(1, 1, TileType.wall)]
        [DataRow(0, 0, TileType.floor)]
        public void GetCell_CellInArray_ReturnsExpectedType(int x, int y, TileType expectedType)
        {
            var testingLevel = CreateTestingLevel();

            var cellType = testingLevel.GetCell(x, y);

            Assert.AreEqual(expectedType, cellType);
        }


        [TestMethod]
        [DataRow(-1, -1)]
        [DataRow(5, 5)]
        public void GetCell_CellOutOfArray_ReturnsWall(int x, int y)
        {
            var testingLevel = CreateTestingLevel();

            var cellType = testingLevel.GetCell(x, y);

            Assert.AreEqual(TileType.wall, cellType);
        }

        [TestMethod]
        [DataRow(0, 0, TileType.floor)]
        [DataRow(0, 2, TileType.wall)]
        [DataRow(1, 1, TileType.floor)]

        public void SetCell_CellInArray_ReturnsSameType(int x, int y, TileType type)
        {
            var testingLevel = CreateTestingLevel();

            testingLevel.SetCell(x, y, type);

            var cellType = testingLevel.GetCell(x, y);
            Assert.AreEqual(type, cellType);
        }

        [TestMethod]
        [DataRow(-1, -1, TileType.floor)]
        [DataRow(2, 2, TileType.floor)]
        public void SetCell_CellOutOfArray_ReturnsWall(int x, int y, TileType type)
        {
            var testingLevel = CreateTestingLevel();

            testingLevel.SetCell(x, y, type);

            var cellType = testingLevel.GetCell(x, y);
            Assert.AreEqual(TileType.wall, cellType);
        }

        [TestMethod]
        public void SetArea_SelectLargestArea_ReturnsLevelWithOneLargestArea()
        {
            var expectedLevelData = new TileType[,] {
                {TileType.wall, TileType.wall, TileType.floor, },
                {TileType.wall, TileType.wall, TileType.floor, },
            };
            var expectedLevel = new Level(expectedLevelData);

            var testingLevel = CreateTestingLevel();
            var area = CreateExpextedLargestAreaFromTestingLevel();

            testingLevel.SetArea(area);

            CollectionAssert.AreEqual(expectedLevel.LevelData(), testingLevel.LevelData());
        }


        [TestMethod]
        public void SetArea_AreaOutOfLevelRange_ReturnsLevelWithCroppedArea()
        {
            var expected = new[]{
                TileType.wall, TileType.floor,
            };

            var level = new Level(1, 2);
            var area = new Area();
            area.Add(new Vector2Int(0, 1));
            area.Add(new Vector2Int(1, 1));

            level.SetArea(area);

            CollectionAssert.AreEqual(expected, level.LevelData());
        }

        [TestMethod]
        public void AddArea_ReturnsLevelWithAddedArea()
        {
            var expected = new TileType[]{
                TileType.floor, TileType.floor, TileType.floor,
                TileType.wall, TileType.wall, TileType.floor,
            };

            var testingLevel = CreateTestingLevel();
            var area = new Area();
            area.Add(new Vector2Int(0, 1));

            testingLevel.AddArea(area);

            CollectionAssert.AreEqual(expected, testingLevel.LevelData());
        }

        [TestMethod]
        public void CreateFromArea_Area_ReturnsCreatingNewLevelContainingArea()
        {
            var expectedLeveData = new[] { TileType.wall, TileType.floor };
            var area = new Area();
            area.Add(new Vector2Int(0, 1));

            var level = Level.CreateFromArea(area);

            CollectionAssert.AreEqual(expectedLeveData, level.LevelData());
        }


        [TestMethod]
        public void FindLargestArea_LargestAreaFromTestingLevel_ReturnsExpectedArea()
        {
            var testingLevel = CreateTestingLevel();

            var actualArea = testingLevel.FindLargestArea();

            var expectedArea = CreateExpextedLargestAreaFromTestingLevel();
            CollectionAssert.AreEquivalent(expectedArea.GetCells(), actualArea.GetCells());
        }


        [TestMethod]
        [DataRow(0, 1, 3)]
        [DataRow(0, 0, 0)]
        [DataRow(1, 2, 1)]
        public void CountWalkableNeighbours_CellInArray_ReturnExpectedCount(int x, int y, int expectedCount)
        {
            var testingLevel = CreateTestingLevel();

            var neighborFloorCount = testingLevel.CountWalkableNeighbours(x, y);

            Assert.AreEqual(expectedCount, neighborFloorCount);
        }


        [TestMethod]
        [DataRow(-1, -1, 1)]
        [DataRow(-1, 3, 1)]
        [DataRow(5, 0, 0)]
        public void CountWalkableNeighbours_CellOutOfArray_ReturnNeighbourCount(int x, int y, int expectedCount)
        {
            var testingLevel = CreateTestingLevel();

            var neighborFloorCount = testingLevel.CountWalkableNeighbours(x, y);

            Assert.AreEqual(expectedCount, neighborFloorCount);
        }

        [TestMethod]
        public void Clear_ReturnLevelFillOfWalls()
        {
            var expected = new[]{
                TileType.wall, TileType.wall,
            };
            var level = new Level(
                new TileType[,]{
                { TileType.wall, TileType.floor, },
                });

            level.Clear();

            CollectionAssert.AreEqual(expected, level.LevelData());
        }


        [TestMethod]
        public void SetRectangles_SingleRectangle_ReturnsLevelWithSingleRectangle()
        {
            var expected = new TileType[]{
                TileType.wall, TileType.floor, TileType.wall,
                TileType.wall, TileType.wall, TileType.wall,
            };

            var testingLevel = CreateTestingLevel();
            var rectangle = new Room(new Vector2Int(0, 1), new Vector2Int(1, 1)) as Rectangle;
            List<Rectangle> rectangles = new List<Rectangle>();
            rectangles.Add(rectangle);

            testingLevel.SetRectangles(rectangles);

            CollectionAssert.AreEqual(expected, testingLevel.LevelData());
        }


        [TestMethod]
        public void SetRectangles_RectangleOutOfLevelRange_ReturnsLevelWithCroppedRectangle()
        {
            var expected = new TileType[]{
                TileType.wall, TileType.wall, TileType.wall,
                TileType.wall, TileType.wall, TileType.floor,
            };

            var testingLevel = CreateTestingLevel();
            var rectangle = new Room(new Vector2Int(1, 2), new Vector2Int(3, 3)) as Rectangle;
            List<Rectangle> rectangles = new List<Rectangle>();
            rectangles.Add(rectangle);

            testingLevel.SetRectangles(rectangles);
            
            CollectionAssert.AreEqual(expected, testingLevel.LevelData());
        }


        [TestMethod]
        public void AddRectangles_AddSingleRectangle_ReturnsLevelWithAddedRectangle()
        {
            var expected = new TileType[]{
                TileType.floor, TileType.floor, TileType.floor,
                TileType.wall, TileType.wall, TileType.floor,
            };

            var testingLevel = CreateTestingLevel();
            var rectangle = new Room(new Vector2Int(0, 1), new Vector2Int(1, 1)) as Rectangle;
            List<Rectangle> rectangles = new List<Rectangle>();
            rectangles.Add(rectangle);

            testingLevel.AddRectangles(rectangles);

            CollectionAssert.AreEqual(expected, testingLevel.LevelData());
        }


        private Level CreateTestingLevel()
        {
            TileType[,] levelTestData = new TileType[,] {
                {TileType.floor, TileType.wall, TileType.floor, },
                {TileType.wall, TileType.wall, TileType.floor, },
            };
            return new Level(levelTestData);
        }


        private Area CreateExpextedLargestAreaFromTestingLevel()
        {
            var expectedCells = new[] {
                new Vector2Int(0,2),
                new Vector2Int(1,2),
            };
            Area expectedArea = new Area();
            foreach (var cell in expectedCells)
            {
                expectedArea.Add(cell);
            }

            return expectedArea;
        }
    }
}
