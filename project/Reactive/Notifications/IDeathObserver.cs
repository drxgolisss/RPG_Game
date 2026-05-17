namespace ConsoleRpgStage1.Reactive.Notifications;

public interface IDeathObserver
{
    void OnDeath(EnemyDeathEvent deathEvent);
}
