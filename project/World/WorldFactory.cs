using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.Items;
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
        PlacePredefinedItems(world);
        return world;
    }

    private static void PlacePredefinedItems(World world)
    {
        world.AddItem(new Position(1, 1), new ShortSwordItem());
        world.AddItem(new Position(2, 3), new BattleAxeItem());
        world.AddItem(new Position(3, 6), new GreatswordItem());

        world.AddItem(new Position(1, 3), new ItemCoin());
        world.AddItem(new Position(2, 5), new ItemGold());

        world.AddItem(new Position(4, 3), new ItemRock());
        world.AddItem(new Position(5, 5), new BrokenBottleItem());
        world.AddItem(new Position(6, 7), new RustyGearItem());
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

        world.SetTile(new Position(0, 0), new FloorTile());
        world.SetTile(new Position(0, 1), new FloorTile());
        world.SetTile(new Position(1, 0), new FloorTile());
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
