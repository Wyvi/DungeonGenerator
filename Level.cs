using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator
{
    public enum TypeOfTiles
    {
        wall = 0,
        floor = 1,
    }

    public class Level
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        private TypeOfTiles[,] level;

        public Level(int width, int height)
        {
            this.Width = width; 
            this.Height = height;
            level = new TypeOfTiles[width, height];
        }


        public int CountWalkableNeighbours(int cellPosX, int cellPosY)
        {
            int count = 0;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (!(x == 0 && y == 0) && GetCellFromLevel(cellPosX + x, cellPosY + y) != TypeOfTiles.wall)
                    {
                        count++;
                    }
                }
            }
            return count;
        }


        public TypeOfTiles GetCellFromLevel(int x, int y)
        {
            if (IsCellInLevel(x,y))
            {
                return level[x, y];
            }
            return TypeOfTiles.wall;
        }


        public void SetCellType(int x, int y, TypeOfTiles type)
        {
            if (IsCellInLevel(x,y))
            {
                level[x, y] = type;
            }
        }


        private bool IsCellInLevel(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < level.GetLength(0) && y < level.GetLength(1))
                return true;
            return false;
        }


        public void WriteToConsole()
        {
            Console.WriteLine();
            for (int x = 0; x < level.GetLength(0); x++)
            {
                for (int y = 0; y < level.GetLength(1); y++)
                {
                    switch ((TypeOfTiles)level[x, y])
                    {
                        case TypeOfTiles.wall:
                            Console.Write("#");
                            break;
                        case TypeOfTiles.floor:
                            Console.Write(" ");
                            break;
                    }
                }
                Console.Write('\n');
            }
        }
    }
}
