using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassFoundational;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace ClassDetails
{
    public class ArcaneArcher : ClassFoundation 
    {
        public ArcaneArcher()
        {
            Name = "Arcane Archer";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            BABType = StatBlockInfo.BABType.Fast;
            IsPrestigeClass = true;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Martial;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Shield;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Tower;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Medium;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Heavy;
            PrestigeBABMin = 6;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.STEALTH, StatBlockInfo.SkillNames.SURVIVAL };
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Point-Blank Shot", "Precise Shot", "Weapon Focus (longbow)|Weapon Focus (shortbow)|Weapon Focus (composite longbow)|Weapon Focus (composite shortbow)" };
        }

        public override void GetSpellOverLoads(ref int overloadLevel, ref int overloadClassLevel, int casterLevel, int classLevel)
        {
            overloadClassLevel += classLevel;
            overloadLevel += base.GetOverloadPrestige(classLevel, OverloadPrestigeType.BlocksOfThree);
        }
    }
}
