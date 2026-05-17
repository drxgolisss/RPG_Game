namespace ConsoleRpgStage1.Reactive.Notifications;

public interface INoiseSubject
{
    void Subscribe(INoiseObserver observer);

    void Unsubscribe(INoiseObserver observer);

    void NotifyNoise(NoiseEvent noiseEvent);
}
