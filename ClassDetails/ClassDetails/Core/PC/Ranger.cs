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
    public class Ranger : ClassFoundation 
    {
        public Ranger()
        {
            Name = "Ranger";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 6;
            CanCastSpells = true;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.Good;
            RefSaveType = StatBlockInfo.SaveBonusType.Good;
            WillSaveType = StatBlockInfo.SaveBonusType.Poor;
            BABType = StatBlockInfo.BABType.Fast;
            ClassArchetypes = new List<string>() {"Battle Scout", "Beastmaster", "Deep Walker", "Falconer", "Guide", "Horse Lord", "Infiltrator",
                                                  "Nirmathi Irregular", "Sable Company Marine", "Shapeshifter", "Skirmisher", "Spirit Ranger",
                                                  "Trapper", "Trophy Hunter", "Urban Ranger", "Warden", "Wild Stalker","Hooded Champion" };
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Martial;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Shield;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Medium;
            SetFeatures();
        }

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_DUNGEONEERING, StatBlockInfo.SkillNames.KNOWLEDGE_GEOGRAPHY, 
                                     StatBlockInfo.SkillNames.KNOWLEDGE_NATURE, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.STEALTH, StatBlockInfo.SkillNames.SURVIVAL, StatBlockInfo.SkillNames.SWIM };
        }

        public override List<string> ClassArchetypeSkills(string Archetype)
        {

            switch (Archetype)
            {
                case "Urban Ranger":
                    return new List<string> {StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DISABLE_DEVICE, StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_DUNGEONEERING, StatBlockInfo.SkillNames.KNOWLEDGE_GEOGRAPHY, 
                                     StatBlockInfo.SkillNames.KNOWLEDGE_LOCAL, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.STEALTH, StatBlockInfo.SkillNames.SURVIVAL, StatBlockInfo.SkillNames.SWIM };
                default:
                    return ClassSkills();
            }
        }

        public override string GetSpellBonusAbility()
        {
            return StatBlockInfo.WIS;
        }

        public override List<int> GetSpellsPerLevel(int ClassLevel)
        {
            //Bonus Spells handled in elsewhere
            List<int> Levels = new List<int>();
            Levels.Add(0); //no 0 level spells

            switch (ClassLevel)
            {
                case 4:
                    Levels.Add(0); //1st Wis bonus only
                    break;
                case 5:
                case 6:
                    Levels.Add(1); //1st
                    break;
                case 7:
                    Levels.Add(1); //1st
                    Levels.Add(0); //2nd Wis bonus only
                    break;
                case 8:
                    Levels.Add(1); //1st
                    Levels.Add(1); //2nd
                    break;
                case 9:
                    Levels.Add(2); //1st
                    Levels.Add(1); //2nd
                    break;
                case 10:
                    Levels.Add(2); //1st
                    Levels.Add(1); //2nd
                    Levels.Add(0); //3rd Wis bonus only
                    break;
                case 11:
                    Levels.Add(2); //1st
                    Levels.Add(1); //2nd
                    Levels.Add(1); //3rd 
                    break;
                case 12:
                    Levels.Add(2); //1st
                    Levels.Add(2); //2nd
                    Levels.Add(1); //3rd 
                    break;
                case 13:
                    Levels.Add(3); //1st
                    Levels.Add(2); //2nd
                    Levels.Add(1); //3rd 
                    Levels.Add(0); //4th Wis bonus only
                    break;
                case 14:
                    Levels.Add(3); //1st
                    Levels.Add(2); //2nd
                    Levels.Add(1); //3rd 
                    Levels.Add(1); //4th
                    break;
                case 15:
                    Levels.Add(3); //1st
                    Levels.Add(2); //2nd
                    Levels.Add(2); //3rd 
                    Levels.Add(1); //4th
                    break;
                case 16:
                    Levels.Add(3); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(2); //3rd 
                    Levels.Add(1); //4th
                    break;
                case 17:
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(2); //3rd 
                    Levels.Add(1); //4th
                    break;
                case 18:
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(2); //3rd 
                    Levels.Add(2); //4th
                    break;
                case 19:
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(3); //3rd 
                    Levels.Add(2); //4th
                    break;
                case 20:
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(3); //3rd 
                    Levels.Add(3); //4th
                    break;
            }
            return Levels;
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            //Combat Style Feats
            int Count = 0;
            for (int a = 1; a <= 18; a++)
            {
                if (a == 2)
                {
                    Count++;
                }
                 if (a == 3)  //Endurance at 3rd
                {
                    Count++;
                }               
                if (a == 6)
                {
                    Count++;
                }
                if (a == 10)
                {
                    Count++;
                }
                if (a == 14)
                {
                    Count++;
                }
                if (a == 18)
                {
                    Count++;
                }
                if (ClassLevel == a)
                {
                    break;
                }
            }
            return Count;
        }

        private void SetFeatures()
        {           }
    }
}
