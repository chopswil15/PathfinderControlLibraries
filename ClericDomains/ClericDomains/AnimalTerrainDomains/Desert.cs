using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Desert : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Desert()
        {
            Name = "Desert";
            Dieties = new List<string>();
            Description = "The spirits and secrets of the endless wastes are yours to command.";
            DomainSpells = new Dictionary<string, int>()
            {
                {"cloak of shade",1},
                {"shifting sand", 2},
                {"cup of dust",3},
                {"hallucinatory terrain", 4},
                {"transmute rock to mud",5},
                {"sirocco",6},
                {"sunbeam",7},
                {"sunburst",8},
                {"horrid wilting",9}
            };
          
        }
    }
}
