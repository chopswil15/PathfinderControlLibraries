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
    public class RazmiranPriest : ClassFoundation 
    {
        public RazmiranPriest()
        {
            Name = "Razmiran Priest";
            ClassAlignments = CommonMethods.GetNonGoodAlignments();
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
            return new List<string> {StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.DIPLOMACY, 
                StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION, StatBlockInfo.SkillNames.SENSE_MOTIVE, 
                StatBlockInfo.SkillNames.USE_MAGIC_DEVICE };
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "False Casting" };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Bluff, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.UseMagicDevice, Value = 5 });
            return temp;
        }

        public override void GetSpellOverLoads(ref int overloadLevel, ref int overloadClassLevel, int casterLevel, int classLevel)
        {
            overloadClassLevel += classLevel;
            overloadLevel += base.GetOverloadPrestige(classLevel, OverloadPrestigeType.BlocksOfThree);
        }
    }
}
