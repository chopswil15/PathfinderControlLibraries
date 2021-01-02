using OnGoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClericDomains
{
    public class Catastrophe : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Catastrophe()
        {
            Name = "Catastrophe";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"true strike",1},
                {"gust of wind", 2},
                {"call lightning",3},
                {"inflict critical wounds", 4},
                {"shout",5},
                {"harm",6},
                {"control weather",7},
                {"earthquake",8},
                {"implosion",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
           // AddAbilities();
        }  
    }
}
