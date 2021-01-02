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
    public class SleeplessDetective : ClassFoundation 
    {
        public SleeplessDetective()
        {
            Name = "Sleepless Detective";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            BABType = StatBlockInfo.BABType.Medium;
            IsPrestigeClass = true;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.DISABLE_DEVICE, StatBlockInfo.SkillNames.DISGUISE, StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_LOCAL, 
                              StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.SENSE_MOTIVE };
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Alertness" };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Craft, SubType = "alchemy", Value = 5 });            
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeLocal, Value = 2 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Perception, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.SenseMotive, Value = 5 });
            return temp;
        }
    }
}
