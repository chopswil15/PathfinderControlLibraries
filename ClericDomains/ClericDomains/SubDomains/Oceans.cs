using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Oceans : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Oceans()
        {
            Name = "Oceans";
            Dieties = new List<string>();
            Description = string.Empty;
            DomainSpells = new Dictionary<string, int>()
            {
                {"obscuring mist",1},
                {"slipstream", 2},
                {"water walk",3},
                {"control water", 4},
                {"ice storm",5},
                {"cone of cold",6},
                {"elemental body IV [water]",7},
                {"horrid wilting",8},
                {"tsunami",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }

        private void AddAbilities()
        {

        }
    }
}
