using System.Text;
using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.UI;
using ConsoleRpgStage1.World;

var worldFactory = new WorldFactory();
var world = worldFactory.CreateDefault();
var player = new Player(new Position(0, 0));
var renderer = new Renderer();

var lastMessage = "Use WASD/arrows to move, E to pick up, I for inventory, Q/Esc to quit.";
var inventoryMode = false;
var selectedInventoryIndex = 0;
var awaitingUnequipHand = false;

Console.OutputEncoding = Encoding.UTF8;
Console.CursorVisible = false;

try
{
    while (true)
    {
        selectedInventoryIndex = ClampInventoryIndex(player, selectedInventoryIndex);
        Render(world, player, renderer, inventoryMode, selectedInventoryIndex, awaitingUnequipHand, lastMessage);

        var key = Console.ReadKey(true);

        if (inventoryMode)
        {
            if (key.Key == ConsoleKey.I || key.Key == ConsoleKey.Escape)
            {
                inventoryMode = false;
                awaitingUnequipHand = false;
                lastMessage = "Inventory mode disabled.";
                continue;
            }

            if (awaitingUnequipHand)
            {
                if (key.Key == ConsoleKey.L)
                {
                    var result = player.TryUnequip(player.LeftHand);
                    lastMessage = result.Message;
                }
                else if (key.Key == ConsoleKey.R)
                {
                    var result = player.TryUnequip(player.RightHand);
                    lastMessage = result.Message;
                }
                else
                {
                    lastMessage = "Unequip cancelled. Press U, then L or R.";
                }

                awaitingUnequipHand = false;
                continue;
            }

            if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
            {
                if (player.Inventory.Count > 0)
                {
                    selectedInventoryIndex = Math.Max(0, selectedInventoryIndex - 1);
                }

                continue;
            }

            if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
            {
                if (player.Inventory.Count > 0)
                {
                    selectedInventoryIndex = Math.Min(player.Inventory.Count - 1, selectedInventoryIndex + 1);
                }

                continue;
            }

            if (key.Key == ConsoleKey.D)
            {
                if (player.Inventory.Count == 0)
                {
                    lastMessage = "Inventory is empty.";
                    continue;
                }

                player.TryDropItem(selectedInventoryIndex, world, out lastMessage);
                selectedInventoryIndex = ClampInventoryIndex(player, selectedInventoryIndex);
                continue;
            }

            if (key.Key == ConsoleKey.L)
            {
                if (player.Inventory.Count == 0)
                {
                    lastMessage = "Inventory is empty.";
                    continue;
                }

                var result = player.TryEquipItem(selectedInventoryIndex, player.LeftHand);
                lastMessage = result.Message;
                selectedInventoryIndex = ClampInventoryIndex(player, selectedInventoryIndex);
                continue;
            }

            if (key.Key == ConsoleKey.R)
            {
                if (player.Inventory.Count == 0)
                {
                    lastMessage = "Inventory is empty.";
                    continue;
                }

                var result = player.TryEquipItem(selectedInventoryIndex, player.RightHand);
                lastMessage = result.Message;
                selectedInventoryIndex = ClampInventoryIndex(player, selectedInventoryIndex);
                continue;
            }

            if (key.Key == ConsoleKey.U)
            {
                awaitingUnequipHand = true;
                lastMessage = "Choose hand to unequip: L or R.";
                continue;
            }

            lastMessage = "Inventory controls: Up/Down, D drop, L equip left, R equip right, U then L/R unequip.";
            continue;
        }

        if (key.Key == ConsoleKey.Q || key.Key == ConsoleKey.Escape)
        {
            break;
        }

        if (key.Key == ConsoleKey.I)
        {
            inventoryMode = true;
            awaitingUnequipHand = false;
            selectedInventoryIndex = ClampInventoryIndex(player, selectedInventoryIndex);
            lastMessage = "Inventory mode enabled.";
            continue;
        }

        if (TryGetDirection(key.Key, out var direction))
        {
            lastMessage = player.TryMove(direction, world) ? "Moved." : "Cannot move there.";
            continue;
        }

        if (key.Key == ConsoleKey.E)
        {
            lastMessage = player.TryPickUp(world) ? "Picked up item." : "No items to pick up.";
            continue;
        }

        lastMessage = "Unknown key in game mode.";
    }
}
finally
{
    Console.CursorVisible = true;
}

static int ClampInventoryIndex(Player player, int index)
{
    if (player.Inventory.Count == 0)
    {
        return 0;
    }

    if (index < 0)
    {
        return 0;
    }

    return Math.Min(index, player.Inventory.Count - 1);
}

static bool TryGetDirection(ConsoleKey key, out Direction direction)
{
    if (key == ConsoleKey.W || key == ConsoleKey.UpArrow)
    {
        direction = Direction.Up;
        return true;
    }

    if (key == ConsoleKey.S || key == ConsoleKey.DownArrow)
    {
        direction = Direction.Down;
        return true;
    }

    if (key == ConsoleKey.A || key == ConsoleKey.LeftArrow)
    {
        direction = Direction.Left;
        return true;
    }

    if (key == ConsoleKey.D || key == ConsoleKey.RightArrow)
    {
        direction = Direction.Right;
        return true;
    }

    direction = Direction.Up;
    return false;
}

static void Render(
    World world,
    Player player,
    Renderer renderer,
    bool inventoryMode,
    int selectedInventoryIndex,
    bool awaitingUnequipHand,
    string message)
{
    const int screenWidth = 80;
    const int panelRows = 12;
    const int inventoryRows = 6;

    var frame = renderer.BuildFrame(world, player);
    var mapLines = frame.Split('\n').Select(line => line.TrimEnd('\r')).ToList();

    var itemsAtFeet = world.GetItems(player.Position);
    var leftItem = player.Equipment.LeftItem?.Name ?? "empty";
    var rightItem = player.Equipment.RightItem?.Name ?? "empty";

    var panelLines = new List<string>
    {
        $"Mode: {(inventoryMode ? "INVENTORY" : "GAME")}",
        $"Inventory: {player.Inventory.Count} | Coins: {player.Coins} | Gold: {player.Gold}",
        $"Equipped: Left={leftItem}, Right={rightItem}",
        itemsAtFeet.Count == 0
            ? "Items here: none"
            : $"Items here: {string.Join(", ", itemsAtFeet.Select(item => item.Name))}",
        $"Message: {message}"
    };

    if (inventoryMode)
    {
        panelLines.Add(awaitingUnequipHand
            ? "Inventory help: U active -> press L or R to unequip, Esc/I close inventory"
            : "Inventory help: Up/Down or W/S select, D drop, L equip left, R equip right, U then L/R unequip");
        panelLines.Add("Inventory list:");

        var inventoryCount = player.Inventory.Count;
        var startIndex = 0;

        if (inventoryCount > inventoryRows)
        {
            startIndex = selectedInventoryIndex - inventoryRows / 2;
            if (startIndex < 0)
            {
                startIndex = 0;
            }

            if (startIndex > inventoryCount - inventoryRows)
            {
                startIndex = inventoryCount - inventoryRows;
            }
        }

        for (var row = 0; row < inventoryRows; row++)
        {
            var index = startIndex + row;
            if (index >= inventoryCount)
            {
                panelLines.Add(string.Empty);
                continue;
            }

            var prefix = index == selectedInventoryIndex ? ">" : " ";
            panelLines.Add($"{prefix} [{index}] {player.Inventory[index].Name}");
        }
    }
    else
    {
        panelLines.Add("Game help: WASD/arrows move, E pick up, I inventory, Q/Esc quit");
    }

    while (panelLines.Count < panelRows)
    {
        panelLines.Add(string.Empty);
    }

    if (panelLines.Count > panelRows)
    {
        panelLines = panelLines.Take(panelRows).ToList();
    }

    var sb = new StringBuilder((world.Rows + panelRows) * (screenWidth + Environment.NewLine.Length));

    for (var row = 0; row < world.Rows; row++)
    {
        var line = row < mapLines.Count ? mapLines[row] : string.Empty;
        sb.AppendLine(FitLine(line, screenWidth));
    }

    foreach (var panelLine in panelLines)
    {
        sb.AppendLine(FitLine(panelLine, screenWidth));
    }

    Console.SetCursorPosition(0, 0);
    Console.Write(sb.ToString());

    var safeRow = Math.Min(world.Rows + panelRows, Console.BufferHeight - 1);
    Console.SetCursorPosition(0, safeRow);
}

static string FitLine(string line, int width)
{
    if (line.Length >= width)
    {
        return line[..width];
    }

    return line.PadRight(width);
}
