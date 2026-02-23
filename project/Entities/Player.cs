using ConsoleRpgStage1.Core;

namespace ConsoleRpgStage1.Entities;

public sealed class Player
{
    public Player(Position startPosition)
    {
        Position = startPosition;
    }

    public Position Position { get; private set; }

    public bool TryMove(Direction direction, World.World world)
    {
        var newPosition = new Position(
            Position.Row + direction.DeltaRow,
            Position.Col + direction.DeltaCol);

        if (!world.CanEnter(newPosition))
        {
            return false;
        }

        Position = newPosition;
        return true;
    }
}
