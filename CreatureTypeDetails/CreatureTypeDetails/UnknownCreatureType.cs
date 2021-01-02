using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using CreatureTypeFoundational;

namespace CreatureTypeDetails
{
    public class UnknownCreatureType : CreatureTypeFoundation
    {
        public UnknownCreatureType()
        {
            Name = "UnknownCreatureType";
            SkillRanksPerLevel = -10;
            HitDiceType = StatBlockInfo.HitDiceCategories.d1;
            FortSaveType = StatBlockInfo.SaveBonusType.None;
            RefSaveType = StatBlockInfo.SaveBonusType.None;
            WillSaveType = StatBlockInfo.SaveBonusType.None;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> ();
        }
    }
}
