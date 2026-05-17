using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.Entities;
using GameWorld = ConsoleRpgStage1.World.World;

namespace ConsoleRpgStage1.Reactive.Movement;

public sealed class RandomWalkMovementStrategy : IEnemyMovementStrategy
{
    private static readonly IReadOnlyList<Direction> Directions =
    [
        Direction.Up,
        Direction.Down,
        Direction.Left,
        Direction.Right
    ];

    private readonly Random _random;

    public RandomWalkMovementStrategy(Random? random = null)
    {
        _random = random ?? Random.Shared;
    }

    public void Move(Enemy enemy, GameWorld world)
    {
        ArgumentNullException.ThrowIfNull(enemy);
        ArgumentNullException.ThrowIfNull(world);

        var candidates = new List<Position>();

        foreach (var direction in Directions)
        {
            var target = new Position(
                enemy.Position.Row + direction.DeltaRow,
                enemy.Position.Col + direction.DeltaCol);

            if (world.CanEnter(target) && world.GetEnemies(target).Count == 0)
            {
                candidates.Add(target);
            }
        }

        if (candidates.Count == 0)
        {
            return;
        }

        var selectedTarget = candidates[_random.Next(candidates.Count)];
        world.MoveEnemy(enemy, selectedTarget);
    }
}
