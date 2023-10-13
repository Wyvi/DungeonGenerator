namespace DungeonGenerator;
class Program
{

    static void Main(string[] args)
    {
        var start = DateTime.Now;
        var caveGenerator = new CaveGenerator();
        var dungeonLevel = caveGenerator.GenerateLevel(new LevelParameters(20,40,0.4));
        Console.WriteLine((DateTime.Now - start).ToString());
        dungeonLevel.WriteToConsole();
    }
}