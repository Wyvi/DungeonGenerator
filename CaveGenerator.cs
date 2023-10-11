using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DungeonGenerator
{
    public class CaveGenerator : ILevelGenerator
    {
        private int neigboursToStayFloor = 3;
        private int neigboursToCreateFloor = 5;
        private int levelCellsChecking = 3;

        private double minCaveSizeModifier = 0.75;


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
                        level.SetCellType(x, y, TypeOfTiles.floor);
                    }
                }
            }
            return level;
        }

        private Level CellsLifeChecking(Level level)
        {
            var checkedLevel = new Level(level.Width, level.Height);
            for(int x=0; x<level.Width; x++)
                for(int y=0; y < level.Height; y++)
                {
                    var floorNeighbours = level.CountWalkableNeighbours(x, y);
                    
                    if(floorNeighbours < neigboursToStayFloor)
                    {
                        checkedLevel.SetCellType(x, y, TypeOfTiles.wall);
                    }
                    else if(floorNeighbours >= neigboursToCreateFloor)
                    {
                        checkedLevel.SetCellType(x, y, TypeOfTiles.floor);
                    }
                    else
                    {
                        checkedLevel.SetCellType(x, y, level.GetCellFromLevel(x, y));
                    }
                }
            return checkedLevel;
        }

        public Level GenerateLevel(LevelParameters parameters)
        {
            var level = RandomLevelGenerator(parameters);

            for (int i=0; i<levelCellsChecking; i++)
            {
                level = CellsLifeChecking(level);
            }

            int minCaveSize = (int)(minCaveSizeModifier * parameters.Width * parameters.Height * parameters.WalkableFloorChance);
            if (level.OnlyOneConectedArea(minCaveSize))
            {
                return level;
            }
            else
            {
                return GenerateLevel(parameters);
            }
        }
    }
}
