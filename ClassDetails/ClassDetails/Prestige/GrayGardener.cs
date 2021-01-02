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
    public class GrayGardener : ClassFoundation 
    {
        public GrayGardener()
        {
            Name = "Gray Gardener";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 6;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            BABType = StatBlockInfo.BABType.Medium;
            IsPrestigeClass = true;
        }      

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.DISGUISE,
                StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_LOCAL, StatBlockInfo.SkillNames.PERCEPTION,
                                      StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.STEALTH };
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {          
            int count = 0;
            if (ClassLevel >= 4) count++;
            if (ClassLevel >= 8) count++;
            if (ClassLevel >= 10) count++;

            return count;
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Combat Reflexes" };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Bluff,  Value = 2 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Diplomacy,Value = 2 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Disguise, Value = 2 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Intimidate, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Perception, Value = 2 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.SenseMotive, Value = 5 });
            return temp;
        }

        public override void GetSpellOverLoads(ref int overloadLevel, ref int overloadClassLevel, int casterLevel, int classLevel)
        {
            overloadClassLevel += classLevel;
            overloadLevel += base.GetOverloadPrestige(classLevel, OverloadPrestigeType.BlocksOfThree);
        }
    }
}
