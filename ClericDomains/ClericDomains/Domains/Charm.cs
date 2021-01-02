using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Charm : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Charm()
        {
            Name = "Charm";
            Dieties = new List<string>() { "Calistria", "Cayden Cailean", "Norgorber", "Shelyn" };
            Description = "You can baffle and befuddle foes with a touch or a smile, and your beauty and grace are divine.";
            DomainSpells = new Dictionary<string, int>()
            {
                {"charm person",1},
                {"calm emotions", 2},
                {"suggestion",3},
                {"heroism", 4},
                {"charm monster",5},
                {"geas/quest",6},
                {"insanity",7},
                {"demand",8},
                {"dominate monster",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }

        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("DazingTouch", 0, "Dazing Touch",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities,
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("CharmingSmile", 0, "Charming Smile",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities,
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
