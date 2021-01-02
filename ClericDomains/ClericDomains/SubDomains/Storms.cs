using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Storms : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Storms()
        {
            Name = "Storms";
            Dieties = new List<string>();
            Description = string.Empty;
            DomainSpells = new Dictionary<string, int>()
            {
                {"obscuring mist",1},
                {"fog cloud", 2},
                {"call lightning",3},
                {"sleet storm", 4},
                {"call lightning storm",5},
                {"sirocco",6},
                {"control weather",7},
                {"whirlwind",8},
                {"storm of vengeance",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }

        private void AddAbilities()
        {

        }
    }
}
