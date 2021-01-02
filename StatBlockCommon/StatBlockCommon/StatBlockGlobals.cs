using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using CommonStatBlockInfo;
using EquipmentBasic;
using StatBlockCommon.Spell_SB;
using StatBlockCommon.Individual_SB;
using Utilities;
using PathfinderGlobals;

namespace StatBlockCommon
{
    public static class StatBlockGlobals
    {      
        public static void UpdateForType(Type type, Object source, Object destination)
        {
            FieldInfo[] myObjectFields = type.GetFields(
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo fi in myObjectFields)
            {
                fi.SetValue(destination, fi.GetValue(source));
            }
        }       

        #region Enums

      public enum SpellBlockType
        {
            None = 0,
            SpellsPrepared,
            SpellsKnown,

        }

        #endregion

        #region Structs

        public struct BodySlots
        {
            public string Armor;
            public string Belts;
            public string Body;
            public string Chest;
            public string Eyes;
            public string Feet;
            public string Hands;
            public string Head;
            public string Headband;
            public string Neck;
            public string RingLeft;
            public string RingRight;
            public string Shield;
            public string Shoulders;
            public string Wrist;
        }

        public struct AC_Values
        {
            public int Base;
            public int Touch;
            public int FlatFooted;
            public Armor Armor;
            public Armor Shield;
        }

        public struct StatBlockModifier
        {
            public StatBlockInfo.ModifierTypes ModType;
            public int Duration; //rounds, 0 = ongoing
            public string Name;
            public int Modifier;
            public string ConditionGroup; //optional, if you have a +1 AC bonus vs undead, ConditionGroup = undead
        }

        public struct StatBlockModifierTotal
        {
            public string Name;
            public int Modifier;
            public string ModifierList;
        }

        public struct WeaponInfo
        {
            public string Name;
            public string[] AttackBonuses;
            public int NumAttacks;
            public string Damage;
            public int DamageMultipier;
            public string CriticalInfo;

            public WeaponInfo(string Info)
            {
                int Start = 0;
                string[] Attacks = null;

                string Hold = Info;
                if (Hold.Substring(0, 1) == "+")
                {
                    Start = 2;
                }
                else
                {
                    Start = 1;
                }
                if (Hold.Substring(0, 3) == "* +")
                {
                    Start = 4;
                }
                int Pos = Hold.IndexOf("+", Start);
                Name = Hold.Substring(0, Pos).Trim();
                Hold = Hold.Replace(Name, string.Empty).Trim();

                Pos = Hold.IndexOf(PathfinderConstants.SPACE);
                string Temp = Hold.Substring(0, Pos).Trim();
                AttackBonuses = Temp.Split('/');
                if (Temp.IndexOf("/") >= 0)
                {
                    Attacks = Temp.Split('/');
                    NumAttacks = Attacks.GetUpperBound(0) + 1;
                }
                else
                {
                    NumAttacks = 1;
                }
                Hold = Hold.Replace(Temp, string.Empty).Trim();

                Pos = Hold.IndexOf("/");
                //defaults 
                DamageMultipier = 2;
                CriticalInfo = "20";

                if (Pos == -1)
                {
                    //only damge 
                    Hold = Utility.RemoveParentheses(Hold);
                    Damage = Hold;
                }
                else
                {
                    Hold = Hold.Replace(PathfinderConstants.PAREN_LEFT, string.Empty).Trim();
                    Pos = Hold.IndexOf("/");
                    Damage = Hold.Substring(0, Pos).Trim();
                    Hold = Hold.Replace(Damage + "/", string.Empty).Trim();  //fix 1st only

                    Hold = Hold.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Trim();
                    Pos = Hold.IndexOf("x");
                    if (Pos >= 0)
                    {
                        Hold = Hold.Replace("x", string.Empty).Trim();
                        Pos = Hold.IndexOf("/");
                        if (Pos == -1)
                        {
                            DamageMultipier = Convert.ToInt32(Hold);
                            Pos = 1;
                        }
                        else
                        {
                            DamageMultipier = Convert.ToInt32(Hold.Substring(0, Pos).Trim());
                        }
                        Temp = Hold.Substring(0, Pos);
                        Hold = Hold.Replace(Temp, string.Empty).Trim();
                    }
                    else
                    {
                        CriticalInfo = Hold;
                    }
                }
                if (Pos >= 0)
                {
                    CriticalInfo = Hold;
                }
            }
        }

        public struct InitInfo
        {
            public string Name;
            public float Init;
            public int Order;
            public int InitMod;
        }

        public struct ActionInfo
        {
            public string ActionFrom;
            public string ActionTo;
            public StatBlockInfo.ActionTypes ActionType;
            public bool Touch;
            public string ActionEffect;
            public List<IndividualStatBlock_Combat> Targets;
            public WeaponInfo WeaponInfo;
            public SpellStatBlock_Combat SpellInfo;

            public void Reset()
            {
                ActionFrom = string.Empty;
                ActionTo = string.Empty;
                ActionType = StatBlockInfo.ActionTypes.None;
                Targets = null;
                WeaponInfo = new WeaponInfo();
                SpellInfo = null;
            }
        }


        //public struct OnGoingStatBlockEffect
        //{
        //    public string StatBlockField;
        //    public int Duration;  //in rounds 0=indefinite            
        //}

        public struct Ability
        {
            public StatBlockInfo.AbilityName Name;
            public int Score;
            public int ScoreTemp;
            public int AbilityModifier;
            public List<StatBlockModifier> Modifiers;

            public Ability(StatBlockInfo.AbilityName Name, int Score)
            {
                this.Name = Name;
                this.Score = Score;
                ScoreTemp = Score;
                AbilityModifier = 0;
                Modifiers = new List<StatBlockModifier>();
                AbilityModifier = StatBlockInfo.GetAbilityModifier(Score);
            }
        }

        #endregion       
    }
}
