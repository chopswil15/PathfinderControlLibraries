namespace StatBlockChecker
{
    public interface ISkillCalculation
    {
        string Ability { get; }
        int AbilityMod { get; }
        int ArmorCheckPenalty { get; }
        int ClassSkill { get; }
        int ExtraMod { get; }
        bool ExtraSkillUsed { get; }
        string Formula { get; }
        int Mod { get; }
        string Name { get; }
        int Rank { get; }

        string ToString();
    }
}