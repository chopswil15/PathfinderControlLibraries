using CommonStatBlockInfo;

namespace StatBlockChecker
{
    public interface IArmorClassData
    {
        StatBlockInfo.ACMods ACMods_Computed { get; set; }
        StatBlockInfo.ACMods ACMods_SB { get; set; }
        int MaxDexMod { get; set; }
        int TotalArmorCheckPenalty { get; set; }
    }
}