using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Thought : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Thought()
        {
            Name = "Thought";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"comprehend languages",1},
                {"detect thoughts", 2},
                {"seek thoughts",3},
                {"divination", 4},
                {"telepathic bond",5},
                {"find the path",6},
                {"legend lore",7},
                {"mind blank",8},
                {"foresight",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
        }     
    }
}
