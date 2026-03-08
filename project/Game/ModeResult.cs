namespace ConsoleRpgStage1.Game;

public sealed class ModeResult
{
    private ModeResult(bool shouldExit, IGameMode? nextMode, string? message)
    {
        ShouldExit = shouldExit;
        NextMode = nextMode;
        Message = message;
    }

    public bool ShouldExit { get; }

    public IGameMode? NextMode { get; }

    public string? Message { get; }

    public static ModeResult Continue(string? message = null)
    {
        return new ModeResult(false, null, message);
    }

    public static ModeResult SwitchTo(IGameMode nextMode, string? message = null)
    {
        return new ModeResult(false, nextMode, message);
    }

    public static ModeResult Exit(string? message = null)
    {
        return new ModeResult(true, null, message);
    }
}
