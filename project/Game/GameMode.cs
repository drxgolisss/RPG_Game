using ConsoleRpgStage1.Core;

namespace ConsoleRpgStage1.Game;

public sealed class GameMode : IGameMode
{
    private static readonly IReadOnlyList<string> BaseHelpLines = new[]
    {
        "Move: WASD/arrows",
        "Inventory: I",
        "Quit: Q/Esc"
    };

    private readonly Dictionary<ConsoleKey, Func<GameContext, ModeResult>> _keyMap;

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
        var helpLines = new List<string>(BaseHelpLines);

        if (context.World.GetItems(context.Player.Position).Count > 0)
        {
            helpLines.Add("Pick up: E");
        }

        return helpLines;
    }
}
