using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator.Corridor
{
    public class CorridorPart : Rectangle
    {
        public bool IsVertical { get; init; }

        private CorridorPart(bool isVertical, Vector2Int bottomLeftcorner, Vector2Int size, DungeonSettings settings) : base(bottomLeftcorner, size, settings)
        {
            this.IsVertical = isVertical;
        }

        public static CorridorPart? CreateCorridor(bool isVertical, Vector2Int start, int length, DungeonSettings settings)
        {
            if (length == 0)
            {
                return null;
            }

            var corridorWidth = settings.CorridorWidth;
            int x = start.X;
            int y = start.Y;

            int width = corridorWidth;
            int height = corridorWidth;

            if (isVertical)
            {
                y += (length > 0) ? 0 : length;
                height = Math.Abs(length) + 1;
            }
            else
            {
                x += (length > 0) ? 0 : length;
                width = Math.Abs(length) + 1;
            }
            var bottomLeftCorner = new Vector2Int(x, y);
            var size = new Vector2Int(width, height);

            return new CorridorPart(isVertical, bottomLeftCorner, size, settings);
        }
    }
}
