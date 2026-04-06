namespace ConsoleRpgStage1.World.Building;

public sealed class DungeonGroundsStrategy : IDungeonBuildStrategy
{
    private readonly int _centralRoomHeight;
    private readonly int _centralRoomWidth;
    private readonly int _chambersCount;
    private readonly int _enemiesCount;
    private readonly int _itemsCount;
    private readonly int _pathsCount;
    private readonly int _weaponsCount;

    public DungeonGroundsStrategy(
        int centralRoomWidth = 10,
        int centralRoomHeight = 6,
        int chambersCount = 5,
        int pathsCount = 8,
        int enemiesCount = 4,
        int itemsCount = 6,
        int weaponsCount = 3)
    {
        if (centralRoomWidth <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(centralRoomWidth), "Central room width must be greater than zero.");
        }

        if (centralRoomHeight <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(centralRoomHeight), "Central room height must be greater than zero.");
        }

        if (chambersCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(chambersCount), "Chambers count cannot be negative.");
        }

        if (pathsCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pathsCount), "Paths count cannot be negative.");
        }

        if (enemiesCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(enemiesCount), "Enemies count cannot be negative.");
        }

        if (itemsCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(itemsCount), "Items count cannot be negative.");
        }

        if (weaponsCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(weaponsCount), "Weapons count cannot be negative.");
        }

        _centralRoomWidth = centralRoomWidth;
        _centralRoomHeight = centralRoomHeight;
        _chambersCount = chambersCount;
        _pathsCount = pathsCount;
        _enemiesCount = enemiesCount;
        _itemsCount = itemsCount;
        _weaponsCount = weaponsCount;
    }

    public DungeonBuilder Configure(DungeonBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder
            .StartWith(new FilledDungeonProcedure())
            .Apply(new AddCentralRoomProcedure(_centralRoomWidth, _centralRoomHeight))
            .Apply(new AddChambersProcedure(_chambersCount))
            .Apply(new AddPathsProcedure(_pathsCount))
            .Apply(new AddEnemiesProcedure(_enemiesCount))
            .Apply(new AddItemsProcedure(_itemsCount))
            .Apply(new AddWeaponsProcedure(_weaponsCount));
    }
}
