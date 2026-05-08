using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Items;
using ConsoleRpgStage1.World.Building;

namespace ConsoleRpgStage1.World.Themes;

public sealed class MetalTheme : IDungeonTheme
{
    public string Name => "Metalworks";

    public IDungeonBuildStrategy GenerationStrategy => new DungeonGroundsStrategy(
        centralRoomWidth: 12,
        centralRoomHeight: 5,
        chambersCount: 4,
        pathsCount: 10,
        enemiesCount: 5,
        itemsCount: 5,
        weaponsCount: 4,
        itemFactories: ItemPool,
        weaponFactories: WeaponPool,
        enemyFactories: EnemyTemplates,
        mandatoryArtifactFactory: MandatoryArtifactFactory);

    public IReadOnlyList<Func<Item>> ItemPool =>
    [
        static () => new RustyGearItem(),
        static () => new ThemedJunkItem("Metal Fragment", 'm'),
        static () => new ThemedJunkItem("Broken Tool", 't')
    ];

    public IReadOnlyList<Func<Weapon>> WeaponPool =>
    [
        static () => new BlasterItem(),
        static () => new BattleAxeItem(),
        static () => new GreatswordItem()
    ];

    public Func<Item> MandatoryArtifactFactory => static () => new ArtifactItem("Heart of the Forge", '*');

    public IReadOnlyList<Func<Enemy>> EnemyTemplates =>
    [
        static () => new Enemy(health: 12, attack: 3, armor: 2, name: "Cleaning Robot", symbol: 'R'),
        static () => new Enemy(health: 10, attack: 5, armor: 1, name: "Rust Guard", symbol: 'G'),
        static () => new Enemy(health: 14, attack: 4, armor: 2, name: "Forge Sentinel", symbol: 'F')
    ];

    public string IntroductoryMessage => "You step into old metalworks filled with heat, smoke, and echoing machinery.";
}
