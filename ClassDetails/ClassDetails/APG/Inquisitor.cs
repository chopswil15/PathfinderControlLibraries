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
    public class Inquisitor : ClassFoundation 
    {
        public Inquisitor()
        {
            Name = "Inquisitor";
            DomainSpellUse = true;
            Domains = new List<IDomain>();
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 6;
            CanCastSpells = true;
            HasSpellsPerDay = true;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.Good;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
            BABType = StatBlockInfo.BABType.Medium;
            ClassArchetypes = new List<string>() { "Exorcist", "Heretic", "Iconoclast", "Infiltrator", "Preacher", "Sin Eater", "Spellbreaker",
                                                    "Vampire Hunter", "Witch Hunter","Sanctified Slayer" };
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Extra;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Medium;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Shield;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.DISGUISE, StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA, 
                                     StatBlockInfo.SkillNames.KNOWLEDGE_DUNGEONEERING, StatBlockInfo.SkillNames.KNOWLEDGE_NATURE, StatBlockInfo.SkillNames.KNOWLEDGE_PLANES, StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION,
                                     StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.STEALTH, StatBlockInfo.SkillNames.SURVIVAL, StatBlockInfo.SkillNames.SWIM };
        }

        public override List<string> GetWeaponProficienciesExtra()
        {
            return new List<string> { "hand crossbow", "longbow", "repeating light crossbow", "repeating heavy crossbow", "shortbow" };
        }

        public override string GetSpellBonusAbility()
        {
            return StatBlockInfo.WIS;
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            int Count = 0;
            //teamwork feat
            if (ClassLevel >= 3) Count++;
            if (ClassLevel >= 6) Count++;
            if (ClassLevel >= 9) Count++;
            if (ClassLevel >= 12) Count++;
            if (ClassLevel >= 15) Count++;
            if (ClassLevel >= 18) Count++;

            return Count;
        }

        public override List<int> GetSpellsPerLevel(int ClassLevel)
        {
            //Bonus Spells handled in elsewhere
            List<int> Levels = new List<int>();            

            switch (ClassLevel)
            {
                case 1:
                    Levels.Add(4); //0th
                    Levels.Add(2); //1st
                    break;
                case 2:
                    Levels.Add(5); //0th
                    Levels.Add(3); //1st
                    break;
                case 3:
                    Levels.Add(6); //0th
                    Levels.Add(4); //1st
                    break;
                case 4:
                    Levels.Add(6); //0th
                    Levels.Add(4); //1st
                    Levels.Add(2); //2nd
                    break;
                case 5:
                    Levels.Add(6); //0th
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    break;
                case 6:
                    Levels.Add(6); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    break;
                case 7:
                    Levels.Add(6); //0th
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(2); //3rd
                    break;
                case 8:
                    Levels.Add(6); //0th
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd
                    break;
                case 9:
                    Levels.Add(6); //0th
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    break;
                case 10:
                    Levels.Add(6); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(2); //4th
                    break;
                case 11:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(3); //4th
                    break;
                case 12:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    break;
                case 13:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(2); //5th
                    break;
                case 14:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(3); //5th
                    break;
                case 15:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    break;
                case 16:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(4); //5th
                    Levels.Add(2); //6th
                    break;
                case 17:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(4); //5th
                    Levels.Add(3); //6th
                    break;
                case 18:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(4); //5th
                    Levels.Add(4); //6th
                    break;
                case 19:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(5); //5th
                    Levels.Add(4); //6th
                    break;
                case 20:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(6); //4th
                    Levels.Add(5); //5th
                    Levels.Add(5); //6th
                    break;
            }
            
            return Levels;
        }

        public override List<int> GetSpellsPerDay(int ClassLevel)
        {
            List<int> Levels = new List<int>();
            Levels.Add(0); //no 0 level spell per day limits

            switch (ClassLevel)
            {
                case 1:
                    Levels.Add(0); //1st Wis bonus only
                    break;
                case 2:
                    Levels.Add(1); //1st
                    break;
                case 3:
                    Levels.Add(2); //1st
                    break;
                case 4:
                    Levels.Add(3); //1st
                    Levels.Add(1);  //2nd
                    break;
                case 5:
                    Levels.Add(4); //1st
                    Levels.Add(2); //2nd
                    break;
                case 6:
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    break;
                case 7:
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(1); //3rd 
                    break;
                case 8:
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(2); //3rd 
                    break;
                case 9:
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd
                    break;
                case 10:
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd 
                    Levels.Add(1); //4th
                    break;
                case 11:
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd 
                    Levels.Add(2); //4th
                    break;
                case 12:
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd 
                    Levels.Add(3); //4th
                    break;
                case 13:
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd 
                    Levels.Add(3); //4th
                    Levels.Add(1); //5th
                    break;
                case 14:
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd 
                    Levels.Add(4); //4th
                    Levels.Add(2); //5th
                    break;
                case 15:
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd 
                    Levels.Add(4); //4th
                    Levels.Add(3); //5th
                    break;
                case 16:
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd 
                    Levels.Add(4); //4th
                    Levels.Add(3); //5th
                    Levels.Add(1); //6th
                    break;
                case 17:
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd 
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(2); //6th
                    break;
                case 18:
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd 
                    Levels.Add(5); //4th
                    Levels.Add(4); //5th
                    Levels.Add(3); //6th
                    break;
                case 19:
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd 
                    Levels.Add(5); //4th
                    Levels.Add(5); //5th
                    Levels.Add(4); //6th
                    break;
                case 20:
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd 
                    Levels.Add(5); //4th
                    Levels.Add(5); //5th
                    Levels.Add(5); //6th
                    break;
            }           

            return Levels;
        }

        public override void ProcessDomains(string DomainsString)
        {
            if (!DomainsString.Contains("inquisition"))
            {
                Domains = DomainUltility.ProcessDomains(DomainsString);
            }
        }

        public override int GetDomainSpellsPerLevel(int ClassLevel)
        {
            return DomainUltility.GetDomainSpellsPerLevel(ClassLevel, Domains);
        }
    }
}
