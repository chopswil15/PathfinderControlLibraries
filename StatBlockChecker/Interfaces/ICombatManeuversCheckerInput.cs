namespace StatBlockChecker
{
    public interface ICombatManeuversCheckerInput
    {
        int DodgeBonus { get; set; }
        string Formula { get; set; }
        int OnGoing { get; set; }
        ISizeData SizeData { get; set; }
        int TotalHD { get; set; }
    }
}