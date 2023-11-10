using DungeonGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonGenerator.Corridor;
using DungeonGenerator.Rooms;

namespace DungeonGeneratorTests.CorridorTests
{
    [TestClass]
    public class CorridorGeneratorTest
    {
        [TestMethod]
        public void Generate_TwoRooms_ReturnsAreaConnectingRooms()
        {
            var expected = new[] {
                new Vector2Int(0, 0),
                new Vector2Int(1, 0),
                new Vector2Int(2, 0),
                new Vector2Int(2, 1),
            };

            var settings = new DungeonSettings(1, 1);
            var rooms = new List<Room>() {
                new Room(new Vector2Int(0,0) , new Vector2Int(1,1), settings),
                new Room(new Vector2Int(2,2) , new Vector2Int(1,1), settings),
            };
            var generator = new CorridorGenerator(rooms);

            var result = generator.Generate();

            CollectionAssert.AreEqual(expected, result.GetCells());
        }
    }
}
