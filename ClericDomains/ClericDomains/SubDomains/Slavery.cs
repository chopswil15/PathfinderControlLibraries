using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Slavery : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Slavery()
        {
            Name = "Slavery";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"charm person",1},
                {"align weapon [law]", 2},
                {"magic circle against chaos",3},
                {"order’s wrath", 4},
                {"dominate person",5},
                {"hold monster",6},
                {"dictum",7},
                {"binding",8},
                {"summon monster IX [law]",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
           // AddAbilities();
        } 
    }
}
