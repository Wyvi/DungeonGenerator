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
            if (width < 1) 
            {
                throw new ArgumentException("width must be >= 1", nameof(width));
            }
            if (height < 1)
            {
                throw new ArgumentException("height must be >= 1", nameof(height));
            }
            if (walkableFloorChance < 0 || walkableFloorChance > 1)
            {
                throw new ArgumentException("walkableFloorChance must be must be from 0 to 1", nameof(walkableFloorChance));
            }

            Width = width;
            Height = height;
            WalkableFloorChance = walkableFloorChance;
        }
    }
}
