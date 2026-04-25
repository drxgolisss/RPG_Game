namespace ConsoleRpgStage1.Configuration;

public interface IConfigLoader
{
    GameConfig Load(string path);
}
