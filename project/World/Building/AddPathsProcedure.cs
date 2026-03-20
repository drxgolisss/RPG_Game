using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.World.Tiles;

namespace ConsoleRpgStage1.World.Building;

public sealed class AddPathsProcedure : IDungeonBuildProcedure
{
    private readonly int _pathCount;
    private readonly Random _random;

    public AddPathsProcedure(int pathCount, Random? random = null)
    {
        if (pathCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pathCount), "Path count cannot be negative.");
        }

        _pathCount = pathCount;
        _random = random ?? Random.Shared;
    }

    public void Apply(World world)
    {
        if (_pathCount == 0 || world.Rows < 3 || world.Cols < 3)
        {
            return;
        }

        for (var index = 0; index < _pathCount; index++)
        {
            var from = CreatePoint(world);
            var to = CreatePoint(world);
            CarvePath(world, from, to);
        }
    }

    private Position CreatePoint(World world)
    {
        var row = _random.Next(1, world.Rows - 1);
        var col = _random.Next(1, world.Cols - 1);
        return new Position(row, col);
    }

    private void CarvePath(World world, Position from, Position to)
    {
        if (_random.Next(0, 2) == 0)
        {
            CarveHorizontal(world, from.Row, from.Col, to.Col);
            CarveVertical(world, to.Col, from.Row, to.Row);
            return;
        }

        CarveVertical(world, from.Col, from.Row, to.Row);
        CarveHorizontal(world, to.Row, from.Col, to.Col);
    }

    private static void CarveHorizontal(World world, int row, int startCol, int endCol)
    {
        var minCol = Math.Min(startCol, endCol);
        var maxCol = Math.Max(startCol, endCol);

        for (var col = minCol; col <= maxCol; col++)
        {
            world.SetTile(new Position(row, col), new FloorTile());
        }
    }

    private static void CarveVertical(World world, int col, int startRow, int endRow)
    {
        var minRow = Math.Min(startRow, endRow);
        var maxRow = Math.Max(startRow, endRow);

        for (var row = minRow; row <= maxRow; row++)
        {
            world.SetTile(new Position(row, col), new FloorTile());
        }
    }
}
