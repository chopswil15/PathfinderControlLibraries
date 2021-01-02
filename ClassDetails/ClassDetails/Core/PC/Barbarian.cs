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
    public class Barbarian : ClassFoundation
    { 
        public Barbarian()
        {
            Name = "Barbarian";
            ClassAlignments = CommonMethods.GetNonLawfulAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d12;
            FortSaveType = StatBlockInfo.SaveBonusType.Good;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Poor;
            BABType = StatBlockInfo.BABType.Fast;
            ClassArchetypes = new List<string>() {"Armored Hulk", "Breaker", "Brutal Pugilist",  "Drunken Brute", "Elemental Kin", "Hurler",
                                                  "Invulnerable Rager", "Mounted Fury", "Savage Barbarian", "Scarred Rager", "Sea Reaver",
                                                  "Superstitious", "Titan Mauler", "Totem Warrior", "True Primitive", "Urban Barbarian", "Wild Rager",
                                                  "Pack Rager","Mad Dog","Unchained"};
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Martial;
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
            return new List<string> {StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_NATURE, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SURVIVAL,StatBlockInfo.SkillNames.SWIM };
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            int count = 0;
            if (Archetype == "Pack Rager")
            {
                if (ClassLevel >= 2) count++;
                if (ClassLevel >= 6) count++;
                if (ClassLevel >= 10) count++;
                if (ClassLevel >= 14) count++;
                if (ClassLevel >= 18) count++;
            }

            return count;
        }

        private void SetFeatures()
        {
            ClassFeature = new OnGoingSpecialAbility("FastMovement", 0, "Fast Movement",
                 OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
                 OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 1);            

            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("Rage", 0, "Rage",
               OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1);            

            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("UncannyDodge", 0, "Uncanny Dodge",
               OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 2);            

            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("TrapSense", 0, "Trap Sense",
               OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 2);
            
            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("ImprovedUncannyDodge", 0, "Improved Uncanny Dodge",
                OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 5);
            
            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("DamageReduction", 0, "Damage Reduction",
               OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 7);
            
            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("GreaterRage", 0, "Greater Rage",
               OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 11);
            
            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("IndomitableWill", 0, "Indomitable Will",
               OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 14);
            
            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("TirelessRage", 0, "Tireless Rage",
               OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 17);
            
            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("MightyRage", 0, "Mighty Rage",
               OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 20);
            
            Features.Add(ClassFeature);

        }

        //public void UncannyDodge(StatBlock.clsCombatStatBlock SB)
        //{
        //    SB.SetCondition(StatBlock.clsCombatStatBlock.ConditionTypes.FlatFooted, false);
        //}  
    }
}
