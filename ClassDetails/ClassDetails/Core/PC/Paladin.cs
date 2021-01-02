using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OnGoing;
using CommonStatBlockInfo;
using ClassFoundational;

namespace ClassDetails
{
    public class Paladin : ClassFoundation 
    {
        public Paladin()
        {
            Name = "Paladin";
            ClassAlignments = new List<string>() { "LG" };
            SkillRanksPerLevel = 2;
            CanCastSpells = true;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.Good;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
            BABType = StatBlockInfo.BABType.Fast;
            ClassArchetypes = new List<string>() { "Divine Defender", "Divine Hunter", "Empyreal Knight", "Holy Gun", "Holy Tactician", "Hospitaler",
                                                   "Sacred Servant", "Sacred Shield", "Shining Knight", "Sword of Valor", "Undead Scourge",
                                                   "Warrior of the Holy Light","Holy Guide" };
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Martial;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Shield;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Medium;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Heavy;
            SetFeatures();
        }

        public override List<string> ClassArchetypeSkills(string Archetype)
        {
            return ClassSkills();
        }

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.KNOWLEDGE_NOBILITY, StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION, StatBlockInfo.SkillNames.PROFESSION, 
                                     StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SPELLCRAFT };
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {            
            int count = 0;
            switch (Archetype)
            {
                case "Holy Guide":
                    if (ClassLevel >= 6) count++; //Teamwork Feat (Ex)
                    break;
            }

            return count;
        }

        public override string GetSpellBonusAbility()
        {
            return StatBlockInfo.CHA;
        }

        public override List<int> GetSpellsPerLevel(int ClassLevel)
        {
            //Bonus Spells handled in elsewhere
            List<int> Levels = new List<int>();
            Levels.Add(0); //no 0 level spells

            switch (ClassLevel)
            {
                case 4:
                    Levels.Add(0); //1st Cha bonus only
                    break;
                case 5:
                case 6:
                    Levels.Add(1); //1st
                    break;
                case 7:
                    Levels.Add(1); //1st
                    Levels.Add(0); //2nd Cha bonus only
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
                    Levels.Add(0); //3rd Cha bonus only
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
                    Levels.Add(0); //4th Cha bonus only
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
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd 
                    Levels.Add(3); //4th
                    break;
            }
            return Levels;
        }

        private void SetFeatures()
        {
            Features.Add(new OnGoingSpecialAbility("AuraOfGood", 0, "Aura of Good",
               OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 1));

            Features.Add(new OnGoingSpecialAbility("DetectEvil", 0, "Detect Evil",
              OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities,
              OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            Features.Add(new OnGoingSpecialAbility("SmiteEvil", 0, "Smite Evil",
              OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities,
              OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            Features.Add(new OnGoingSpecialAbility("DivineGrace", 0, "Divine Grace",
             OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities,
             OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 2));

            Features.Add(new OnGoingSpecialAbility("LayOnHands", 0, "Lay On Hands",
             OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities,
             OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 2));

            Features.Add(new OnGoingSpecialAbility("AuraOfCourage", 0, "Aura of Courage",
             OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities,
             OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 3));

            Features.Add(new OnGoingSpecialAbility("DivineHealth", 0, "Divine Health",
             OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
             OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 3));

            Features.Add(new OnGoingSpecialAbility("Mercy", 0, "Mercy",
             OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities,
             OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 3));

            Features.Add(new OnGoingSpecialAbility("ChannelPositiveEnergy", 0, "Channel Positive Energy",
             OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities,
             OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 4));

            Features.Add(new OnGoingSpecialAbility("DivineBond", 0, "Divine Bond",
             OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities,
             OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 5));

            Features.Add(new OnGoingSpecialAbility("AuraOfResolve", 0, "Aura of Resolve",
             OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities,
             OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));

            Features.Add(new OnGoingSpecialAbility("AuraOfJustice", 0, "Aura of Justice",
             OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities,
             OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 11));

            Features.Add(new OnGoingSpecialAbility("AuraOfFaith", 0, "Aura of Faith",
             OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities,
             OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 14));

            Features.Add(new OnGoingSpecialAbility("AuraOfRighteousness", 0, "Aura of Righteousness",
             OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities,
             OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 17));

            Features.Add(new OnGoingSpecialAbility("HolyChampion", 0, "Holy Champion",
             OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities,
             OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 20));            
        }

        public override void ProcessPowers(int ClassLevel)
        {               
            foreach (OnGoingSpecialAbility Ability in Features)
            {
                if (Ability.StartLevel <= ClassLevel)
                {
                    ClassPowers.AddRange(ClassPowerReflectionWrapper.AddClassPower(Ability.Name));
                }
            }                  
        }
    }
}
