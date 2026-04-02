using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.Game.Instructions;

namespace ConsoleRpgStage1.Game;

public sealed class GameMode : IGameMode
{
    private readonly Dictionary<ConsoleKey, Func<GameContext, ModeResult>> _keyMap;
    private readonly InstructionBuilder _instructionBuilder;

    public GameMode()
    {
        _keyMap = new Dictionary<ConsoleKey, Func<GameContext, ModeResult>>
        {
            [ConsoleKey.Q] = _ => ModeResult.Exit(),
            [ConsoleKey.Escape] = _ => ModeResult.Exit(),
            [ConsoleKey.I] = context => ModeResult.SwitchTo(context.InventoryMode, "Inventory mode enabled."),
            [ConsoleKey.E] = context => ModeResult.Continue(context.TryPickUp()),
            [ConsoleKey.W] = context => ModeResult.Continue(context.TryMove(Direction.Up)),
            [ConsoleKey.UpArrow] = context => ModeResult.Continue(context.TryMove(Direction.Up)),
            [ConsoleKey.S] = context => ModeResult.Continue(context.TryMove(Direction.Down)),
            [ConsoleKey.DownArrow] = context => ModeResult.Continue(context.TryMove(Direction.Down)),
            [ConsoleKey.A] = context => ModeResult.Continue(context.TryMove(Direction.Left)),
            [ConsoleKey.LeftArrow] = context => ModeResult.Continue(context.TryMove(Direction.Left)),
            [ConsoleKey.D] = context => ModeResult.Continue(context.TryMove(Direction.Right)),
            [ConsoleKey.RightArrow] = context => ModeResult.Continue(context.TryMove(Direction.Right))
        };
        _instructionBuilder = new InstructionBuilder()
            .StartWith(new BaseInstructionsProcedure(
                "Move: WASD/arrows",
                "Inventory: I",
                "Quit: Q/Esc"))
            .Apply(new PickupInstructionProcedure());
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
}
