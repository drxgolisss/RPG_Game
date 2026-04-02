namespace ConsoleRpgStage1.Game.Instructions;

public sealed class InventoryUnequipInstructionProcedure : IInstructionBuildProcedure
{
    public void Apply(GameContext context, List<string> instructionLines)
    {
        if (context.Player.Equipment.LeftItem != null || context.Player.Equipment.RightItem != null)
        {
            instructionLines.Add("Start unequip: U");
        }
    }
}
