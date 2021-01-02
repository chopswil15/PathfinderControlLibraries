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
    public class KnightOfOzem : ClassFoundation 
    {
        public KnightOfOzem()
        {
            Name = "Knight Of Ozem";
            ClassAlignments = CommonMethods.GetGoodAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            BABType = StatBlockInfo.BABType.Fast;
            IsPrestigeClass = true;
        }      

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA,StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SENSE_MOTIVE,StatBlockInfo.SkillNames.SPELLCRAFT };
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
           //Teamwork Feat (Ex)
            int count = 0;
            if (ClassLevel >= 2) count++;
            if (ClassLevel >= 5) count++;
            if (ClassLevel >= 8) count++;

           //Aegis Feat (Ex)
            if (ClassLevel >= 3) count++;
            if (ClassLevel >= 6) count++;
            if (ClassLevel >= 9) count++;

            return count;
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeReligion, Value = 5 });
            return temp;
        }

    }
}
