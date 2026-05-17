namespace ConsoleRpgStage1.Reactive.Notifications;

public interface INoiseObserver
{
    void OnNoise(NoiseEvent noiseEvent);
}
