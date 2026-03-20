using ConsoleRpgStage1.Core;

namespace ConsoleRpgStage1.World.Building;

internal static class DungeonPlacementHelper
{
    public static List<Position> GetWalkablePositions(World world)
    {
        var positions = new List<Position>();

        for (var row = 0; row < world.Rows; row++)
        {
            for (var col = 0; col < world.Cols; col++)
            {
                var position = new Position(row, col);
                if (world.CanEnter(position))
                {
                    positions.Add(position);
                }
            }
        }

        return positions;
    }

    public static void Shuffle<T>(IList<T> values, Random random)
    {
        for (var index = values.Count - 1; index > 0; index--)
        {
            var swapIndex = random.Next(index + 1);
            (values[index], values[swapIndex]) = (values[swapIndex], values[index]);
        }
    }
}
