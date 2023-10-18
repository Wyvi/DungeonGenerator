using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator.Cave
{
    public class CaveGenerator : ILevelGenerator
    {
        private int neigboursToStayFloor = 3;
        private int neigboursToCreateFloor = 5;
        private int levelCellsChecking = 3;

        private double minCaveSizeModifier = 0.75;

        private int maxNumOfAttempts = 15;
        private Area largestAreaInThisSession = Area.Smaller();


        private Level RandomLevelGenerator(LevelParameters parameters)
        {
            var random = new Random();
            var level = new Level(parameters.Width, parameters.Height);
            for (int x = 0; x < level.Width; x++)
            {
                for (int y = 0; y < level.Height; y++)
                {
                    if (random.NextDouble() < parameters.WalkableFloorChance)
                    {
                        level.SetCell(x, y, TileType.floor);
                    }
                }
            }
            return level;
        }

        private Level CellsLifeChecking(Level level)
        {
            var checkedLevel = new Level(level.Width, level.Height);
            for (int x = 0; x < level.Width; x++)
                for (int y = 0; y < level.Height; y++)
                {
                    var floorNeighbours = level.CountWalkableNeighbours(x, y);

                    if (floorNeighbours < neigboursToStayFloor)
                    {
                        checkedLevel.SetCell(x, y, TileType.wall);
                    }
                    else if (floorNeighbours >= neigboursToCreateFloor)
                    {
                        checkedLevel.SetCell(x, y, TileType.floor);
                    }
                    else
                    {
                        checkedLevel.SetCell(x, y, level.GetCell(x, y));
                    }
                }
            return checkedLevel;
        }


        public Level GenerateLevel(LevelParameters parameters)
        {
            return GenerateCave(parameters, maxNumOfAttempts);
        }

        private Level GenerateCave(LevelParameters parameters, int numOfAttempt)
        {
            var level = RandomLevelGenerator(parameters);
            for (int i = 0; i < levelCellsChecking; i++)
            {
                level = CellsLifeChecking(level);
            }

            int minCaveSize = (int)(minCaveSizeModifier * parameters.Width * parameters.Height * parameters.WalkableFloorChance);
            var area = level.FindLargestArea();
            largestAreaInThisSession = new[] { largestAreaInThisSession, area }.Max();

            if (largestAreaInThisSession.Count() >= minCaveSize || numOfAttempt <= 1)
            {
                level.SetArea(largestAreaInThisSession);
                largestAreaInThisSession = Area.Smaller();
                return level;
            }
            else
            {
                return GenerateCave(parameters, --numOfAttempt);
            }
        }


    }
}
