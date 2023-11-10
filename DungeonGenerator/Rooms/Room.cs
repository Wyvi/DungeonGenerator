using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator.Rooms
{
    public class Room : Rectangle
    {

        public Room(Vector2Int bottomLeftcorner, Vector2Int size, DungeonSettings settings): base(bottomLeftcorner, size, settings)
        {
            
        }
    }
}
