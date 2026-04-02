using ConsoleRpgStage1.Items;
using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.World.Tiles;

namespace ConsoleRpgStage1.World;

public sealed class Cell
{
    private readonly List<Enemy> _enemies = new();
    private readonly List<Item> _items = new();

    public Cell(Tile tile)
    {
        Tile = tile;
    }

    public Tile Tile { get; set; }

    public IReadOnlyList<Enemy> Enemies => _enemies;

    public IReadOnlyList<Item> Items => _items;

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public bool RemoveEnemy(Enemy enemy)
    {
        return _enemies.Remove(enemy);
    }

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
