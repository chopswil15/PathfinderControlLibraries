using CommonStatBlockInfo;

using Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathfinderGlobals;
using ClassFoundational;

namespace ClassDetails
{
    public class LionBlade : ClassFoundation
    {
        public LionBlade()
        {
            Name = "Lion Blade";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 6;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            BABType = StatBlockInfo.BABType.Medium;
            IsPrestigeClass = true;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.DISABLE_DEVICE, StatBlockInfo.SkillNames.DISGUISE, StatBlockInfo.SkillNames.INTIMIDATE,
                 StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.PERFORM, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.STEALTH };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Bluff, Value = 3 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Diplomacy, Value = 3 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Disguise, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Stealth, Value = 5 });
            return temp;
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Deceitful", "Improved Initiative", "Skill Focus (Perform)" };
        }
    }
}
