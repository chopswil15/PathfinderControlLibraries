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
    public class DaggermarkPoisoner : ClassFoundation 
    {
        public DaggermarkPoisoner()
        {
            Name = "Daggermark Poisoner";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            BABType = StatBlockInfo.BABType.Medium;
            IsPrestigeClass = true;
        }

      

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.DISGUISE,StatBlockInfo.SkillNames.ESCAPE_ARTIST, StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.SLEIGHT_OF_HAND,StatBlockInfo.SkillNames.STEALTH };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Craft, SubType = "alchemy", Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Craft,SubType="traps" ,Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Heal, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.SleightofHand, Value = 5 });
            return temp;
        }
    }
}
