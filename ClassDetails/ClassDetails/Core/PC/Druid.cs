using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OnGoing;
using CommonStatBlockInfo;
using ClericDomains;
using PathfinderGlobals;
using ClassFoundational;

namespace ClassDetails
{
    public class Druid : ClassFoundation 
    {
        public Druid()
        {
            Name = "Druid";
            ClassAlignments = CommonMethods.GetNeutralAlignments();
            SkillRanksPerLevel = 4;
            DomainSpellUse = true;
            CanCastSpells = true;
            Domains = new List<IDomain>();
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.Good;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
            BABType = StatBlockInfo.BABType.Medium;
            ClassArchetypes = new List<string>() { "Ape Shaman","Aquatic Druid","Arctic Druid","Bat Shaman","Bear Shaman","Blight Druid","Boar Shaman",
                                                   "Cave Druid","Desert Druid","Dragon Shaman","Eagle Shaman","Jungle Druid","Lion Shaman","Menhir Savant",
                                                   "Mooncaller","Mountain Druid","Pack Lord","Plains Druid","Reincarnated Druid","Saurian Shaman",
                                                   "Serpent Shaman","Shark Shaman","Storm Druid","Swamp Druid","Tempest Druid","Urban Druid",
                                                   "Wolf Shaman","World Walker","Ancient Guardian", "Troll Fury","Nature Fang","Sky Druid" };
            
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Extra;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Shield;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Medium;
            SetFeatures();
        }

        public override List<string> GetWeaponProficienciesExtra()
        {
            return new List<string> { "club", "dagger", "dart", "quarterstaff", "scimitar", "scythe", "sickle", "shortspear", "sling", "spear" };
        }

        public override List<string> ClassArchetypeSkills(string Archetype)
        {
            return ClassSkills();
        }

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.KNOWLEDGE_GEOGRAPHY, StatBlockInfo.SkillNames.KNOWLEDGE_NATURE, StatBlockInfo.SkillNames.PERCEPTION,
                                     StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.SURVIVAL, StatBlockInfo.SkillNames.SWIM };
        }

        public override string GetSpellBonusAbility()
        {
            return StatBlockInfo.WIS;
        }

        public override List<int> GetSpellsPerLevel(int ClassLevel)
        {
            //Bonus Spells handled in elsewhere
            List<int> Levels = new List<int>();
            switch (ClassLevel)
            {
                case 1:
                    Levels.Add(3); //0th
                    Levels.Add(1); //1st
                    break;
                case 2:
                    Levels.Add(4); //0th
                    Levels.Add(2); //1st
                    break;
                case 3:
                    Levels.Add(4); //0th
                    Levels.Add(2); //1st
                    Levels.Add(1); //2nd
                    break;
                case 4:
                    Levels.Add(4); //0th
                    Levels.Add(3); //1st
                    Levels.Add(2); //2nd
                    break;
                case 5:
                    Levels.Add(4); //0th
                    Levels.Add(3); //1st
                    Levels.Add(2); //2nd
                    Levels.Add(1); //3rd
                    break;
                case 6:
                    Levels.Add(4); //0th
                    Levels.Add(3); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(2); //3rd
                    break;
                case 7:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(2); //3rd
                    Levels.Add(1); //4th
                    break;
                case 8:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(2); //4th
                    break;
                case 9:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(2); //4th
                    Levels.Add(1); //5th
                    break;
                case 10:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(3); //4th
                    Levels.Add(2); //5th
                    break;
                case 11:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(3); //4th
                    Levels.Add(2); //5th
                    Levels.Add(1); //6th
                    break;
                case 12:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(3); //4th
                    Levels.Add(3); //5th
                    Levels.Add(2); //6th
                    break;
                case 13:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(3); //5th
                    Levels.Add(2); //6th
                    Levels.Add(1); //7th
                    break;
                case 14:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(3); //5th
                    Levels.Add(3); //6th
                    Levels.Add(2); //7th
                    break;
                case 15:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(3); //6th
                    Levels.Add(2); //7th
                    Levels.Add(1); //8th
                    break;
                case 16:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(3); //6th
                    Levels.Add(3); //7th
                    Levels.Add(2); //8th
                    break;
                case 17:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(4); //6th
                    Levels.Add(3); //7th
                    Levels.Add(2); //8th
                    Levels.Add(1); //9th
                    break;
                case 18:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(4); //6th
                    Levels.Add(3); //7th
                    Levels.Add(3); //8th
                    Levels.Add(2); //9th
                    break;
                case 19:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(4); //6th
                    Levels.Add(4); //7th
                    Levels.Add(3); //8th
                    Levels.Add(3); //9th
                    break;
                case 20:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(4); //6th
                    Levels.Add(4); //7th
                    Levels.Add(4); //8th
                    Levels.Add(4); //9th
                    break;
            }

            return Levels;
        }

        public override void ProcessDomains(string DomainsString)
        {
            Domains = DomainUltility.ProcessDomains(DomainsString);
        }

        public override int GetDomainSpellsPerLevel(int ClassLevel)
        {
            return DomainUltility.GetDomainSpellsPerLevel(ClassLevel, Domains);
        }

        private void SetFeatures()
        {     }
    }
}
