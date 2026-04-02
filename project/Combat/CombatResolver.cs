using ConsoleRpgStage1.Entities;
using ConsoleRpgStage1.Items;

namespace ConsoleRpgStage1.Combat;

public sealed class CombatResolver
{
    public CombatResult ResolveTurn(Player player, Enemy enemy, IAttackStyle attackStyle)
    {
        ArgumentNullException.ThrowIfNull(player);
        ArgumentNullException.ThrowIfNull(enemy);
        ArgumentNullException.ThrowIfNull(attackStyle);

        var weapon = player.Equipment.LeftItem ?? player.Equipment.RightItem;
        var attackDamage = weapon?.GetAttackDamage(player, attackStyle) ?? 0;
        var damageToEnemy = Math.Max(0, attackDamage - enemy.Armor);
        var projectedEnemyHealth = Math.Max(0, enemy.Health - damageToEnemy);
        var enemyDefeated = projectedEnemyHealth == 0;

        var playerDefense = CalculatePlayerDefense(player, weapon, attackStyle);
        var damageToPlayer = enemyDefeated ? 0 : Math.Max(0, enemy.Attack - playerDefense);
        var projectedPlayerHealth = Math.Max(0, player.Stats.Health - damageToPlayer);
        var playerDefeated = projectedPlayerHealth == 0;

        return new CombatResult(
            damageToEnemy,
            damageToPlayer,
            enemyDefeated,
            playerDefeated,
            $"Player used {attackStyle.Name} attack.");
    }

    private static int CalculatePlayerDefense(Player player, Weapon? weapon, IAttackStyle attackStyle)
    {
        if (weapon == null)
        {
            return Math.Max(0, player.Stats.Dexterity + player.GetLuckModifier());
        }

        return Math.Max(0, weapon.GetDefenseStrength(player, attackStyle) + player.GetLuckModifier());
    }
}
