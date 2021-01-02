using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Stars : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Stars()
        {
            Name = "Stars";
            Dieties = new List<string>();
            Description = "";
            DomainSpells = new Dictionary<string, int>()
            {
                {"feather fall",1},
                {"hypnotic pattern", 2},
                {"fly",3},
                {"lesser planar binding", 4},
                {"overland flight",5},
                {"planar binding",6},
                {"sunbeam",7},
                {"greater planar binding",8},
                {"meteor swarm",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
           // AddAbilities();
        }
    }
}
