using ConsoleRpgStage1.Items;
using ConsoleRpgStage1.World.Tiles;

namespace ConsoleRpgStage1.World;

public sealed class Cell
{
    public Cell(Tile tile)
    {
        Tile = tile;
    }

    public Tile Tile { get; set; }

    public List<Item> Items { get; } = new();
}
