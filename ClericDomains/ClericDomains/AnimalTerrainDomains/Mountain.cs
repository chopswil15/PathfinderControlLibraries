using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Mountain : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Mountain()
        {
            Name = "Mountain";
            Dieties = new List<string>();
            Description = "TYou have mastered the mighty powers of the great mountains that pierce the sky and stand aloof above the lowlands.";
            DomainSpells = new Dictionary<string, int>()
            {
                {"stone fist",1},
                {"stone call", 2},
                {"cloak of winds",3},
                {"stoneskin", 4},
                {"geyser",5},
                {"suffocation",6},
                {"flesh to stone",7},
                {"reverse gravity",8},
                {"clashing rocks",9}
            };
          
        }
    }
}
