using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Decay : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Decay()
        {
            Name = "Decay";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"entangle",1},
                {"barkskin", 2},
                {"contagion",3},
                {"poison", 4},
                {"wall of thorns",5},
                {"harm",6},
                {"animate plants",7},
                {"control plants",8},
                {"shambler",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }    
 
        private void AddAbilities()
        {}
    }
}
