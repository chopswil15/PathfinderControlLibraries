using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Darkness : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Darkness()
        {
            Name = "Darkness";
            Dieties = new List<string>() { "Zon-Kuthon" };
            Description = "You manipulate shadows and darkness.";
            BonusFeats = new List<string>() { "Blind-Fight" };
            DomainSpells = new Dictionary<string, int>()
            {
                {"obscuring mist",1},
                {"blindness/deafness", 2},
                {"deeper darkness",3},
                {"shadow conjuration", 4},
                {"summon monster V",5},
                {"shadow walk",6},
                {"power word blind",7},
                {"greater shadow evocation",8},
                {"shades",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        } 
 
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("TouchOfDarkness", 0, "Touch of Darkness",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("EyesOfDarkness", 0, "Eyes of Darkness",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
