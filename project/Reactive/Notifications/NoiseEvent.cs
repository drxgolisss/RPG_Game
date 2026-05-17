using ConsoleRpgStage1.Core;
using GameWorld = ConsoleRpgStage1.World.World;

namespace ConsoleRpgStage1.Reactive.Notifications;

public sealed class NoiseEvent
{
    public NoiseEvent(GameWorld world, Position source, int range, string description)
    {
        World = world ?? throw new ArgumentNullException(nameof(world));

        if (range < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(range), "Noise range cannot be negative.");
        }

        Source = source;
        Range = range;
        Description = string.IsNullOrWhiteSpace(description) ? "Noise" : description.Trim();
    }

    public GameWorld World { get; }

    public Position Source { get; }

    public int Range { get; }

    public string Description { get; }
}
