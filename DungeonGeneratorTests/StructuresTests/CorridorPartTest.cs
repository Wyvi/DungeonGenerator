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
        public void Create_VerticalPart_ReturnCoridorPart(int x, int y, int length)
        {
            var expected = new[]
            {
                new Vector2Int(x,y),
                new Vector2Int(x,y+length),
            };

            var part = CorridorPart.Create(true, new Vector2Int(x, y), length);
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
        [DataRow(5, 0, 0)]
        public void Create_HorizontalPart_ReturnCoridorPart(int x, int y, int length)
        {
            var expected = new[]
            {
                new Vector2Int(x,y),
                new Vector2Int(x+length,y),
            };

            var part = CorridorPart.Create(false, new Vector2Int(x, y), length);
            var result = new[]
            {
                part.TopRightCorner,
                part.BottomLeftCorner,
            };

            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        public void Create_VerticalPartBetweenRooms_ReturnTrimmedCorridorPart()
        {
            var expected = new[]
            {
                new Vector2Int(2,4),
                new Vector2Int(2,7),
            };

            var room1 = new Room(new Vector2Int(0, 0), new Vector2Int(5, 5));
            var room2 = new Room(new Vector2Int(5, 5), new Vector2Int(5, 5));

            var part = CorridorPart.Create(true, room1, room2);
            var result = new[]
            {
                part.BottomLeftCorner,
                part.TopRightCorner,
            };

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Create_HorizontalPartBetweenRooms_ReturnTrimmedCorridorPart()
        {
            var expected = new[]
            {
                new Vector2Int(4,2),
                new Vector2Int(7,2),
            };

            var room1 = new Room(new Vector2Int(0, 0), new Vector2Int(5, 5));
            var room2 = new Room(new Vector2Int(5, 5), new Vector2Int(5, 5));

            var part = CorridorPart.Create(false, room1, room2);
            var result = new[]
            {
                part.BottomLeftCorner,
                part.TopRightCorner,
            };

            CollectionAssert.AreEqual(expected, result);
        }


        [TestMethod]
        public void Create_CornerCoordinateLesThenZero_ThrowArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() 
                => CorridorPart.Create(false, new Vector2Int(0, 0), -1)
                );
        }
    }
}
