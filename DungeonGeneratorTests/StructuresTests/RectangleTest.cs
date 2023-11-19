using DungeonGenerator;
using DungeonGenerator.Structures;

namespace DungeonGeneratorTests.StructuresTests
{
    [TestClass]
    public class RectangleTest
    {
        Room room = new Room(new Vector2Int(0, 0), new Vector2Int(1, 1));

        Room overlapping = new Room(new Vector2Int(0, 0), new Vector2Int(1, 2));
        Room touching = new Room(new Vector2Int(1, 1), new Vector2Int(1, 1));
        Room nonOverlapping = new Room(new Vector2Int(2, 2), new Vector2Int(1, 1));


        [TestMethod]
        public void IsOverlapping_OverlappingRoom_ReturnsTrue()
        {
            var result = room.IsOverlapping(overlapping);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsOverlapping_TouchingRoom_ReturnsFalse()
        {
            var result = room.IsOverlapping(touching);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsOverlappingWithWall_OverlappingRoom_ReturnsTrue()
        {
            var result = room.IsOverlappingWithWall(overlapping);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsOverlappingWithWall_TouchingRoom_ReturnsTrue()
        {
            var result = room.IsOverlappingWithWall(touching);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsOverlappingWithWall_NonOverlappingRoom_ReturnsFalse()
        {
            var result = room.IsOverlappingWithWall(nonOverlapping);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsTouching_TouchingRoom_ReturnsTrue()
        {
            var result = room.IsTouching(touching);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsTouching_OverlappingRoom_ReturnsFalse()
        {
            var result = room.IsTouching(overlapping);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsTouching_NonOverlappingRoom_ReturnsFalse()
        {
            var result = room.IsTouching(nonOverlapping);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsTouching_ListOfNotTouchingRooms_ReturnsFalse()
        {
            var rooms = new List<Rectangle>(new[] {overlapping,nonOverlapping});
            
            var result = room.IsTouching(rooms);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsTouching_ListWithTouchingRoom_ReturnsTrue()
        {
            var rooms = new List<Rectangle>(new[] { overlapping, nonOverlapping, touching });
            
            var result = room.IsTouching(rooms);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsWholeIn_SameRoom_ReturnTrue()
        {
            var result = room.IsWholeIn(room);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsWholeIn_SmallerRoom_ReturnFalse()
        {
            var result = overlapping.IsWholeIn(room);

            Assert.IsFalse(result);
        }

    }
}
