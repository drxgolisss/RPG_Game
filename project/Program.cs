using System.Text;
using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Game;
using ConsoleRpgStage1.UI;
using ConsoleRpgStage1.World;

var worldFactory = new WorldFactory();
var world = worldFactory.CreateDungeonGrounds();
var player = new Player(new Position(world.Rows / 2, world.Cols / 2));
var renderer = new Renderer();

var gameMode = new GameMode();
var inventoryMode = new InventoryMode();
var context = new GameContext(
    world,
    player,
    renderer,
    gameMode,
    inventoryMode,
    "Use WASD/arrows to move, E to pick up, I for inventory, Q/Esc to quit.");
var screenComposer = new GameScreenComposer();

Console.OutputEncoding = Encoding.UTF8;
Console.CursorVisible = false;

try
{
    while (true)
    {
        context.ClampSelection();

        var frame = screenComposer.Build(context);
        Console.SetCursorPosition(0, 0);
        Console.Write(frame);
        Console.SetCursorPosition(0, screenComposer.GetSafeCursorRow(context));

        var key = Console.ReadKey(true);
        var result = context.CurrentMode.HandleKey(key, context);

        if (context.ApplyResult(result))
        {
            if (result.Message != null)
            {
                var finalFrame = screenComposer.Build(context);
                Console.SetCursorPosition(0, 0);
                Console.Write(finalFrame);
                Console.SetCursorPosition(0, screenComposer.GetSafeCursorRow(context));
            }

            break;
        }
    }
}
finally
{
    Console.CursorVisible = true;
}
