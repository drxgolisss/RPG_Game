using ConsoleRpgStage1.Items;

namespace ConsoleRpgStage1.Entities;

public sealed class RightHand : Hand
{
    public RightHand() : base("Right")
    {
    }

    public override EquipResult TryEquip(Equipment equipment, Weapon weapon)
    {
        return equipment.TryEquipRight(weapon);
    }

    public override EquipResult TryUnequip(Player player, Equipment equipment)
    {
        return equipment.TryUnequipRight(player);
    }
}
