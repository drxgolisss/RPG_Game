using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.World.Tiles;

namespace ConsoleRpgStage1.World.Building;

public sealed class AddCentralRoomProcedure : IDungeonBuildProcedure
{
    private readonly int _height;
    private readonly int _width;

    public AddCentralRoomProcedure(int width, int height)
    {
        if (width <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(width), "Width must be greater than zero.");
        }

        if (height <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(height), "Height must be greater than zero.");
        }

        _width = width;
        _height = height;
    }

    public void Apply(World world)
    {
        if (world.Rows < 3 || world.Cols < 3)
        {
            return;
        }

        var roomWidth = Math.Min(_width, world.Cols - 2);
        var roomHeight = Math.Min(_height, world.Rows - 2);

        if (roomWidth <= 0 || roomHeight <= 0)
        {
            return;
        }

        var startRow = (world.Rows - roomHeight) / 2;
        var startCol = (world.Cols - roomWidth) / 2;

        CarveRectangle(world, startRow, startCol, roomHeight, roomWidth);
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
