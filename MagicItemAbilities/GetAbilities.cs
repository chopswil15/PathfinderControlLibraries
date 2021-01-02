using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MagicItemAbilityWrapper;
using OnGoing;
using CommonStatBlockInfo;


namespace MagicItemAbilities
{
    public class GetAbilities
    {
        //to be called via reflection
        //all methods are magic item names
        // plus or minus values in names should be paramater

        private static MagicItemAbilitiesWrapper ReturnEmptyWrapper()
        {
            return new MagicItemAbilitiesWrapper();
        }

        #region Armor

        public MagicItemAbilitiesWrapper LionsShield()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;            
            wrapper.EquimentBaseString = "+2 heavy steel shield";
            //OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
            //                                       OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Shield
            //                                      , "L AC Mod", armor., string.Empty);

            //wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper ImprovedFireResistanceBreastplate()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Resist,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Resist_Fire
                                                  , "Improved Fire Resistance Breastplate", 20, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        #endregion Armor

        #region Rod

        public MagicItemAbilitiesWrapper ElementalMetamagicRod()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper ReachMetamagicRod()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper RodOfIce()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper RodOfMetalAndMineralDetection()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper RodOfSplendor()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper RodOfWonder()
        {
            return ReturnEmptyWrapper();
        }

        #endregion Rod

        #region Rings

        public MagicItemAbilitiesWrapper MajorRingOfColdResistance()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Resist,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Resist_Cold
                                                  , "Major Ring Of Cold Resistance", 20, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper MajorRingOfElectricityResistance()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Resist,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Resist_Electricity
                                                  , "Major Ring Of Electricity Resistance", 20, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper MajorRingOfFireResistance()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Resist,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Resist_Fire
                                                  , "Major Ring Of Fire Resistance", 20, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper MinorRingOfColdResistance()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Resist,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Resist_Cold
                                                  , "Minor Ring Of Cold Resistance", 10, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper RingOfArcaneSignets()
        {
            return ReturnEmptyWrapper();
        }

        public  MagicItemAbilitiesWrapper RingOfCounterspells()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper RingOfEvasion()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper RingOfFeatherFalling()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper RingOfFerociousAction()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper RingOfFreedomOfMovement()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper RingOfForcefangs()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper RingOfForceShield()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Shield
                                                  , "Ring of Force Shield AC Mod", 2, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper RingOfInvisibility()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper RingOfManiacalDevices()
        {
            return ReturnEmptyWrapper();
        }


        public MagicItemAbilitiesWrapper RingOfMindShielding()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper RingOfProtection(int Bonus)
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Deflection
                                                  , "Ring of Protection +" + Bonus + " AC Mod", Bonus, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper RingOfRegeneration()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper RingOfResistance(int Bonus)
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.None
                                                  , "+ " + Bonus + " Ring of Resistance Save Mod", Bonus, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }


        public MagicItemAbilitiesWrapper RingOfSpellTurning()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper RingOfSplendidSecurity()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper RingOfSwimming()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Skill,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Skill_Name
                                                  , "Ring of Swimming +5 Skill Mod", 5, StatBlockInfo.SkillNames.SWIM);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper RingOfTheRam()
        {
            return ReturnEmptyWrapper();
        }

        #endregion Rings

        #region Staff

        public MagicItemAbilitiesWrapper StaffOfDivination()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper StaffOfEvocation()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper StaffOfFire()
        {
            return ReturnEmptyWrapper();
        }

        #endregion Staff

        #region WondrousItems

        #region A

        public MagicItemAbilitiesWrapper AegisOfRecovery()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper AlluringGoldenApple()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper ArmbandsOfTheBrawler()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper AmuletOfBulletProtection(int Bonus)
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper AmuletOfNaturalArmor(int Bonus)
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Natural
                                                  , "Amulet of Natural Armor +" + Bonus + " AC Mod", Bonus, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper AmuletOfMightyFists(int Bonus)
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.NaturalAttack,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Natural
                                                  , "Amulet of Mighty Fists +" + Bonus + " attack Mod", Bonus, string.Empty);
            wrapper.AddOnGoingStatBlockModifier(SBMod);
            SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.NaturalDamage,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Damage_Natural
                                                  , "Amulet of Mighty Fists +" + Bonus + " damage Mod", Bonus, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper AmuletOfProofAgainstDetectionAndLocation()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper ArrowMagnet()
        {
            return ReturnEmptyWrapper();
        }

        #endregion A

        #region B

        public MagicItemAbilitiesWrapper BandagesOfRapidRecovery()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper BeadOfForce()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper BeltOfGiantStrength(int Bonus)
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Str
                                                  , "Belt of Giant Strength +" + Bonus + " Ability Mod", Bonus, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper BeltOfIncredibleDexterity(int Bonus)
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Dex
                                                  , "Belt of Incredible Dexterity +" + Bonus + " Ability Mod", Bonus, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper BeltOfMightyConstitution(int Bonus)
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Con
                                                  , "Belt of Mighty Constitution +" + Bonus + " Ability Mod", Bonus, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper BeltOfPhysicalMight(int Bonus, string Abilities)
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;

            List<string> PossibleAbilities = new List<string> { "Strength", "Dexterity", "Constitution" };
            OnGoingStatBlockModifier SBMod = null;
            OnGoingStatBlockModifier.StatBlockModifierSubTypes subType = OnGoingStatBlockModifier.StatBlockModifierSubTypes.None;

            foreach (string ability in PossibleAbilities)
            {
                switch (ability)
                {
                    case "Strength":
                    case StatBlockInfo.STR:
                        subType = OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Str;
                        break;
                    case "Dexterity":
                    case StatBlockInfo.DEX:
                        subType = OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Dex;
                        break;
                    case "Constitution":
                    case StatBlockInfo.CON:
                        subType = OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Con;
                        break;
                }
                if (Abilities.Contains(ability))
                {
                    SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability, subType
                                                   , "Belt of Physical Might +" + Bonus + " Ability Mod-" + ability, Bonus, string.Empty);

                    wrapper.AddOnGoingStatBlockModifier(SBMod);
                }
            }
            return wrapper;
        }

        public MagicItemAbilitiesWrapper BeltOfPhysicalPerfection(int Bonus)
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Con
                                                  , "Belt of Physical Perfection +" + Bonus + " Ability Mod", Bonus, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);

            wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Str
                                                  , "Belt of Physical Perfection +" + Bonus + " Ability Mod", Bonus, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);

            wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Dex
                                                  , "Belt of Physical Perfection +" + Bonus + " Ability Mod", Bonus, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);


            return wrapper;
        }

        public MagicItemAbilitiesWrapper BeltOfTumbling()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper BeltOfThunderousCharging()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Str
                                                  , "Belt of Thunderous Charging +2 Str Ability Mod", 2, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);

            return wrapper;
        }

        public MagicItemAbilitiesWrapper BoneRazor()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper BootsOfElvenkind()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Skill,
                                                  OnGoingStatBlockModifier.StatBlockModifierSubTypes.Skill_Name
                                                 , "+5 boots of elvenkind Skill Mod", 5, StatBlockInfo.SkillNames.ACROBATICS);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper BootsOfEscape()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper BootsOfGusto()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper BootsOfSpeed()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper BootsOfStridingAndSpringing()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper BootsOfTeleportation()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper BootsOfTheCat()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper BootsOfTheWinterlands()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper BottledScream()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper BraceletOfFriends()
        {
            return ReturnEmptyWrapper();
        }


        public MagicItemAbilitiesWrapper BracersOfArmor(int Bonus)
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Armor
                                                  , "Bracers of Armor +" +  Bonus + " AC Mod", Bonus, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper LesserBracersOfArchery()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Attack,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.None
                                                  , "Lesser Bracers of Archery +1 attack mod", 1, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper BroochOfShielding()
        {
            return ReturnEmptyWrapper();
        }

        #endregion B

        #region C

        public MagicItemAbilitiesWrapper CampfireBead()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper CircletOfPersuasion()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Skill,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Skill_Ability_Cha
                                                  , "+3 Circlet Of Persuasion Skill Mod", 3, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper CloakOfElvenkind()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Skill,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Skill_Name
                                                  , "+5 Cloak of Elvenkind Skill Mod", 5, StatBlockInfo.SkillNames.STEALTH);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper CloakOfEtherealness()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper CloakOfResistance(int Bonus)
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.None
                                                  , "+ " + Bonus + " Cloak of Resistance Save Mod", Bonus, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper CloakOfFangs()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.None
                                                  , "Cloak of Fangs Save Mod", 1, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper ConcealingPocket()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper CrownOfFangs()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper CrownOfInfernalMajesty()
        {
            return ReturnEmptyWrapper();
        }        

        public MagicItemAbilitiesWrapper CrystalBall()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper CubeOfForce()
        {
            return ReturnEmptyWrapper();
        }

        #endregion C

        #region D

        public MagicItemAbilitiesWrapper DeathwatchEyes()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper DiabolusBell()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper DustOfDisappearance()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper DustOfTracelessness()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper DustOfIllusion()
        {
            return ReturnEmptyWrapper();
        }

        #endregion D

        #region E

        public MagicItemAbilitiesWrapper EyesOfCharming()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper EyesOfEmbersight()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper EyesOfTheDamned()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper EyesOfTheEagle()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Skill,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Skill_Name
                                                  , "+5 Eyes Of The Eagle Skill Mod", 5, StatBlockInfo.SkillNames.PERCEPTION);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }


        public MagicItemAbilitiesWrapper ElementalGem()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper ElixirOfHiding()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Skill,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Skill_Name
                                                  , "+10 Elixir Of Hiding Skill Mod", 10, StatBlockInfo.SkillNames.STEALTH);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper ElixirOfSwimming()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper ElixirOfTruth()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper ElixirOfTumbling()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper ElixirOfVision()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper EndlessBandolier()
        {
            return ReturnEmptyWrapper();
        }

        #endregion E

        #region F

        public MagicItemAbilitiesWrapper FeatherStepSlippers()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper FungalSlippers()
        {
            return ReturnEmptyWrapper();
        }

        #endregion F

        #region G

        public MagicItemAbilitiesWrapper GetawayBoots()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper GlovesOfArcaneStriking()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper GloveOfStoring()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper GlovesOfSwimmingAndClimbing()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Skill,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Skill_Name
                                                  , "+5 Gloves Of Swimming And Climbing Skill Mod", 5, StatBlockInfo.SkillNames.SWIM);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Skill,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Skill_Name
                                                  , "+5 Gloves Of Swimming And Climbing Skill Mod", 5, StatBlockInfo.SkillNames.CLIMB);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper GolembaneScarab()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper GogglesOfMinuteSeeing()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Skill,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Skill_Name
                                                  , "+5 Goggles of Minute Seeing Skill Mod", 5, StatBlockInfo.SkillNames.DISABLE_DEVICE);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper GraveSalt()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper GrimLantern()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper GunfightersPoncho()
        {
            return ReturnEmptyWrapper();
        }

        #endregion G

        #region H

        public MagicItemAbilitiesWrapper HandOfTheMage()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper HandyHaversack()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper HatOfDisguise()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Skill,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Skill_Name
                                                  , "+10 Hat of Disguise Skill Mod", 10, StatBlockInfo.SkillNames.DISGUISE);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper HeadbandOfAlluringCharisma(int Bonus)
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Cha
                                                  , "Headband of Alluring Charisma +" + Bonus + " Ability Mod", Bonus, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }
                                        
        public MagicItemAbilitiesWrapper HeadbandOfMentalProwess(int Bonus, string Abilities)
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;

            List<string> PossibleAbilities = new List<string> { "Intelligence", "Wisdom", "Charisma" };
            OnGoingStatBlockModifier SBMod = null;
            OnGoingStatBlockModifier.StatBlockModifierSubTypes subType = OnGoingStatBlockModifier.StatBlockModifierSubTypes.None;

            foreach (string ability in PossibleAbilities)
            {
                switch (ability)
                {
                    case "Intelligence":
                        subType = OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Int;
                        break;
                    case "Wisdom":
                        subType = OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Wis;
                        break;
                    case "Charisma":
                        subType = OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Cha;
                        break;
                }
                if (Abilities.Contains(ability))
                {
                    SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability, subType
                                                          , "Headband of Mental Prowess +" + Bonus + " Ability Mod-" + ability, Bonus, string.Empty);
                    wrapper.AddOnGoingStatBlockModifier(SBMod);
                }
            }

            
            return wrapper;
        }

        public MagicItemAbilitiesWrapper HeadbandOfMentalSuperiority(int Bonus)
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Wis
                                                  , "Headband of Mental Superiority +" + Bonus + " Ability Mod", Bonus, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);

            SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                                                  OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Int
                                                 , "Headband of Mental Superiority +" + Bonus + " Ability Mod", Bonus, string.Empty);
            wrapper.AddOnGoingStatBlockModifier(SBMod);

            SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                                                 OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Cha
                                                , "Headband of Mental Superiority +" + Bonus + " Ability Mod", Bonus, string.Empty);
            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper HeadbandOfInspiredWisdom(int Bonus)
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Wis
                                                  , "Headband of Inspired Wisdom +" + Bonus + " Ability Mod", Bonus, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper HeadbandOfVastIntelligence(int Bonus)
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Int
                                                  , "Headband of Vast Intelligence +" + Bonus + " Ability Mod", Bonus, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper HelmOfTelepathy()
        {
            return ReturnEmptyWrapper();
        }

        #endregion H

        #region I

        public MagicItemAbilitiesWrapper IounTorch()
        {
            return ReturnEmptyWrapper();
        }

        #endregion I

        #region K

        public MagicItemAbilitiesWrapper KeyOfLockJamming()
        {
            return ReturnEmptyWrapper(); 
        }

        #endregion K

        #region L

        public MagicItemAbilitiesWrapper LifeLinkBadge()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper LongarmBracers()
        {
            return ReturnEmptyWrapper();
        }

        #endregion L

        #region M

        public MagicItemAbilitiesWrapper ManaclesOfCooperation()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper MaskOfTheMantis()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper MaskOfTheSkull()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper MindSentinelMedallion()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper MnemonicVestment()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper MonksRobe()
        {
            return ReturnEmptyWrapper();
        }

        #endregion M

        #region N

        public MagicItemAbilitiesWrapper NecklaceOfAdaptation()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper NecklaceOfStolenBreath()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper Nightdrops()
        {
            return ReturnEmptyWrapper();
        }

        #endregion N

        #region O

        public MagicItemAbilitiesWrapper OilOfSilence()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper OrigamiSwarm()
        {
            return ReturnEmptyWrapper();
        }

        #endregion O

        #region P

        public MagicItemAbilitiesWrapper PhylacteryOfFaithfulness()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper PhylacteryOfNegativeChanneling()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper PlumeOfPanache()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper PortableHole()
        {
            return ReturnEmptyWrapper();
        }

        #endregion P

        #region Q

        public MagicItemAbilitiesWrapper QuickChangeMask()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper QuickRunnersShirt()
        {
            return ReturnEmptyWrapper();
        }

        #endregion Q

        #region R

        public MagicItemAbilitiesWrapper RobeOfBones()
        {
            return ReturnEmptyWrapper();
        }


        public MagicItemAbilitiesWrapper RobeOfNeedles()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper RobeOfScintillatingColors()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper RobeOfUsefulItems()
        {
            return ReturnEmptyWrapper();
        }

        #endregion R

        #region S

        public MagicItemAbilitiesWrapper SalveOfSlipperiness()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper ScabbardOfHoning()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper ScarabOfProtection()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper SharkToothAmulet()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper ShieldCloak()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Shield
                                                  , "+1 Shield Cloak AC Mod", 1,string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper ShoesOfTheFirewalker()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Resist,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Resist_Fire
                                                  , "Shoes Of The Firewalker Resist Mod", 10, "");

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper Silversheen()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper SleevesOfManyGarments()
        {
            return ReturnEmptyWrapper();
        }


        public MagicItemAbilitiesWrapper SlippersOfSpiderClimbing()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Skill,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Skill_Name
                                                  , "+8 Slippers Of Spider Climbing Skill Mod", 8, StatBlockInfo.SkillNames.CLIMB);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper SpireTransportToken()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper SpellguardBracers()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper StalkersMask()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Skill,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Skill_Name
                                                  , "+5 Stalker's Mask Stealth Skill Mod", 5, StatBlockInfo.SkillNames.STEALTH);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper Stormlure()
        {
            return ReturnEmptyWrapper();
        }


        public MagicItemAbilitiesWrapper SwarmbaneClasp()
        {
            return ReturnEmptyWrapper();
        }

        #endregion S

        #region T

        public MagicItemAbilitiesWrapper TentacleCloak()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper ThirdEye()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper TravelersAnyTool()
        {
            return ReturnEmptyWrapper();
        }

        #endregion T

        #region U

        public MagicItemAbilitiesWrapper UnfetteredShirt()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper UnguentOfTimelessness()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper UniversalSolvent()
        {
            return ReturnEmptyWrapper();
        }

        #endregion U

        #region W

        public MagicItemAbilitiesWrapper WarPaintOfTheTerribleVisage()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper Wayfinder()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper WingedBoots()
        {
            return ReturnEmptyWrapper();
        }

        #endregion W

        #endregion WondrousItems


        #region IounStones

        public MagicItemAbilitiesWrapper AmberSpindleIounStone()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.None
                                                  , "Amber Spindle Ioun Stone Save Mod", 1, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);

            //SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow,
            //                                      OnGoingStatBlockModifier.StatBlockModifierSubTypes.SavingThrow_Ref
            //                                     , "Amber Spindle Ioun Stone Ref Mod", 1, string.Empty);

            //wrapper.AddOnGoingStatBlockModifier(SBMod);

            //SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow,
            //                                      OnGoingStatBlockModifier.StatBlockModifierSubTypes.SavingThrow_Will
            //                                     , "Amber Spindle Ioun Stone Will Mod", 1, string.Empty);

            //wrapper.AddOnGoingStatBlockModifier(SBMod);

            return wrapper;
        }

        public MagicItemAbilitiesWrapper DeepRedSphereIounStone()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Dex
                                                  , "Deep Red Sphere Ioun Stone Dex Mod", 2, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper DustyRosePrismIounStone()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Insight
                                                  , "Dusty Rose Prism Ioun Stone AC Mod", 1,string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper IridescentSpindleIounStone()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper PaleBlueRhomboidIounStone()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Str
                                                  , "Pale Blue Rhomboid Ioun Stone Str Mod", 2, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper PaleGreenPrismIounStone()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            int mod = 1;
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;         

            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Attack,
               OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                    "Pale Green Prism Ioun Stone Attack Mod", mod, string.Empty);
            wrapper.AddOnGoingStatBlockModifier(SBMod);

            SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                     "Pale Green Prism Ioun Stone Save Mod", mod, string.Empty);
            wrapper.AddOnGoingStatBlockModifier(SBMod);

            SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Skill,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                     "Pale Green Prism Ioun Stone Skill Mod", mod, string.Empty);
           

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper PinkAndGreenSphereIounStone()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Cha
                                                  , "Pink and Green Sphere Ioun Stone Cha Mod", 2, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }


        public MagicItemAbilitiesWrapper PinkRhomboidIounStone()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Con
                                                  , "Pink Rhomboid Ioun Stone Con Mod", 2, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper ScarletAndBlueSphereIounStone()
        {
            MagicItemAbilitiesWrapper wrapper = new MagicItemAbilitiesWrapper();
            wrapper.Activation = MagicItemAbilitiesWrapper.MagicItemAbilityActivation.Constant;
            OnGoingStatBlockModifier SBMod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                                                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Int
                                                  , "Scarlet And Blue Sphere Ioun Stone Int Mod", 2, string.Empty);

            wrapper.AddOnGoingStatBlockModifier(SBMod);
            return wrapper;
        }

        public MagicItemAbilitiesWrapper TourmalineSphereIounStone()
        {
            return ReturnEmptyWrapper();
        }

        public MagicItemAbilitiesWrapper VibrantPurplePrismIounStone()
        {
            return ReturnEmptyWrapper();
        }


        #endregion IounStones


        #region Cursed

        public MagicItemAbilitiesWrapper FlaskOfCurses()
        {
            return ReturnEmptyWrapper();
        }

        #endregion Cursed
    }
}
