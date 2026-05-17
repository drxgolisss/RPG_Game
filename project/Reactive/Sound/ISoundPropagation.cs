using ConsoleRpgStage1.Core;
using GameWorld = ConsoleRpgStage1.World.World;

namespace ConsoleRpgStage1.Reactive.Sound;

public interface ISoundPropagation
{
    int? GetDistance(GameWorld world, Position source, Position target, int range);

    bool CanReach(GameWorld world, Position source, Position target, int range);
}
