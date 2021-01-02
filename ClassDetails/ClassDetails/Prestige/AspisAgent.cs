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
    public class AspisAgent : ClassFoundation 
    {
        public AspisAgent()
        {
            Name = "Aspis Agent";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            BABType = StatBlockInfo.BABType.Medium;
            IsPrestigeClass = true;
        }      

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.DISGUISE, StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA, StatBlockInfo.SkillNames.KNOWLEDGE_HISTORY, StatBlockInfo.SkillNames.SENSE_MOTIVE, 
                             StatBlockInfo.SkillNames.SLEIGHT_OF_HAND, StatBlockInfo.SkillNames.USE_MAGIC_DEVICE };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Appraise, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Bluff, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Craft, SubType= "traps", Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.DisableDevice, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Intimidate, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeHistory, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Perception, Value = 5 });
            return temp;
        }
    }
}
