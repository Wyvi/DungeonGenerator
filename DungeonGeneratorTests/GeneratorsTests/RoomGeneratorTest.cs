using DungeonGenerator.Generators;
using DungeonGenerator.Structures;

namespace DungeonGeneratorTests.GeneratorsTests
{
    [TestClass]
    public class RoomGeneratorTest
    {
        [TestMethod]
        public void GenerateLevel_CreateRoomsWithCorridors_ReturnSingleWalkableArea()
        {
            LevelParameters parameters = new LevelParameters(20, 20, 0.6);
            var caveGenerator = new CaveGenerator();

            var level = caveGenerator.GenerateLevel(parameters);

            var numOfWalkableAreas = level.NumOfConectedAreas();
            Assert.AreEqual(1, numOfWalkableAreas);
        }

        [TestMethod]
        public void GenerateLevel_CreateRoomInSmallLevel_ReturnSingleWalkableArea()
        {
            LevelParameters parameters = new LevelParameters(1, 1, 0.6);
            var caveGenerator = new CaveGenerator();

            var level = caveGenerator.GenerateLevel(parameters);

            var numOfWalkableAreas = level.NumOfConectedAreas();
            Assert.AreEqual(1, numOfWalkableAreas);
        }
    }
}
