using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Healing : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Healing()
        {
            Name = "Healing";
            Dieties = new List<string>() { "Irori", "Pharasma", "Sarenrae" };
            Description = "Your touch staves off pain and death, and your healing magic is particularly vital and potent.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"cure light wounds",1},
                {"cure moderate wounds", 2},
                {"cure serious wounds",3},
                {"cure critical wounds", 4},
                {"breath of life",5},
                {"heal",6},
                {"regenerate",7},
                {"mass cure critical wounds",8},
                {"mass heal",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }     
 
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("RebukeDeath", 0, "Rebuke Death",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("HealersBlessing", 0, "Healer’s Blessing",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 6));
        }
    }
}
