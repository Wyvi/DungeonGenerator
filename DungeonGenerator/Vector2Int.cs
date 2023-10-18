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


        public static float Distance(Vector2Int a, Vector2Int b)
        {
            return (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }


        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public static Vector2Int operator +(Vector2Int a, Vector2Int b) =>
            new Vector2Int(a.X + b.X, a.Y + b.Y);
        public static Vector2Int operator -(Vector2Int a, Vector2Int b) =>
            new Vector2Int(a.X - b.X, a.Y - b.Y);
        public static Vector2Int operator /(Vector2Int a, int b) =>
            new Vector2Int(a.X / b, a.Y / b);
        public static Vector2Int operator *(Vector2Int a, int b) =>
            new Vector2Int(a.X * b, a.Y * b);


    }
}
