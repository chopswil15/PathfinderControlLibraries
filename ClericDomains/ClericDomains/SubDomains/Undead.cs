using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Undead: IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Undead()
        {
            Name = "Undead";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"cause fear",1},
                {"ghoul touch", 2},
                {"animate dead",3},
                {"enervation", 4},
                {"slay living",5},
                {"create undead",6},
                {"destruction",7},
                {"create greater undead",8},
                {"energy drain",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
        } 
    }
}
