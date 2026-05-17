using ConsoleRpgStage1.Core;
using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Reactive.Species;

namespace ConsoleRpgStage1.Reactive.Notifications;

public sealed class EnemyDeathEvent
{
    public EnemyDeathEvent(Enemy deadEnemy, EnemySpeciesGroup speciesGroup, Position position)
    {
        DeadEnemy = deadEnemy ?? throw new ArgumentNullException(nameof(deadEnemy));
        SpeciesGroup = speciesGroup ?? throw new ArgumentNullException(nameof(speciesGroup));
        Position = position;
    }

    public Enemy DeadEnemy { get; }

    public EnemySpeciesGroup SpeciesGroup { get; }

    public Position Position { get; }
}
