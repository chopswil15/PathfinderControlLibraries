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
    public class MasterSpy : ClassFoundation 
    {
        public MasterSpy()
        {
            Name = "Master Spy";
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
            return new List<string> { StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.DISABLE_DEVICE, StatBlockInfo.SkillNames.DISGUISE, StatBlockInfo.SkillNames.ESCAPE_ARTIST, StatBlockInfo.SkillNames.KNOWLEDGE_ALL, StatBlockInfo.SkillNames.LINGUISTICS, 
                                      StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SLEIGHT_OF_HAND, StatBlockInfo.SkillNames.STEALTH, StatBlockInfo.SkillNames.USE_MAGIC_DEVICE };
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Deceitful", "Iron Will" };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Bluff, Value = 7 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Disguise, Value = 7 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.SenseMotive, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Perception, Value = 5 });
            return temp;
        }
    }
}
