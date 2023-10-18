using DungeonGenerator;
using DungeonGenerator.Cave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGeneratorTests.CaveTests
{
    [TestClass]
    public class CaveGeneratorTest
    {
        [TestMethod]
        public void GenerateLevel_CreateCave_ReturnSingleWalkableArea()
        {
            LevelParameters parameters = new LevelParameters(1, 1, 0.0);
            var caveGenerator = new CaveGenerator();

            var level = caveGenerator.GenerateLevel(parameters);

            var numOfWalkableAreas = level.NumOfConectedAreas();
            Assert.AreEqual(1, numOfWalkableAreas);
        }

    }
}
