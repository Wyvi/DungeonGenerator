using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator
{
    public readonly struct Vector2Int
    {
        public int X { get; init; }
        public int Y { get; init; }

        public Vector2Int(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

    }
}
