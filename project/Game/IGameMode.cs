namespace ConsoleRpgStage1.Game;

public interface IGameMode
{
    string Name { get; }

    IReadOnlyList<string> GetHelpLines(GameContext context);

    ModeResult HandleKey(ConsoleKeyInfo key, GameContext context);

    void OnEnter(GameContext context)
    {
    }
}
