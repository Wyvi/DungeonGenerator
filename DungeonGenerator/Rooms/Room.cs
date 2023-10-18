using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator.Rooms
{
    public class Room
    {
        public Vector2Int FirstCorner { get; init; }
        public Vector2Int LastCorner { get; init; }

        public Vector2Int Size { get; init; }
        public Vector2Int CenterNearCell { get; init; }

        public Room(Vector2Int corner, Vector2Int size)
        {
            if (corner.X < 0 || corner.Y < 0)
            {
                throw new ArgumentException("Corner coordinates must be < 0", nameof(corner));
            }
            if (size.X < 1 || size.Y < 1)
            {
                throw new ArgumentException("Size coordinates must be < 0", nameof(size));
            }
            FirstCorner = corner;
            Size = size;
            var additionalSize = new Vector2Int(size.X - 1, size.Y - 1);
            LastCorner = corner + additionalSize;
            CenterNearCell = corner + additionalSize / 2;
        }

        public bool Overlaps(Room room)
        {
            var firstCornerWithWall = new Vector2Int(FirstCorner.X - 1, FirstCorner.Y - 1);
            var lastCornerWithWall = new Vector2Int(LastCorner.X + 1, LastCorner.Y + 1);
            bool xOverlaping = Math.Min(lastCornerWithWall.X, room.LastCorner.X) >= Math.Max(firstCornerWithWall.X, room.FirstCorner.X);
            bool yOverlaping = Math.Min(lastCornerWithWall.Y, room.LastCorner.Y) >= Math.Max(firstCornerWithWall.Y, room.FirstCorner.Y);

            if (xOverlaping && yOverlaping)
            {
                return true;
            }
            return false;
        }


        public override string ToString()
        {
            return $"Room : {FirstCorner.ToString()}, {LastCorner.ToString()}";
        }
    }
}
