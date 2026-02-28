using ConsoleRpgStage1.Items;

namespace ConsoleRpgStage1.Entities;

public abstract class Hand
{
    protected Hand(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public abstract EquipResult TryEquip(Equipment equipment, Weapon weapon);

    public abstract EquipResult TryUnequip(Player player, Equipment equipment);
}
