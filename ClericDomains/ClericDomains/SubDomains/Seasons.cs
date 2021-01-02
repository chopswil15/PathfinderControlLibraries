using OnGoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClericDomains
{
    public class Seasons : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Seasons()
        {
            Name = "Seasons";
            Dieties = new List<string>(); ;
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"goodberry",1},
                {"fog cloud", 2},
                {"call lightning",3},
                {"blight", 4},
                {"ice storm",5},
                {"control winds",6},
                {"control weather",7},
                {"sunburst",8},
                {"storm of vengeance",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
          //  AddAbilities();
        }
    }
}
