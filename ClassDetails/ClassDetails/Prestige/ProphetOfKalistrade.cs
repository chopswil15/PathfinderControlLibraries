using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassFoundational;
using CommonStatBlockInfo;
using Skills;

namespace ClassDetails
{
    public class ProphetOfKalistrade : ClassFoundation 
    {
        public ProphetOfKalistrade()
        {
            Name = "Prophet of Kalistrade";
            ClassAlignments = new List<string>() { "LN" };
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            BABType = StatBlockInfo.BABType.Medium;
            IsPrestigeClass = true;
        }      

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.APPRAISE, StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.DIPLOMACY,StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_LOCAL,StatBlockInfo.SkillNames.KNOWLEDGE_NOBILITY, 
                            StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.USE_MAGIC_DEVICE };
        }


        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeLocal, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Profession,SubType="merchant", Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.SenseMotive, Value = 2 });
            return temp;
        }
    }
}
