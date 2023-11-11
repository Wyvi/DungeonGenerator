namespace DungeonGenerator.Structures
{
    public class DungeonSettings
    {
        public int MinWallThickness { get; init; }
        public int CorridorWidth { get; init; }

        public DungeonSettings(int minWallThickness = 1, int corridorWidth = 1)
        {
            MinWallThickness = minWallThickness;
            CorridorWidth = corridorWidth;
        }
    }
}
