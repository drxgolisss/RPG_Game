namespace ConsoleRpgStage1.Game.Controls;

public sealed class ModeActionBinding
{
    private readonly Func<GameContext, ModeResult> _execute;
    private readonly Func<GameContext, bool> _isVisible;
    private readonly Func<IReadOnlyList<ConsoleKey>, string, string> _helpLineFactory;

    public ModeActionBinding(
        string description,
        IEnumerable<ConsoleKey> keys,
        Func<GameContext, ModeResult> execute,
        Func<GameContext, bool>? isVisible = null,
        Func<IReadOnlyList<ConsoleKey>, string, string>? helpLineFactory = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentNullException.ThrowIfNull(keys);
        ArgumentNullException.ThrowIfNull(execute);

        Description = description;
        Keys = keys.ToArray();

        if (Keys.Count == 0)
        {
            throw new ArgumentException("At least one key must be provided.", nameof(keys));
        }

        _execute = execute;
        _isVisible = isVisible ?? (_ => true);
        _helpLineFactory = helpLineFactory ?? ((bindingKeys, bindingDescription) => $"{bindingDescription}: {FormatKeys(bindingKeys)}");
    }

    public string Description { get; }

    public IReadOnlyList<ConsoleKey> Keys { get; }

    public ModeResult Execute(GameContext context)
    {
        return _execute(context);
    }

    public bool IsVisible(GameContext context)
    {
        return _isVisible(context);
    }

    public string BuildHelpLine()
    {
        return _helpLineFactory(Keys, Description);
    }

    public static string FormatKeys(IEnumerable<ConsoleKey> keys)
    {
        var keyNames = keys
            .Select(FormatKey)
            .Distinct()
            .ToArray();

        return string.Join("/", keyNames);
    }

    private static string FormatKey(ConsoleKey key)
    {
        return key switch
        {
            ConsoleKey.UpArrow => "Up",
            ConsoleKey.DownArrow => "Down",
            ConsoleKey.LeftArrow => "Left",
            ConsoleKey.RightArrow => "Right",
            ConsoleKey.Escape => "Esc",
            ConsoleKey.D1 => "1",
            ConsoleKey.D2 => "2",
            ConsoleKey.D3 => "3",
            ConsoleKey.NumPad1 => "1",
            ConsoleKey.NumPad2 => "2",
            ConsoleKey.NumPad3 => "3",
            _ => key.ToString().Replace("Arrow", string.Empty)
        };
    }
}
