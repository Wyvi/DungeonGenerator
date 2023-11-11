namespace DungeonGenerator.Structures
{
    public class LevelParameters
    {
        public int Width { get; init; }
        public int Height { get; init; }
        public double WalkableFloorChance { get; init; }

        public DungeonSettings Settings { get; init; }

        public LevelParameters(int width, int height, double walkableFloorChance, DungeonSettings settings)
        {
            if (width < 1)
            {
                throw new ArgumentException("width must be >= 1", nameof(width));
            }
            if (height < 1)
            {
                throw new ArgumentException("height must be >= 1", nameof(height));
            }
            if (walkableFloorChance < 0 || walkableFloorChance > 1)
            {
                throw new ArgumentException("walkableFloorChance must be must be from 0 to 1", nameof(walkableFloorChance));
            }

            Width = width;
            Height = height;
            WalkableFloorChance = walkableFloorChance;
            Settings = settings;
        }
    }
}
