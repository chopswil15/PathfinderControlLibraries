using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class DarkTapestry : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public DarkTapestry()
        {
            Name = "Dark Tapestry";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
               {"feather fall",1},
                {"summon monster II", 2},
                {"fly",3},
                {"lesser planar binding", 4},
                {"summon monster V",5},
                {"planar binding",6},
                {"insanity",7},
                {"greater planar binding",8},
                {"interplanetary teleport",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
           // AddAbilities();
        }   
    }
}
