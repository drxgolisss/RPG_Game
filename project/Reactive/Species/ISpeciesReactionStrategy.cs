using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Reactive.Notifications;

namespace ConsoleRpgStage1.Reactive.Species;

public interface ISpeciesReactionStrategy
{
    void React(Enemy survivor, EnemyDeathEvent deathEvent);
}
