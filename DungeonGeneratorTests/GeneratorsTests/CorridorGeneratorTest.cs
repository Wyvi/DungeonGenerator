using DungeonGenerator;
using DungeonGenerator.Generators;
using DungeonGenerator.Structures;

namespace DungeonGeneratorTests.GeneratorsTests
{
    [TestClass]
    public class CorridorGeneratorTest
    {
        [TestMethod]
        public void Generate_TwoRooms_ReturnsTwoCorridorParts()
        {
            var rooms = new List<Room>() {
                new Room(new Vector2Int(0,0) , new Vector2Int(1,1)),
                new Room(new Vector2Int(2,2) , new Vector2Int(1,1)),
            };
            var corridorParts = new CorridorGenerator(rooms).Generate();

            var result = corridorParts.Count;

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Generate_TwoRooms_ReturnsCorridorConnectingRooms()
        {
            var expected = new[] {
                new Vector2Int(0, 0),
                new Vector2Int(0, 2),
                new Vector2Int(0, 2),
                new Vector2Int(2, 2),
            };

            var rooms = new List<Room>() {
                new Room(new Vector2Int(0,0) , new Vector2Int(1,1)),
                new Room(new Vector2Int(2,2) , new Vector2Int(1,1)),
            };

            var corridorParts = new CorridorGenerator(rooms).Generate();
            var result = new List<Vector2Int>();
            corridorParts.ForEach(part =>
            {
                result.Add(part.BottomLeftCorner);
                result.Add(part.TopRightCorner);
            });

            CollectionAssert.AreEquivalent(expected, result);
        }
    }
}
