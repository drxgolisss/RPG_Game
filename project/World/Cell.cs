using ConsoleRpgStage1.Items;
using ConsoleRpgStage1.World.Tiles;

namespace ConsoleRpgStage1.World;

public sealed class Cell
{
    private readonly List<Item> _items = new();

    public Cell(Tile tile)
    {
        Tile = tile;
    }

    public Tile Tile { get; set; }

    public IReadOnlyList<Item> Items => _items;

    public void AddItem(Item item)
    {
        _items.Add(item);
    }

    public bool TryTakeFirstItem(out Item item)
    {
        if (_items.Count == 0)
        {
            item = null!;
            return false;
        }

        item = _items[0];
        _items.RemoveAt(0);
        return true;
    }
}
