using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OnGoing;
using CommonStatBlockInfo;
using PathfinderGlobals;
using ClassFoundational;

namespace ClassDetails
{
    public class Cavalier : ClassFoundation 
    {
        public Cavalier()
        {
            Name = "Cavalier";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.Good;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Poor;
            BABType = StatBlockInfo.BABType.Fast;
            ClassArchetypes = new List<string>() { "Beast Rider", "Emissary", "Gendarme", "Honor Guard", "Luring Cavalier", "Musketeer",
                                                   "Standard Bearer", "Strategist","Charger","Huntmaster","Constable","Fell Rider" };
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Martial;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Shield;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Tower;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Medium;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Heavy;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SWIM };
        }

        public override List<string> ClassArchetypeSkills(string Archetype)
        {
            List<string> temp = ClassSkills();

            switch (Archetype)
            {
                case "Huntmaster":
                    temp.AddRange(new List<string> { StatBlockInfo.SkillNames.KNOWLEDGE_NATURE, StatBlockInfo.SkillNames.STEALTH, StatBlockInfo.SkillNames.SURVIVAL });
                    break;
            }

            return temp;

        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            //one at 1st
            int Count = 1;

            //bonus combat feats
            if (ClassLevel >= 6) Count++;
            if (ClassLevel >= 12) Count++;
            if (ClassLevel >= 18) Count++;

            //teamwork feat
            if (ClassLevel >= 9) Count++;
            if (ClassLevel >= 17) Count++;


            return Count;
        }
    }
}
