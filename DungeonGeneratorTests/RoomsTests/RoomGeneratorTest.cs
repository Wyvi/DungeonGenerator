using DungeonGenerator.Cave;
using DungeonGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGeneratorTests.RoomsTests
{
    [TestClass]
    public class RoomGeneratorTest
    {
        [TestMethod]
        public void GenerateLevel_CreateRoomsWithCorridors_ReturnSingleWalkableArea()
        {
            LevelParameters parameters = new LevelParameters(12, 12, 0.6);
            var caveGenerator = new CaveGenerator();

            var level = caveGenerator.GenerateLevel(parameters);

            var numOfWalkableAreas = level.NumOfConectedAreas();
            Assert.AreEqual(1, numOfWalkableAreas);
        }

        [TestMethod]
        public void GenerateLevel_CreateRoomWithSmallLevel_ReturnSingleWalkableArea()
        {
            LevelParameters parameters = new LevelParameters(1, 1, 0.6);
            var caveGenerator = new CaveGenerator();

            var level = caveGenerator.GenerateLevel(parameters);

            var numOfWalkableAreas = level.NumOfConectedAreas();
            Assert.AreEqual(1, numOfWalkableAreas);
        }
    }
}
