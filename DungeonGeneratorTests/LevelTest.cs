using DungeonGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGeneratorTests
{
    [TestClass]
    public class LevelTest
    {
        [TestMethod]
        [DataRow(1, 1, TileType.wall)]
        [DataRow(3, 3, TileType.floor)]
        [DataRow(-1, -1, TileType.wall)]
        [DataRow(5, 5, TileType.wall)]
        public void GetCellFromLevel_CellInArray_ReturnsExpectedType(int x, int y, TileType expectedType)
        {
            var testingLevel = CreateTestingLevel();

            var cellType = testingLevel.GetCellFromLevel(x, y);

            Assert.AreEqual(expectedType, cellType);
        }


        [TestMethod]
        [DataRow(-1, -1)]
        [DataRow(5, 5)]
        public void GetCellFromLevel_CellOutOfArray_ReturnsWall(int x, int y)
        {
            var testingLevel = CreateTestingLevel();

            var cellType = testingLevel.GetCellFromLevel(x, y);
            
            Assert.AreEqual(TileType.wall, cellType);
        }

        [TestMethod]
        [DataRow(0, 0, TileType.floor)]
        [DataRow(0, 1, TileType.wall)]
        [DataRow(3, 4, TileType.floor)]
        [DataRow(0, 4, TileType.wall)]

        public void SetCellType_CellInArray_ReturnsSameType(int x, int y, TileType type)
        {
            var testingLevel = CreateTestingLevel();

            testingLevel.SetCellType(x, y, type);

            var cellType = testingLevel.GetCellFromLevel(x, y);
            Assert.AreEqual(type, cellType);
        }

        [TestMethod]
        [DataRow(-1, -1, TileType.floor)]
        [DataRow(5, 5, TileType.floor)]
        public void SetCellType_CellOutOfArray_ReturnsWall(int x, int y, TileType type)
        {
            var testingLevel = CreateTestingLevel();

            testingLevel.SetCellType(x, y, type);

            var cellType = testingLevel.GetCellFromLevel(x, y);
            Assert.AreEqual(TileType.wall, cellType);
        }

        [TestMethod]
        public void LevelFromArea_SelectLargestArea_ReturnsLevelWithOneLargestArea()
        {
            var expectedLevelData = new TileType[,] {
                {TileType.wall, TileType.wall, TileType.floor, TileType.floor, TileType.floor },
                {TileType.wall, TileType.wall, TileType.floor, TileType.floor, TileType.floor },
                {TileType.wall, TileType.wall, TileType.floor, TileType.floor, TileType.floor },
                {TileType.wall, TileType.wall, TileType.wall, TileType.floor, TileType.wall },
            };
            var expectedLevel = new Level(expectedLevelData);
            
            var testingLevel = CreateTestingLevel();
            var area = CreateExpextedLargestAreaFromTestingLevel();

            testingLevel.LevelFromArea(area);

            CollectionAssert.AreEqual(expectedLevel.LevelData(), testingLevel.LevelData());
        }


        [TestMethod]
        public void LevelFromArea_AreaOutOfLevelRange_ReturnsLevelWithCroppedArea()
        {
            var expected = new[]{
                TileType.wall, TileType.floor,
            };

            var level = new Level(1, 2);
            var area = new Area();
            area.Add(new Vector2Int(0, 1));
            area.Add(new Vector2Int(1, 1));

            level.LevelFromArea(area);

            CollectionAssert.AreEqual(expected, level.LevelData());
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
        [DataRow(2, 2, 5)]
        [DataRow(0, 0, 0)]
        [DataRow(1, 3, 8)]
        public void CountWalkableNeighbours_CellInArray_ReturnExpectedCount(int x, int y, int expectedCount)
        {
            var testingLevel = CreateTestingLevel();
            
            var neighborFloorCount = testingLevel.CountWalkableNeighbours(x, y);

            Assert.AreEqual(expectedCount, neighborFloorCount);
        }


        [TestMethod]
        [DataRow(-1, -1, 1)]
        [DataRow(-1, 3, 3)]
        [DataRow(5, 0, 0)]
        public void CountWalkableNeighbours_CellOutOfArray_ReturnNeighbourCount(int x, int y, int expectedCount)
        {
            var testingLevel = CreateTestingLevel();
            
            var neighborFloorCount = testingLevel.CountWalkableNeighbours(x, y);

            Assert.AreEqual(expectedCount, neighborFloorCount);
        }


        private Level CreateTestingLevel()
        {
            TileType[,] levelTestData = new TileType[,] {
                {TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor },
                {TileType.wall, TileType.wall, TileType.floor, TileType.floor, TileType.floor },
                {TileType.floor, TileType.wall, TileType.floor, TileType.floor, TileType.floor },
                {TileType.floor, TileType.floor, TileType.wall, TileType.floor, TileType.wall },
            };
            return new Level(levelTestData);
        }

        private Area CreateExpextedLargestAreaFromTestingLevel()
        {
            var expectedCells = new[] {
                new Vector2Int(0,2), new Vector2Int(0,3), new Vector2Int(0,4),
                new Vector2Int(1,2), new Vector2Int(1,3), new Vector2Int(1,4),
                new Vector2Int(2,2), new Vector2Int(2,3), new Vector2Int(2,4),
                                     new Vector2Int(3,3),
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
