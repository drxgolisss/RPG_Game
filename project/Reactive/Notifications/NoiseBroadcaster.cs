namespace ConsoleRpgStage1.Reactive.Notifications;

public sealed class NoiseBroadcaster : INoiseSubject
{
    private readonly List<INoiseObserver> _observers = new();

    public void Subscribe(INoiseObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);

        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void Unsubscribe(INoiseObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);
        _observers.Remove(observer);
    }

    public void NotifyNoise(NoiseEvent noiseEvent)
    {
        ArgumentNullException.ThrowIfNull(noiseEvent);

        foreach (var observer in _observers.ToArray())
        {
            observer.OnNoise(noiseEvent);
        }
    }
}
