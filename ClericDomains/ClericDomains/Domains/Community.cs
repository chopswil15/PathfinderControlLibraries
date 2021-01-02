using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Community : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Community()
        {
            Name = "Community";
            Dieties = new List<string>() { "Erastil" };
            Description = "Your touch can heal wounds, and your presence instills unity and strengthens emotional bonds.";
            DomainSpells = new Dictionary<string, int>()
            {
                {"bless",1},
                {"shield other", 2},
                {"prayer",3},
                {"status", 4},
                {"telepathic bond",5},
                {"heroes’ feast",6},
                {"refuge",7},
                {"mass cure critical wounds",8},
                {"miracle",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }

        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("Calming Touch", 0, "Calming Touch",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("Unity", 0, "Unity",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
