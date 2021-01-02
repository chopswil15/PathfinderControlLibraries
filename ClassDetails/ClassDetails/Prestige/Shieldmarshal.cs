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
    public class Shieldmarshal : ClassFoundation 
    {
        public Shieldmarshal()
        {
            Name = "Shieldmarshal";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            BABType = StatBlockInfo.BABType.Fast;
            IsPrestigeClass = true;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.DISABLE_DEVICE, StatBlockInfo.SkillNames.INTIMIDATE,StatBlockInfo.SkillNames.KNOWLEDGE_ENGINEERING, 
                         StatBlockInfo.SkillNames.KNOWLEDGE_LOCAL, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.STEALTH, StatBlockInfo.SkillNames.SURVIVAL };
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Gunsmithing", "Quick Draw" };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill{SkillName = SkillData.SkillNames.Diplomacy,Value=5});
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Intimidate, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeLocal, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Intimidate, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.SenseMotive, Value = 5 });
            return temp;
        }
    }
}
