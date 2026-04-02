namespace ConsoleRpgStage1.Game.Instructions;

public sealed class InstructionBuilder
{
    private readonly List<IInstructionBuildProcedure> _procedures = new();

    public InstructionBuilder StartWith(IInstructionStarterProcedure starterProcedure)
    {
        ArgumentNullException.ThrowIfNull(starterProcedure);

        if (_procedures.Count > 0)
        {
            throw new InvalidOperationException("Starter procedure must be the first instruction procedure in the builder.");
        }

        _procedures.Add(starterProcedure);
        return this;
    }

    public InstructionBuilder Apply(IInstructionBuildProcedure procedure)
    {
        ArgumentNullException.ThrowIfNull(procedure);

        if (_procedures.Count == 0)
        {
            throw new InvalidOperationException("A starter procedure must be added before other instruction procedures.");
        }

        _procedures.Add(procedure);
        return this;
    }

    public IReadOnlyList<string> Build(GameContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (_procedures.Count == 0)
        {
            throw new InvalidOperationException("Instruction builder requires a starter procedure.");
        }

        var instructionLines = new List<string>();

        foreach (var procedure in _procedures)
        {
            procedure.Apply(context, instructionLines);
        }

        return instructionLines;
    }
}
