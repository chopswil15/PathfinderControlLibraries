using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassFoundational;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace ClassDetails
{
    public class Eidolon : ClassFoundation
    {
        public Eidolon()
        {
            Name = "Eidolon";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 0;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.Varies;
            RefSaveType = StatBlockInfo.SaveBonusType.Varies;
            WillSaveType = StatBlockInfo.SaveBonusType.Varies;
            BABType = StatBlockInfo.BABType.Eidolon;
            ClassArchetypes = new List<string>();         
            //SetFeatures();               
        }

        public override List<string> ClassArchetypeSkills(string Archetype)
        {
            return ClassSkills();
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.BLUFF,StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.KNOWLEDGE_PLANES, StatBlockInfo.SkillNames.PERCEPTION };
        }
    }
}
