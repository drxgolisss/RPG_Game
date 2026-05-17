using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Items;
using ConsoleRpgStage1.Reactive.Notifications;
using ConsoleRpgStage1.Reactive.Species;
using ConsoleRpgStage1.World.Building;

namespace ConsoleRpgStage1.World.Themes;

public interface IDungeonTheme
{
    string Name { get; }

    IDungeonBuildStrategy CreateGenerationStrategy(INoiseSubject noiseSubject);

    IReadOnlyList<Func<Item>> ItemPool { get; }

    IReadOnlyList<Func<Weapon>> WeaponPool { get; }

    Func<Item> MandatoryArtifactFactory { get; }

    IReadOnlyList<Func<Enemy>> EnemyTemplates { get; }

    IReadOnlyList<EnemySpeciesDefinition> EnemySpeciesDefinitions { get; }

    string IntroductoryMessage { get; }
}
