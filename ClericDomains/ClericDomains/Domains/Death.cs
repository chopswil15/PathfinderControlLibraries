using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Death : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Death()
        {
            Name = "Death";
            Dieties = new List<string>() { "Norgorber", "Pharasma", "Urgathoa", "Zon-Kuthon" };
            Description = "You can cause the living to bleed at a touch, and find comfort in the presence of the dead.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"cause fear",1},
                {"death knell", 2},
                {"animate dead",3},
                {"death ward", 4},
                {"slay living",5},
                {"create undead",6},
                {"destruction",7},
                {"create greater undead",8},
                {"wail of the banshee",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        } 
 
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("BleedingTouch", 0, "Bleeding Touch",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("DeathsEmbrace", 0, "Death’s Embrace",
                OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
