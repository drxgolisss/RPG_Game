using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.Items;
using ConsoleRpgStage1.World.Tiles;

namespace ConsoleRpgStage1.World;

public sealed class World
{
    private readonly Cell[,] _cells;

    public World(int rows, int cols, Tile defaultTile)
    {
        if (rows <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rows), "Rows must be greater than zero.");
        }

        if (cols <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cols), "Cols must be greater than zero.");
        }

        Rows = rows;
        Cols = cols;
        _cells = new Cell[rows, cols];

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                _cells[row, col] = new Cell(defaultTile);
            }
        }
    }

    public int Rows { get; }

    public int Cols { get; }

    public bool InBounds(Position p)
    {
        return p.Row >= 0 && p.Row < Rows && p.Col >= 0 && p.Col < Cols;
    }

    public bool CanEnter(Position p)
    {
        if (!InBounds(p))
        {
            return false;
        }

        return _cells[p.Row, p.Col].Tile.IsPassable;
    }

    public Cell GetCell(Position p)
    {
        EnsureInBounds(p);
        return _cells[p.Row, p.Col];
    }

    public void SetTile(Position p, Tile tile)
    {
        EnsureInBounds(p);
        _cells[p.Row, p.Col].Tile = tile;
    }

    public void AddItem(Position p, Item item)
    {
        EnsureInBounds(p);
        _cells[p.Row, p.Col].AddItem(item);
    }

    public IReadOnlyList<Item> GetItems(Position p)
    {
        EnsureInBounds(p);
        return _cells[p.Row, p.Col].Items;
    }

    private void EnsureInBounds(Position p)
    {
        if (!InBounds(p))
        {
            throw new ArgumentOutOfRangeException(nameof(p), $"Position ({p.Row},{p.Col}) is outside world bounds.");
        }
    }
}
