namespace DungeonGenerator.Kruskal
{
    public class Edge : IComparable<Edge>
    {
        public int FirstPointIndex { get; init; }
        public int SecondPointIndex { get; init; }
        public float Distance { get; init; }

        public Edge(int firstPointIndex, int secondPointIndex, float distance)
        {
            if (firstPointIndex < 0)
            {
                throw new ArgumentException("Point index must be >0.", nameof(firstPointIndex));
            }
            if (secondPointIndex < 0)
            {
                throw new ArgumentException("Point index must be >0.", nameof(secondPointIndex));
            }
            if (firstPointIndex == secondPointIndex)
            {
                throw new ArgumentException("Secon point index must be different", nameof(secondPointIndex));
            }
            FirstPointIndex = firstPointIndex;
            SecondPointIndex = secondPointIndex;
            Distance = distance;
        }

        public bool ContainPoint(int pointIndex)
        {
            if (FirstPointIndex == pointIndex || SecondPointIndex == pointIndex)
            {
                return true;
            }
            return false;
        }

        public int AnotherPoint(int point)
        {
            if (point == FirstPointIndex)
            {
                return SecondPointIndex;
            }
            else if (point == SecondPointIndex)
            {
                return FirstPointIndex;
            }
            else
            {
                throw new ArgumentException("The point is not part of the edge.", nameof(point));
            }
        }

        public int CompareTo(Edge? other)
        {
            if (other == null)
            {
                return 1;
            }
            return Distance.CompareTo(other.Distance);
        }

        public override string ToString()
        {
            return $"({FirstPointIndex}, {SecondPointIndex}, {Distance})";
        }
    }
}
