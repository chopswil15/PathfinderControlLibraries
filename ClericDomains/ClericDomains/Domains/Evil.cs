using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;


namespace ClericDomains
{
    public class Evil : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Evil()
        {
            Name = "Evil";
            Dieties = new List<string>() { "Asmodeus", "Lamashtu", "Norgorber", "Rovagug", "Urgathoa", "Zon-Kuthon" };
            Description = "You are sinister and cruel, and have wholly pledged your soul to the cause of evil.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"protection from good",1},
                {"align weapon [evil]", 2},
                {"magic circle against good",3},
                {"unholy blight", 4},
                {"dispel good",5},
                {"create undead",6},
                {"blasphemy",7},
                {"unholy aura",8},
                {"summon monster IX [evil]",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }  
 
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("TouchOfEvil", 0, "Touch of Evil",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("ScytheOfEvil", 0, "Scythe of Evil",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
