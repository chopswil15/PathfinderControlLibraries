using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Scalykind : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Scalykind()
        {
            Name = "Scalykind";
            Dieties = new List<string>() { "Dahak", "Ydersius" };
            Description = "You are a true lord of reptiles, able to induce pain, panic, and confusion with a mere glance, and your mesmerizing eyes can even drive weak creatures into unconsciousness.";
            DomainSpells = new Dictionary<string, int>()
            {
                {"magic fang",1},
                {"animal trance", 2},
                {"greater magic fang",3},
                {"poison", 4},
                {"animal growth",5},
                {"eyebite",6},
                {"creeping doom",7},
                {"animal shapes",8},
                {"shapechange",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }

        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("VenomousStare", 0, "Venomous Stare", 
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("SerpentCompanion", 0, "Serpent Companion",   
                OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 4));
        }
    }
}
