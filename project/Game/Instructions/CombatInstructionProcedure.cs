namespace ConsoleRpgStage1.Game.Instructions;

public sealed class CombatInstructionProcedure : IInstructionBuildProcedure
{
    public void Apply(GameContext context, List<string> instructionLines)
    {
        if (context.HasEnemyNearby())
        {
            instructionLines.Add("Attack nearby enemy: 1 normal, 2 stealth, 3 magical");
        }
    }
}
