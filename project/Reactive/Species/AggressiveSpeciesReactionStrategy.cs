using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Logging;
using ConsoleRpgStage1.Reactive.Notifications;

namespace ConsoleRpgStage1.Reactive.Species;

public sealed class AggressiveSpeciesReactionStrategy : ISpeciesReactionStrategy
{
    public void React(Enemy survivor, EnemyDeathEvent deathEvent)
    {
        ArgumentNullException.ThrowIfNull(survivor);
        ArgumentNullException.ThrowIfNull(deathEvent);

        survivor.ApplyStatChange(attackDelta: 1, armorDelta: 1);
        GameLogger.Instance.AddEntry(
            $"{survivor.Name} became enraged after {deathEvent.DeadEnemy.Name} died. Attack and armor increased.");
    }
}
