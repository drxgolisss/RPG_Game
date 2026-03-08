namespace ConsoleRpgStage1.Game;

public sealed class InventoryMode : IGameMode
{
    private static readonly IReadOnlyList<string> BrowseHelpLines = new[]
    {
        "Up/Down or W/S select",
        "D drop, L equip left, R equip right, U then L/R unequip"
    };

    private static readonly IReadOnlyList<string> AwaitUnequipHelpLines = new[]
    {
        "Awaiting hand selection",
        "Press L or R to unequip"
    };

    private readonly Dictionary<ConsoleKey, Func<GameContext, ModeResult>> _globalKeyMap;
    private readonly Dictionary<ConsoleKey, Func<GameContext, ModeResult>> _browseKeyMap;
    private readonly Dictionary<ConsoleKey, Func<GameContext, ModeResult>> _awaitUnequipKeyMap;
    private bool _awaitingUnequipHand;

    public InventoryMode()
    {
        _globalKeyMap = new Dictionary<ConsoleKey, Func<GameContext, ModeResult>>
        {
            [ConsoleKey.I] = context => CloseInventory(context),
            [ConsoleKey.Escape] = context => CloseInventory(context)
        };

        _browseKeyMap = new Dictionary<ConsoleKey, Func<GameContext, ModeResult>>
        {
            [ConsoleKey.UpArrow] = context =>
            {
                context.MoveSelectionUp();
                return ModeResult.Continue();
            },
            [ConsoleKey.W] = context =>
            {
                context.MoveSelectionUp();
                return ModeResult.Continue();
            },
            [ConsoleKey.DownArrow] = context =>
            {
                context.MoveSelectionDown();
                return ModeResult.Continue();
            },
            [ConsoleKey.S] = context =>
            {
                context.MoveSelectionDown();
                return ModeResult.Continue();
            },
            [ConsoleKey.D] = context => ModeResult.Continue(context.TryDropSelectedItem()),
            [ConsoleKey.L] = context => ModeResult.Continue(context.TryEquipSelectedLeft()),
            [ConsoleKey.R] = context => ModeResult.Continue(context.TryEquipSelectedRight()),
            [ConsoleKey.U] = _ =>
            {
                _awaitingUnequipHand = true;
                return ModeResult.Continue("Choose hand to unequip: L or R.");
            }
        };

        _awaitUnequipKeyMap = new Dictionary<ConsoleKey, Func<GameContext, ModeResult>>
        {
            [ConsoleKey.L] = context =>
            {
                _awaitingUnequipHand = false;
                return ModeResult.Continue(context.TryUnequipLeft());
            },
            [ConsoleKey.R] = context =>
            {
                _awaitingUnequipHand = false;
                return ModeResult.Continue(context.TryUnequipRight());
            }
        };
    }

    public string Name => "INVENTORY";

    public IReadOnlyList<string> HelpLines => _awaitingUnequipHand ? AwaitUnequipHelpLines : BrowseHelpLines;

    public void OnEnter(GameContext context)
    {
        _awaitingUnequipHand = false;
        context.ClampSelection();
    }

    public ModeResult HandleKey(ConsoleKeyInfo key, GameContext context)
    {
        if (_globalKeyMap.TryGetValue(key.Key, out var globalCommand))
        {
            _awaitingUnequipHand = false;
            return globalCommand(context);
        }

        if (_awaitingUnequipHand)
        {
            if (_awaitUnequipKeyMap.TryGetValue(key.Key, out var awaitCommand))
            {
                return awaitCommand(context);
            }

            _awaitingUnequipHand = false;
            return ModeResult.Continue("Unequip cancelled. Press U, then L or R.");
        }

        return _browseKeyMap.TryGetValue(key.Key, out var browseCommand)
            ? browseCommand(context)
            : ModeResult.Continue("Inventory controls: Up/Down, D drop, L equip left, R equip right, U then L/R unequip.");
    }

    private static ModeResult CloseInventory(GameContext context)
    {
        return ModeResult.SwitchTo(context.GameMode, "Inventory mode disabled.");
    }
}
