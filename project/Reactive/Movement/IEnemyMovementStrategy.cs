using ConsoleRpgStage1.Entities;
using GameWorld = ConsoleRpgStage1.World.World;

namespace ConsoleRpgStage1.Reactive.Movement;

public interface IEnemyMovementStrategy
{
    void Move(Enemy enemy, GameWorld world);
}
