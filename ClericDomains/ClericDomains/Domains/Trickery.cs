using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Trickery : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Trickery()
        {
            Name = "Trickery";
            Dieties = new List<string>() { "Abadar", "Cayden Cailean", "Desna" };
            Description = "You are a master of illusions and deceptions.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"disguise self",1},
                {"invisibility", 2},
                {"nondetection",3},
                {"confusion", 4},
                {"false vision",5},
                {"mislead",6},
                {"screen",7},
                {"mass invisibility",8},
                {"time stop",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }   
        
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("Copycat", 0, "Copycat",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("MastersIllusion", 0, "Master’s Illusion",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
