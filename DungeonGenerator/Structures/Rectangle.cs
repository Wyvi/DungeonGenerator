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
            if (bottomLeftcorner.x < 0 || bottomLeftcorner.y < 0)
            {
                throw new ArgumentException("Corner coordinates must be < 0", nameof(bottomLeftcorner));
            }
            if (size.x < 1 || size.y < 1)
            {
                throw new ArgumentException("Size coordinates must be > 0", nameof(size));
            }
            BottomLeftCorner = bottomLeftcorner;
            Size = size;
            var additionalSize = new Vector2Int(size.x - 1, size.y - 1);
            TopRightCorner = bottomLeftcorner + additionalSize;
        }


        private bool IsOverlapping(Vector2Int bottomLeft, Vector2Int topRight)
        {
            bool xOverlaping = Math.Min(TopRightCorner.x, topRight.x)
                >= Math.Max(BottomLeftCorner.x, bottomLeft.x);
            bool yOverlaping = Math.Min(TopRightCorner.y, topRight.y)
                >= Math.Max(BottomLeftCorner.y, bottomLeft.y);

            if (xOverlaping && yOverlaping)
            {
                return true;
            }
            return false;
        }


        public bool IsOverlapping(Rectangle rect)
        {
            return IsOverlapping(rect.BottomLeftCorner, rect.TopRightCorner);
        }


        public bool IsTouching(Rectangle rect)
        {
            return !IsOverlapping(rect) && IsOverlappingWithWall(rect);
        }


        public bool IsTouching(List<Rectangle> rects)
        {
            var overlap = rects.FirstOrDefault(r => IsTouching(r));
            return overlap != null;
        }


        public bool IsOverlappingWithWall(Rectangle rect)
        {
            var bottomLeftWithWall = new Vector2Int(
                rect.BottomLeftCorner.x - Wall,
                rect.BottomLeftCorner.y - Wall);
            var topRightWithWall = new Vector2Int(
                rect.TopRightCorner.x + Wall,
                rect.TopRightCorner.y + Wall);

            return IsOverlapping(bottomLeftWithWall, topRightWithWall);
        }


        public bool IsWholeIn(Rectangle rect)
        {
            bool bottom = rect.BottomLeftCorner.x <= BottomLeftCorner.x
                && rect.BottomLeftCorner.y <= BottomLeftCorner.y;
            bool top = rect.TopRightCorner.x >= TopRightCorner.x
                && rect.TopRightCorner.y >= TopRightCorner.y;
            return bottom && top;
        }


        public override string ToString()
        {
            return $"Rectangle : {BottomLeftCorner.ToString()}, {TopRightCorner.ToString()}";
        }
    }
}
