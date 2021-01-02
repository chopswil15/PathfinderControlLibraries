using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeatFoundational
{
    public enum FeatTypes
    {
        None = 0,
        General = 1,
        Combat,
        Racial,
        ItemCreation,
        Metamagic,
        Monster,
        Local,
        Achievent,
        Story,
        Mythic
    }

    public class FeatFoundation
    {
        public string Name { get; set; }
        public FeatTypes Type {get; set;}
        public List<string> Races { get; set; } //only if type = racial
        public List<string> PreRequistFeats {get; set;}
        public List<string> PreRequistSkillRanks { get; set; }
        public string Source { get; set; }
        public string SelectedItem { get; set; }
        public bool BonusFeat { get; set; }
        public int BAB { get; set; }
        public int Int { get; set; }
        public int Str { get; set; }
        public int Dex { get; set; }
        public int Wis { get; set; }
        public int Con { get; set; }
        public int Cha { get; set; }
        public bool Multiple { get; set; }

        public FeatFoundation()
        {
            PreRequistFeats = new List<string>();
            PreRequistSkillRanks = new List<string>();
            Races = new List<string>();
            BAB = -1;
            Int = -1;
            Str = -1;
            Wis = -1;
            Con = -1;
            Cha = -1;
            Dex = -1;
            Multiple = false;
        }

        public virtual void ApplyFeatToAC(ref int mod, ref string calculation, string OptionalSelectedItem)
        {

        }
    }
}
