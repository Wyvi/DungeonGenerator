namespace DungeonGenerator.Structures
{
    public class Room : Rectangle
    {
        public Vector2Int CenterNearCell { get; init; }

        public Room(Vector2Int bottomLeftcorner, Vector2Int size): base(bottomLeftcorner, size)
        {
            var additionalSize = new Vector2Int(size.x - 1, size.y - 1);
            CenterNearCell = bottomLeftcorner + additionalSize / 2;
        }
    }
}
