using ConsoleRpgStage1.Core;
using GameWorld = ConsoleRpgStage1.World.World;

namespace ConsoleRpgStage1.Reactive.Sound;

public sealed class SoundPropagationService : ISoundPropagation
{
    private static readonly IReadOnlyList<Direction> Directions =
    [
        Direction.Up,
        Direction.Down,
        Direction.Left,
        Direction.Right
    ];

    public int? GetDistance(GameWorld world, Position source, Position target, int range)
    {
        ArgumentNullException.ThrowIfNull(world);

        if (range < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(range), "Sound range cannot be negative.");
        }

        if (!world.InBounds(source) || !world.InBounds(target))
        {
            return null;
        }

        if (source == target)
        {
            return 0;
        }

        if (!world.CanEnter(source) || !world.CanEnter(target))
        {
            return null;
        }

        var visited = new HashSet<Position> { source };
        var queue = new Queue<(Position Position, int Distance)>();
        queue.Enqueue((source, 0));

        while (queue.Count > 0)
        {
            var (current, distance) = queue.Dequeue();
            if (distance >= range)
            {
                continue;
            }

            foreach (var direction in Directions)
            {
                var next = new Position(
                    current.Row + direction.DeltaRow,
                    current.Col + direction.DeltaCol);

                if (visited.Contains(next) || !world.CanEnter(next))
                {
                    continue;
                }

                var nextDistance = distance + 1;
                if (next == target)
                {
                    return nextDistance;
                }

                visited.Add(next);
                queue.Enqueue((next, nextDistance));
            }
        }

        return null;
    }

    public bool CanReach(GameWorld world, Position source, Position target, int range)
    {
        return GetDistance(world, source, target, range).HasValue;
    }
}
