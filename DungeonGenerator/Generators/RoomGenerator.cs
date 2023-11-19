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

            IEnumerable<Rectangle> corridors = new CorridorGenerator(rooms).Generate();
            IEnumerable<Rectangle> rectRooms = rooms;

            var level = new Level(parameters.Width, parameters.Height);
            level.SetRectangles(rectRooms.ToList());
            level.AddRectangles(corridors.ToList());

            //in some cases, one of the rooms cannot be connected by a corridor
            var area = level.FindLargestArea();
            level.SetArea(area);
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
                    var room = RandomRoom(parameters);
                    var overlap = rooms.FirstOrDefault(a => a.IsOverlappingWithWall(room));
                    if (overlap == null)
                    {
                        rooms.Add(room);
                    }
                }
            }
            if (rooms.Count == 0)
            {
                rooms.Add(OriginRoom(maxRoomSize));
            }
            return rooms;
        }


        private Room RandomRoom(LevelParameters parameters)
        {
            var corner = RandomCorner(parameters);
            var size = RandomSize(corner, parameters);
            return new Room(corner, size);
        }


        private Vector2Int RandomCorner(LevelParameters parameters)
        {
            var randX = random.Next(parameters.Width - minRoomSize);
            var randY = random.Next(parameters.Height - minRoomSize);
            return new Vector2Int(randX, randY);
        }


        private Vector2Int RandomSize(Vector2Int corner, LevelParameters parameters)
        {
            var maxWidth = Math.Min(maxRoomSize, parameters.Width - corner.x + 1);
            var maxHeight = Math.Min(maxRoomSize, parameters.Height - corner.y + 1);
            return new Vector2Int(
                        minRoomSize + random.Next(maxWidth - minRoomSize),
                        minRoomSize + random.Next(maxHeight - minRoomSize));
        }


        private Room OriginRoom(int size)
        {
            return new Room(
                    new Vector2Int(0, 0),
                    new Vector2Int(size, size)
                    );
        }
    }
}
