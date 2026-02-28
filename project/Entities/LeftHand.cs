using ConsoleRpgStage1.Items;

namespace ConsoleRpgStage1.Entities;

public sealed class LeftHand : Hand
{
    public LeftHand() : base("Left")
    {
    }

    public override EquipResult TryEquip(Equipment equipment, Weapon weapon)
    {
        return equipment.TryEquipLeft(weapon);
    }

    public override EquipResult TryUnequip(Player player, Equipment equipment)
    {
        return equipment.TryUnequipLeft(player);
    }
}
