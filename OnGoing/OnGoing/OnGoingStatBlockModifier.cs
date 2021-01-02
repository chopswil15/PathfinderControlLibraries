using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OnGoing
{
    public class OnGoingStatBlockModifier : IOnGoing
    {
        public int Duration { get; set; } //rounds, 0 = ongoing
        public OnGoingType OnGoingType
        {
            get { return OnGoingType.StatBlock; }
        }
        public StatBlockModifierTypes ModType { get; set; }
        public StatBlockModifierSubTypes SubType { get; set; }
        public string Name { get; set; }
        public int Modifier { get; set; }
        public string ConditionGroup { get; set; }
          //optional, if you have a +1 AC bonus vs undead, ConditionGroup = undead

        public OnGoingStatBlockModifier(int Duration, StatBlockModifierTypes ModType, StatBlockModifierSubTypes SubType,
                             string Name, int Modifier, string ConditionGroup)
        {
            this.Duration = Duration;
            this.ModType = ModType;
            this.SubType = SubType;
            this.Name = Name;
            this.Modifier = Modifier;
            this.ConditionGroup = ConditionGroup;
        }

        public enum StatBlockModifierTypes
        {
            None = 0,
            Ability = 1,
            Initiative ,
            AC ,
            SavingThrow ,
            CMB ,
            Speed ,
            Skill ,
            TemporaryHP,
            Attack,
            Damage ,
            Immune ,
            DR ,
            Resist ,
            DefensiveAbilities ,
            Senses ,
            NaturalAttack,
            NaturalDamage,
            SR
        }

        public enum StatBlockModifierSubTypes
        {
            None = 0,
            AC_Enhancement = 1,
            AC_Deflection ,
            AC_Natural ,
            AC_Dodge,
            AC_Dex ,
            AC_Shield ,
            AC_Size ,
            AC_Armor,
            AC_Wis,
            AC_Monk,
            Ability_Str,
            Ability_Dex ,
            Ability_Con ,
            Ability_Int , 
            Ability_Wis,
            Ability_Cha ,
            SavingThrow_Fort ,
            SavingThrow_Ref ,
            SavingThrow_Will  ,
            Override,
            Skill_Name,
            Skill_Ability_Cha,
            AC_Sacred,
            AC_Insight,
            Damage_Natural,
            AC_Luck,
            Attack_Luck,
            Resist_Acid,
            Resist_Cold,
            Resist_Electricity,
            Resist_Fire,
            Resist_Sonic
        }
    }
}
