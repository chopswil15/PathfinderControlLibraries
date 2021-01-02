namespace StatBlockChecker
{
    public interface IHitDiceHitPointData
    {
        int FalseLife { get; set; }
        int HDModifier { get; set; }
        int MaxFalseLife { get; set; }
        int MaxHPMod { get; set; }
        string MaxHPModFormula { get; set; }
        int TotalHD { get; set; }
    }
}