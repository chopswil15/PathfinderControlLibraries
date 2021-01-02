using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Knowledge : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Knowledge()
        {
            Name = "Knowledge";
            Dieties = new List<string>() { "Calistria", "Irori", "Nethys", "Norgorber", "Pharasma" };
            Description = "You are a scholar and a sage of legends.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"comprehend languages",1},
                {"detect thoughts", 2},
                {"speak with dead",3},
                {"divination", 4},
                {"true seeing",5},
                {"find the path",6},
                {"legend lore",7},
                {"discern location",8},
                {"foresight",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }     
 
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("LoreKeeper", 0, "Lore Keeper",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("RemoteViewing", 0, "Remote Viewing",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 6));
        }
    }
}
