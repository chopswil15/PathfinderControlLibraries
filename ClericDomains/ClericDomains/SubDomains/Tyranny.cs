using OnGoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClericDomains
{
    public class Tyranny : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Tyranny()
        {
            Name = "Tyranny";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"command",1},
                {"align weapon [law]", 2},
                {"bestow curse",3},
                {"order’s wrath", 4},
                {"dispel chaos",5},
                {"hold monster",6},
                {"symbol of persuasion",7},
                {"shield of law",8},
                {"summon monster IX [law]",9}
            };
        }
    }
}
