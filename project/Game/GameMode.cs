using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.Game.Controls;
using ConsoleRpgStage1.Game.Instructions;

namespace ConsoleRpgStage1.Game;

public sealed class GameMode : IGameMode
{
    private readonly IReadOnlyList<ModeActionBinding> _actionBindings;
    private readonly Dictionary<ConsoleKey, Func<GameContext, ModeResult>> _keyMap;
    private readonly InstructionBuilder _instructionBuilder;

    public GameMode()
    {
        _actionBindings =
        [
            new ModeActionBinding(
                "Move up",
                [ConsoleKey.W, ConsoleKey.UpArrow],
                context => ModeResult.Continue(context.TryMove(Direction.Up))),
            new ModeActionBinding(
                "Move down",
                [ConsoleKey.S, ConsoleKey.DownArrow],
                context => ModeResult.Continue(context.TryMove(Direction.Down))),
            new ModeActionBinding(
                "Move left",
                [ConsoleKey.A, ConsoleKey.LeftArrow],
                context => ModeResult.Continue(context.TryMove(Direction.Left))),
            new ModeActionBinding(
                "Move right",
                [ConsoleKey.D, ConsoleKey.RightArrow],
                context => ModeResult.Continue(context.TryMove(Direction.Right))),
            new ModeActionBinding(
                "Pick up",
                [ConsoleKey.E],
                context => ModeResult.Continue(context.TryPickUp()),
                context => context.World.GetItems(context.Player.Position).Count > 0),
            new ModeActionBinding(
                "Normal attack",
                [ConsoleKey.D1, ConsoleKey.NumPad1],
                context => context.TryNormalAttack(),
                context => context.HasEnemyNearby()),
            new ModeActionBinding(
                "Stealth attack",
                [ConsoleKey.D2, ConsoleKey.NumPad2],
                context => context.TryStealthAttack(),
                context => context.HasEnemyNearby()),
            new ModeActionBinding(
                "Magical attack",
                [ConsoleKey.D3, ConsoleKey.NumPad3],
                context => context.TryMagicalAttack(),
                context => context.HasEnemyNearby()),
            new ModeActionBinding(
                "Inventory",
                [ConsoleKey.I],
                context => ModeResult.SwitchTo(context.InventoryMode, "Inventory mode enabled.")),
            new ModeActionBinding(
                "Quit",
                [ConsoleKey.Q, ConsoleKey.Escape],
                _ => ModeResult.Exit())
        ];

        _keyMap = BuildKeyMap(_actionBindings);

        _instructionBuilder = new InstructionBuilder()
            .StartWith(new ActionBindingsInstructionProcedure(() => _actionBindings));
    }

    public string Name => "GAME";

    public ModeResult HandleKey(ConsoleKeyInfo key, GameContext context)
    {
        return _keyMap.TryGetValue(key.Key, out var command)
            ? command(context)
            : ModeResult.Continue("Unknown key in game mode.");
    }

    public IReadOnlyList<string> GetHelpLines(GameContext context)
    {
        return _instructionBuilder.Build(context);
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
