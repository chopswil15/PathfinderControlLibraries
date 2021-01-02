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
    public class AldoriSwordlord : ClassFoundation 
    {
        public AldoriSwordlord()
        {
            Name = "Aldori Swordlord";
            ClassAlignments = CommonMethods.GetNonGoodAlignments();
            SkillRanksPerLevel = 2;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            BABType = StatBlockInfo.BABType.Fast;
            IsPrestigeClass = true;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_NOBILITY, StatBlockInfo.SkillNames.PERFORM, StatBlockInfo.SkillNames.SENSE_MOTIVE };
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            //Dueling Mastery
            return 1;
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Acrobatics, Value = 3 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Intimidate, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeNobility, Value = 3 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.SenseMotive, Value = 3 });
            return temp;
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Dazzling Display", "Exotic Weapon Proficiency (Aldori dueling sword)", "Weapon Finesse", "Weapon Focus (Aldori dueling sword)" };
        }
    }
}
