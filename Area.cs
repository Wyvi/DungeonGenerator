using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DungeonGenerator
{
    public class Area : IComparable
    {
        private List<Point> cells;
        
        public Area()
        {
            cells = new List<Point>();
        }

        public void Add(Point p)
        {
            cells.Add(p);
        }

        public List<Point> GetCells()
        {
            return cells;
        }

        public int Count()
        {
            return cells.Count;
        }

        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;
            Area? area = obj as Area;
            if(area!= null)
            {
                return Count().CompareTo(area.Count());
            }
            else
            {
                throw new ArgumentException("Object is not a Area");
            }
        }
    }
}
