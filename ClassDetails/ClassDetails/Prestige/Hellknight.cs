using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using OnGoing;
using PathfinderGlobals;
using CommonStatBlockInfo;
using ClassFoundational;

namespace ClassDetails
{
    public class Hellknight : ClassFoundation
    {
        public Hellknight()
        {
            Name = "Hellknight";
            ClassAlignments = CommonMethods.GetLawfulAlignments();
            SkillRanksPerLevel = 2;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            BABType = StatBlockInfo.BABType.Fast;
            ClassArchetypes = new List<string>() { "Order of the Chain", "Order of the Gate", "Order of the God Claw", "Order of the Nail", 
                     "Order of the Pyre", "Order of the Rack", "Order of the Scourge" };
            IsPrestigeClass = true;
            PrestigeBABMin = 5;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_LOCAL, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SENSE_MOTIVE };
        }
    }
}
