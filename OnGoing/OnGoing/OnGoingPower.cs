using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnGoing
{
    public class OnGoingPower : IOnGoing
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public int CasterLevel { get; set; }
        public PowerBaseType PowerBase { get; set; }
        public OnGoingType OnGoingType
        {
            get { return OnGoingType.Power; }
        }
        //public StatBlockGlobals.CombatActionTypes ActionType;
        public int ChargeCost { get; set; }  //some powers cost charges, 0 = none
        public string StatBlockChange { get; set; }
        public string PowerSource { get; set; }  //i.e. from ring of jumping
        public string UseRate { get; set; }  //i.e 1/day how often you can use the power
        public int RoundsInactive { get; set; } // once you use the power, UseRate, how long until you can use
        //the power again, 0 = ready to use
        //if AlwaysOn = true then should = 0
        public bool AlwaysOn { get; set; } //if true UseRate should be empty and vice versa
        //AlwaysOn = true means an Effect always exists
        public string SpellSource { get; set; } //spell name that gives stats on how power works
        public bool DestroyedByUse { get; set; } //one shot use i.e. bead of force

        public OnGoingPower(string name, int duration, int casterLevel, PowerBaseType powerBase, 
                            int chargeCost, string statBlockChange,
                            string powerSource, string useRate, int roundsInactive, bool alwaysOn,
                            string spellSource, bool destroyedByUse)            
        {
            Name = name;
            Duration = duration;
            CasterLevel = casterLevel;
            PowerBase = powerBase;
            ChargeCost = chargeCost;
            StatBlockChange = statBlockChange;
            PowerSource = powerSource;
            UseRate = useRate;
            RoundsInactive = roundsInactive;
            AlwaysOn = alwaysOn;
            SpellSource = spellSource;
            DestroyedByUse = destroyedByUse;
        }

        public enum PowerBaseType
        {
            Other = 0,
            MagicItem = 1,
            Class = 2,
            Race = 3,
            Bloodline = 4,
            Domain = 5
        }
    }
}
