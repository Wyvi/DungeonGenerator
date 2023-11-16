using DungeonGenerator;
using DungeonGenerator.Structures;

namespace DungeonGeneratorTests.StructuresTests
{
    [TestClass]
    public class RectangleTest
    {
        [TestMethod]
        public void Overlaps_OverlapingRooms_ReturnsTrue()
        {
            var room1 = new Room(new Vector2Int(0, 0), new Vector2Int(1, 1));
            var room2 = new Room(new Vector2Int(0, 0), new Vector2Int(1, 1));

            var overlaps = room1.OverlapsWall(room2);

            Assert.IsTrue(overlaps);
        }

        [TestMethod]
        public void Overlaps_OverlapBorderWall_ReturnsTrue()
        {
            var room1 = new Room(new Vector2Int(0, 0), new Vector2Int(1, 1));
            var room2 = new Room(new Vector2Int(1, 1), new Vector2Int(1, 1));

            var overlaps = room1.OverlapsWall(room2);

            Assert.IsTrue(overlaps);
        }

        [TestMethod]
        public void Overlaps_NonOverlappingRooms_ReturnsFalse()
        {
            var room1 = new Room(new Vector2Int(0, 0), new Vector2Int(1, 1));
            var room2 = new Room(new Vector2Int(2, 2), new Vector2Int(1, 1));

            var overlaps = room1.OverlapsWall(room2);

            Assert.IsFalse(overlaps);
        }
    }
}
