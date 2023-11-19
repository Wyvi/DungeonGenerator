
namespace DungeonGenerator
{
    public readonly struct Vector2Int
    {
        public int x { get; init; }
        public int y { get; init; }

        public float magnitude { get; init; }

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
            magnitude = (float)Math.Sqrt(Math.Pow(this.x, 2) + Math.Pow(this.y, 2));
        }


        public static float Distance(Vector2Int a, Vector2Int b)
        {
            return (float)Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2));
        }


        public override string ToString()
        {
            return $"({x}, {y})";
        }

        public static Vector2Int operator +(Vector2Int a, Vector2Int b) =>
            new Vector2Int(a.x + b.x, a.y + b.y);
        public static Vector2Int operator -(Vector2Int a, Vector2Int b) =>
            new Vector2Int(a.x - b.x, a.y - b.y);
        public static Vector2Int operator /(Vector2Int a, int b) =>
            new Vector2Int(a.x / b, a.y / b);
        public static Vector2Int operator *(Vector2Int a, int b) =>
            new Vector2Int(a.x * b, a.y * b);


    }
}
