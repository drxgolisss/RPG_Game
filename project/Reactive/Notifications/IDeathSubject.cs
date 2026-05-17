namespace ConsoleRpgStage1.Reactive.Notifications;

public interface IDeathSubject
{
    void Subscribe(IDeathObserver observer);

    void Unsubscribe(IDeathObserver observer);

    void NotifyDeath(EnemyDeathEvent deathEvent);
}
