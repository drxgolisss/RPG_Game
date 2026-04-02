namespace ConsoleRpgStage1.Game.Instructions;

public sealed class InventoryContentInstructionProcedure : IInstructionBuildProcedure
{
    public void Apply(GameContext context, List<string> instructionLines)
    {
        if (context.Player.Inventory.Count == 0)
        {
            return;
        }

        instructionLines.Add("Drop: D");
        instructionLines.Add("Equip left/right: L/R");
    }
}
