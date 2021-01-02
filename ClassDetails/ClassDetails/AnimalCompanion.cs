using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassFoundational;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace ClassDetails
{
    public class AnimalCompanion : ClassFoundation
    {

        public AnimalCompanion()
        {
            Name = "Animal Companion";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 0;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.AnimalCompanionGood;
            RefSaveType = StatBlockInfo.SaveBonusType.AnimalCompanionGood;
            WillSaveType = StatBlockInfo.SaveBonusType.AnimalCompanionPoor;
            BABType = StatBlockInfo.BABType.AnimalCompanion;
            ClassArchetypes = new List<string>() { "Bodyguard", "Charger", "Racer", "Totem Guide" };         
            SetFeatures();               
        }

        public override List<string> ClassArchetypeSkills(string Archetype)
        {
            return ClassSkills();
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS , StatBlockInfo.SkillNames.CLIMB , StatBlockInfo.SkillNames.FLY ,  StatBlockInfo.SkillNames.PERCEPTION , StatBlockInfo.SkillNames.STEALTH , StatBlockInfo.SkillNames.SWIM };
        }

        private void SetFeatures()
        {

        }
    }
}
