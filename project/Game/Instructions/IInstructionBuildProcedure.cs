namespace ConsoleRpgStage1.Game.Instructions;

public interface IInstructionBuildProcedure
{
    void Apply(GameContext context, List<string> instructionLines);
}
