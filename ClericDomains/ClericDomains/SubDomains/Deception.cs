using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Deception : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Deception()
        {
            Name = "Deception";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"disguise self",1},
                {"mirror image", 2},
                {"nondetection",3},
                {"confusion", 4},
                {"false vision",5},
                {"mislead",6},
                {"project image",7},
                {"mass invisibility",8},
                {"time stop",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            // AddAbilities();
        }
    }
}
