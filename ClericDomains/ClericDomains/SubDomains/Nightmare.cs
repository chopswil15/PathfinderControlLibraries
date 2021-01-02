using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using PathfinderGlobals;

namespace ClericDomains
{
    public class Nightmare : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Nightmare()
        {
            Name = "Nightmare";
            Dieties = new List<string>();
            Description = PathfinderConstants.SPACE;
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"lesser confusion",1},
                {"touch of idiocy", 2},
                {"rage",3},
                {"phantasmal killer", 4},
                {"nightmare",5},
                {"cloak of dreams",6},
                {"insanity",7},
                {"scintillating pattern",8},
                {"weird",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }   
 
        private void AddAbilities()
        {}
    }
}
