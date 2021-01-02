using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PathfinderGlobals;
using CommonStatBlockInfo;
using ClassFoundational;

namespace ClassDetails
{
    public class EldritchKnight : ClassFoundation 
    {
        public EldritchKnight()
        {
            Name = "Eldritch Knight";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 2;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            BABType = StatBlockInfo.BABType.Fast;
            IsPrestigeClass = true;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.None;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.None;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.None;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA, StatBlockInfo.SkillNames.KNOWLEDGE_NOBILITY, StatBlockInfo.SkillNames.LINGUISTICS, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SENSE_MOTIVE, 
                StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.SWIM };
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            int temp = 0;
            if (ClassLevel >= 1) temp++;
            if (ClassLevel >= 5) temp++;
            if (ClassLevel >= 9) temp++;

            return temp;
        }

        public override void GetSpellOverLoads(ref int overloadLevel, ref int overloadClassLevel, int casterLevel, int classLevel)
        {
            overloadClassLevel += classLevel;
            overloadLevel += base.GetOverloadPrestige(classLevel, OverloadPrestigeType.MinusOne);
        }
    }
}
