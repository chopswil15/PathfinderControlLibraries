using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonStatBlockInfo;
using ClassFoundational;
using PathfinderGlobals;

namespace ClassDetails
{
    public class Expert : ClassFoundation
    {
        public Expert()
        {
            Name = "Expert";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 6;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.Poor;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
            BABType = StatBlockInfo.BABType.Medium;
            IsNPC_Class = true;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> {"Extra 10" };
        }

        public override List<CheckClassError> CheckClass(int classLevel)
        {
            return new List<CheckClassError>();
        }
    }
}
