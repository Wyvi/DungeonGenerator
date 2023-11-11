using DungeonGenerator.Generators;
using DungeonGenerator.Structures;

namespace DungeonGeneratorTests.GeneratorsTests
{
    [TestClass]
    public class CaveGeneratorTest
    {
        [TestMethod]
        public void GenerateLevel_CreateCave_ReturnSingleWalkableArea()
        {
            LevelParameters parameters = new LevelParameters(40, 40, 0.0,new DungeonSettings());
            var caveGenerator = new CaveGenerator();

            var level = caveGenerator.GenerateLevel(parameters);

            var numOfWalkableAreas = level.NumOfConectedAreas();
            Assert.AreEqual(1, numOfWalkableAreas);
        }

        [TestMethod]
        public void GenerateLevel_CreateSmallCave_ReturnSingleWalkableArea()
        {
            LevelParameters parameters = new LevelParameters(1, 1, 0.0, new DungeonSettings());
            var caveGenerator = new CaveGenerator();

            var level = caveGenerator.GenerateLevel(parameters);

            var numOfWalkableAreas = level.NumOfConectedAreas();
            Assert.AreEqual(1, numOfWalkableAreas);
        }

    }
}
