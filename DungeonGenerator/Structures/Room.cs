namespace DungeonGenerator.Structures
{
    public class Room : Rectangle
    {
        public Vector2Int CenterNearCell { get; init; }

        public Room(Vector2Int bottomLeftcorner, Vector2Int size, DungeonSettings settings): base(bottomLeftcorner, size, settings)
        {
            var additionalSize = new Vector2Int(size.X - 1, size.Y - 1);
            CenterNearCell = bottomLeftcorner + additionalSize / 2;
        }
    }
}
