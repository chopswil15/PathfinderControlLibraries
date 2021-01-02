using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OnGoing;
using ClericDomains;
using CommonStatBlockInfo;
using PathfinderGlobals;
using ClassFoundational;

namespace ClassDetails
{
    public class Cleric : ClassFoundation 
    {   
        public Cleric()
        {
            Name = "Cleric";
            ClassAlignments = CommonMethods.GetAlignments();
            DomainSpellUse = true;
            CanCastSpells = true;
            Domains = new List<IDomain>();
            SkillRanksPerLevel = 2;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.Good;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
            BABType = StatBlockInfo.BABType.Medium;
            ClassArchetypes = new List<string>(){ "Cloistered Cleric", "Crusader", "Divine Strategist", "Evangelist", "Hidden Priest",
                                                  "Mendevian Priest", "Merciful Healer", "Separatist", "Theologian", "Undead Lord",
                                                  "Variant Channeling", "Varisian Pilgrim","Demonic Apostle","Elder Mythos Cultist", "Disciple Of Orcus"};
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Shield;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Medium;
            SetFeatures();
        }

        public override List<string> ClassArchetypeSkills(string Archetype)
        {
            return ClassSkills();
        }

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.APPRAISE, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA, StatBlockInfo.SkillNames.KNOWLEDGE_HISTORY, StatBlockInfo.SkillNames.KNOWLEDGE_NOBILITY,
                                     StatBlockInfo.SkillNames.KNOWLEDGE_PLANES, StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION, StatBlockInfo.SkillNames.LINGUISTICS, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SPELLCRAFT };
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
                    Levels.Add(2); //1st
                    break;
                case 2:
                    Levels.Add(4); //0th
                    Levels.Add(3); //1st
                    break;
                case 3:
                    Levels.Add(4); //0th
                    Levels.Add(3); //1st
                    Levels.Add(2); //2nd
                    break;
                case 4:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    break;
                case 5:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(2); //3rd
                    break;
                case 6:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd
                    break;
                case 7:
                    Levels.Add(4); //0th
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(2); //4th
                    break;
                case 8:
                    Levels.Add(4); //0th
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(3); //4th
                    break;
                case 9:
                    Levels.Add(4); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(3); //4th
                    Levels.Add(2); //5th
                    break;
                case 10:
                    Levels.Add(4); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(3); //5th
                    break;
                case 11:
                    Levels.Add(4); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(3); //5th
                    Levels.Add(2); //6th
                    break;
                case 12:
                    Levels.Add(4); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(3); //6th
                    break;
                case 13:
                    Levels.Add(4); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(4); //5th
                    Levels.Add(3); //6th
                    Levels.Add(2); //7th
                    break;
                case 14:
                    Levels.Add(4); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(4); //5th
                    Levels.Add(4); //6th
                    Levels.Add(3); //7th
                    break;
                case 15:
                    Levels.Add(4); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(5); //5th
                    Levels.Add(4); //6th
                    Levels.Add(3); //7th
                    Levels.Add(2); //8th
                    break;
                case 16:
                    Levels.Add(4); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(5); //5th
                    Levels.Add(4); //6th
                    Levels.Add(4); //7th
                    Levels.Add(3); //8th
                    break;
                case 17:
                    Levels.Add(4); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(5); //5th
                    Levels.Add(5); //6th
                    Levels.Add(4); //7th
                    Levels.Add(3); //8th
                    Levels.Add(2); //9th
                    break;
                case 18:
                    Levels.Add(4); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(5); //5th
                    Levels.Add(5); //6th
                    Levels.Add(4); //7th
                    Levels.Add(4); //8th
                    Levels.Add(3); //9th
                    break;
                case 19:
                    Levels.Add(4); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(5); //5th
                    Levels.Add(5); //6th
                    Levels.Add(5); //7th
                    Levels.Add(4); //8th
                    Levels.Add(4); //9th
                    break;
                case 20:
                    Levels.Add(4); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(5); //5th
                    Levels.Add(5); //6th
                    Levels.Add(5); //7th
                    Levels.Add(5); //8th
                    Levels.Add(5); //9th
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
        {            
            ClassFeature = new OnGoingSpecialAbility("ChannelEnergy", 0, "Channel Energy",
               OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1);
            
            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("DomainPowers", 0, "Domain Powers",
               OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 1);
            
            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("Orisons", 0, "Orisons",
               OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 1);
            
            Features.Add(ClassFeature);


            ClassFeature = new OnGoingSpecialAbility("SpontaneousCasting", 0, "Spontaneous Casting",
               OnGoingSpecialAbility.SpecialAbilityTypes.None,
               OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 1);
            
            Features.Add(ClassFeature);
        }
    }
}
