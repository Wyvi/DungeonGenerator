using DungeonGenerator.Corridor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator.Rooms
{
    internal class RoomGenerator : ILevelGenerator
    {
        private int minRoomSize = 3;
        private int maxRoomSize = 8;
        private Random random = new Random();

        public Level GenerateLevel(LevelParameters parameters)
        {
            var levelArea = parameters.Width * parameters.Height;
            List<Room> rooms = new List<Room>();
            var generateRoomChance = Math.Pow(parameters.WalkableFloorChance, 2);

            for (int i = 0; i < levelArea / 2; i++)
            {
                if (random.NextDouble() < generateRoomChance)
                {
                    var randX = random.Next(parameters.Width - minRoomSize);
                    var randY = random.Next(parameters.Height - minRoomSize);
                    var roomCorner = new Vector2Int(randX, randY);
                    var roomSize = new Vector2Int(
                        minRoomSize + random.Next(maxRoomSize - minRoomSize),
                        minRoomSize + random.Next(maxRoomSize - minRoomSize));
                    var room = new Room(roomCorner, roomSize);
                    var overlap = rooms.FirstOrDefault(a => a.Overlaps(room));
                    if (overlap == null)
                    {
                        rooms.Add(room);
                    }
                }
            }
            if (rooms.Count == 0)
            {
                rooms.Add(new Room(new Vector2Int(0, 0), new Vector2Int(maxRoomSize, maxRoomSize)));
            }
            var corridor = new CorridorGenerator(rooms).Generate();
            var level = new Level(parameters.Width, parameters.Height);
            level.SetRooms(rooms);
            level.AddArea(corridor);
            return level;
        }
    }
}
