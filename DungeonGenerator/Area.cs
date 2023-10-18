using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator
{
    public class Area : IComparable<Area>
    {
        private List<Vector2Int> cells;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Area()
        {
            cells = new List<Vector2Int>();
            Width = 0;
            Height = 0;
        }

        public static Area Smaller()
        {
            var area = new Area();
            area.Add(new Vector2Int(0, 0));
            return area;
        }

        public void Add(Vector2Int cell)
        {
            if (!cells.Contains(cell))
            {
                cells.Add(cell);
                Width = Math.Max(Width, cell.X + 1);
                Height = Math.Max(Height, cell.Y + 1);
            }
        }

        public void AddCorridor(Vector2Int cell1, Vector2Int cell2)
        {
            int xDist = cell2.X - cell1.X;
            if (xDist != 0)
            {
                int xStep = xDist / Math.Abs(xDist);
                int x = cell1.X;

                while (x != cell2.X)
                {
                    Add(new Vector2Int(x, cell1.Y));
                    x += xStep;
                }
            }

            int yDist = cell2.Y - cell1.Y;
            if (yDist != 0)
            {
                int yStep = yDist / Math.Abs(yDist);
                int y = cell1.Y;
                while (y != cell2.Y)
                {
                    Add(new Vector2Int(cell2.X, y));
                    y += yStep;
                }
            }
        }

        public ReadOnlyCollection<Vector2Int> GetCells()
        {
            return cells.AsReadOnly();
        }

        public int Count()
        {
            return cells.Count;
        }

        public int CompareTo(Area? obj)
        {
            if (obj == null)
                return 1;

            return Count().CompareTo(obj.Count());
        }

        public override string ToString()
        {
            var cellsArray = cells.Select(x => x.ToString()).ToArray();
            return $"Width: {Width}, Height: {Height}, cels: {{{string.Join(", ", cellsArray)}}}";
        }
    }
}
