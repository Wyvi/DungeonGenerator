using DungeonGenerator.Structures;

namespace DungeonGenerator.Generators
{
    public interface ILevelGenerator
    {
        Level GenerateLevel(LevelParameters parameters);
    }
}
