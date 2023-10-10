using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator
{
    public class LevelParameters
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public double WalkableFloorChance {  get; private set; }

        public LevelParameters(int width, int height, double walkableFloorChance)
        {
            Width = width;
            Height = height;
            WalkableFloorChance = walkableFloorChance;
        }
    }
}
