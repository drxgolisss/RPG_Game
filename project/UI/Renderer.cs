using System.Text;
using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.Entities;
using GameWorld = ConsoleRpgStage1.World.World;

namespace ConsoleRpgStage1.UI;

public sealed class Renderer
{
    public string BuildFrame(GameWorld world)
    {
        return BuildFrame(world, null);
    }

    public string BuildFrame(GameWorld world, Player? player)
    {
        var sb = new StringBuilder(world.Rows * (world.Cols + Environment.NewLine.Length));

        for (var row = 0; row < world.Rows; row++)
        {
            for (var col = 0; col < world.Cols; col++)
            {
                var currentPosition = new Position(row, col);
                var cell = world.GetCell(currentPosition);

                var symbol = player != null && player.Position == currentPosition
                    ? '¶'
                    : cell.Items.Count > 0
                        ? cell.Items[0].Symbol
                        : cell.Tile.Symbol;

                sb.Append(symbol);
            }

            if (row < world.Rows - 1)
            {
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }
}
