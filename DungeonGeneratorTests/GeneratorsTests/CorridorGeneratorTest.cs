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
            var settings = new DungeonSettings(1, 1);
            var rooms = new List<Room>() {
                new Room(new Vector2Int(0,0) , new Vector2Int(1,1), settings),
                new Room(new Vector2Int(2,2) , new Vector2Int(1,1), settings),
            };
            var corridorParts = new CorridorGenerator(rooms, settings).Generate();

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

            var settings = new DungeonSettings(1, 1);
            var rooms = new List<Room>() {
                new Room(new Vector2Int(0,0) , new Vector2Int(1,1), settings),
                new Room(new Vector2Int(2,2) , new Vector2Int(1,1), settings),
            };

            var corridorParts = new CorridorGenerator(rooms, settings).Generate();
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
