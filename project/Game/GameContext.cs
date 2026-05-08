using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.Combat;
using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Logging;
using ConsoleRpgStage1.UI;
using GameWorld = ConsoleRpgStage1.World.World;

namespace ConsoleRpgStage1.Game;

public sealed class GameContext
{
    private static readonly IAttackStyle NormalAttackStyle = new NormalAttackStyle();
    private static readonly IAttackStyle StealthAttackStyle = new StealthAttackStyle();
    private static readonly IAttackStyle MagicalAttackStyle = new MagicalAttackStyle();
    private static readonly IReadOnlyList<Direction> CombatDirections =
    [
        Direction.Up,
        Direction.Down,
        Direction.Left,
        Direction.Right
    ];

    private readonly CombatResolver _combatResolver;

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
        _combatResolver = new CombatResolver();
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

        if (result.IsSuccess)
        {
            GameLogger.Instance.AddEntry(result.Message);
        }

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

        if (result.IsSuccess)
        {
            GameLogger.Instance.AddEntry(result.Message);
        }

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
        if (Player.TryMove(direction, World))
        {
            return "Moved.";
        }

        GameLogger.Instance.AddEntry($"Attempted to walk into a wall while moving {direction}.");
        return "Cannot move there.";
    }

    public string TryPickUp()
    {
        var items = World.GetItems(Player.Position);
        if (items.Count == 0)
        {
            return "No items to pick up.";
        }

        var itemName = items[0].Name;

        if (!Player.TryPickUp(World))
        {
            return "No items to pick up.";
        }

        GameLogger.Instance.AddEntry($"Picked up {itemName}.");
        return "Picked up item.";
    }

    public ModeResult ShowEventLog()
    {
        new EventLogScreen().Show();
        return ModeResult.Continue("Event log closed.");
    }

    public bool HasEnemyNearby()
    {
        return TryFindAdjacentEnemy(out _);
    }

    public ModeResult TryNormalAttack()
    {
        return ResolveCombatTurn(NormalAttackStyle);
    }

    public ModeResult TryStealthAttack()
    {
        return ResolveCombatTurn(StealthAttackStyle);
    }

    public ModeResult TryMagicalAttack()
    {
        return ResolveCombatTurn(MagicalAttackStyle);
    }

    private ModeResult ResolveCombatTurn(IAttackStyle attackStyle)
    {
        if (!TryFindAdjacentEnemy(out var enemy))
        {
            return ModeResult.Continue("No enemy nearby.");
        }

        var combatResult = _combatResolver.ResolveTurn(Player, enemy, attackStyle);
        GameLogger.Instance.AddEntry($"{Player.Name} used {attackStyle.Name.ToLowerInvariant()} attack and dealt {combatResult.DamageToEnemy} damage to {enemy.Name}.");

        if (combatResult.EnemyDefeated)
        {
            World.RemoveEnemy(enemy.Position, enemy);
            GameLogger.Instance.AddEntry($"Defeated {enemy.Name}.");
        }
        else
        {
            GameLogger.Instance.AddEntry($"{enemy.Name} attacked {Player.Name} and dealt {combatResult.DamageToPlayer} damage.");
        }

        var message = BuildCombatMessage(enemy, attackStyle, combatResult);

        if (combatResult.PlayerDefeated)
        {
            return ModeResult.Exit(message);
        }

        return ModeResult.Continue(message);
    }

    private string BuildCombatMessage(Enemy enemy, IAttackStyle attackStyle, CombatResult combatResult)
    {
        var parts = new List<string>
        {
            $"Used {attackStyle.Name.ToLowerInvariant()} attack.",
            $"{enemy.Name} took {combatResult.DamageToEnemy} damage."
        };

        if (combatResult.EnemyDefeated)
        {
            parts.Add($"{enemy.Name} defeated.");
        }
        else
        {
            parts.Add($"{enemy.Name} HP: {enemy.Health}.");
            parts.Add($"{Player.Name} took {combatResult.DamageToPlayer} damage.");
        }

        if (combatResult.PlayerDefeated)
        {
            parts.Add("Game over: you were defeated.");
            parts.Add($"Log file: {GameLogger.Instance.LogFilePath}");
        }
        else
        {
            parts.Add($"Your HP: {Player.Stats.Health}.");
        }

        return string.Join(" ", parts);
    }

    private bool TryFindAdjacentEnemy(out Enemy enemy)
    {
        foreach (var direction in CombatDirections)
        {
            var position = new Position(
                Player.Position.Row + direction.DeltaRow,
                Player.Position.Col + direction.DeltaCol);

            if (!World.InBounds(position))
            {
                continue;
            }

            var enemies = World.GetEnemies(position);
            if (enemies.Count == 0)
            {
                continue;
            }

            enemy = enemies[0];
            return true;
        }

        enemy = null!;
        return false;
    }
}
