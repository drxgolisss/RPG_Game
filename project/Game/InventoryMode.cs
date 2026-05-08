using ConsoleRpgStage1.Game.Controls;
using ConsoleRpgStage1.Game.Instructions;
using ConsoleRpgStage1.Logging;

namespace ConsoleRpgStage1.Game;

public sealed class InventoryMode : IGameMode
{
    private readonly IReadOnlyList<ModeActionBinding> _awaitUnequipBindings;
    private readonly IReadOnlyList<ModeActionBinding> _browseBindings;
    private readonly IReadOnlyList<ModeActionBinding> _globalBindings;
    private readonly Dictionary<ConsoleKey, Func<GameContext, ModeResult>> _globalKeyMap;
    private readonly Dictionary<ConsoleKey, Func<GameContext, ModeResult>> _browseKeyMap;
    private readonly Dictionary<ConsoleKey, Func<GameContext, ModeResult>> _awaitUnequipKeyMap;
    private readonly InstructionBuilder _awaitUnequipInstructionBuilder;
    private readonly InstructionBuilder _browseInstructionBuilder;
    private bool _awaitingUnequipHand;

    public InventoryMode()
    {
        _globalBindings =
        [
            new ModeActionBinding(
                "Close inventory",
                [ConsoleKey.I, ConsoleKey.Escape],
                context => CloseInventory(context)),
            new ModeActionBinding(
                "Event log",
                [ConsoleKey.J],
                context => context.ShowEventLog())
        ];

        _browseBindings =
        [
            new ModeActionBinding(
                "Select previous",
                [ConsoleKey.UpArrow, ConsoleKey.W],
                context =>
                {
                    context.MoveSelectionUp();
                    return ModeResult.Continue();
                }),
            new ModeActionBinding(
                "Select next",
                [ConsoleKey.DownArrow, ConsoleKey.S],
                context =>
                {
                    context.MoveSelectionDown();
                    return ModeResult.Continue();
                }),
            new ModeActionBinding(
                "Drop",
                [ConsoleKey.D],
                context => ModeResult.Continue(context.TryDropSelectedItem()),
                context => context.Player.Inventory.Count > 0),
            new ModeActionBinding(
                "Equip left",
                [ConsoleKey.L],
                context => ModeResult.Continue(context.TryEquipSelectedLeft()),
                context => context.Player.Inventory.Count > 0),
            new ModeActionBinding(
                "Equip right",
                [ConsoleKey.R],
                context => ModeResult.Continue(context.TryEquipSelectedRight()),
                context => context.Player.Inventory.Count > 0),
            new ModeActionBinding(
                "Start unequip",
                [ConsoleKey.U],
                _ =>
                {
                    _awaitingUnequipHand = true;
                    return ModeResult.Continue("Choose hand to unequip: L or R.");
                },
                context => context.Player.Equipment.LeftItem != null || context.Player.Equipment.RightItem != null)
        ];

        _awaitUnequipBindings =
        [
            new ModeActionBinding(
                "Unequip left",
                [ConsoleKey.L],
                context =>
                {
                    _awaitingUnequipHand = false;
                    return ModeResult.Continue(context.TryUnequipLeft());
                }),
            new ModeActionBinding(
                "Unequip right",
                [ConsoleKey.R],
                context =>
                {
                    _awaitingUnequipHand = false;
                    return ModeResult.Continue(context.TryUnequipRight());
                })
        ];

        _globalKeyMap = BuildKeyMap(_globalBindings);
        _browseKeyMap = BuildKeyMap(_browseBindings);
        _awaitUnequipKeyMap = BuildKeyMap(_awaitUnequipBindings);

        _browseInstructionBuilder = new InstructionBuilder()
            .StartWith(new ActionBindingsInstructionProcedure(() => _browseBindings.Concat(_globalBindings).ToArray()));

        _awaitUnequipInstructionBuilder = new InstructionBuilder()
            .StartWith(new BaseInstructionsProcedure("Awaiting hand selection"))
            .Apply(new ActionBindingsInstructionProcedure(() => _awaitUnequipBindings.Concat(_globalBindings).ToArray()));
    }

    public string Name => "INVENTORY";

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
            GameLogger.Instance.AddEntry($"Unknown key pressed while choosing item to unequip: {key.Key}.");
            return ModeResult.Continue("Unequip cancelled. Press U, then L or R.");
        }

        if (_browseKeyMap.TryGetValue(key.Key, out var browseCommand))
        {
            return browseCommand(context);
        }

        GameLogger.Instance.AddEntry($"Unknown key pressed in inventory mode: {key.Key}.");
        return ModeResult.Continue("Inventory controls: Up/Down, D drop, L equip left, R equip right, U then L/R unequip.");
    }

    private static ModeResult CloseInventory(GameContext context)
    {
        return ModeResult.SwitchTo(context.GameMode, "Inventory mode disabled.");
    }

    public IReadOnlyList<string> GetHelpLines(GameContext context)
    {
        if (_awaitingUnequipHand)
        {
            return _awaitUnequipInstructionBuilder.Build(context);
        }

        return _browseInstructionBuilder.Build(context);
    }

    private static Dictionary<ConsoleKey, Func<GameContext, ModeResult>> BuildKeyMap(IEnumerable<ModeActionBinding> bindings)
    {
        var keyMap = new Dictionary<ConsoleKey, Func<GameContext, ModeResult>>();

        foreach (var binding in bindings)
        {
            foreach (var key in binding.Keys)
            {
                keyMap[key] = binding.Execute;
            }
        }

        return keyMap;
    }
}
