using DungeonGenerator.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator
{
    public abstract class Rectangle
    {
        public Vector2Int BottomLeftCorner { get; init; }
        public Vector2Int TopRightCorner { get; init; }

        public Vector2Int Size { get; init; }
        public Vector2Int CenterNearCell { get; init; }

        protected DungeonSettings settings;

        protected Rectangle(Vector2Int bottomLeftcorner, Vector2Int size, DungeonSettings settings)
        {
            if (bottomLeftcorner.X < 0 || bottomLeftcorner.Y < 0)
            {
                throw new ArgumentException("Corner coordinates must be < 0", nameof(bottomLeftcorner));
            }
            if (size.X < 1 || size.Y < 1)
            {
                throw new ArgumentException("Size coordinates must be < 0", nameof(size));
            }
            BottomLeftCorner = bottomLeftcorner;
            Size = size;
            var additionalSize = new Vector2Int(size.X - 1, size.Y - 1);
            TopRightCorner = bottomLeftcorner + additionalSize;
            CenterNearCell = bottomLeftcorner + additionalSize / 2;
            this.settings = settings;
        }

        public bool Overlaps(Rectangle rect)
        {
            var wallThickness = settings.MinWallThickness;
            var firstCornerWithWall = new Vector2Int(
                BottomLeftCorner.X - wallThickness, 
                BottomLeftCorner.Y - wallThickness);
            var lastCornerWithWall = new Vector2Int(
                TopRightCorner.X + wallThickness, 
                TopRightCorner.Y + wallThickness);
            bool xOverlaping = Math.Min(lastCornerWithWall.X, rect.TopRightCorner.X) >= Math.Max(firstCornerWithWall.X, rect.BottomLeftCorner.X);
            bool yOverlaping = Math.Min(lastCornerWithWall.Y, rect.TopRightCorner.Y) >= Math.Max(firstCornerWithWall.Y, rect.BottomLeftCorner.Y);

            if (xOverlaping && yOverlaping)
            {
                return true;
            }
            return false;
        }


        public override string ToString()
        {
            return $"Rectangle : {BottomLeftCorner.ToString()}, {TopRightCorner.ToString()}";
        }
    }
}
