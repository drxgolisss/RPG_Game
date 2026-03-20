using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.World.Tiles;

namespace ConsoleRpgStage1.World.Building;

public sealed class AddChambersProcedure : IDungeonBuildProcedure
{
    private readonly int _count;
    private readonly int _maxHeight;
    private readonly int _maxWidth;
    private readonly int _minHeight;
    private readonly int _minWidth;
    private readonly Random _random;

    public AddChambersProcedure(int count, int minWidth = 3, int maxWidth = 8, int minHeight = 3, int maxHeight = 6, Random? random = null)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Count cannot be negative.");
        }

        if (minWidth <= 0 || maxWidth <= 0 || minHeight <= 0 || maxHeight <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(minWidth), "Room sizes must be greater than zero.");
        }

        if (minWidth > maxWidth)
        {
            throw new ArgumentException("Minimum width cannot be greater than maximum width.", nameof(minWidth));
        }

        if (minHeight > maxHeight)
        {
            throw new ArgumentException("Minimum height cannot be greater than maximum height.", nameof(minHeight));
        }

        _count = count;
        _minWidth = minWidth;
        _maxWidth = maxWidth;
        _minHeight = minHeight;
        _maxHeight = maxHeight;
        _random = random ?? Random.Shared;
    }

    public void Apply(World world)
    {
        if (_count == 0 || world.Rows < 3 || world.Cols < 3)
        {
            return;
        }

        var maxCarveWidth = world.Cols - 2;
        var maxCarveHeight = world.Rows - 2;

        if (maxCarveWidth <= 0 || maxCarveHeight <= 0)
        {
            return;
        }

        var widthUpperBound = Math.Min(_maxWidth, maxCarveWidth);
        var heightUpperBound = Math.Min(_maxHeight, maxCarveHeight);

        if (_minWidth > widthUpperBound || _minHeight > heightUpperBound)
        {
            return;
        }

        for (var index = 0; index < _count; index++)
        {
            var roomWidth = _random.Next(_minWidth, widthUpperBound + 1);
            var roomHeight = _random.Next(_minHeight, heightUpperBound + 1);

            var maxStartCol = world.Cols - roomWidth;
            var maxStartRow = world.Rows - roomHeight;

            if (maxStartCol <= 0 || maxStartRow <= 0)
            {
                continue;
            }

            var startCol = _random.Next(1, maxStartCol);
            var startRow = _random.Next(1, maxStartRow);

            CarveRectangle(world, startRow, startCol, roomHeight, roomWidth);
        }
    }

    private static void CarveRectangle(World world, int startRow, int startCol, int height, int width)
    {
        for (var row = startRow; row < startRow + height; row++)
        {
            for (var col = startCol; col < startCol + width; col++)
            {
                world.SetTile(new Position(row, col), new FloorTile());
            }
        }
    }
}
