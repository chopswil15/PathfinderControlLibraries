using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using PathfinderGlobals;

namespace CommonStatBlockInfo
{
    public static class StatBlockInfo
    {
        public static class SkillNames
        {
            public static string ACROBATICS = "Acrobatics";
            public static string APPRAISE = "Appraise";
            public static string BLUFF = "Bluff";
            public static string CLIMB = "Climb";
            public static string CRAFT = "Craft";
            public static string DIPLOMACY = "Diplomacy";
            public static string DISABLE_DEVICE = "Disable Device";
            public static string DISGUISE = "Disguise";
            public static string ESCAPE_ARTIST = "Escape Artist";
            public static string FLY = "Fly";
            public static string HANDLE_ANIMAL = "Handle Animal";
            public static string HEAL = "Heal";
            public static string INTIMIDATE = "Intimidate";
            public static string KNOWLEDGE_ALL = "Knowledge (all)";
            public static string KNOWLEDGE_ONE = "Knowledge (1)";
            public static string KNOWLEDGE_ARCANA = "Knowledge (arcana)";
            public static string KNOWLEDGE_DUNGEONEERING = "Knowledge (dungeoneering)";
            public static string KNOWLEDGE_ENGINEERING = "Knowledge (engineering)";
            public static string KNOWLEDGE_GEOGRAPHY = "Knowledge (geography)";
            public static string KNOWLEDGE_HISTORY = "Knowledge (history)";
            public static string KNOWLEDGE_LOCAL = "Knowledge (local)";
            public static string KNOWLEDGE_NATURE = "Knowledge (nature)";
            public static string KNOWLEDGE_NOBILITY = "Knowledge (nobility)";
            public static string KNOWLEDGE_PLANES = "Knowledge (planes)";
            public static string KNOWLEDGE_RELIGION = "Knowledge (religion)";
            public static string LINGUISTICS = "Linguistics";
            public static string PERCEPTION = "Perception";
            public static string PERFORM = "Perform";
            public static string PROFESSION = "Profession";
            public static string RIDE = "Ride";
            public static string SENSE_MOTIVE = "Sense Motive";
            public static string SLEIGHT_OF_HAND = "Sleight of Hand";
            public static string SPELLCRAFT = "Spellcraft";
            public static string STEALTH = "Stealth";
            public static string SURVIVAL = "Survival";
            public static string SWIM = "Swim";
            public static string USE_MAGIC_DEVICE = "Use Magic Device";
        }      

        public const string STR = "Str";
        public const string INT = "Int";
        public const string WIS = "Wis";
        public const string DEX = "Dex";
        public const string CON = "Con";
        public const string CHA = "Cha";

        #region Enums

        [Flags]
        //update GetMetaMagicPowers() too
        public enum MetaMagicPowers
        {
            None = 0,
            empowered = 1 << 0,
            enlarged = 1 << 1,
            extended = 1 << 2,
            heightened = 1 << 3,
            maximized = 1 << 4,
            quickened = 1 << 5,
            silenced = 1 << 6,
            stilled = 1 << 7,
            widened = 1 << 8,
            silent = 1 << 9,
            corrupt = 1 << 10,
            merciful = 1 << 11,
            dazing = 1 << 12,
            intensified = 1 << 13,
            bouncing = 1 << 14,
            reach = 1 << 15,
            aquatic = 1 << 16
        }
       
        [Flags]
        public enum WeaponProficiencies
        {
            None = 0,
            Simple = 2,
            Martial = 4,
            Exotic = 8,
            Extra = 16,
            SimpleOne = 32
        }

        [Flags]
        public enum ArmorProficiencies
        {
            None = 0,
            Light = 2,
            Medium = 4,
            Heavy = 8
        }

        [Flags]
        public enum ShieldProficiencies
        {
            None = 0,
            Shield = 2,
            Tower = 4,
            Extra = 8
        }

        public enum HitDiceCategories
        {
            None = 0,
            d1 = 1, //for damage that only 1 hp and need HD, can write as 1d1
            d2 = 2,
            d3 = 3,
            d4 = 4,
            d6 = 6,
            d8 = 8,
            d10 = 10,
            d12 = 12
        }

        public enum SizeCategories
        {
            Fine = 1,
            Diminutive = 2,
            Tiny = 3,
            Small = 4,
            Medium = 5,
            Large = 6,
            Huge = 7,
            Gargantuan = 8,
            Colossal = 9
        }

        public enum AbilityName
        {
            Strength = 1,
            Dexterity = 2,
            Constitution = 3,
            Intelligence = 4,
            Wisdom = 5,
            Charisma = 6
        }

        public enum Health
        {
            Alive = 0,
            Disabled = 1,
            Dying = 2,
            Dead = 3
        }

        public enum ACType
        {
            Base = 0,
            Touch = 1,
            FlatFooted = 2
        }

        public enum ActionTypes
        {
            None = 0,
            Melee = 1,
            Ranged = 2,
            Spell = 3,
            Unarmed = 4,
            ActivateMagicItem = 5,
            CombatManeuver = 6
        }

        public enum CombatActionTypes
        {
            StandardAction = 1,
            MoveAction = 2,
            FullRoundAction = 3,
            FreeAction = 4,
            SwiftAction = 5,
            ImmediateAction = 6
        }

        public enum EffectModifierType
        {
            None = 0,
            ClassFeature = 1,
            Feat = 2,
            Condition = 3,
            Health = 4,
            MagicItem = 5,
            Spell = 6
        }

        public enum ModifierTypes
        {
            None = 0,
            Ability = 1,
            Initiative = 2,
            AC = 3,
            SavingThrow = 4,
            CMB = 5,
            Speed = 6,
            Skill = 7,
            TemporaryHP = 8,
            Attack = 9
        }

        public enum SaveBonusType
        {
            None = 0,
            Good = 1,
            Poor = 2,
            Varies = 3,
            PrestigePoor = 4,
            PrestigeGood = 5,
            AnimalCompanionGood = 6,
            AnimalCompanionPoor = 7
        }

        public enum BABType
        {
            Unknown = 0,
            Slow = 1,
            Medium = 2,
            Fast = 3,
            AnimalCompanion = 4,
            Mythic = 5,
            Eidolon  = 6
        }

        #endregion

        #region Structs

        public struct ACMods
        {
            public int Enhancement;
            public int Deflection;
            public int Sacred;
            public int Natural;
            public int Dodge;
            public int Dex;
            public int Shield;
            public int Size;
            public int Armor;
            public int Rage;
            public int Insight;
            public int Profane;
            public int Luck;
            public int Wis; //monk only
            public int Defending; // from wepons or armor that has defending Special Ability
            public int Monk; //monk class ac bonus
            public int BloodRage; //bloodrager class only

            public ACMods(string acModString)
            {                
                Enhancement = 0;
                Deflection = 0;
                Sacred = 0;
                Natural = 0;
                Dodge = 0;
                Dex = 0;
                Shield = 0;
                Size = 0;
                Armor = 0;
                Rage = 0;
                Insight = 0;
                Wis = 0;
                Profane = 0;
                Luck = 0;
                Defending = 0;
                Monk = 0;
                BloodRage = 0;

                if (acModString.Length == 0) return;

                int Pos = acModString.IndexOf(PathfinderConstants.PAREN_RIGHT);
                acModString = acModString.Substring(0, Pos).Trim();

                acModString = acModString.Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
                acModString = acModString.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                Pos = acModString.IndexOf("|");
                if (Pos != -1)
                {
                    acModString = acModString.Substring(0,Pos).Trim();
                }
                List<string> Mods = acModString.Split(',').ToList();
                string hold = string.Empty;

                foreach (string mod in Mods)
                {
                    try
                    {
                        ParseOneACMod(ref Pos, ref hold, mod);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ParseOneACMod: Issue with " + mod + " --" + ex.Message);
                    }
                }
            }

            private void ParseOneACMod(ref int Pos, ref string hold, string mod)
            {
                if (mod.Contains("["))
                {
                    int Pos3 = mod.IndexOf("[");
                    mod = mod.Substring(0, Pos3);
                }
                hold = mod;
                if (mod.Contains("enhancement"))
                {
                    hold = hold.Replace("enhancement", string.Empty).Trim();
                    Enhancement = Convert.ToInt32(hold);
                }
                if (mod.Contains("profane"))
                {
                    hold = hold.Replace("profane", string.Empty).Trim();
                    Profane = Convert.ToInt32(hold);
                }
                if (mod.Contains("deflection"))
                {
                    hold = hold.Replace("deflection", string.Empty).Trim();
                    if (hold.Contains(PathfinderConstants.SPACE))
                    {
                        hold = hold.Substring(0, hold.IndexOf(PathfinderConstants.SPACE));
                    }
                    Deflection = Convert.ToInt32(hold);
                }
                if (mod.Contains("natural armor"))
                {
                    hold = hold.Replace("natural armor", string.Empty).Trim();
                    Natural = Convert.ToInt32(hold);
                }
                if (mod.Contains("natural"))
                {
                    hold = hold.Replace("natural", string.Empty).Trim();
                    Natural = Convert.ToInt32(hold);
                }
                if (mod.Contains("sacred"))
                {
                    hold = hold.Replace("sacred", string.Empty).Trim();
                    Sacred = Convert.ToInt32(hold);
                }
                if (mod.Contains("luck"))
                {
                    hold = hold.Replace("luck", string.Empty).Trim();
                    Luck = Convert.ToInt32(hold);
                }
                if (mod.Contains("dodge"))
                {
                    hold = hold.Replace("dodge", string.Empty).Trim();
                    Dodge = Convert.ToInt32(hold);
                }
                if (mod.Contains(StatBlockInfo.DEX))
                {
                    hold = hold.Replace(StatBlockInfo.DEX, string.Empty).Trim();
                    Dex = Convert.ToInt32(hold);
                }
                if (mod.Contains("shield"))
                {
                    hold = hold.Replace("shield", string.Empty).Trim();
                    Shield = Convert.ToInt32(hold);
                }
                if (mod.Contains("size"))
                {
                    hold = hold.Replace("size", string.Empty).Trim();
                    Size = Convert.ToInt32(hold);
                }
                if (mod.Contains("mage armor"))
                {
                    hold = hold.Replace("mage armor", string.Empty).Trim();
                    Armor = Convert.ToInt32(hold);
                }
                if (mod.Contains("armor"))
                {
                    hold = hold.Replace("armor", string.Empty).Trim();
                    Armor = Convert.ToInt32(hold);
                }
                if (mod.Contains("bloodrage"))
                {
                    hold = hold.Replace("bloodrage", string.Empty).Trim();
                    BloodRage = Convert.ToInt32(hold);
                }
                if (mod.Contains("rage") && !mod.Contains("bloodrage"))
                {
                    hold = hold.Replace("rage", string.Empty).Trim();
                    Rage = Convert.ToInt32(hold);
                }
                if (mod.Contains("insight"))
                {
                    hold = hold.Replace("insight", string.Empty).Trim();
                    Insight = Convert.ToInt32(hold);
                }
                if (mod.Contains(StatBlockInfo.WIS))
                {
                    hold = hold.Replace(StatBlockInfo.WIS, string.Empty).Trim();
                    Wis = Convert.ToInt32(hold);
                }
                if (mod.Contains("defending"))
                {
                    hold = hold.Replace("defending", string.Empty).Trim();
                    Pos = hold.IndexOf(PathfinderConstants.SPACE);
                    hold = hold.Substring(0, Pos);
                    Defending = Convert.ToInt32(hold);
                }
                if (mod.Contains("monk"))
                {
                    hold = hold.Replace("monk", string.Empty).Trim();
                    Monk = Convert.ToInt32(hold);
                }                
            }

            public int ComputeAC()
            {
                return 10 + Enhancement + Deflection + Sacred + Natural + Dodge + Dex + Shield + Size + Armor + Rage + Wis + Insight + Profane + Luck + Defending + Monk + BloodRage;
            }

            public string ComputeACFormula()
            {
                StringBuilder SB = new StringBuilder(200);               
                SB.Append("10");

                if (Enhancement != 0) SB.Append(" +" + Enhancement.ToString() + " enhancement");
                if (Deflection != 0) SB.Append(" +" + Deflection.ToString() + " deflection");
                if (Sacred != 0) SB.Append(" +" + Sacred.ToString() + " sacred");
                if (Natural != 0)  SB.Append(CommonMethods.GetStringValue(Natural) + " natural");
                if (Dodge != 0) SB.Append(" +" + Dodge.ToString() + " dodge");
                if (Dex != 0) SB.Append(CommonMethods.GetStringValue(Dex)+ " Dex");
                if (Shield != 0) SB.Append(" +" + Shield.ToString() + " shield");
                if (Size != 0) SB.Append(CommonMethods.GetStringValue(Size) + " size");
                if (Armor != 0) SB.Append(" +" + Armor.ToString() + " armor");
                if (Rage != 0) SB.Append(" +" + Rage.ToString() + " rage");
                if (Insight != 0) SB.Append(" +" + Insight.ToString() + " insight");
                if (Profane != 0) SB.Append(" +" + Profane.ToString() + " profane");
                if (Luck != 0) SB.Append(" +" + Luck.ToString() + " luck");
                if (Defending != 0) SB.Append(" +" + Defending.ToString() + " defending");
                if (Monk != 0) SB.Append(" +" + Monk.ToString() + " monk");
                if (Wis != 0) SB.Append(" +" + Wis.ToString() + " wis");
                if (BloodRage != 0) SB.Append(" +" + BloodRage.ToString() + " bloodrage");
               
                return SB.ToString();
            }

            public int FindModValue(string Mod)
            {
                int modValue = -100;
                switch (Mod)
                {
                    case "enhancement":
                        modValue = Enhancement;
                        break;
                    case "deflection":
                        modValue = Deflection;
                        break;
                    case "sacred":
                        modValue = Sacred;
                        break;
                    case "Natural":
                        modValue = Natural;
                        break;
                    case "dodge":
                        modValue = Dodge;
                        break;
                    case StatBlockInfo.DEX:
                        modValue = Dex;
                        break;
                    case "shield":
                        modValue = Shield;
                        break;
                    case "size":
                        modValue = Size;
                        break;
                    case "armor":
                        modValue = Armor;
                        break;
                    case "rage":
                        modValue = Rage;
                        break;
                    case "insight":
                        modValue = Insight;
                        break;
                    case "profane":
                        modValue = Profane;
                        break;
                    case "luck":
                        modValue = Luck;
                        break;
                    case StatBlockInfo.WIS:
                        modValue = Wis;
                        break;
                    case "defending":
                        modValue = Defending;
                        break;
                    case "monk":
                        modValue = Monk;
                        break;
                    case "bloodrage":
                        modValue = BloodRage;
                        break;
                }

                return modValue;
            }

            public void AddModEffect(OnGoingStatBlockModifier Mod)
            {
                if (Mod.ModType == OnGoingStatBlockModifier.StatBlockModifierTypes.AC)
                {
                    switch (Mod.SubType)
                    {
                        case OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Armor:
                            Armor += Mod.Modifier;
                            break;
                        case OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Deflection:
                            Deflection += Mod.Modifier;
                            break;
                        case OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Dex:
                            Dex += Mod.Modifier;
                            break;
                        case OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Dodge:
                            Dodge += Mod.Modifier;
                            break;
                        case OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Enhancement:
                            Enhancement += Mod.Modifier;
                            break;
                        case OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Natural:
                            Natural += Mod.Modifier;
                            break;
                        case OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Shield:
                            Shield += Mod.Modifier;
                            break;
                        case OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Size:
                            Size += Mod.Modifier;
                            break;
                        case OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Wis:
                            Wis += Mod.Modifier;
                            break;
                        case OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Insight:
                            Insight += Mod.Modifier;
                            break;
                        case OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Sacred:
                            Sacred += Mod.Modifier;
                            break;
                        case OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Monk:
                            Monk += Mod.Modifier;
                            break;
                    }
                }
            }

            public override bool Equals(object obj)
            {
                if (obj is ACMods && obj != null)
                {  
                    ACMods temp = (ACMods)obj;
                    if (this.ComputeAC() != temp.ComputeAC()) return false; //quick check

                    if (temp.Enhancement == this.Enhancement && temp.Deflection == this.Deflection && temp.Natural == this.Natural &&
                        temp.Dodge == this.Dodge && temp.Dex == this.Dex && temp.Shield == this.Shield && temp.Size == this.Size
                        && temp.Armor == this.Armor && temp.Rage == this.Rage && temp.Wis == this.Wis && temp.Profane == this.Profane
                        && temp.Luck == this.Luck && temp.Defending == this.Defending 
                        && temp.Monk == this.Monk && this.Insight == temp.Insight && this.BloodRage == temp.BloodRage)
                    {
                        return true;
                    }
                }
                return false;
            }

            public override string ToString()
            {
                StringBuilder SB = new StringBuilder(200);                 

                if (Armor != 0)
                {
                    SB.Append(GetSign(Armor) + Armor.ToString() + " armor, ");
                }
                if (Deflection != 0)
                {
                    SB.Append(GetSign(Deflection) + Deflection.ToString() + " deflection, ");
                }
                if (Dex != 0)
                {
                    SB.Append(GetSign(Dex) + Dex.ToString() + " Dex, ");
                }
                if (Dodge != 0)
                {
                    SB.Append(GetSign(Dodge) + Dodge.ToString() + " dodge, ");
                }              
                if (Enhancement != 0)
                {
                    SB.Append(GetSign(Enhancement) + Enhancement.ToString() + " enhancement, ");
                }                
                if (Natural != 0)
                {
                    SB.Append(GetSign(Natural) + Natural.ToString() + " natural, ");
                }
                if (Rage != 0)
                {
                    SB.Append(GetSign(Rage) + Rage.ToString() + " rage, ");
                } 
                if (Shield != 0)
                {
                    SB.Append(GetSign(Shield) + Shield.ToString() + " shield, ");
                }
                if (Size != 0)
                {
                    SB.Append(GetSign(Size) + Size.ToString() + " size, ");
                }
                if (Wis != 0)
                {
                    SB.Append(GetSign(Wis) + Wis.ToString() + " Wis, ");
                }
                if (Profane != 0)
                {
                    SB.Append(GetSign(Profane) + Profane.ToString() + " profane, ");
                }
                if (Monk != 0)
                {
                    SB.Append(GetSign(Monk) + Monk.ToString() + " monk, ");
                }
                if (Luck != 0)
                {
                    SB.Append(GetSign(Luck) + Luck.ToString() + " luck, ");
                }
                if (Defending != 0)
                {
                    SB.Append(GetSign(Defending) + Defending.ToString() + " defending, ");
                }
                if (Insight != 0)
                {
                    SB.Append(GetSign(Insight) + Insight.ToString() + " insight, ");
                }
                if (BloodRage != 0)
                {
                    SB.Append(GetSign(BloodRage) + BloodRage.ToString() + " bloodRage, ");
                }

                string temp = SB.ToString();
                temp = temp.Trim();
                if (temp.Length > 1) temp = temp.Substring(0, temp.Length - 1); //remove last comma
                
                return temp;
            }

            private string GetSign(int Value)
            {
                if (Value > 0)
                {
                    return "+";
                }
                else
                {
                    return string.Empty;
                }
            }
            private string GetOppositeSign(int Value)
            {
                if (Value > 0)
                {
                    return "-";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public struct HDBlockInfo
        {
            public int Multiplier;
            public HitDiceCategories HDType;
            public int Modifier;
            public bool IsRacialHD;

            public void ParseHDBlock(string Block)
            {
                if (Block.Length == 0) return;
                if (Block == "—") return;
                if (Convert.ToInt32(Block[0]) == 8212) return;                
                if (Block == "entangle") return;
                if (Block == "1")
                {
                    Multiplier = 1;
                    HDType = HitDiceCategories.d1;
                    return;
                }

                Block = Block.Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
                Block = Block.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);

                if (Block.Contains(","))
                {
                    int Pos2 = Block.IndexOf(",");
                    Block = Block.Substring(Pos2 + 1).Trim();
                }

                int Pos = Block.IndexOf(";");
                if (Pos >= 0 && Block.IndexOf("HD") >= 0)
                {
                    Block = Block.Substring(0,Pos).Trim();
                    Block = Block.Replace("HD", string.Empty);
                    Multiplier = Convert.ToInt32(Block);
                    return;
                }

                Pos = Block.IndexOf(",");
                if (Pos >= 0)
                {
                    Block = Block.Substring(0, Pos).Trim();
                    Block = Block.Replace("HD", string.Empty);
                    Multiplier = Convert.ToInt32(Block);
                    return;
                }

                Pos = Block.IndexOf(PathfinderConstants.SPACE);
                if (Pos >= 0)
                {
                    Block = Block.Substring(0, Pos).Trim();
                }
                Pos = Block.IndexOf("/");
                if (Pos >= 0)
                {
                    Block = Block.Substring(0, Pos).Trim();
                }

                try
                {
                    Pos = Block.IndexOf("d");
                    Multiplier = Convert.ToInt32(Block.Substring(0, Pos));
                    Block = Block.Substring(Pos);
                    Pos = Block.IndexOf("+");
                    if (Pos == -1)
                    {
                        Pos = Block.IndexOf("-");
                    }
                    if (Pos == -1)
                    {
                        Modifier = 0;
                    }
                    else
                    {
                        Block = Block.Replace(";", string.Empty);
                        Modifier = Convert.ToInt32(Block.Substring(Pos));
                        Block = Block.Substring(0, Pos).Trim();
                    }
                    Block = Block.Replace("d", string.Empty);
                }
                catch
                {
                    Block = "0";
                }
                HDType = (HitDiceCategories) Convert.ToInt32(Block);
            }

            public override bool Equals(object obj)
            {
                if (obj is HDBlockInfo && obj == null) return false;                
                HDBlockInfo temp = (HDBlockInfo)obj;
                return (temp.Multiplier == Multiplier && temp.HDType == HDType && temp.Modifier == Modifier);
            }

            public override string ToString()
            {
                string hdTypeString = HDType == HitDiceCategories.None ? " No HD Type found " : HDType.ToString();
                string temp = Multiplier.ToString() + hdTypeString;
                if (Modifier != 0)
                {
                    if (Modifier > 0)
                    {
                        temp = temp + "+" + Modifier.ToString();
                    }
                    else
                    {
                        temp = temp + Modifier.ToString();
                    }
                }
                return temp;
            }
        }

       
        #endregion

        #region Methods

        public static HDBlockInfo GetSpecialClassUnarmedDamage(int ClassLevel, SizeCategories ClassSize)
        {
            HDBlockInfo UnarmedDamage = new HDBlockInfo();
            string temp = string.Empty;

            switch (ClassSize)
            {
                case SizeCategories.Small:
                    if (ClassLevel <= 3) temp = "1d4";
                    else if (ClassLevel <= 7) temp = "1d6";
                    else if (ClassLevel <= 11) temp = "1d8";
                    else if (ClassLevel <= 15) temp = "1d10";
                    else if (ClassLevel <= 19) temp = "2d6";
                    else temp = "2d8";
                    break;
                case SizeCategories.Medium:
                    if (ClassLevel <= 3) temp = "1d6";
                    else if (ClassLevel <= 7) temp = "1d8";
                    else if (ClassLevel <= 11) temp = "1d10";
                    else if (ClassLevel <= 15) temp = "2d6";
                    else if (ClassLevel <= 19) temp = "2d8";
                    else temp = "2d10";
                    break;
                case SizeCategories.Large:
                    if (ClassLevel <= 3) temp = "1d8";
                    else if (ClassLevel <= 7) temp = "2d6";
                    else if (ClassLevel <= 11) temp = "2d8";
                    else if (ClassLevel <= 15) temp = "3d6";
                    else if (ClassLevel <= 19) temp = "3d8";
                    else temp = "4d8";
                    break;
            }

            UnarmedDamage.ParseHDBlock(temp);
            return UnarmedDamage;
        }       

        public static int ComputeAnimalCompanionHD(int Level)
        {
            int diff = Level - 3;
            int mod;
            if (diff < 0)
            {
                mod = 1;
            }
            else
            {
                mod = (diff / 4) * -1;
            }

            return Level + mod;
        }

        public static int AnimalCompanionNaturalArmorBonus(int Level)
        {
            if (Level <= 2) return 0;
            return (Level / 3) * 2;
        }

        public static int ComputeBAB(int Level, BABType BAB)
        {            
            switch (BAB)
            {
                case BABType.Fast:
                    return Level;
                case BABType.Medium:
                    if (Level <= 0) return 0;
                    int mod = (Level - 1) / 4;
                    return Level - (mod + 1);
                case BABType.Slow:
                    return (Level / 2);
                case BABType.AnimalCompanion:
                    int animalHD = ComputeAnimalCompanionHD(Level);
                    return (ComputeBAB(animalHD, BABType.Medium));
                case BABType.Mythic:
                    return 0;
                case BABType.Eidolon:
                    return ComputeEidolonBAB(Level);
                case BABType.Unknown:
                    return -100;
            }
            return -100;
        }

        public static int ComputeEidolonHD(int Level)
        {
            return ComputeEidolonBAB(Level);
        }

        public static int ComputeEidolonBAB(int Level)
        {
            int diff = Level - 3;
            int mod;
            if (diff <= 0)
            {
                mod = 0;
            }
            else
            {
                mod = ((Level / 4) * -1);

            }

            return Level + mod;
        }

        public static int ParseSaveBonues(int level, SaveBonusType SB)
        {
            int mod_base = 0;
            int hold = 0;

            switch (SB)
            {
                case SaveBonusType.Good:
                    if (level % 2 == 0)
                    {
                        mod_base = 2;
                    }
                    else
                    {
                        mod_base = 1;
                    }
                    hold = level - (level / 2) + mod_base;
                    break;
                case SaveBonusType.Poor:
                    hold = level / 3;
                    break;
                case SaveBonusType.PrestigeGood:
                    if (level > 2)
                    {
                        level -= 3;
                        if (level % 2 == 0)
                        {
                            mod_base = 2;
                        }
                        else
                        {
                            mod_base = 1;
                        }
                    }
                    hold = level - (level / 2) + mod_base;
                    break;
                case SaveBonusType.PrestigePoor:
                    hold = (level + 1) / 3;
                    break;
                case SaveBonusType.AnimalCompanionGood:
                    //alternate between PrestigeGood and PrestigePoor 3,2,3,3,2,3,3 pattern
                    switch (level)
                    {
                        case 1:
                        case 2:
                        case 3:
                            hold = 3;
                            break;
                        case 4:
                        case 5:
                            hold = 4;
                            break;
                        case 6:
                        case 7:
                        case 8:
                            hold = 5;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            hold = 6;
                            break;
                        case 12:
                        case 13:
                            hold = 7;
                            break;
                        case 14:
                        case 15:
                        case 16:
                            hold = 8;
                            break;
                        case 17:
                        case 18:
                        case 19:
                            hold = 9;
                            break;
                        case 20:
                            hold = 10;
                            break;
                    }

                    break;
                case SaveBonusType.AnimalCompanionPoor:
                    hold = (level + 2) / 4;
                    break;
            }
            return hold;
        }

        public static SaveBonusType ComputeSaveBonusType(int HD, int SaveModScore, int BaseSaveValue)
        {            
            int diff = BaseSaveValue - GetAbilityModifier(SaveModScore);
            int Good = ParseSaveBonues(HD, SaveBonusType.Good);
            int Poor = ParseSaveBonues(HD, SaveBonusType.Poor);

            if (diff == Poor)
            {
                return SaveBonusType.Poor;
            }
            return SaveBonusType.Good;
        }

        public static BABType ComputeBABType(int HD, int BABValue)
        {
            int Fast = ComputeBAB(HD, BABType.Fast);
            int Medium = ComputeBAB(HD, BABType.Medium);
            int Slow = ComputeBAB(HD, BABType.Slow);
            
            if (BABValue == Fast)
            {
                return BABType.Fast;
            }
            if (BABValue == Medium)
            {
                return BABType.Medium;
            }
            return BABType.Slow;
        }

        public static string ComputeCRForNonRacialHD_Classes(string classes, out string formula)
        {
            List<string> Classes = classes.Split('/').ToList();
            List<string> NPCClasses = CommonMethods.GetNPCClasses();
            string temp = string.Empty;
            string find = string.Empty;
            int totalClassLevels = 0;
            int CR = 0;            
            int Pos =0;
            bool PCClassExists = false;
            formula = string.Empty;

            foreach (string one_class in Classes)
            {
                temp = one_class.Trim();
                Pos = temp.LastIndexOf(PathfinderConstants.SPACE);
                if (Pos >= 0)
                {
                    temp = temp.Substring(Pos);
                }
                try
                {
                    formula += one_class + PathfinderConstants.SPACE;
                    totalClassLevels += Convert.ToInt32(temp);
                }
                catch (Exception ex)
                {
                    throw new Exception("ComputeCRForNonRacialHDClasses: Level Parse Issue - " + temp + PathfinderConstants.SPACE + ex.Message);
                }
                temp = one_class.Replace(temp, string.Empty).Trim();
                find = string.Empty;
                if (!(NPCClasses.Exists(item => item.ToLower() == temp)))
                {
                    PCClassExists = true;
                }
                
            }

            if (PCClassExists)
            {
                formula += " Base Classes -1";
                CR = totalClassLevels - 1;
            }
            else //only NPC Classes
            {
                formula += " NPC Classes only -2";
                CR = totalClassLevels - 2;                
            }

            if (CR == 0)
            {
                temp = "1/2";
            }
            else if (CR == -1)
            {
                temp = "1/3";
            }
            else
            {
                temp = CR.ToString();
            }

            return temp;
        }

        public static int GetXP(string CR)
        {
            int XP = -1;
            switch (CR)
            {
                case "1/8":
                    XP = 50;
                    break;
                case "1/6":
                    XP = 65;
                    break;
                case "1/4":
                    XP = 100;
                    break;
                case "1/3":
                    XP = 135;
                    break;
                case "1/2":
                case "½":
                    XP = 200;
                    break;
                case "-":
                    XP = 0;
                    break;
                default:
                    try
                    {
                        int CRValue = Convert.ToInt32(CR);

                        if (CRValue % 2 == 0)
                        {
                            XP = Convert.ToInt32(Math.Pow(2, (CRValue / 2 - 1)) * 600);
                        }
                        else
                        {
                            XP = Convert.ToInt32(Math.Pow(2, ((CRValue - 1) / 2)) * 400);
                        }
                    }
                    catch
                    {

                    }
                    break;
            }
            return XP;
        }

        public static int GetMythicFeats(int MythicValue)
        {
            if (MythicValue <= 0) return 0;

            int count = 0;

            if (MythicValue >= 1) count++;
            if (MythicValue >= 3) count++;
            if (MythicValue >= 5) count++;
            if (MythicValue >= 7) count++;
            if (MythicValue >= 9) count++;

            return count;
        }

        public static int GetHDFeats(int HD)
        {
            return (HD % 2 == 0) ? HD / 2 : (HD / 2) + 1;           
        }

        public static int Parse_d8HD(string HD)
        {
            HD = HD.Replace(PathfinderConstants.PAREN_LEFT, string.Empty).Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
            int Pos = HD.IndexOf(";");
            if (Pos >= 0)
            {
                HD = HD.Substring(Pos + 1).Trim();//6d6+2d8+18
            }

            int Pos2 = HD.IndexOf("+");
            Pos = HD.IndexOf("d8", Pos2);
            while(Pos >= 0)
            {
               HD = HD.Substring(Pos2 + 1);
               Pos2 = HD.IndexOf("+");
               Pos = HD.IndexOf("d8", Pos2);
            }
            Pos = HD.IndexOf("d8");
            if (Pos >= 0)
            {
                return Convert.ToInt32(HD.Substring(0, Pos));
                
            }
            return 0;
        }

        public static int ParseHD(string HD)
        {
            HD = HD.Replace(PathfinderConstants.PAREN_LEFT, string.Empty).Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
            int Pos = HD.IndexOf(";");
            if (Pos >= 0)
            {
                HD = HD.Substring(0,Pos);
                HD = HD.Replace("HD", string.Empty);
                return Convert.ToInt32(HD);
            }

            List<string> HDs = HD.Split('+').ToList();
            string temp;
            int Count = 0;

            foreach (string hd in HDs)
            {
                if (hd.IndexOf("d") >= 0)
                {
                    temp = hd.Substring(0,hd.IndexOf("d"));
                    Count += Convert.ToInt32(temp);
                }
            }
            return Count;
        }

        public static int GetAbilityModifier(int score)
        {
            if (score == 0) return 0;
            int mod_base;

            if (score % 2 == 0)
            {
                mod_base = 10;
            }
            else
            {
                mod_base = 11;
            }

            return (score - mod_base) / 2;
        }

        public static string ChangeAC_Mod(string OrigMods, string FindMod, int Value, bool Add)
        {
            // if Add == false then Replace
            
            int Pos;
            string NewMods = OrigMods;

            if (NewMods.IndexOf(FindMod) >= 0)
            {
                Pos = NewMods.IndexOf(FindMod);
                int Pos2 = NewMods.LastIndexOf(",", Pos);
                if (Pos2 != -1)
                {
                    string temp2 = NewMods.Substring(Pos2, Pos - Pos2) + FindMod;
                    if (Add)
                    {
                        string temp = temp2.Replace(",", string.Empty).Trim();
                        Pos = temp.IndexOf(PathfinderConstants.SPACE);
                        int OldValue = Convert.ToInt32(temp.Substring(0, Pos));
                        Value += OldValue;
                    }
                    NewMods = NewMods.Replace(temp2, ", +" + Value.ToString() + PathfinderConstants.SPACE + FindMod);
                }
                else //1st mod
                {
                    string temp2 = NewMods.Substring(0, Pos) + FindMod;
                    if (Add)
                    {
                        string temp = temp2.Replace(PathfinderConstants.PAREN_LEFT, string.Empty).Trim();
                        Pos = temp.IndexOf(PathfinderConstants.SPACE);
                        int OldValue = Convert.ToInt32(temp.Substring(0, Pos));
                        Value += OldValue;
                    }
                    NewMods = NewMods.Replace(temp2, ", +" + Value.ToString() + PathfinderConstants.SPACE + FindMod);
                }
            }
            else
            {
                if (NewMods.Length == 0)  //no mods at all
                {
                    NewMods = "(+" + Value.ToString() + PathfinderConstants.SPACE + FindMod + PathfinderConstants.PAREN_RIGHT;
                }
                else  //mods, but no FindMod value
                {
                    Pos = NewMods.IndexOf(PathfinderConstants.PAREN_RIGHT);
                    NewMods = NewMods.Insert(Pos, ", +" + Value.ToString() + PathfinderConstants.SPACE + FindMod);
                }
            }

            return NewMods;
        }

        public static string AddFeat(string OrigFeats, string NewFeat)
        {
            if (OrigFeats.Length == 0) return NewFeat;
            if (OrigFeats.IndexOf(NewFeat) >= 0) return OrigFeats;

            return OrigFeats += ", " + NewFeat;
        }

        public static string AddRacialMod(string OrigRacialMods, string NewRacialMod)
        {
            if (OrigRacialMods.Length == 0) return NewRacialMod;
            if (OrigRacialMods.IndexOf(NewRacialMod) >= 0) return OrigRacialMods;

            return OrigRacialMods += ", " + NewRacialMod;
        }

        public static List<int> GetSpellBonus(int score, bool IsInquisitor)
        {
            List<int> SB = new List<int>();
            int mult;
            SB.Add(0); //0 level, which never gets a bonus

            //if (IsInquisitor) return SB; //no bonus spells for Inquisitor

            for (int a = 1; a <= 9; a++)
            {
                mult = (a - 1) * 2;
                if (score >= 12 + mult) 
                {
                    SB.Add(((score - (12 + mult)) / 8) + 1);
                }
            }           

            return SB;
        }

        public static int GetSizeModifier(SizeCategories Size)
        {
            switch (Size)
            {
                case SizeCategories.Colossal:
                    return -8;
                case SizeCategories.Gargantuan:
                    return -4;
                case SizeCategories.Huge:
                    return -2;
                case SizeCategories.Large:
                    return -1;
                case SizeCategories.Medium:
                    return 0;
                case SizeCategories.Small:
                    return 1;
                case SizeCategories.Tiny:
                    return 2;
                case SizeCategories.Diminutive:
                    return 4;
                case SizeCategories.Fine:
                    return 8;
            }

            return -100;
        }

        public static int GetSizeModifier(string Size)
        {
            return GetSizeModifier(GetSizeEnum(Size));
        }

        public static SizeCategories GetSizeEnum(string Size)
        {
            return (SizeCategories)Enum.Parse(typeof(SizeCategories), Size);
        }

        public static string GetSizeFromEnum(SizeCategories Size)
        {
            return Size.ToString();
        }

        public static SizeCategories ReduceSize(string Size)
        {
            int SizeValue = Convert.ToInt32(GetSizeEnum(Size));
            return (SizeCategories)(SizeValue - 1);
        }

        public static SizeCategories ReduceSize(SizeCategories Size)
        {
            int SizeValue = Convert.ToInt32(Size);
            return (SizeCategories)(SizeValue - 1);
        }

        public static SizeCategories IncreaseSize(string Size)
        {
            int SizeValue = Convert.ToInt32(GetSizeEnum(Size));
            return (SizeCategories)(SizeValue + 1);
        }

        public static SizeCategories IncreaseSize(SizeCategories Size)
        {
            int SizeValue = Convert.ToInt32(Size);
            return (SizeCategories)(SizeValue + 1);
        }

        private static int GetSizeDiffFromMedium(SizeCategories Size)
        {
            int SizeValue = Convert.ToInt32(Size);
            int MediumValue = Convert.ToInt32(SizeCategories.Medium);

            return SizeValue - MediumValue;
        }

        public static int GetSizeDifference(SizeCategories StartSize,SizeCategories EndSize)
        {
            int StartValue = Convert.ToInt32(StartSize);
            int EndValue = Convert.ToInt32(EndSize);

            return StartValue - EndValue;
        }

        public static string ChangeWeaponDamageSize(string MediumDamage, SizeCategories NewSize)
        {
            if (MediumDamage == "—") return MediumDamage;

            switch (NewSize)
            {
                case SizeCategories.Large:
                    switch (MediumDamage)
                    {
                        case "1d2":
                            return "1d3";
                        case "1d3":
                            return "1d4";
                        case "1d4":
                            return "1d6";
                        case "1d6":
                            return "1d8";
                        case "1d8":
                            return "2d6";
                        case "1d10":
                            return "2d8";
                        case "1d12":
                            return "3d6";
                        case "2d4":
                            return "2d6";
                        case "2d6":
                            return "3d6";
                        case "2d8":
                            return "3d8";
                        case "2d10":
                            return "4d8";
                    }
                    break;
                case SizeCategories.Tiny:
                    switch (MediumDamage)
                    {
                        case "1d2":
                            return "-";
                        case "1d3":
                            return "1";
                        case "1d4":
                            return "1d2";
                        case "1d6":
                            return "1d3";
                        case "1d8":
                            return "1d4";
                        case "1d10":
                            return "1d6";
                        case "1d12":
                            return "1d8";
                        case "2d4":
                            return "1d4";
                        case "2d6":
                            return "1d8";
                        case "2d8":
                            return "1d10";
                        case "2d10":
                            return "2d6";
                    }
                    break;
                default:
                    List<string> nond10 = new List<string> { "1d2", "1d3", "1d4", "1d6", "1d8", "2d6", "3d6", "4d6", "6d6", "8d6", "12d6" };
                    List<string> d10 = new List<string> { "1d10", "2d8", "3d8", "4d8", "6d8", "8d8", "12d8" };
                    int diff = GetSizeDiffFromMedium(NewSize);
                    int index;
                    if (nond10.Contains(MediumDamage))
                    {
                        index = nond10.IndexOf(MediumDamage);
                        return nond10[index + diff];
                    }
                    else
                    {
                        index = d10.IndexOf(MediumDamage);
                        return d10[index + diff];
                    }

            }

            throw new Exception("Size not supported: " + NewSize.ToString());
        }


        #endregion

    }
}
