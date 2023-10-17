using DungeonGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGeneratorTests
{
    [TestClass]
    public class RoomTest
    {
        [TestMethod]
        public void Overlaps_OverlapingRooms_ReturnsTrue()
        {
            var room1 = new Room(new Vector2Int(0, 0), new Vector2Int(1, 1));
            var room2 = new Room(new Vector2Int(0, 0), new Vector2Int(1, 1));

            var overlaps = room1.Overlaps(room2);

            Assert.IsTrue(overlaps);
        }

        [TestMethod]
        public void Overlaps_OverlapBorderWall_ReturnsTrue()
        {
            var room1 = new Room(new Vector2Int(0, 0), new Vector2Int(1, 1));
            var room2 = new Room(new Vector2Int(1, 1), new Vector2Int(1, 1));

            var overlaps = room1.Overlaps(room2);

            Assert.IsTrue(overlaps);
        }

        [TestMethod]
        public void Overlaps_NonOverlappingRooms_ReturnsFalse()
        {
            var room1 = new Room(new Vector2Int(0, 0), new Vector2Int(1, 1));
            var room2 = new Room(new Vector2Int(2, 2), new Vector2Int(1, 1));

            var overlaps = room1.Overlaps(room2);

            Assert.IsFalse(overlaps);
        }
    }
}
