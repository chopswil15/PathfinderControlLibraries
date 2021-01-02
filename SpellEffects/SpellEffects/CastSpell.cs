using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon;
using Dice;
using OnGoing;
using StatBlockCommon.Individual_SB;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace SpellEffects
{
    public class CastSpell
    {
        //these methods should be called via reflection
        //these methods are for applying the spell's effects, i.e the spell is going to effect them in some way
        //all targets have been checked for immunity, failed their saves, failed their spell resistance, etc.
        //*****
        //Make sure Dice.dll & OnGoing.dll are in same folder as SpellEffects.dll
        //******

        public struct OnGoingGameEffect
        {
            public string Name;
            public int Duration; //in rounds 0=indefinite
            //either Damage or DamgeStr is populated
            public int Damage;  //fixed damage 
            public string DamageStr; // damage is re-computed, i.e 2d6
        }

        private int GetTenMinutesPerLevel(int CasterLevel)
        {
            return CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE * 10;
        }

        #region A Spells


        public void Acid_Arrow(int CasterLevel, IndividualStatBlock_Combat target)
        {
            if (CasterLevel > 18)
            {
                CasterLevel = 18;
            }

            int duration = 1 + (CasterLevel / 3);
            int Damage = -1 * DiceRoll.RollComplexDice("2d4");
            target.TakeDamage(Damage);

            int temp = CasterLevel / 3;
            if (temp > 0)
            {
                OnGoingDamage OnGoingDamage = new OnGoingDamage("Acid Arrow Damage", duration, "2d4");
                target.AddOnGoingDamage(OnGoingDamage);
            }
        }

        public void Acid_Fog(int CasterLevel, OnGoingGameEffect GameEffect)
        {            
            GameEffect.Name = "Acid Fog";
            GameEffect.DamageStr = "2d6";
            GameEffect.Duration = CasterLevel;
        }

        public void Acid_Splash(IndividualStatBlock_Combat target)
        {
            int Damage = -1 * DiceRoll.RollComplexDice("1d3");
            target.TakeDamage(Damage);
        }

        public void Aid(int CasterLevel, IndividualStatBlock_Combat target)
        {
            if (CasterLevel > 10)
            {
                CasterLevel = 10;
            }
            int tempHP = DiceRoll.RollDice(8) + CasterLevel;
            int duration = CasterLevel * 10;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.TemporaryHP,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                     "Aid- Tempory HP", tempHP, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);

            StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Attack,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                   "Aid- Attack", 1, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);

            StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                  "Aid- save vs fear", 1, "fear");
            StatBlockMod.Name = "Aid- save vs fear";
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Air_Walk(int CasterLevel, IndividualStatBlock_Combat target)       {        }

        public void Age_Resistance_Lesser(int CasterLevel, IndividualStatBlock_Combat target)       {        }

        public void Alarm(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {        }

        public void Alchemical_Allocation(int CasterLevel, IndividualStatBlock_Combat target) { }

        public void Alter_Self(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE * 10;

        }

        public void Amplify_Stench(int CasterLevel, IndividualStatBlock_Combat target)       {        }

        public void Ant_Haul(int CasterLevel, IndividualStatBlock_Combat target)       {        }

        public void Animal_Growth(int CasterLevel, IndividualStatBlock_Combat target)       {        }

        public void Animate_Dead(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {        }

        public void Anticipate_Peril(int CasterLevel, IndividualStatBlock_Combat target)       {        }

        public void Antilife_Shell(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {        }

        public void Arcane_Eye(int CasterLevel) { }
        
        public void Arcane_Sight(int CasterLevel, IndividualStatBlock_Combat target)       {        }

        public void Arcane_Sight_Greater(int CasterLevel, IndividualStatBlock_Combat target)        {        }

        public void Archons_Aura(int CasterLevel, IndividualStatBlock_Combat target) { }

        public void Aspect_Of_The_Bear(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
               OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Natural,
                    "Aspect of the Bear- AC Mod", 2, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Aspect_Of_The_Falcon(int CasterLevel, IndividualStatBlock_Combat target) { }

        public void Aura_Of_Doom(int CasterLevel) { }

        #endregion A Spells

        #region B Spells

        public void Barkskin(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE * 10;
            if (CasterLevel > 12) CasterLevel = 12; //max effects 12th level
            int mod = 2;
            CasterLevel -= 3;
            mod += CasterLevel / 3;

             OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Natural,
                     "Barkskin- AC Mod", mod, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Bears_Endurance(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
               OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Con,
                "Bear's Endurance- Con Bonus", 4, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod); 
        }

        public void Beastspeak(int CasterLevel, IndividualStatBlock_Combat target) { }


        public void Bestow_Curse(IndividualStatBlock_Combat target)      {       }

        public void Bless(int CasterLevel, List<IndividualStatBlock_Combat> targets)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;

        }

        public void Blessing_Of_Fervor(int CasterLevel, List<IndividualStatBlock_Combat> targets)      {        }

        public void Bless_Weapon(int CasterLevel)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;

        }

        public void Blink(int CasterLevel, IndividualStatBlock_Combat target)       {        }

        public void Blur(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;

        }

        public void Blurred_Movement(int CasterLevel, IndividualStatBlock_Combat target) { }

        public void Bombers_Eye(int CasterLevel, IndividualStatBlock_Combat target)
        {

        }

        public void Bouncy_Body(int CasterLevel, IndividualStatBlock_Combat target)
        {

        }

        public void Bulls_Strength(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
               OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Str,
                "Bull's Strength- Str Bonus", 4, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        #endregion B Spells

        #region C Spells

        public void Call_Lightning(int CasterLevel, IndividualStatBlock_Combat target)       {      }

        public void Call_Lightning_Storm(int CasterLevel, IndividualStatBlock_Combat target)      {      }

        public void Cats_Grace(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Dex,
                 "Cat's Grace- Dex Bonus", 4, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);    
        }

        public void Cause_Fear(int CasterLevel, IndividualStatBlock_Combat target)       {      }

        public void Charm_Monster(int CasterLevel, IndividualStatBlock_Combat target)       {      }
        public void Charm_Monster_Mass(int CasterLevel, List<IndividualStatBlock_Combat> targets) { }

        public void Charm_Person(int CasterLevel, IndividualStatBlock_Combat target)       {       }

        public void Clairaudience_Clairvoyance(int CasterLevel)       {                 }

        public void Cloak_Of_Chaos(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {        }

        public void Color_Spray(List<IndividualStatBlock_Combat> targets)
        {
            int HD;
            OnGoingCondition Condition;

            foreach (IndividualStatBlock_Combat target in targets)
            {
                HD = target.GetHD();

                if (HD <= 2)
                {
                    int Rounds = DiceRoll.RollComplexDice("2d4");
                    int Rounds2nd = DiceRoll.RollDice(4);

                    if (target.IsUndead() == false)
                    {
                        Condition = new OnGoingCondition(ConditionTypes.Unconscious, Rounds);                        
                        target.AddOnGoingCondition(Condition);
                    }

                    Condition = new OnGoingCondition(ConditionTypes.Blinded, Rounds + Rounds2nd);
                    target.AddOnGoingCondition(Condition);

                    Condition = new OnGoingCondition(ConditionTypes.Stunned, Rounds + Rounds2nd + 1);
                    target.AddOnGoingCondition(Condition);
                }

                if (HD == 3 | HD == 4)
                {
                    int Rounds = DiceRoll.RollDice(4);

                    Condition = new OnGoingCondition(ConditionTypes.Blinded, Rounds);
                    target.AddOnGoingCondition(Condition);

                    Condition = new OnGoingCondition(ConditionTypes.Stunned, Rounds + 1);
                    target.AddOnGoingCondition(Condition);
                }

                if (HD >= 5)
                {
                    Condition = new OnGoingCondition(ConditionTypes.Stunned, 1);
                    target.AddOnGoingCondition(Condition);
                }
            }
        }

        public void Command_Undead(int CasterLevel, IndividualStatBlock_Combat target) { }

        public void Comprehend_Languages(int CasterLevel, IndividualStatBlock_Combat target) { }

        public void Cone_of_Cold(int CasterLevel, List<IndividualStatBlock_Combat> targets)
        {
            if (CasterLevel > 15) //max 15 damage
            {
                CasterLevel = 15;
            }

            int Damage = -1 * DiceRoll.RollComplexDice(CasterLevel.ToString() + "d6");

            foreach (IndividualStatBlock_Combat target in targets)
            {
                if (target.SaveModifer != 0)
                {
                    target.TakeDamage(Damage);
                }
                else
                {
                    int temp = (int)(Damage * target.SaveModifer);
                    target.TakeDamage(temp);
                    target.SaveModifer = 0;
                }
            }
        }

        public void Contagion(int CasterLevel, IndividualStatBlock_Combat target) { }

        public void Contingency(int CasterLevel, IndividualStatBlock_Combat target)      {        }

        public void Continual_Flame(int CasterLevel) { }

        public void Control_Weather(int CasterLevel)       {        }

        public void Countless_Eyes(int CasterLevel, IndividualStatBlock_Combat target)      {        }

        public void Create_Food_and_Water(int CasterLevel)       {        }

        public void Cure_Moderate_Wounds(int CasterLevel, IndividualStatBlock_Combat target)
        {
            if (CasterLevel > 10) //max 10
            {
                CasterLevel = 10;
            }

            int Healing = DiceRoll.RollComplexDice("2d8+" + CasterLevel.ToString());

            target.ReceiveHealing(Healing);
        }

        #endregion C Spells

        #region D Spells

        public void Dancing_Lights(int CasterLevel)      {       }

        public void Darkness(int CasterLevel, IndividualStatBlock_Combat target)       {        }

        public void Darkvision(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_HOUR;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Senses,
              OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
               "Darkvision- Senses", 0, "darkvision 60 ft.");
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Death_Knell(int CasterLevel, IndividualStatBlock_Combat target)       {        }

        public void Death_Ward(int CasterLevel, IndividualStatBlock_Combat target) { }

        public void Deathwatch(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {        }

        public void Delay_Poison(int CasterLevel, IndividualStatBlock_Combat target)      {       }

        public void Discern_Lies(int CasterLevel, List<IndividualStatBlock_Combat> targets)      {       }

        public void Deeper_Darkness(int CasterLevel)      {       }

        public void Defensive_Shock(int CasterLevel, IndividualStatBlock_Combat target)       {       }

        public void Defile_Armor(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int mod = CasterLevel / 4;
            if (mod > 5) mod = 5;

            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
             OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Enhancement,
              "Defile Armor - AC mod", mod, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Delayed_Blast_Fireball(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {        }

        public void Desecrate(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {        }

        public void Detect_Chaos(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {        }

        public void Detect_Evil(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {        }

        public void Detect_Good(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {        }

        public void Detect_Poison(int CasterLevel)       {        }

        public void Detect_Law(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {        }

        public void Detect_Magic(int CasterLevel, List<IndividualStatBlock_Combat> targets)        {        }

        public void Detect_Secret_Doors(int CasterLevel)        {        }

        public void Detect_Snares_And_Pits(int CasterLevel)      {        }

        public void Detect_Scrying(IndividualStatBlock_Combat target)       {        }

        public void Detect_Thoughts(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {        }

        public void Detect_Undead(int CasterLevel)       {        }

        public void Dimensional_Anchor(int CasterLevel, List<IndividualStatBlock_Combat> targets) { }

        public void Dimension_Door(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {       }

        public void Disguise_Self(int CasterLevel, IndividualStatBlock_Combat target)       {        }

        public void Dispel_Magic(int CasterLevel, IndividualStatBlock_Combat target) { }

        public void Dispel_Magic_Greater(int CasterLevel, IndividualStatBlock_Combat target) { }

        public void Displacement(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel; //1 round/level

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.DefensiveAbilities,
               OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                "Displacement- Defensive Abilities", 0, "displacement (50% miss chance)");
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }


        public void Divine_Favor(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = PathfinderConstants.ROUNDS_PER_MINUTE;
            int bonus = 1; //min
            if(CasterLevel >=4) bonus++;
            if (CasterLevel >= 7) bonus++; //+3 max

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Attack,
             OnGoingStatBlockModifier.StatBlockModifierSubTypes.Attack_Luck,
              "Divine Favor- Attack Mod", bonus, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);

            StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Damage,
             OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
              "Divine Favor- Damage Bonus", bonus, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Divine_Power(int CasterLevel, IndividualStatBlock_Combat target)
        {

        }

        public void Dominate_Person(int CasterLevel, IndividualStatBlock_Combat target)
        {

        }

        public void Dream(IndividualStatBlock_Combat target)
        {

        }

        #endregion D Spells

        #region E Spells

        public void Eagles_Splendor(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Cha,
                 "Eagle's Splendor- Cha Bonus", 4, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Echolocation(int CasterLevel)       {       }

        public void Effortless_Armor(int CasterLevel, IndividualStatBlock_Combat target)       {       }

        public void Endure_Elements(int CasterLevel, IndividualStatBlock_Combat target)      {       }

        public void Enervation(int CasterLevel, List<IndividualStatBlock_Combat> targets)      {       }

        public void Enlarge_Person(int CasterLevel, IndividualStatBlock_Combat target)      {        }

        public void Enter_Image(int CasterLevel, IndividualStatBlock_Combat target)      {        }

        public void Entropic_Shield(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;
        }

        public void Expeditious_Retreat(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;
        }


        public void Evolution_Surge(int CasterLevel, IndividualStatBlock_Combat target)       {        }

        public void Evolution_Surge_Greater(int CasterLevel, IndividualStatBlock_Combat target) { }


        #endregion E Spells

        #region F Spells

        public void False_Life(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_HOUR;
            if (CasterLevel > 10) CasterLevel = 10;
            int mod = DiceRoll.RollDice(10) + CasterLevel;            

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.TemporaryHP,
               OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                "False Life- Temp HP", mod, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void False_Life_Greater(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_HOUR;
            if (CasterLevel > 20) CasterLevel = 20;
            int mod = DiceRoll.RollDice(10) + DiceRoll.RollDice(10) + DiceRoll.RollDice(10) + CasterLevel;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.TemporaryHP,
               OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                "False Life, Greater- Temp HP", mod, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Fear(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {        }

        public void Feather_Fall(int CasterLevel, List<IndividualStatBlock_Combat> targets) { }   

        public void Feather_Step(int CasterLevel, IndividualStatBlock_Combat target)      {        }

        public void Fickle_Winds(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {        }

        public void Finger_Of_Death(int CasterLevel, IndividualStatBlock_Combat target) { }

        public void Fireball(int CasterLevel, List<IndividualStatBlock_Combat> targets)        {        }

        public void Fire_Shield(int CasterLevel, IndividualStatBlock_Combat target)        {        }

        public void Flame_Arrow(int CasterLevel)       {        }

        public void Flames_Of_The_Faithful(int CasterLevel)       {        }
        public void Flame_Strike(int CasterLevel, List<IndividualStatBlock_Combat> targets) { }

        public string Flare(IndividualStatBlock_Combat target)
        {
            if (target.CheckCondition(ConditionTypes.Dazzled))
            {
                return target.name + " already Dazzled";
            }
            else
            {
                OnGoingCondition Condition = new OnGoingCondition(ConditionTypes.Dazzled, PathfinderConstants.ROUNDS_PER_MINUTE);
                target.AddOnGoingCondition(Condition);
                return target.name + " Dazzled for " + PathfinderConstants.ROUNDS_PER_MINUTE.ToString() + " rounds.";
            }
        }

        public void Fly(int CasterLevel, IndividualStatBlock_Combat target)      {      }

        public void Freedom_Of_Movement(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = GetTenMinutesPerLevel(CasterLevel);
        }

        public void Foresight(int CasterLevel,IndividualStatBlock_Combat target)
        {
            int duration = GetTenMinutesPerLevel(CasterLevel);
            OnGoingStatBlockModifier  StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow,
                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.SavingThrow_Ref,
                        "Foresight- Save Mod", 2, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);

            StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
                 OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Insight,
                      "Foresight- AC Mod", 2, string.Empty);

            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Foxs_Cunning(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Int,
                 "Fox's Cunning- Int Bonus", 4, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }


        #endregion F Spells

        #region G Spells

        public void Gaseous_Form(int CasterLevel, IndividualStatBlock_Combat target) { }

        public void Getaway(int CasterLevel, List<IndividualStatBlock_Combat> targets)     {       }

        public void Gentle_Repose()     {      }

        public void Ghost_Sound(int CasterLevel)      {       }

        public void Glibness(int CasterLevel, IndividualStatBlock_Combat target)     {       }

        public void Globe_Of_Invulnerability_Lesser(int CasterLevel, List<IndividualStatBlock_Combat> targets)     {        }

        public void Good_Hope(int CasterLevel, List<IndividualStatBlock_Combat> targets)      {        }

        public void Gravity_Bow(int CasterLevel, IndividualStatBlock_Combat target)      {       }

        #endregion G Spells

        #region H Spells

        public void Hallucinatory_Terrain(int CasterLevel, List<IndividualStatBlock_Combat> targets)     {        }

        public void Harm(int CasterLevel, IndividualStatBlock_Combat target)    {      }

        public void Haste(int CasterLevel, List<IndividualStatBlock_Combat> targets)
        {
            int duration = CasterLevel; //1 round/level
            OnGoingStatBlockModifier StatBlockMod = null;

            foreach (IndividualStatBlock_Combat target in targets)
            {
                StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Attack,
                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                        "Haste- Attack Mod", 1, string.Empty);
                target.AddOnGoingStatBlockMod(StatBlockMod);

                StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow,
                    OnGoingStatBlockModifier.StatBlockModifierSubTypes.SavingThrow_Ref,
                         "Haste- Save Mod", 1, string.Empty);
                target.AddOnGoingStatBlockMod(StatBlockMod);

                StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Dodge,
                        "Haste- AC Mod", 1, string.Empty);
                target.AddOnGoingStatBlockMod(StatBlockMod);
            }
        }

        public void Heal(int CasterLevel, IndividualStatBlock_Combat target)    {      }

        public void Heroism(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE * 10;
            int mod = 2;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Attack,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                     "Heroism- Attack Mod", mod, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);

            StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                     "Heroism- Save Mod", mod, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);

            StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Skill,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                     "Heroism- Skill Mod", mod, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Heroism_Greater(int CasterLevel, IndividualStatBlock_Combat target)     {       }

        public void Heroes_Feast()     {      }

        public void Hide_From_Animals(int CasterLevel, List<IndividualStatBlock_Combat> targets)     {       }

        public void Hide_From_Undead(int CasterLevel, List<IndividualStatBlock_Combat> targets)    {       }

        public void Hold_Monster(int CasterLevel, IndividualStatBlock_Combat target) { }

        public void Holy_Smite(int CasterLevel, List<IndividualStatBlock_Combat> targets)    {       }

        #endregion H Spells

        #region I Spells

        public void Ice_Storm(int CasterLevel, List<IndividualStatBlock_Combat> targets) { }

        public void Icicle_Dagger(int CasterLevel)        {       }

        public void Illusion_Of_Calm(int CasterLevel, IndividualStatBlock_Combat target)      {        }

        public void Inflict_Serious_Wounds(IndividualStatBlock_Combat target)       {        }

        public void Innocence(int CasterLevel, IndividualStatBlock_Combat target)       {        }

        public void Instant_Armor(int CasterLevel, IndividualStatBlock_Combat target)       {        }

        public void Invisibility(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;
            OnGoingCondition Condition = new OnGoingCondition(ConditionTypes.Invisible, duration);
            //target.AddOnGoingCondition(Condition);
        }

        public void Invisibility_Greater(int CasterLevel, IndividualStatBlock_Combat target)      {        }

        public void Invisibility_Purge(int CasterLevel, IndividualStatBlock_Combat target)       {        }

        public void Invisibility_Sphere(int CasterLevel)       {        }

        public void Ironskin(int CasterLevel, IndividualStatBlock_Combat target)       {        }

        #endregion I Spells

        #region J Spells

        public void Jump(int CasterLevel, IndividualStatBlock_Combat target)
        {

        }

        #endregion J Spells

        #region K Spells

        public void Keen_Edge(int CasterLevel, IndividualStatBlock_Combat target)
        {

        }

        public void Keen_Senses(int CasterLevel, IndividualStatBlock_Combat target)
        {

        }

        public void Know_Direction(int CasterLevel, IndividualStatBlock_Combat target)
        {

        }
        

        #endregion K Spells

        #region L Spells

        public void Lead_Blades(int CasterLevel, IndividualStatBlock_Combat target)      {      }

        public void Levitate(int CasterLevel, IndividualStatBlock_Combat target)    {      }

        public void Life_Pact(int CasterLevel, IndividualStatBlock_Combat target) { }

        public void Light(int CasterLevel)      {              }

        public void Limited_Wish()       {        }

        public void Locate_Object(int CasterLevel)       {        }

        public void Long_Arm(int CasterLevel, IndividualStatBlock_Combat target) { }

        public void Longstrider(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_HOUR;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Speed,
               OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                    "Longstrider- Speed Mod", 10, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        #endregion L Spells

        #region M Spells

        public void Mage_Hand(int CasterLevel) { }

        public void Mages_Faithful_Hound(int CasterLevel)      {       }

        public void Magic_Aura(int CasterLevel, IndividualStatBlock_Combat target)      {       }

        public void Magic_Stone() { }

        public void Magic_Circle_Against_Evil(int CasterLevel, List<IndividualStatBlock_Combat> targets)      {      }

        public void Magic_Circle_Against_Good(int CasterLevel, List<IndividualStatBlock_Combat> targets)     {     }

        public void Magic_Fang(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.NaturalAttack,
               OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                "Magic Fang- Attack Bonus", 1, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);

            StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.NaturalDamage,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                 "Magic Fang- Damage Bonus", 1, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Magic_Fang_Greater(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_HOUR;
            int mod = CasterLevel / 4;
            if (mod > 5) mod = 5;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.NaturalAttack,
               OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                "Greater Magic Fang- Attack Bonus", mod, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);

            StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.NaturalDamage,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                 "Greater Magic Fang- Damage Bonus", mod, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Magic_Mouth(int CasterLevel, IndividualStatBlock_Combat target)     {      }

        public void Magic_Weapon(int CasterLevel, IndividualStatBlock_Combat target)
        {           
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Attack,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                 "Magic Weapon- Attack Bonus", 1, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);

            StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Damage,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                 "Magic Weapon- Damage Bonus", 1, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Magic_Weapon_Greater(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int mod = CasterLevel / 4;
            if (mod > 5) mod = 5;
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_HOUR;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Attack,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                 "Greater Magic Weapon- Attack Bonus", mod, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);

            StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Damage,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                 "Greater Magic Weapon- Damage Bonus", mod, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Mage_Armor(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_HOUR;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Armor,
                 "Mage Armor- AC Bonus", 4, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public string Magic_Missile(List<IndividualStatBlock_Combat> targets)
        {
            int Damage = 0;
            string message = string.Empty;

            foreach (IndividualStatBlock_Combat target in targets)
            {
                Damage = -1 * DiceRoll.RollComplexDice("1d4+1");
                target.TakeDamage(Damage);
                message += target.name + " takes " + Math.Abs(Damage).ToString() + " points damage\r\n"; 
            }
            return message;
        }

        public void Magic_Vestment(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_HOUR;
            if (CasterLevel > 20) CasterLevel = 20; //max effects 20th level            
            int mod = CasterLevel / 4;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
               OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Armor,
                    "Magic Vestment- AC Mod", mod, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Major_Image(int CasterLevel, List<IndividualStatBlock_Combat> targets)     {      }

        public void Maze(int CasterLevel, IndividualStatBlock_Combat target)    {     }

        public void Meld_Into_Stone(int CasterLevel, IndividualStatBlock_Combat target)     {     }

        public void Message(int CasterLevel, List<IndividualStatBlock_Combat> targets)     {      }

        public void Mind_Blank(int CasterLevel, IndividualStatBlock_Combat target)    {     }

        public void Minor_Image(int CasterLevel, List<IndividualStatBlock_Combat> targets)    {      }

        public void Miracle()     {      }

        public void Mirage_Arcana(int CasterLevel, List<IndividualStatBlock_Combat> targets)     {      }

        public void Mirror_Image(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;
            int mod = DiceRoll.RollDice(4) + (CasterLevel / 3);
            if (mod > 8) mod = 8; // number of images

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.DefensiveAbilities,
              OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                   "Mirror Image- Defensive Abilities Mod", 0, "mirror image (" + mod.ToString() + " images)");
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Misdirection(int CasterLevel, IndividualStatBlock_Combat target)   { }

        public void Mislead(int CasterLevel) { }

        public void Moment_Of_Prescience(int CasterLevel, IndividualStatBlock_Combat target) { }


        #endregion M Spells

        #region N Spells

        public void Nondetection(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_HOUR;


        }

        #endregion N Spells

        #region O Spells

        public void Obscuring_Mist(int CasterLevel)
        {

        }

        public void Overland_Flight(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_HOUR;
            int mod = CasterLevel / 2;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Skill,
                  OnGoingStatBlockModifier.StatBlockModifierSubTypes.Skill_Name,
                   "Overland Flight- Skill Mod", mod , StatBlockInfo.SkillNames.FLY);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Owls_Wisdom(int CasterLevel, IndividualStatBlock_Combat target)
        {            
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_HOUR;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Wis,
                 "Owl's Wisdom- Wis Bonus", 4, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);          
        }

        #endregion O Spells

        #region P Spells

        public void Pass_Without_Trace(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {       }

        public void Permanent_Image(int CasterLevel) { }
        public void Persistent_Image(int CasterLevel) { }
        public void Plane_Shift(int CasterLevel, List<IndividualStatBlock_Combat> targets) { }
        public void Polymorph(int CasterLevel, IndividualStatBlock_Combat target) { }
        public void Power_Word_Stun(int CasterLevel, IndividualStatBlock_Combat target) { }
        public void Prayer(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {       }

        public void Produce_Flame(int CasterLevel)       {        }

        public void Project_Image(int CasterLevel) { }

        public void Protection_From_Arrows(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_HOUR;
            int mod = 10 * CasterLevel;
            if (mod > 100) mod = 100;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.DR,
              OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                   "Protection From Arrows- DR Mod", 0, "10/magic vs. ranged weapons (" + mod.ToString() + "points)");
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Protection_From_Energy(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE * 10;
            int mod = 12 * CasterLevel;
            if (mod > 120) mod = 120;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Immune,
              OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                   "Protection From Energy- Immune Mod", 0, "(protection from energy," + mod.ToString() + "points)");
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Protection_From_Evil(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE; // 1 minute/level


            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow,
                    OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                         "Protection from Evil- Save Mod", 2, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);

            StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
               OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Deflection,
                    "Protection from Evil- AC Mod", 2, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);

        }

        public void Protection_From_Good(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE; // 1 minute/level


            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow,
                    OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                         "Protection from Good- Save Mod", 2, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);

            StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
               OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Deflection,
                    "Protection from Good- AC Mod", 2, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);

        }

        public void Prying_Eyes(int CasterLevel)      {      }

        public void Poison(int CasterLevel, IndividualStatBlock_Combat target)     {              }

        public void Purify_Food_And_Drink(int CasterLevel) { }

        #endregion P Spells

        #region R Spells

        public void Rage()      {      }

        public void Ray_Of_Enfeeblement(int CasterLevel, List<IndividualStatBlock_Combat> targets)      {       }

        public void Ray_Of_Sickening(int CasterLevel, List<IndividualStatBlock_Combat> targets)      {        }

        public void Read_Magic(int CasterLevel, IndividualStatBlock_Combat target) { }

        public void Resistance(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = PathfinderConstants.ROUNDS_PER_MINUTE; // 1 minute

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow,
               OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                    "Resistance- Save Mod", 1, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Resist_Energy(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE * 10;
            int mod = 10; //min
            if (CasterLevel >= 7) mod += 10;
            if (CasterLevel >= 11) mod += 30;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Resist,
              OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                   "Resist Energy- Resist Mod", 1, mod.ToString() + " (resist energy)");
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Resist_Energy_Communal(int CasterLevel, List<IndividualStatBlock_Combat> targets)      {       }
        public void Restoration_Greater(int CasterLevel, IndividualStatBlock_Combat target) { }

        public void Restoration_Lesser(int CasterLevel, IndividualStatBlock_Combat target)     {       }

        public void Returning_Weapon(int CasterLevel) { }

        public void Returning_Weapon_Communal(int CasterLevel)      {       }

        public void Righteous_Might(int CasterLevel, IndividualStatBlock_Combat target)      {        }

        #endregion R Spells

        #region S Spells

        public void Sacred_Bond(int CasterLevel, IndividualStatBlock_Combat target)     {          }

        public void Sanctuary(int CasterLevel, IndividualStatBlock_Combat target)    {       }

        public void Sanctify_Armor(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int mod = CasterLevel / 4;
            if (mod > 5) mod = 5;

            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
             OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Enhancement,
              "Sanctify Armor - AC mod", mod, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }
               
        public void Scorching_Ray(int CasterLevel, List<IndividualStatBlock_Combat> targets)      {       }

        public void Screen(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {      }

        public void Scrying()      {       }

        public void Scrying_Greater()     {      }

        public void Searing_Light(int CasterLevel, IndividualStatBlock_Combat target)      {      }

        public void See_Invisibility(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = GetTenMinutesPerLevel(CasterLevel);

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Senses,
               OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                    "See Invisibility- Senses", 0, "see invisibility");
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Sending(IndividualStatBlock_Combat target)        {                 }

        public void Shield(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE; //1min/per level

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
               OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Shield,
                    "Shield- AC Mod", 4, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Shield_Of_Law(int CasterLevel, List<IndividualStatBlock_Combat> targets)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;

            foreach (IndividualStatBlock_Combat target in targets)
            {
                OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow,
                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                    "Shield of Law- Save Bonus", 4, string.Empty);
                target.AddOnGoingStatBlockMod(StatBlockMod);

                StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Deflection,
                 "Shield of Law- AC Bonus", 4, string.Empty);
                target.AddOnGoingStatBlockMod(StatBlockMod);
            }
        }

        public void Shield_Of_Faith(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;  
            if (CasterLevel > 18) CasterLevel = 18; //max effects 18th level            
            int mod = 2 + (CasterLevel / 6);

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
               OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Deflection,
                    "Shield of Faith- AC Mod", mod, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Shield_Other(int CasterLevel, IndividualStatBlock_Combat target)      {       }

        public void Shillelagh()      {       }

        public void Shock_Shield(int CasterLevel, IndividualStatBlock_Combat target)    {      }

        public void Silent_Image(int CasterLevel)      {       }
        public void Slay_Living(int CasterLevel, IndividualStatBlock_Combat target)      {       }
        public void Slipstream(int CasterLevel, IndividualStatBlock_Combat target) { }

        public void Speak_With_Animals(int CasterLevel, IndividualStatBlock_Combat target)     {       }

        public void Speak_With_Dead(int CasterLevel, IndividualStatBlock_Combat target)    {       }

        public void Speak_With_Plants(int CasterLevel, IndividualStatBlock_Combat target) { }

        public void Spectral_Hand(int CasterLevel)     {      }

        public void Spider_Climb(int CasterLevel, IndividualStatBlock_Combat target)      {       }

        public void Spike_Growth(int CasterLevel)     {      }

        public void Spite(int CasterLevel, IndividualStatBlock_Combat target)    {     }

        public void Spellstaff(int CasterLevel) { }

        public void Spell_Immunity(int CasterLevel, IndividualStatBlock_Combat target)     {       }

        public void Spell_Immunity_Greater(int CasterLevel, IndividualStatBlock_Combat target)    {       }

        public void Spell_Resistance(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;
            int mod = 12 + CasterLevel;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.SR,
               OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                    "Spell_Resistance- SR Mod", mod, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Spell_Turning(int CasterLevel, IndividualStatBlock_Combat target)     {     }

        public void Statue(int CasterLevel, IndividualStatBlock_Combat target)      {       }

        public void Status(int CasterLevel, IndividualStatBlock_Combat target)    {       }

        public void Steady_Saddle(int CasterLevel) { }

        public void Stone_Tell(int CasterLevel, IndividualStatBlock_Combat target)     {       }

        public void Storm_Of_Vengeance(int CasterLevel, List<IndividualStatBlock_Combat> targets)     {        }

        public void Strong_Jaw(int CasterLevel, IndividualStatBlock_Combat target)      {        }

        public void Stoneskin(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE * 10;            
            int mod = 10 * CasterLevel;
            if (mod > 150) mod = 150;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.DR,
              OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                   "Stoneskin- DR Mod", 0, "10/adamantine (" + mod + " points)");
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Suggestion(int CasterLevel, IndividualStatBlock_Combat target)      {        }

        public void Summon_Natures_Ally_I(int CasterLevel)      {        }

        public void Summon_Natures_Ally_Iv(int CasterLevel)      {        }

        public void Summon_Monster_Ii(int CasterLevel)      {       }

        public void Symbol_Of_Pain() { }

        public void Symbol_Of_Persuasion()       {       }

        public void Symbol_Of_Scrying()      {       }

        #endregion S Spells

        #region T Spells

        public void Telekinesis(int CasterLevel) { }

        public void Telepathic_Bond(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {        }

        public void Teleport(int CasterLevel, List<IndividualStatBlock_Combat> targets)        {        }

        public void Teleport_Greater(int CasterLevel, List<IndividualStatBlock_Combat> targets) { }

        public void Threefold_Aspect(IndividualStatBlock_Combat target)        {        }

        public void Thorn_Body(int CasterLevel, IndividualStatBlock_Combat target) { }
        
        public void Tongues() { }

        public void Touch_Of_The_Sea() { }

        public void Transformation(int CasterLevel, IndividualStatBlock_Combat target)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;

            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
               OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Str,
                "Transformation- Str Bonus", 4, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);

            StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
              OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Dex,
               "Transformation- Dex Bonus", 4, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);

            StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
              OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Con,
               "Transformation- Con Bonus", 4, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);

            StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
             OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Natural,
              "Transformation- AC Bonus", 4, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);

            StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow,
             OnGoingStatBlockModifier.StatBlockModifierSubTypes.SavingThrow_Fort,
              "Transformation- Fort Bonus", 5, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);

            StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.Attack,
             OnGoingStatBlockModifier.StatBlockModifierSubTypes.Override,
              "Transformation- Attack Override", target.HDValue(), string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }


        public void Tree_Shape(int CasterLevel, IndividualStatBlock_Combat target)
        {

        }

        public void True_Seeing(int CasterLevel, IndividualStatBlock_Combat target)
        {

        }

        public void True_Strike(IndividualStatBlock_Combat target)
        {

        }

        #endregion T Spells

        #region U Spells

        public void Undetectable_Alignment(int CasterLevel, IndividualStatBlock_Combat target)        {        }

        public void Unhallow(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {        }

        public void Unholy_Aura(int CasterLevel, List<IndividualStatBlock_Combat> targets)
        {
            int duration = CasterLevel * PathfinderConstants.ROUNDS_PER_MINUTE;

            foreach (IndividualStatBlock_Combat target in targets)
            {
                OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow,
                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                    "Unholy Aura- Save Bonus", 4, string.Empty);
                target.AddOnGoingStatBlockMod(StatBlockMod);

                StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Deflection,
                 "Unholy Aura- AC Bonus", 4, string.Empty);
                target.AddOnGoingStatBlockMod(StatBlockMod);
            }
        }

        public void Unholy_Blight(int CasterLevel, List<IndividualStatBlock_Combat> targets) { }


        public void Unseen_Servant(int CasterLevel)       {        }

        #endregion U Spells

        #region V Spells

        public void Vanish(int CasterLevel, IndividualStatBlock_Combat target)      {       }

        public void Vampiric_Touch(IndividualStatBlock_Combat target)       {        }

        public void Veil(int CasterLevel, List<IndividualStatBlock_Combat> targets)       {        }

        public void Ventriloquism(int CasterLevel)       {        }

        public void Virtue(IndividualStatBlock_Combat target)
        {
            OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(PathfinderConstants.ROUNDS_PER_MINUTE, OnGoingStatBlockModifier.StatBlockModifierTypes.TemporaryHP,
              OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                   "Virtue- Tempory HP", 1, string.Empty);
            target.AddOnGoingStatBlockMod(StatBlockMod);
        }

        public void Vision_of_Hell(int CasterLevel, List<IndividualStatBlock_Combat> targets)      {        }

        #endregion V Spells

        #region W Spells

        public void Water_Breathing(int CasterLevel)       {        }

        public void Wall_Of_Force(int CasterLevel)       {        }

        public void Wall_Of_Stone(int CasterLevel)       {        }

        public void Wall_Of_Thorns(int CasterLevel)       {        }

        public void Water_Walk(int CasterLevel, List<IndividualStatBlock_Combat> targets)      {        }

        public void Ward_the_Faithful(int CasterLevel, List<IndividualStatBlock_Combat> targets)
        {
            int AC_Bonus = 2;
            int SaveBonus = 2;

            if (CasterLevel >= 12)
            {
                AC_Bonus++;
                SaveBonus++;
            }

            if (CasterLevel >= 18)
            {
                AC_Bonus++;
                SaveBonus++;
            }

            int duration = GetTenMinutesPerLevel(CasterLevel);

            foreach (IndividualStatBlock_Combat target in targets)
            {
                OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow,
                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                    "Ward the faithful- Save Bonus", SaveBonus, string.Empty);
                target.AddOnGoingStatBlockMod(StatBlockMod);

                StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.AC,
                OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Deflection,
                 "Ward the faithful- AC Bonus", AC_Bonus, string.Empty);
                target.AddOnGoingStatBlockMod(StatBlockMod);
            }
        }
        public void Waves_Of_Exhaustion(int CasterLevel, List<IndividualStatBlock_Combat> targets) { }

        public void Weapon_Of_Awe(int CasterLevel)      {       }

        public void Web(int CasterLevel)      {        }

        public void Wind_Wall(int CasterLevel)      {       }

        public void Wish()      {       }

        public void Wrathful_Mantle(int CasterLevel, List<IndividualStatBlock_Combat> targets)
        {
            int duration = GetTenMinutesPerLevel(CasterLevel);
            int SaveBonus = CasterLevel / 4;

            if (SaveBonus > 5) SaveBonus = 5;

            foreach (IndividualStatBlock_Combat target in targets)
            {
                OnGoingStatBlockModifier StatBlockMod = new OnGoingStatBlockModifier(duration, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow,
                   OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,
                    "Wrathful Mantle- Save Bonus", SaveBonus, string.Empty);
                target.AddOnGoingStatBlockMod(StatBlockMod);
            }
        }

        #endregion W Spells
    }
}
