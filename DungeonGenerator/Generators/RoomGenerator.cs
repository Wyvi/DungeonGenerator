using DungeonGenerator.Structures;

namespace DungeonGenerator.Generators
{
    internal class RoomGenerator : ILevelGenerator
    {
        private const int minRoomSize = 3;
        private const int maxRoomSize = 8;
        private Random random = new Random();

        public Level GenerateLevel(LevelParameters parameters)
        {
            var rooms = GenerateRooms(parameters);

            IEnumerable<Rectangle> corridors = new CorridorGenerator(rooms, parameters.Settings).Generate();
            IEnumerable<Rectangle> rectRooms = rooms;

            var level = new Level(parameters.Width, parameters.Height);
            level.SetRectangles(rectRooms.ToList());
            level.AddRectangles(corridors.ToList());
            return level;
        }


        private List<Room> GenerateRooms(LevelParameters parameters)
        {
            var levelArea = parameters.Width * parameters.Height;
            var generateRoomChance = Math.Pow(parameters.WalkableFloorChance, 3);
            List<Room> rooms = new List<Room>();

            for (int i = 0; i < levelArea / 2; i++)
            {
                if (random.NextDouble() < generateRoomChance)
                {
                    var randX = random.Next(parameters.Width - minRoomSize);
                    var randY = random.Next(parameters.Height - minRoomSize);
                    var roomCorner = new Vector2Int(randX, randY);

                    var maxWidth = Math.Min(maxRoomSize, parameters.Width - randX + 1);
                    var maxHeight = Math.Min(maxRoomSize, parameters.Height - randY + 1);
                    var roomSize = new Vector2Int(
                        minRoomSize + random.Next(maxWidth - minRoomSize),
                        minRoomSize + random.Next(maxHeight - minRoomSize));
                    var room = new Room(roomCorner, roomSize, parameters.Settings);
                    var overlap = rooms.FirstOrDefault(a => a.Overlaps(room));
                    if (overlap == null)
                    {
                        rooms.Add(room);
                    }
                }
            }
            if (rooms.Count == 0)
            {
                rooms.Add(new Room(
                    new Vector2Int(0, 0),
                    new Vector2Int(maxRoomSize, maxRoomSize),
                    new DungeonSettings(1, 1)));
            }
            return rooms;
        }

    }
}
