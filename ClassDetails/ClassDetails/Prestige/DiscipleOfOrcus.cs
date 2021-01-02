using ClassFoundational;
using CommonStatBlockInfo;

using Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassDetails
{
    public class DiscipleOfOrcus : ClassFoundation
    {
        public DiscipleOfOrcus()
        {
            Name = "Disciple Of Orcus";
            ClassAlignments = new List<string> {"CE" };
            SkillRanksPerLevel = 2;
            CanCastSpells = true;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            BABType = StatBlockInfo.BABType.Medium;
            IsPrestigeClass = true;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.KNOWLEDGE_LOCAL,StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION, 
                StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.SPELLCRAFT };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeReligion, Value = 6 });
            return temp;
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Great Fortitude", "Power Attack" };
        }
    }
}
