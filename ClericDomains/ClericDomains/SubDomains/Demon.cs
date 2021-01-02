using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Demon: IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Demon()
        {
            Name = "Demon";
            Dieties = new List<string>();
            Description = string.Empty;
            DomainSpells = new Dictionary<string, int>()
            {
                {"doom",1},
                {"align weapon [chaos]", 2},
                {"rage",3},
                {"chaos hammer", 4},
                {"dispel law",5},
                {"planar binding [demon]",6},
                {"word of chaos",7},
                {"cloak of chaos",8},
                {"summon monster IX [chaos]",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
          //  AddAbilities();
        }

    }
}
