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
    public class HorizonWalker : ClassFoundation
    {
        public HorizonWalker()
        {
            Name = "Horizon Walker";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 6;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            BABType = StatBlockInfo.BABType.Fast;
            ClassArchetypes = new List<string>();
            IsPrestigeClass = true;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.KNOWLEDGE_GEOGRAPHY, StatBlockInfo.SkillNames.KNOWLEDGE_NATURE, StatBlockInfo.SkillNames.KNOWLEDGE_PLANES, StatBlockInfo.SkillNames.LINGUISTICS, 
                StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.STEALTH, StatBlockInfo.SkillNames.SURVIVAL, StatBlockInfo.SkillNames.SWIM };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeGeography, Value = 6 });
            return temp;
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Endurance" };
        }
    }
}
