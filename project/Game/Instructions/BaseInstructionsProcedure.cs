namespace ConsoleRpgStage1.Game.Instructions;

public sealed class BaseInstructionsProcedure : IInstructionStarterProcedure
{
    private readonly IReadOnlyList<string> _instructionLines;

    public BaseInstructionsProcedure(params string[] instructionLines)
    {
        _instructionLines = instructionLines ?? Array.Empty<string>();
    }

    public void Apply(GameContext context, List<string> instructionLines)
    {
        instructionLines.AddRange(_instructionLines);
    }
}
