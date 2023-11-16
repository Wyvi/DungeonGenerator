namespace DungeonGenerator.Structures
{
    public abstract class Rectangle
    {
        public Vector2Int BottomLeftCorner { get; init; }
        public Vector2Int TopRightCorner { get; init; }

        public Vector2Int Size { get; init; }

        protected const int Wall = 1;

        protected Rectangle(Vector2Int bottomLeftcorner, Vector2Int size)
        {
            if (bottomLeftcorner.X < 0 || bottomLeftcorner.Y < 0)
            {
                throw new ArgumentException("Corner coordinates must be < 0", nameof(bottomLeftcorner));
            }
            if (size.X < 1 || size.Y < 1)
            {
                throw new ArgumentException("Size coordinates must be > 0", nameof(size));
            }
            BottomLeftCorner = bottomLeftcorner;
            Size = size;
            var additionalSize = new Vector2Int(size.X - 1, size.Y - 1);
            TopRightCorner = bottomLeftcorner + additionalSize;
        }


        private bool Overlaps(Vector2Int bottomLeft, Vector2Int topRight)
        {
            bool xOverlaping = Math.Min(TopRightCorner.X, topRight.X)
                >= Math.Max(BottomLeftCorner.X, bottomLeft.X);
            bool yOverlaping = Math.Min(TopRightCorner.Y, topRight.Y)
                >= Math.Max(BottomLeftCorner.Y, bottomLeft.Y);

            if (xOverlaping && yOverlaping)
            {
                return true;
            }
            return false;
        }


        public bool Overlaps(Rectangle rect)
        {
            return Overlaps(rect.BottomLeftCorner, rect.TopRightCorner);
        }


        public bool OverlapsWall(Rectangle rect)
        {
            var bottomLeftWithWall = new Vector2Int(
                rect.BottomLeftCorner.X - Wall,
                rect.BottomLeftCorner.Y - Wall);
            var topRightWithWall = new Vector2Int(
                rect.TopRightCorner.X + Wall,
                rect.TopRightCorner.Y + Wall);

            return Overlaps(bottomLeftWithWall, topRightWithWall);
        }


        public override string ToString()
        {
            return $"Rectangle : {BottomLeftCorner.ToString()}, {TopRightCorner.ToString()}";
        }
    }
}
