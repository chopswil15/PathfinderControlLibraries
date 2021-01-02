using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonStatBlockInfo;
using ClassFoundational;
using PathfinderGlobals;

namespace ClassDetails
{
    public class Commoner : ClassFoundation
    {
        public Commoner()
        {
            Name = "Commoner";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 2;
            HitDiceType = StatBlockInfo.HitDiceCategories.d6;
            FortSaveType = StatBlockInfo.SaveBonusType.Poor;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Poor;
            BABType = StatBlockInfo.BABType.Slow;
            IsNPC_Class = true;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.SimpleOne;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SWIM };
        }

        public override List<CheckClassError> CheckClass(int classLevel)
        {
            return new List<CheckClassError>();
        }
    }
}
