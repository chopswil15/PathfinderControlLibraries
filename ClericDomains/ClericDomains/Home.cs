using OnGoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClericDomains
{
    public class Home : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Home()
        {
            Name = "Home";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
               {"alarm",1},
                {"shield other", 2},
                {"glyph of warding",3},
                {"status", 4},
                {"telepathic bond",5},
                {"heroes’ feast",6},
                {"refuge",7},
                {"mass cure critical wounds",8},
                {"guards and wards",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            // AddAbilities();
        }
    }
}
