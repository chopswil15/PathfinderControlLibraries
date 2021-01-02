using StatBlockCommon;
using StatBlockParsing;

namespace StatBlockChecker
{
    public interface IMeleeWeaponCheckerInput
    {
        int ACcDefendingMod { get; set; }
        string BaseAtk { get; set; }
        bool DontUseRacialHD { get; set; }
        bool HasRaceBase { get; set; }
        RaceBase.RaceType RaceBaseType { get; set; }
        string RaceName { get; set; }
        string RaceWeapons { get; set; }
        int RacialHDValue { get; set; }
        string Size { get; set; }
        int SizeMod { get; set; }
    }
}