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
        
        public Area()
        {
            cells = new List<Vector2Int>();
        }

        public void Add(Vector2Int p)
        {
            if (!cells.Contains(p))
            {
                cells.Add(p);
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
    }
}
