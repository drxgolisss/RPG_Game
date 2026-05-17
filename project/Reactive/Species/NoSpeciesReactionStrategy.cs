using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Reactive.Notifications;

namespace ConsoleRpgStage1.Reactive.Species;

public sealed class NoSpeciesReactionStrategy : ISpeciesReactionStrategy
{
    public void React(Enemy survivor, EnemyDeathEvent deathEvent)
    {
        ArgumentNullException.ThrowIfNull(survivor);
        ArgumentNullException.ThrowIfNull(deathEvent);
    }
}
