using ConsoleRpgStage1.Entities;
using GameWorld = ConsoleRpgStage1.World.World;

namespace ConsoleRpgStage1.Reactive.Movement;

public sealed class StationaryMovementStrategy : IEnemyMovementStrategy
{
    public void Move(Enemy enemy, GameWorld world)
    {
        ArgumentNullException.ThrowIfNull(enemy);
        ArgumentNullException.ThrowIfNull(world);
    }
}
