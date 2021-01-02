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
    public class Monk : ClassFoundation 
    {
        public Monk()
        {
            Name = "Monk";
            ClassAlignments = CommonMethods.GetLawfulAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.Good;
            RefSaveType = StatBlockInfo.SaveBonusType.Good;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
            BABType = StatBlockInfo.BABType.Medium;
            ClassArchetypes = new List<string>() {"Drunken Master", "Flowing Monk", "Hungry Ghost Monk", "Ki Mystic", "Maneuver Master", "Martial Artist",
                                                  "Master of Many Styles", "Monk of the Empty Hand", "Monk Of The Four Winds", "Monk Of The Healing Hand",
                                                  "Monk Of The Lotus", "Monk Of The Sacred Mountain", "Qinggong Monk", "Sensei", "Sohei", "Tetori",
                                                  "Weapon Adept", "Zen Archer" ,"Gray Disciple","Hellcat","Monk Of The Seven Forms","Monk Of The Empty Hand"};
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.None;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.None;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Extra;
            SetFeatures();
        }

        public override List<string> GetWeaponProficienciesExtra()
        {
            return new List<string> { "brass knuckles", "cestus", "club", "light crossbow", "heavy crossbow", "dagger", "handaxe",
                    "javelin", "kama", "nunchaku", "quarterstaff", "sai", "shortspear", "short sword", "shuriken",
                    "siangham", "sling", "spear", "temple sword", "unarmed strike","flurry of blows" };
        }

        public override List<string> ClassArchetypeSkills(string Archetype)
        {
            return ClassSkills();
        }

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.ESCAPE_ARTIST, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_HISTORY, StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION, StatBlockInfo.SkillNames.PERCEPTION, 
                                     StatBlockInfo.SkillNames.PERFORM, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.STEALTH, StatBlockInfo.SkillNames.SWIM };
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            int Count = 2;


            //one new feat on even levels plus one at 1st
            if (ClassLevel >= 1) Count++;
            if (ClassLevel >= 2) Count++;
            if (ClassLevel >= 6) Count++;
            if (ClassLevel >= 10) Count++;
            if (ClassLevel >= 14) Count++;
            if (ClassLevel >= 18) Count++;

            return Count;
        }

        private void SetFeatures()
        {           }
    }
}
