using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Items;
using ConsoleRpgStage1.World.Building;

namespace ConsoleRpgStage1.World.Themes;

public interface IDungeonTheme
{
    string Name { get; }

    IDungeonBuildStrategy GenerationStrategy { get; }

    IReadOnlyList<Func<Item>> ItemPool { get; }

    IReadOnlyList<Func<Weapon>> WeaponPool { get; }

    Func<Item> MandatoryArtifactFactory { get; }

    IReadOnlyList<Func<Enemy>> EnemyTemplates { get; }

    string IntroductoryMessage { get; }
}
