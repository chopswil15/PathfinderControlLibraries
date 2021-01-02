using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Nobility : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Nobility()
        {
            Name = "Nobility";
            Dieties = new List<string>() { "Abadar" };
            Description = "You are a great leader, an inspiration to all who follow the teachings of your faith.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"divine favor",1},
                {"enthrall", 2},
                {"magic vestment",3},
                {"discern lies", 4},
                {"greater command",5},
                {"geas/quest",6},
                {"repulsion",7},
                {"demand",8},
                {"storm of vengeance",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }  
 
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("InspiringWord", 0, "Inspiring Word",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("Leadership", 0, "Leadership",
                OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
