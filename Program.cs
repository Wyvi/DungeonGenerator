// See https://aka.ms/new-console-template for more information
namespace DungeonGenerator;
class Program
{

    static void Main(string[] args)
    {
        var caveGenerator = new CaveGenerator();
        var dungeonLevel = caveGenerator.GenerateLevel(new LevelParameters(20,40,0.55));
    }
}