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


        public static CorridorPart Create(bool isVertical, Vector2Int start, int length)
        {
            int x = start.x;
            int y = start.y;
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


        public static CorridorPart Create(bool isVertical, Room start, Room end)
        {
            var startX = start.CenterNearCell.x;
            var startY = start.CenterNearCell.y;
            var lenght = 0;

            if (isVertical)
            {
                startY = (start.CenterNearCell.y < end.CenterNearCell.y)
                    ? start.TopRightCorner.y : start.BottomLeftCorner.y;
                lenght = end.CenterNearCell.y - startY;
            }
            else
            {
                startX = (start.CenterNearCell.x < end.CenterNearCell.x)
                    ? start.TopRightCorner.x : start.BottomLeftCorner.x;
                lenght = end.CenterNearCell.x - startX;
            }

            return Create(isVertical, new Vector2Int(startX, startY), lenght);
        }


    }
}
