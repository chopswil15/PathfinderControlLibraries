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
    public class StormKindler : ClassFoundation 
    {
        public StormKindler()
        {
            Name = "Storm Kindler";
            ClassAlignments = CommonMethods.GetNeutralAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            BABType = StatBlockInfo.BABType.Slow;
            IsPrestigeClass = true;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.KNOWLEDGE_HISTORY, StatBlockInfo.SkillNames.KNOWLEDGE_NATURE, StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION, StatBlockInfo.SkillNames.PERCEPTION , StatBlockInfo.SkillNames.SURVIVAL , StatBlockInfo.SkillNames.SWIM };
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Storm-Lashed" };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Fly, Value = 3 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeHistory, Value = 6 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeNature, Value = 6 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Swim, Value = 3 });
            return temp;
        }
    }
}
