using ConsoleRpgStage1.Game.Controls;

namespace ConsoleRpgStage1.Game.Instructions;

public sealed class ActionBindingsInstructionProcedure : IInstructionStarterProcedure
{
    private readonly Func<IReadOnlyList<ModeActionBinding>> _bindingsProvider;

    public ActionBindingsInstructionProcedure(Func<IReadOnlyList<ModeActionBinding>> bindingsProvider)
    {
        ArgumentNullException.ThrowIfNull(bindingsProvider);
        _bindingsProvider = bindingsProvider;
    }

    public void Apply(GameContext context, List<string> instructionLines)
    {
        foreach (var binding in _bindingsProvider())
        {
            if (!binding.IsVisible(context))
            {
                continue;
            }

            instructionLines.Add(binding.BuildHelpLine());
        }
    }
}
