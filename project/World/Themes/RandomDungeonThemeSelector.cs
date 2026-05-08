namespace ConsoleRpgStage1.World.Themes;

public sealed class RandomDungeonThemeSelector : IDungeonThemeSelector
{
    private readonly IReadOnlyList<IDungeonTheme> _themes;
    private readonly Random _random;

    public RandomDungeonThemeSelector(Random? random = null)
    {
        _themes =
        [
            new LibraryTheme(),
            new MetalTheme(),
            new TreasuryTheme()
        ];
        _random = random ?? Random.Shared;
    }

    public IDungeonTheme SelectTheme()
    {
        return _themes[_random.Next(_themes.Count)];
    }
}
