using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.UI;
using GameWorld = ConsoleRpgStage1.World.World;

namespace ConsoleRpgStage1.Game;

public sealed class GameContext
{
    public GameContext(
        GameWorld world,
        Player player,
        Renderer renderer,
        IGameMode gameMode,
        IGameMode inventoryMode,
        string initialMessage)
    {
        World = world;
        Player = player;
        Renderer = renderer;
        GameMode = gameMode;
        InventoryMode = inventoryMode;
        CurrentMode = gameMode;
        LastMessage = initialMessage;
    }

    public GameWorld World { get; }

    public Player Player { get; }

    public Renderer Renderer { get; }

    public IGameMode GameMode { get; }

    public IGameMode InventoryMode { get; }

    public IGameMode CurrentMode { get; private set; }

    public string LastMessage { get; private set; }

    public int SelectedInventoryIndex { get; private set; }

    public bool ApplyResult(ModeResult result)
    {
        if (result.Message != null)
        {
            LastMessage = result.Message;
        }

        if (result.NextMode != null)
        {
            CurrentMode = result.NextMode;
            CurrentMode.OnEnter(this);
        }

        return result.ShouldExit;
    }

    public void MoveSelectionUp()
    {
        if (Player.Inventory.Count == 0)
        {
            SelectedInventoryIndex = 0;
            return;
        }

        SelectedInventoryIndex = Math.Max(0, SelectedInventoryIndex - 1);
    }

    public void MoveSelectionDown()
    {
        if (Player.Inventory.Count == 0)
        {
            SelectedInventoryIndex = 0;
            return;
        }

        SelectedInventoryIndex = Math.Min(Player.Inventory.Count - 1, SelectedInventoryIndex + 1);
    }

    public void ClampSelection()
    {
        if (Player.Inventory.Count == 0)
        {
            SelectedInventoryIndex = 0;
            return;
        }

        if (SelectedInventoryIndex < 0)
        {
            SelectedInventoryIndex = 0;
        }

        if (SelectedInventoryIndex >= Player.Inventory.Count)
        {
            SelectedInventoryIndex = Player.Inventory.Count - 1;
        }
    }

    public string TryDropSelectedItem()
    {
        if (Player.Inventory.Count == 0)
        {
            return "Inventory is empty.";
        }

        Player.TryDropItem(SelectedInventoryIndex, World, out var message);
        ClampSelection();
        return message;
    }

    public string TryEquipSelectedLeft()
    {
        if (Player.Inventory.Count == 0)
        {
            return "Inventory is empty.";
        }

        var result = Player.TryEquipItem(SelectedInventoryIndex, Player.LeftHand);
        ClampSelection();
        return result.Message;
    }

    public string TryEquipSelectedRight()
    {
        if (Player.Inventory.Count == 0)
        {
            return "Inventory is empty.";
        }

        var result = Player.TryEquipItem(SelectedInventoryIndex, Player.RightHand);
        ClampSelection();
        return result.Message;
    }

    public string TryUnequipLeft()
    {
        var result = Player.TryUnequip(Player.LeftHand);
        ClampSelection();
        return result.Message;
    }

    public string TryUnequipRight()
    {
        var result = Player.TryUnequip(Player.RightHand);
        ClampSelection();
        return result.Message;
    }

    public string TryMove(Direction direction)
    {
        return Player.TryMove(direction, World) ? "Moved." : "Cannot move there.";
    }

    public string TryPickUp()
    {
        return Player.TryPickUp(World) ? "Picked up item." : "No items to pick up.";
    }
}
