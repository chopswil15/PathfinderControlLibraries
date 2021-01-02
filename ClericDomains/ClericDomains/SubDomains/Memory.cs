using OnGoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClericDomains
{
    public class Memory : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Memory()
        {
            Name = "Memory";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"comprehend languages",1},
                {"memory lapse", 2},
                {"speak with dead",3},
                {"divination", 4},
                {"true seeing",5},
                {"modify memory",6},
                {"legend lore",7},
                {"moment of prescience",8},
                {"foresight",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
           // AddAbilities();
        }
    }
}
