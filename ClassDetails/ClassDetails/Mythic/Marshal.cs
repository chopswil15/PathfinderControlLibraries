using ClassFoundational;
using CommonStatBlockInfo;
using PathfinderGlobals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassDetails
{
    public class Marshal : ClassFoundation
    {
        public Marshal()
        {
            Name = "Marshal";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 0;
            HitDiceType = StatBlockInfo.HitDiceCategories.None;
            FortSaveType = StatBlockInfo.SaveBonusType.None;
            RefSaveType = StatBlockInfo.SaveBonusType.None;
            WillSaveType = StatBlockInfo.SaveBonusType.None;
            BABType = StatBlockInfo.BABType.Mythic;
            ClassArchetypes = new List<string>();
            Mythic = true;
        }
    }
}
