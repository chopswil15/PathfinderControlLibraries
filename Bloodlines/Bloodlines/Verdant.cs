using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using Skills;

namespace Bloodlines
{
    public class Verdant : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Verdant()
        {
            Name = "Verdant";
            Description = "Your progenitors infused themselves with raw plant life, binding it into their own tissue and passing it down to their literal seed, giving you innate communion with nature.";
           ClassSkillName = SkillData.SkillNames.KnowledgeNature;
            BonusSpells = new Dictionary<string, int> { {"entangle",3}, 
                                                        {"barkskin", 5},
                                                        {"speak with plants", 7},
                                                        {"command plants", 9},
                                                        {"wall of thorns", 11},
                                                        {"transport via plants", 13},
                                                        {"plant shape III", 15},
                                                        {"animate plants", 17},
                                                        {"shambler", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
