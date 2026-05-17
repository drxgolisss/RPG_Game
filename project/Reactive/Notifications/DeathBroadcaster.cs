namespace ConsoleRpgStage1.Reactive.Notifications;

public sealed class DeathBroadcaster : IDeathSubject
{
    private readonly List<IDeathObserver> _observers = new();

    public void Subscribe(IDeathObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);

        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void Unsubscribe(IDeathObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);
        _observers.Remove(observer);
    }

    public void NotifyDeath(EnemyDeathEvent deathEvent)
    {
        ArgumentNullException.ThrowIfNull(deathEvent);

        foreach (var observer in _observers.ToArray())
        {
            observer.OnDeath(deathEvent);
        }
    }
}
