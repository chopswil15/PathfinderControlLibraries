using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonStatBlockInfo;
using Skills;
using PathfinderGlobals;
using ClassFoundational;

namespace ClassDetails
{
    public class HellknightSignifer : ClassFoundation
    {
        public HellknightSignifer()
        {
            Name = "Hellknight Signifer";
            ClassAlignments = CommonMethods.GetLawfulAlignments();
            SkillRanksPerLevel = 2;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            BABType = StatBlockInfo.BABType.Medium;
            IsPrestigeClass = true;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Heavy;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.INTIMIDATE, 
                StatBlockInfo.SkillNames.LINGUISTICS, StatBlockInfo.SkillNames.KNOWLEDGE_ALL, StatBlockInfo.SkillNames.PROFESSION, 
                StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SPELLCRAFT };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Intimidate, Value = 2 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgePlanes, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Spellcraft, Value = 5 });
            return temp;
        }

        public override void GetSpellOverLoads(ref int overloadLevel, ref int overloadClassLevel, int casterLevel, int classLevel)
        {
            overloadClassLevel += classLevel;
            overloadLevel += base.GetOverloadPrestige(classLevel, OverloadPrestigeType.Full);
        }
    }
}
