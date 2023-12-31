﻿using System.Collections.ObjectModel;
using System.Text;

namespace DungeonGenerator.Structures
{
    public class Level
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        private TileType[,] level;

        public Level(int width, int height)
        {
            if (width < 1)
            {
                throw new ArgumentException("width must be >= 1", nameof(width));
            }
            if (height < 1)
            {
                throw new ArgumentException("height must be >= 1", nameof(height));
            }

            Width = width;
            Height = height;
            level = new TileType[width, height];
        }

        public Level(TileType[,] level)
        {
            if (level.GetLength(0) < 1 || level.GetLength(1) < 1)
            {
                throw new ArgumentException("leve size must be at least [1,1]", nameof(level));
            }

            Width = level.GetLength(0);
            Height = level.GetLength(1);
            this.level = level;
        }



        public ReadOnlyCollection<TileType> LevelData()
        {
            return Array.AsReadOnly(level.Cast<TileType>().ToArray());
        }


        public static Level CreateFromArea(Area area,LevelParameters parameters)
        {
            var level = new Level(parameters.Width, parameters.Height);
            level.SetArea(area);
            return level;
        }


        public void Clear()
        {
            level = new TileType[Width, Height];
        }

        public void SetArea(Area area)
        {
            Clear();
            AddArea(area);
        }

        public void AddArea(Area area)
        {
            foreach (Vector2Int cell in area.GetCells())
            {
                SetCell(cell.x, cell.y, TileType.floor);
            }
        }

        public void SetRectangles(List<Rectangle> rectangles)
        {
            Clear();
            AddRectangles(rectangles);
        }

        public void AddRectangles(List<Rectangle> rectangles)
        {
            foreach (var room in rectangles)
            {
                for (int x = room.BottomLeftCorner.x; x <= room.TopRightCorner.x; x++)
                {
                    for (int y = room.BottomLeftCorner.y; y <= room.TopRightCorner.y; y++)
                    {
                        SetCell(x, y, TileType.floor);
                    }
                }
            }
        }

        public int CountWalkableNeighbours(int cellPosX, int cellPosY)
        {
            int count = 0;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (!(x == 0 && y == 0) && GetCell(cellPosX + x, cellPosY + y) != TileType.wall)
                    {
                        count++;
                    }
                }
            }
            return count;
        }


        public TileType GetCell(int x, int y)
        {
            return GetCell(x, y, level);
        }

        private TileType GetCell(int x, int y, TileType[,] level)
        {
            if (IsCellInLevel(x, y))
            {
                return level[x, y];
            }
            return TileType.wall;
        }


        public void SetCell(int x, int y, TileType type)
        {
            if (IsCellInLevel(x, y))
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
            largestArea = largestArea != null ? largestArea : Area.Smaller();
            return largestArea;
        }

        public int NumOfConectedAreas()
        {
            return FindConectedAreas().Count();
        }


        private List<Area> FindConectedAreas()
        {
            var areas = new List<Area>();
            TileType[,] levelForFill = (TileType[,])level.Clone();
            TileType[] flattened = levelForFill.Cast<TileType>().ToArray();
            int floorIndex = Array.IndexOf(flattened, TileType.floor);
            while (floorIndex >= 0)
            {
                var startingPoint = new Vector2Int(floorIndex / level.GetLength(1), floorIndex % level.GetLength(1));
                areas.Add(FloodFill(startingPoint, levelForFill));

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

                levelForFill[cell.x, cell.y] = TileType.fill;
                area.Add(cell);

                var neighbours = DirectNeighbours(cell);
                foreach (Vector2Int point in neighbours)
                {
                    if (GetCell(point.x, point.y, levelForFill) == TileType.floor && !cellsForCheck.Contains(point))
                    {
                        cellsForCheck.Add(point);
                    }
                }
            }
            return area;
        }

        private Vector2Int[] DirectNeighbours(Vector2Int position)
        {
            Vector2Int[] points = {new Vector2Int(position.x, position.y - 1),
                    new Vector2Int(position.x, position.y + 1),
                    new Vector2Int(position.x - 1, position.y),
                    new Vector2Int(position.x + 1, position.y) };
            return points;
        }

        /// <summary>
        /// Level cells are written to the console for illustrative purposes. 
        /// A positive x-axis pointed to the right and a positive y-axis pointed up.
        /// </summary>
        public void WriteToConsole()
        {
            WriteToConsole(level);
        }


        private void WriteToConsole(TileType[,] level)
        {
            Console.WriteLine();
            for (int y = level.GetLength(1) - 1; y >= 0; y--)
            {
                for (int x = 0; x < level.GetLength(0); x++)
                {
                    switch (level[x, y])
                    {
                        case TileType.wall:
                            Console.Write("##");
                            break;
                        case TileType.floor:
                            Console.Write("  ");
                            break;
                        case TileType.fill:
                            Console.Write("OO");
                            break;
                    }
                }
                Console.Write('\n');
            }
        }


        public override string ToString()
        {
            var str = new StringBuilder();

            for (int x = 0; x < level.GetLength(0); x++)
            {
                var levelXArray = Enumerable.Range(0, level.GetLength(1)).Select(y => level[x, y].ToString()).ToArray();
                str.AppendLine(string.Join(" , ", levelXArray));
            }
            return str.ToString();
        }

    }
}
