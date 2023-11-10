using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator
{
    public class DungeonSettings
    {
        public int MinWallThickness {  get; init; }
        public int CorridorWidth { get; init; }

        public DungeonSettings(int minWallThickness = 1, int corridorWidth = 1) 
        {
            this.MinWallThickness = minWallThickness;
            this.CorridorWidth = corridorWidth;
        }
    }
}
