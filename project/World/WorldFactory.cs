using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.World.Tiles;

namespace ConsoleRpgStage1.World;

public sealed class WorldFactory
{
    public const int DefaultRows = 20;
    public const int DefaultCols = 40;

    public World CreateDefault()
    {
        var world = new World(DefaultRows, DefaultCols, new FloorTile());
        BuildBorderWalls(world);
        BuildInnerWalls(world);
        return world;
    }

    private static void BuildBorderWalls(World world)
    {
        for (var col = 0; col < world.Cols; col++)
        {
            world.SetTile(new Position(0, col), new WallTile());
            world.SetTile(new Position(world.Rows - 1, col), new WallTile());
        }

        for (var row = 1; row < world.Rows - 1; row++)
        {
            world.SetTile(new Position(row, 0), new WallTile());
            world.SetTile(new Position(row, world.Cols - 1), new WallTile());
        }
    }

    private static void BuildInnerWalls(World world)
    {
        for (var col = 8; col <= 30; col++)
        {
            world.SetTile(new Position(5, col), new WallTile());
        }

        for (var row = 8; row <= 15; row++)
        {
            world.SetTile(new Position(row, 18), new WallTile());
        }

        for (var col = 5; col <= 14; col++)
        {
            world.SetTile(new Position(12, col), new WallTile());
        }
    }
}
