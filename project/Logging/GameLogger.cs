namespace ConsoleRpgStage1.Logging;

public sealed class GameLogger
{
    private readonly List<string> _entries = new();
    private readonly object _syncRoot = new();
    private string? _logFilePath;
    private bool _isInitialized;

    private GameLogger()
    {
    }

    public static GameLogger Instance { get; } = new();

    public string LogFilePath => _logFilePath ?? string.Empty;

    public bool IsInitialized => _isInitialized;

    public void Initialize(string playerName, string logDirectoryPath, DateTime gameStartTime)
    {
        lock (_syncRoot)
        {
            if (_isInitialized)
            {
                throw new InvalidOperationException("Game logger has already been initialized.");
            }

            Directory.CreateDirectory(logDirectoryPath);
            _logFilePath = CreateUniqueLogFilePath(playerName, logDirectoryPath, gameStartTime);

            using var stream = new FileStream(_logFilePath, FileMode.CreateNew, FileAccess.Write);
            using var writer = new StreamWriter(stream);
            writer.WriteLine($"Game log for {playerName}");
            writer.WriteLine($"Started: {gameStartTime:yyyy-MM-dd HH:mm:ss}");
            writer.WriteLine();

            _isInitialized = true;
        }
    }

    public void AddEntry(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }

        lock (_syncRoot)
        {
            if (!_isInitialized || _logFilePath == null)
            {
                throw new InvalidOperationException("Game logger must be initialized before adding entries.");
            }

            var entry = $"[{DateTime.Now:HH:mm:ss}] {message}";
            _entries.Add(entry);
            File.AppendAllText(_logFilePath, entry + Environment.NewLine);
        }
    }

    public IReadOnlyList<string> GetRecentEntries(int count)
    {
        lock (_syncRoot)
        {
            var entriesToTake = Math.Clamp(count, 0, _entries.Count);
            return _entries.Skip(_entries.Count - entriesToTake).ToArray();
        }
    }

    public IReadOnlyList<string> GetAllEntries()
    {
        lock (_syncRoot)
        {
            return _entries.ToArray();
        }
    }

    private static string CreateUniqueLogFilePath(string playerName, string logDirectoryPath, DateTime gameStartTime)
    {
        var safePlayerName = SanitizeFileNamePart(playerName);
        var baseFileName = $"{safePlayerName}_{gameStartTime:yyyyMMdd_HHmmss}";
        var logFilePath = Path.Combine(logDirectoryPath, baseFileName + ".log");
        var suffix = 1;

        while (File.Exists(logFilePath))
        {
            logFilePath = Path.Combine(logDirectoryPath, $"{baseFileName}_{suffix}.log");
            suffix++;
        }

        return logFilePath;
    }

    private static string SanitizeFileNamePart(string value)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        var sanitized = new string(value.Select(ch => invalidChars.Contains(ch) ? '_' : ch).ToArray()).Trim();
        return string.IsNullOrWhiteSpace(sanitized) ? "Player" : sanitized;
    }
}
