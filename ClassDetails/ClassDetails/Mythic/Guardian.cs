using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassFoundational;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace ClassDetails
{
    public class Guardian : ClassFoundation
    {
        public Guardian()
        {
            Name = "Guardian";
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
