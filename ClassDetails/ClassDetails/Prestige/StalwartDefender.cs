using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassFoundational;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace ClassDetails
{
    public class StalwartDefender : ClassFoundation 
    {
        public StalwartDefender()
        {
            Name = "Stalwart Defender";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 2;
            HitDiceType = StatBlockInfo.HitDiceCategories.d12;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            BABType = StatBlockInfo.BABType.Fast;
            IsPrestigeClass = true;
            PrestigeBABMin = 7;
        }


        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.SENSE_MOTIVE };
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Dodge", "Endurance", "Toughness" };
        }
    }
}
