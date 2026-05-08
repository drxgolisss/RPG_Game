using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Items;
using ConsoleRpgStage1.World.Building;

namespace ConsoleRpgStage1.World.Themes;

public sealed class LibraryTheme : IDungeonTheme
{
    public string Name => "Ancient Library";

    public IDungeonBuildStrategy GenerationStrategy => new DungeonGroundsStrategy(
        centralRoomWidth: 10,
        centralRoomHeight: 7,
        chambersCount: 6,
        pathsCount: 9,
        enemiesCount: 5,
        itemsCount: 7,
        weaponsCount: 3,
        itemFactories: ItemPool,
        weaponFactories: WeaponPool,
        enemyFactories: EnemyTemplates,
        mandatoryArtifactFactory: MandatoryArtifactFactory);

    public IReadOnlyList<Func<Item>> ItemPool =>
    [
        static () => new ThemedJunkItem("Ancient Book", 'b'),
        static () => new ThemedJunkItem("Wisdom Scroll", 's'),
        static () => new BrokenBottleItem()
    ];

    public IReadOnlyList<Func<Weapon>> WeaponPool =>
    [
        static () => new WizardStaffItem(),
        static () => new BlackWandItem()
    ];

    public Func<Item> MandatoryArtifactFactory => static () => new ArtifactItem("Index of Lost Names", '*');

    public IReadOnlyList<Func<Enemy>> EnemyTemplates =>
    [
        static () => new Enemy(health: 8, attack: 4, armor: 0, name: "Ink Shade", symbol: 'S'),
        static () => new Enemy(health: 11, attack: 3, armor: 1, name: "Book Warden", symbol: 'W'),
        static () => new Enemy(health: 7, attack: 5, armor: 0, name: "Lost Mage", symbol: 'M')
    ];

    public string IntroductoryMessage => "You enter an ancient library where the shelves seem to watch you.";
}
