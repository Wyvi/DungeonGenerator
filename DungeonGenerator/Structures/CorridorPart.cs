namespace DungeonGenerator.Structures
{
    public class CorridorPart : Rectangle
    {
        public bool IsVertical { get; init; }
        private const int MinWidth = 1;

        private CorridorPart(bool isVertical, Vector2Int bottomLeftcorner, Vector2Int size) : base(bottomLeftcorner, size)
        {
            IsVertical = isVertical;
        }

        public static CorridorPart? CreateCorridor(bool isVertical, Vector2Int start, int length)
        {
            if (length == 0)
            {
                return null;
            }

            int x = start.X;
            int y = start.Y;

            int width = MinWidth;
            int height = MinWidth;

            if (isVertical)
            {
                y += length > 0 ? 0 : length;
                height = Math.Abs(length) + 1;
            }
            else
            {
                x += length > 0 ? 0 : length;
                width = Math.Abs(length) + 1;
            }
            var bottomLeftCorner = new Vector2Int(x, y);
            var size = new Vector2Int(width, height);

            return new CorridorPart(isVertical, bottomLeftCorner, size);
        }
    }
}
