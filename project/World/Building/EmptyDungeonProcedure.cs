using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.World.Tiles;

namespace ConsoleRpgStage1.World.Building;

public sealed class EmptyDungeonProcedure : IDungeonStarterProcedure
{
    public void Apply(World world)
    {
        for (var row = 0; row < world.Rows; row++)
        {
            for (var col = 0; col < world.Cols; col++)
            {
                world.SetTile(new Position(row, col), new FloorTile());
            }
        }
    }
}
