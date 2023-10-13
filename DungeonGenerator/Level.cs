using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator
{
    public class Level 
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        private TileType[,] level;

        public Level(int width, int height)
        {
            this.Width = width; 
            this.Height = height;
            level = new TileType[width, height];
        }

        public Level(TileType[,] level)
        {
            this.Width = level.GetLength(0);
            this.Height = level.GetLength(1);
            this.level = level;
        }

        public ReadOnlyCollection<TileType> LevelData()
        {
            return Array.AsReadOnly(level.Cast<TileType>().ToArray());
        }

        public void LevelFromArea(Area area)
        {
            level = new TileType[Width, Height];
            foreach(var cell in area.GetCells())
            {
                SetCellType(cell.X, cell.Y, TileType.floor);
            }
        }

        public int CountWalkableNeighbours(int cellPosX, int cellPosY)
        {
            int count = 0;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (!(x == 0 && y == 0) && GetCellFromLevel(cellPosX + x, cellPosY + y) != TileType.wall)
                    {
                        count++;
                    }
                }
            }
            return count;
        }


        public TileType GetCellFromLevel(int x, int y)
        {
            return GetCellFromLevel(x, y, level);
        }

        private TileType GetCellFromLevel(int x, int y, TileType[,] level)
        {
            if (IsCellInLevel(x, y))
            {
                return level[x, y];
            }
            return TileType.wall;
        }


        public void SetCellType(int x, int y, TileType type)
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


        public Area FindLargestArea()
        {
            var areas = FindConectedAreas();
            var largestArea = areas.Max();
            return largestArea;
        }

        

        private List<Area> FindConectedAreas()
        {
            var areas = new List<Area>();
            TileType[,] levelForFill = (TileType[,]) level.Clone();
            TileType[] flattened = levelForFill.Cast<TileType>().ToArray();
            int floorIndex = Array.IndexOf(flattened, TileType.floor);
            while (floorIndex >= 0)
            {
                var startingPoint = new Vector2Int(floorIndex / level.GetLength(1), floorIndex % level.GetLength(1));
                areas.Add(FloodFill(startingPoint,levelForFill));

                flattened = levelForFill.Cast<TileType>().ToArray();
                floorIndex = Array.IndexOf(flattened, TileType.floor);
            }
            return areas;
        }

        private Area FloodFill(Vector2Int startingPoint, TileType[,] levelForFill)
        {
            var area = new Area();
            var cellsForCheck = new List<Vector2Int>();
            cellsForCheck.Add(startingPoint);
            while (cellsForCheck.Count > 0)
            {
                var cell = cellsForCheck[0];
                cellsForCheck.Remove(cell);
                
                levelForFill[cell.X, cell.Y] = TileType.fill;
                area.Add(cell);

                Vector2Int[] neighbours = DirectNeighbours(cell);
                foreach (var point in neighbours)
                {
                    if(GetCellFromLevel(point.X,point.Y,levelForFill) == TileType.floor && !cellsForCheck.Contains(point))
                    {
                        cellsForCheck.Add(point);
                    }
                }
            }
            return area;
        }

        private Vector2Int[] DirectNeighbours(Vector2Int position)
        {
            Vector2Int[] points = {new Vector2Int(position.X, position.Y - 1),
                    new Vector2Int(position.X, position.Y + 1),
                    new Vector2Int(position.X - 1, position.Y),
                    new Vector2Int(position.X + 1, position.Y) };
            return points;
        }

        public void WriteToConsole()
        {
            WriteToConsole(level);
        }


        private void WriteToConsole(TileType[,] level)
        {
            Console.WriteLine();
            for (int x = 0; x < level.GetLength(0); x++)
            {
                for (int y = 0; y < level.GetLength(1); y++)
                {
                    switch ((TileType)level[x, y])
                    {
                        case TileType.wall:
                            Console.Write("#");
                            break;
                        case TileType.floor:
                            Console.Write(" ");
                            break;
                        case TileType.fill:
                            Console.Write("O");
                            break;
                    }
                }
                Console.Write('\n');
            }
        }

    }
}
