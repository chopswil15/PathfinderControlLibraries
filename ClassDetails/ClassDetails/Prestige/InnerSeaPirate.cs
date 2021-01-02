using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using OnGoing;
using CommonStatBlockInfo;
using Skills;
using PathfinderGlobals;
using ClassFoundational;

namespace ClassDetails
{
    public class InnerSeaPirate : ClassFoundation 
    {
        public InnerSeaPirate()
        {
            Name = "Inner Sea Pirate";
            ClassAlignments = CommonMethods.GetNonLawfulAlignments();
            SkillRanksPerLevel = 6;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            BABType = StatBlockInfo.BABType.Medium;
            IsPrestigeClass = true;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.APPRAISE, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.INTIMIDATE, 
                StatBlockInfo.SkillNames.KNOWLEDGE_NATURE, "Profession (sailor)" , StatBlockInfo.SkillNames.SWIM };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Appraise, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Profession,SubType="sailor", Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Swim, Value = 5 });
            return temp;
        }
    }
}
