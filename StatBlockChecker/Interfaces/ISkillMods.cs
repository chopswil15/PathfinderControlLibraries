using CommonStatBlockInfo;
using MagicItemAbilityWrapper;
using Skills;
using System.Collections.Generic;

namespace StatBlockChecker
{
    public interface ISkillMods
    {
        void ComputeBloodlineSkillMods(SkillsInfo.SkillInfo Info, ref int ClassSkill, ref string formula);
        int ComputeClimbMod(ref string formula);
        int ComputeDomainSkillMods(string SkillName, SkillsInfo.SkillInfo Info, int ClassSkill, ref string formula);
        int ComputeFlyManeuverabilityMod(ref string formula);
        int ComputeFlySizeMod(ref string formula);
        int ComputeInquisitionSkillMods(string SkillName, SkillsInfo.SkillInfo Info, int ClassSkill, ref string formula);
        void ComputeOracleSkillMods(SkillsInfo.SkillInfo Info, ref int ClassSkill);
        int ComputeRaceSkillMod(string SkillName, ref string formula, ref int KnowledgeOne);
        int ComputeSkillMod(string SkillName, StatBlockInfo.AbilityName Ability, string SubValue, ref string formula, SkillsInfo.SkillInfo Info);
        int ComputeStealthSizeMod(ref string formula);
        int ComputeSwimMod(ref string formula);
        int FindExtraSkillsMods(string SkillName, SkillsInfo.SkillInfo Info, ref string formula, int StrMod);
        void GetFamilarMod(string SkillName, SkillsInfo.SkillInfo Info, ref int tempMod, ref string formula);
        int GetRacialModValue(string RacialModName, string SubValue);
        string GetSkillAbilityMod(ref int AbilityMod, Skill skill, string monsterSize);
        bool HasRacialMod(string RacialModName);
        bool HasSkillFocus(string SkillName);
        int MagicItemMods(string SkillName, StatBlockInfo.AbilityName abilityName, List<MagicItemAbilitiesWrapper> MagicItemAbilities, out string formula);
        bool Over10RanksBonus(SkillsInfo.SkillInfo Info, ref int Ranks, ref int ComputedRank, ref int ExtraMod, ref string formula, Skill skill);
    }
}