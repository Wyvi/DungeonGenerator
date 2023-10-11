using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DungeonGenerator
{
    public enum TypeOfTiles
    {
        wall = 0,
        floor = 1,
        fill = 2,
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

        private void LevelFromArea(Area area)
        {
            level = new TypeOfTiles[Width, Height];
            foreach(var cell in area.GetCells())
            {
                SetCellType(cell.X, cell.Y, TypeOfTiles.floor);
            }
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
            return GetCellFromLevel(x, y, level);
        }

        private TypeOfTiles GetCellFromLevel(int x, int y, TypeOfTiles[,] level)
        {
            if (IsCellInLevel(x, y))
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


        public bool OnlyOneConectedArea(int minSize)
        {
            var areas = FindConectedAreas();
            var largestArea = areas.Max();
            if (largestArea != null && largestArea.Count() >= minSize)
            {
                LevelFromArea(largestArea);
                return true;
            }
            return false;
        }

        private List<Area> FindConectedAreas()
        {
            var areas = new List<Area>();
            TypeOfTiles[,] levelForFill = (TypeOfTiles[,]) level.Clone();
            TypeOfTiles[] flattened = levelForFill.Cast<TypeOfTiles>().ToArray();
            int floorIndex = Array.IndexOf(flattened, TypeOfTiles.floor);
            while (floorIndex >= 0)
            {
                var startingPoint = new Point(floorIndex / level.GetLength(1), floorIndex % level.GetLength(1));
                areas.Add(FloodFill(startingPoint,levelForFill));

                flattened = levelForFill.Cast<TypeOfTiles>().ToArray();
                floorIndex = Array.IndexOf(flattened, TypeOfTiles.floor);
            }
            return areas;
        }

        private Area FloodFill(Point startingPoint, TypeOfTiles[,] levelForFill)
        {
            var area = new Area();
            var cellsForCheck = new List<Point>();
            cellsForCheck.Add(startingPoint);
            while (cellsForCheck.Count > 0)
            {
                var cell = cellsForCheck[0];
                cellsForCheck.Remove(cell);
                
                levelForFill[cell.X, cell.Y] = TypeOfTiles.fill;
                area.Add(cell);

                Point[] neighbours = DirectNeighbours(cell);
                foreach (var point in neighbours)
                {
                    if(GetCellFromLevel(point.X,point.Y,levelForFill) == TypeOfTiles.floor && !cellsForCheck.Contains(point))
                    {
                        cellsForCheck.Add(point);
                    }
                }
            }
            return area;
        }

        private Point[] DirectNeighbours(Point position)
        {
            Point[] points = {new Point(position.X, position.Y - 1),
                    new Point(position.X, position.Y + 1),
                    new Point(position.X - 1, position.Y),
                    new Point(position.X + 1, position.Y) };
            return points;
        }

        public void WriteToConsole()
        {
            WriteToConsole(level);
        }


        private void WriteToConsole(TypeOfTiles[,] level)
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
                        case TypeOfTiles.fill:
                            Console.Write("O");
                            break;
                    }
                }
                Console.Write('\n');
            }
        }
    }
}
