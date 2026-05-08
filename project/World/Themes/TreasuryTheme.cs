using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Items;
using ConsoleRpgStage1.World.Building;

namespace ConsoleRpgStage1.World.Themes;

public sealed class TreasuryTheme : IDungeonTheme
{
    public string Name => "Buried Treasury";

    public IDungeonBuildStrategy GenerationStrategy => new DungeonGroundsStrategy(
        centralRoomWidth: 9,
        centralRoomHeight: 6,
        chambersCount: 7,
        pathsCount: 7,
        enemiesCount: 4,
        itemsCount: 8,
        weaponsCount: 2,
        itemFactories: ItemPool,
        weaponFactories: WeaponPool,
        enemyFactories: EnemyTemplates,
        mandatoryArtifactFactory: MandatoryArtifactFactory);

    public IReadOnlyList<Func<Item>> ItemPool =>
    [
        static () => new ItemCoin(),
        static () => new ItemGold(),
        static () => new ThemedJunkItem("Lucky Coin Pouch", 'p')
    ];

    public IReadOnlyList<Func<Weapon>> WeaponPool =>
    [
        static () => new ShortSwordItem(),
        static () => new GreatswordItem()
    ];

    public Func<Item> MandatoryArtifactFactory => static () => new ArtifactItem("Crown of the Deep Vault", '*');

    public IReadOnlyList<Func<Enemy>> EnemyTemplates =>
    [
        static () => new Enemy(health: 9, attack: 5, armor: 1, name: "Angry Briefcase", symbol: 'B'),
        static () => new Enemy(health: 13, attack: 4, armor: 2, name: "Living Safe", symbol: 'S'),
        static () => new Enemy(health: 8, attack: 4, armor: 0, name: "Greedy Spirit", symbol: 'G')
    ];

    public string IntroductoryMessage => "You descend into a buried treasury where every glitter may be bait.";
}
