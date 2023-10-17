namespace DungeonGenerator;
class Program
{


    static void Main(string[] args)
    {
        var ruggedCave = new LevelParameters(30, 30, 0.4);
        var bigCave = new LevelParameters(30, 30, 0.6);

        
        var caveGenerator = new CaveGenerator();
        var dungeonLevel = caveGenerator.GenerateLevel(bigCave);
        dungeonLevel.WriteToConsole();

        dungeonLevel = caveGenerator.GenerateLevel(ruggedCave);
        dungeonLevel.WriteToConsole();
        

        var roomGenerator = new RoomGenerator();
        var roomLevel = roomGenerator.GenerateLevel(bigCave);
        roomLevel.WriteToConsole();
        Console.WriteLine( roomLevel.NumOfConectedAreas().ToString());

        roomLevel = roomGenerator.GenerateLevel(ruggedCave);
        roomLevel.WriteToConsole();
        Console.WriteLine( roomLevel.NumOfConectedAreas().ToString());

       
    }
}