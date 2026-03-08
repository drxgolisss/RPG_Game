namespace ConsoleRpgStage1.Game;

public interface IGameMode
{
    string Name { get; }

    IReadOnlyList<string> HelpLines { get; }

    ModeResult HandleKey(ConsoleKeyInfo key, GameContext context);

    void OnEnter(GameContext context)
    {
    }
}
