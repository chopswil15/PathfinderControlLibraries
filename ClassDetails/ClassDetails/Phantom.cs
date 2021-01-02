using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassFoundational;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace ClassDetails
{
    public class Phantom : ClassFoundation
    {
        public Phantom()
        {
            Name = "Phantom";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 0;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.Varies;
            RefSaveType = StatBlockInfo.SaveBonusType.Varies;
            WillSaveType = StatBlockInfo.SaveBonusType.Varies;
            BABType = StatBlockInfo.BABType.Eidolon;
            ClassArchetypes = new List<string> { "Anger", "Dedication", "Despair", "Fear", "Greed", "Hatred", "Jealousy", "Lust",
                                                  "Pride","Remorse","Zeal"}; //really Emotional Focus        
            //SetFeatures();               
        }

        public override List<string> ClassArchetypeSkills(string Archetype)
        {
            return ClassSkills();
        }

        public override List<string> ClassSkills()
        {
            return new List<string> ();
        }
    }
}
