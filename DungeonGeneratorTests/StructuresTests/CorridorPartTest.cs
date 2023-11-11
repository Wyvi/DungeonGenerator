using DungeonGenerator;
using DungeonGenerator.Structures;

namespace DungeonGeneratorTests.StructuresTests
{
    [TestClass]
    public class CorridorPartTest
    {
        [TestMethod]
        [DataRow (1,0,6)]
        [DataRow (0,5,-5)]
        public void CreateCorridor_CreateVertical_ReturnCoridorPart(int x, int y, int length)
        {
            var expected = new[]
            {
                new Vector2Int(x,y),
                new Vector2Int(x,y+length),
            };

            var part = CorridorPart.CreateCorridor(true, new Vector2Int(x, y), length, new DungeonSettings());
            var result = new[]
            {
                part.TopRightCorner,
                part.BottomLeftCorner,
            };

            CollectionAssert.AreEquivalent(expected, result);
        }


        [TestMethod]
        [DataRow(0, 1, 6)]
        [DataRow(5, 0, -5)]
        public void CreateCorridor_CreateHorizontal_ReturnCoridorPart(int x, int y, int length)
        {
            var expected = new[]
            {
                new Vector2Int(x,y),
                new Vector2Int(x+length,y),
            };

            var part = CorridorPart.CreateCorridor(false, new Vector2Int(x, y), length, new DungeonSettings());
            var result = new[]
            {
                part.TopRightCorner,
                part.BottomLeftCorner,
            };

            CollectionAssert.AreEquivalent(expected, result);
        }


        [TestMethod]
        public void CreateCorridor_ZeroLenght_ReturnNull()
        {
            var result = CorridorPart.CreateCorridor(false, new Vector2Int(0, 0), 0, new DungeonSettings());
            
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreateCorridor_CornerCoordinateLesThen0_ThrowArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() 
                => CorridorPart.CreateCorridor(false, new Vector2Int(0, 0), -1, new DungeonSettings())
                );
        }


    }
}
