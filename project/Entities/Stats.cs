namespace ConsoleRpgStage1.Entities;

public sealed class Stats
{
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Health { get; set; }
    public int Luck { get; set; }
    public int Aggression { get; set; }
    public int Wisdom { get; set; }

    public override string ToString()
        => $"STR={Strength} DEX={Dexterity} HP={Health} LUCK={Luck} AGG={Aggression} WIS={Wisdom}";
}