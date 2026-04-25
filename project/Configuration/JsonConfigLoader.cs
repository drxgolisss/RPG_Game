using System.Text.Json;

namespace ConsoleRpgStage1.Configuration;

public sealed class JsonConfigLoader : IConfigLoader
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public GameConfig Load(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("Game configuration file was not found.", path);
        }

        var json = File.ReadAllText(path);
        var config = JsonSerializer.Deserialize<GameConfig>(json, Options)
            ?? throw new InvalidOperationException("Game configuration file is empty or invalid.");

        if (string.IsNullOrWhiteSpace(config.PlayerName))
        {
            throw new InvalidOperationException("Game configuration must contain a player name.");
        }

        if (string.IsNullOrWhiteSpace(config.LogDirectoryPath))
        {
            throw new InvalidOperationException("Game configuration must contain a log directory path.");
        }

        config.PlayerName = config.PlayerName.Trim();
        config.LogDirectoryPath = config.LogDirectoryPath.Trim();

        return config;
    }
}
