using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonStatBlockInfo;
using OnGoing;

namespace RaceFoundational
{
    public class RaceFoundation
    {
        public List<string> RaceLanguages { get; protected set;}
        public string Name { get; protected set;}
        public StatBlockInfo.SizeCategories Size { get; protected set;}
        public int BaseSpeed { get; protected set;}
        public string Senses { get; protected set; }
        public string Immune { get; protected set; }
        public string Resistance { get; protected set; }
        public string RaceWeapons { get; protected set; }
        public int NaturalArmor { get; protected set; }
        //public List<OnGoingStatBlockModifier> RaceOnGoingModifiers { get; protected set; }


        public RaceFoundation()
        {
            Senses = string.Empty;
            Immune = string.Empty;
            RaceWeapons = string.Empty;
            Resistance = string.Empty;
            NaturalArmor = 0;
        }

        public virtual int BonusFeatCount()
        {
            //override in Human
            return 0;
        }

        public virtual Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int>();
        }

        public virtual List<OnGoingStatBlockModifier> GetRaceOnGoingModifiers()
        {
            return new List<OnGoingStatBlockModifier>();
        }
    }
}
