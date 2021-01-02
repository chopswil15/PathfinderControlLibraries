using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Animal : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Animal()
        {
            Name = "Animal";
            Dieties = new List<string>() { "Erastil", "Gozreh" };
            Description = "You can speak with and befriend animals with ease. In addition, you treat Knowledge (nature) as a class skill.";
            DomainSpells = new Dictionary<string, int>()
            {
                {"calm animals",1},
                {"hold animal", 2},
                {"dominate animal",3},
                {"summon nature’s ally IV [animals]", 4},
                {"beast shape III [animals]",5},
                {"antilife shell",6},
                {"animal shapes",7},
                {"summon nature’s ally VIII [animals]",8},
                {"shapechange",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }

        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("SpeakWithAnimals", 0, "Speak with Animals", 
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("AnimalCompanion", 0, "Animal Companion",   
                OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 4));
        }
    }
}