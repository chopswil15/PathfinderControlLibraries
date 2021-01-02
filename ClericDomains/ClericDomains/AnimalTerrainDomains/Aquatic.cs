using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Aquatic  : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Aquatic()
        {
            Name = "Aquatic";
            Dieties = new List<string>();
            Description = "You master the deeps of the sea, raging rivers, flowing falls, and relentless tides.";
            DomainSpells = new Dictionary<string, int>()
            {
                {"hydraulic push",1},
                {"slipstream", 2},
                {"water breathing",3},
                {"freedom of movement", 4},
                {"black tentacles",5},
                {"freezing sphere",6},
                {"animal shapes",7},
                {"seamantle",8},
                {"tsunami",9}
            };
          
        }
    }
}
