using System.Text;
using ConsoleRpgStage1.Game;

namespace ConsoleRpgStage1.UI;

public sealed class GameScreenComposer
{
    private const int ScreenWidth = 80;
    private const int SeparatorWidth = 3;

    public string Build(GameContext context)
    {
        var world = context.World;
        var player = context.Player;
        var mapWidth = world.Cols;
        var panelWidth = Math.Max(10, ScreenWidth - mapWidth - SeparatorWidth);
        var panelRows = world.Rows;

        var frame = context.Renderer.BuildFrame(world, player);
        var mapLines = frame.Split('\n').Select(line => line.TrimEnd('\r')).ToList();

        var itemsAtFeet = world.GetItems(player.Position);
        var leftItem = player.Equipment.LeftItem?.Name ?? "empty";
        var rightItem = player.Equipment.RightItem?.Name ?? "empty";

        var panelLines = new List<string>();
        AppendWrappedLine(panelLines, $"Mode: {context.CurrentMode.Name}", panelWidth);
        AppendWrappedLine(panelLines, $"Inventory: {player.Inventory.Count} | Coins: {player.Coins} | Gold: {player.Gold}", panelWidth);
        AppendWrappedLine(panelLines, $"Stats: {player.Stats}", panelWidth);
        AppendWrappedLine(panelLines, $"Equipped: Left={leftItem}, Right={rightItem}", panelWidth);
        AppendWrappedLine(
            panelLines,
            itemsAtFeet.Count == 0
                ? "Items here: none"
                : $"Items here: {string.Join(", ", itemsAtFeet.Select(item => item.Name))}",
            panelWidth);
        AppendWrappedLine(panelLines, $"Message: {context.LastMessage}", panelWidth);

        foreach (var helpLine in context.CurrentMode.HelpLines)
        {
            AppendWrappedLine(panelLines, $"Help: {helpLine}", panelWidth);
        }

        if (ReferenceEquals(context.CurrentMode, context.InventoryMode))
        {
            AppendWrappedLine(panelLines, "Inventory list:", panelWidth);
            AppendInventoryRows(panelLines, context, panelRows);
        }

        while (panelLines.Count < panelRows)
        {
            panelLines.Add(string.Empty);
        }

        if (panelLines.Count > panelRows)
        {
            panelLines = panelLines.Take(panelRows).ToList();
        }

        var sb = new StringBuilder(panelRows * (ScreenWidth + Environment.NewLine.Length));

        for (var row = 0; row < panelRows; row++)
        {
            var mapLine = row < mapLines.Count ? mapLines[row] : string.Empty;
            var sideLine = row < panelLines.Count ? panelLines[row] : string.Empty;
            var composedLine = $"{FitLine(mapLine, mapWidth)} | {FitLine(sideLine, panelWidth)}";
            sb.AppendLine(FitLine(composedLine, ScreenWidth));
        }

        return sb.ToString();
    }

    public int GetSafeCursorRow(GameContext context)
    {
        return Math.Min(context.World.Rows, Console.BufferHeight - 1);
    }

    private static void AppendInventoryRows(List<string> panelLines, GameContext context, int panelRows)
    {
        var inventoryCount = context.Player.Inventory.Count;
        var selectedIndex = context.SelectedInventoryIndex;
        var rowsLeft = panelRows - panelLines.Count;

        if (rowsLeft <= 0)
        {
            return;
        }

        if (inventoryCount == 0)
        {
            panelLines.Add("(empty)");
            return;
        }

        var startIndex = 0;
        if (inventoryCount > rowsLeft)
        {
            startIndex = selectedIndex - rowsLeft / 2;
            if (startIndex < 0)
            {
                startIndex = 0;
            }

            if (startIndex > inventoryCount - rowsLeft)
            {
                startIndex = inventoryCount - rowsLeft;
            }
        }

        for (var row = 0; row < rowsLeft; row++)
        {
            var index = startIndex + row;
            if (index >= inventoryCount)
            {
                panelLines.Add(string.Empty);
                continue;
            }

            var prefix = index == selectedIndex ? ">" : " ";
            panelLines.Add($"{prefix} [{index}] {context.Player.Inventory[index].Name}");
        }
    }

    private static void AppendWrappedLine(List<string> panelLines, string line, int width)
    {
        if (line.Length <= width)
        {
            panelLines.Add(line);
            return;
        }

        var words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var current = string.Empty;

        foreach (var word in words)
        {
            if (word.Length > width)
            {
                if (current.Length > 0)
                {
                    panelLines.Add(current);
                    current = string.Empty;
                }

                var start = 0;
                while (start < word.Length)
                {
                    var take = Math.Min(width, word.Length - start);
                    panelLines.Add(word.Substring(start, take));
                    start += take;
                }

                continue;
            }

            if (current.Length == 0)
            {
                current = word;
                continue;
            }

            if (current.Length + 1 + word.Length <= width)
            {
                current += " " + word;
                continue;
            }

            panelLines.Add(current);
            current = word;
        }

        if (current.Length > 0)
        {
            panelLines.Add(current);
        }
    }

    private static string FitLine(string line, int width)
    {
        if (line.Length >= width)
        {
            return line[..width];
        }

        return line.PadRight(width);
    }
}
