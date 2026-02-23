using System.Text;
using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.UI;
using ConsoleRpgStage1.World;

var worldFactory = new WorldFactory();
var world = worldFactory.CreateDefault();
var player = new Player(new Position(0, 0));
var renderer = new Renderer();

Console.OutputEncoding = Encoding.UTF8;
Console.CursorVisible = false;

try
{
    Render(world, player, renderer);

    while (true)
    {
        var key = Console.ReadKey(true);

        if (key.Key == ConsoleKey.Q || key.Key == ConsoleKey.Escape)
        {
            break;
        }

        if (TryGetDirection(key.Key, out var direction))
        {
            player.TryMove(direction, world);
            Render(world, player, renderer);
        }
    }
}
finally
{
    Console.CursorVisible = true;
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

static void Render(World world, Player player, Renderer renderer)
{
    var frame = renderer.BuildFrame(world, player);
    Console.SetCursorPosition(0, 0);
    Console.Write(frame);
}
