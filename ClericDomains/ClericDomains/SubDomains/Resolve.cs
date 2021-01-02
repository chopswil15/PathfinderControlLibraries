using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Resolve : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Resolve()
        {
            Name = "Resolve";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"bless",1},
                {"bull's strength", 2},
                {"magic vestment",3},
                {"spell immunity", 4},
                {"righteous might",5},
                {"heroes' feast",6},
                {"grasping hand",7},
                {"clenched fist",8},
                {"crushing hand",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            // AddAbilities();
        }
    }
}
