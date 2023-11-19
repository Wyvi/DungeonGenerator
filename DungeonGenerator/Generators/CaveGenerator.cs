using DungeonGenerator.Structures;

namespace DungeonGenerator.Generators
{
    public class CaveGenerator : ILevelGenerator
    {
        private const int neigboursToStayFloor = 3;
        private const int neigboursToCreateFloor = 5;
        private const int levelCellsChecking = 3;
        private const int maxNumOfAttempts = 15;

        private Area largestAreaInThisSession = Area.Smaller();


        public Level GenerateLevel(LevelParameters parameters)
        {
            return GenerateCave(parameters, maxNumOfAttempts);
        }


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


        private Level GenerateCave(LevelParameters parameters, int numOfAttempt)
        {
            
            int levelArea = parameters.Width * parameters.Height;
            int minCaveSize = (int)(levelArea * parameters.WalkableFloorChance);
            var area = RandomCave(parameters);
            largestAreaInThisSession = new[] { largestAreaInThisSession, area }.Max();

            if (largestAreaInThisSession.Count() >= minCaveSize || numOfAttempt <= 1)
            {
                var level = Level.CreateFromArea(largestAreaInThisSession,parameters);
                largestAreaInThisSession = Area.Smaller();
                return level;
            }
            else
            {
                return GenerateCave(parameters, --numOfAttempt);
            }
        }

        private Area RandomCave(LevelParameters parameters)
        {
            var level = RandomLevelGenerator(parameters);
            for (int i = 0; i < levelCellsChecking; i++)
            {
                level = CellsLifeChecking(level);
            }
            return level.FindLargestArea();
        }
    }
}
