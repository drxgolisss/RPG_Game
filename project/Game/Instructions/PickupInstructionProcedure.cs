namespace ConsoleRpgStage1.Game.Instructions;

public sealed class PickupInstructionProcedure : IInstructionBuildProcedure
{
    public void Apply(GameContext context, List<string> instructionLines)
    {
        if (context.World.GetItems(context.Player.Position).Count > 0)
        {
            instructionLines.Add("Pick up: E");
        }
    }
}
