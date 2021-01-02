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
    public class MagaambyanArcanist : ClassFoundation 
    {
        public MagaambyanArcanist()
        {
            Name = "Magaambyan Arcanist";
            ClassAlignments = CommonMethods.GetGoodAlignments();
            SkillRanksPerLevel = 2;
            HitDiceType = StatBlockInfo.HitDiceCategories.d6;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            BABType = StatBlockInfo.BABType.Slow;
            IsPrestigeClass = true;
        }      

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.HANDLE_ANIMAL,StatBlockInfo.SkillNames.HEAL,
                StatBlockInfo.SkillNames.KNOWLEDGE_ALL, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.SURVIVAL };
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Scholar", "Spell Mastery" };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Spellcraft, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeArcana, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeNature, Value = 5 });
            return temp;
        }

        public override void GetSpellOverLoads(ref int overloadLevel, ref int overloadClassLevel, int casterLevel, int classLevel)
        {
            overloadClassLevel += classLevel;
            overloadLevel += base.GetOverloadPrestige(classLevel, OverloadPrestigeType.Full);
        }
    }
}
