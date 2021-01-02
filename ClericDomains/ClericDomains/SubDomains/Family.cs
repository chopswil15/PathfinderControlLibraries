using OnGoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClericDomains
{
    public class Family : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }
        public Family()
        {
            Name = "Family";
            Dieties = new List<string>();
            Description = "";
            DomainSpells = new Dictionary<string, int>()
            {
                {"bless",1},
                {"calm emotions", 2},
                {"create food and water",3},
                {"status", 4},
                {"telepathic bond",5},
                {"heroes’ feast",6},
                {"refuge",7},
                {"mass cure critical wounds",8},
                {"miracle",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
           
        }
    }
}
