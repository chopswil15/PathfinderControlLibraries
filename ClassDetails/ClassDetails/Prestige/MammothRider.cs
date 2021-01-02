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
    public class MammothRider : ClassFoundation 
    {
        public MammothRider()
        {
            Name = "Mammoth Rider";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d12;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            BABType = StatBlockInfo.BABType.Fast;
            IsPrestigeClass = true;
            PrestigeBABMin = 6;
        }      

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SURVIVAL };
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            //Born Survivor
            int count = 0;
            if (ClassLevel >= 2) count++;
            if (ClassLevel >= 6) count++;           

            return count;
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.HandleAnimal, Value = 9 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Ride, Value = 9 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Survival, Value = 5 });
            return temp;
        }
    }
}
