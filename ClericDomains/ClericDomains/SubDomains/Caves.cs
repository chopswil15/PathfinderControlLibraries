using OnGoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClericDomains
{
    public class Caves : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Caves()
        {
            Name = "Caves";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"magic stone",1},
                {"create pit", 2},
                {"spiked pit",3},
                {"spike stones", 4},
                {"wall of stone",5},
                {"hungry pit",6},
                {"elemental body IV [earth]",7},
                {"earthquake",8},
                {"elemental swarm [earth]",9}
            };
        }
    }
}
