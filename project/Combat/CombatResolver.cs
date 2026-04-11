using ConsoleRpgStage1.Entities;

namespace ConsoleRpgStage1.Combat;

public sealed class CombatResolver
{
    public CombatResult ResolveTurn(Player player, Enemy enemy, IAttackStyle attackStyle)
    {
        ArgumentNullException.ThrowIfNull(player);
        ArgumentNullException.ThrowIfNull(enemy);
        ArgumentNullException.ThrowIfNull(attackStyle);

        var heldItem = player.Equipment.LeftItem ?? player.Equipment.RightItem;
        var attackDamage = heldItem?.GetAttackDamage(player, attackStyle) ?? attackStyle.CalculateAttackDamageWithoutWeapon(player);
        var damageToEnemy = Math.Max(0, attackDamage - enemy.Armor);
        enemy.ApplyDamage(damageToEnemy);
        var enemyDefeated = enemy.IsDead;

        var playerDefense = heldItem?.GetDefenseStrength(player, attackStyle) ?? attackStyle.CalculateDefenseStrengthWithoutWeapon(player);
        var damageToPlayer = enemyDefeated ? 0 : Math.Max(0, enemy.Attack - playerDefense);
        player.ApplyDamage(damageToPlayer);
        var playerDefeated = player.IsDead;

        return new CombatResult(
            damageToEnemy,
            damageToPlayer,
            enemyDefeated,
            playerDefeated,
            $"Player used {attackStyle.Name} attack.");
    }
}
