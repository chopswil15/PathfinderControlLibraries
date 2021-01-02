using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skills;
using OnGoing;

namespace Bloodlines
{
    public class Stormborn : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Stormborn()
        {
            Name = "Stormborn";
            Description = "";
            ClassSkillName = SkillData.SkillNames.KnowledgeNature;
            BonusSpells = new Dictionary<string, int> { {"shocking grasp",3}, 
                                                        {"gust of wind", 5},
                                                        {"lightning bolt", 7},
                                                        {"shout", 9},
                                                        {"overland flight", 11},
                                                        {"chain lightning", 13},
                                                        {"control weather", 15},
                                                        {"whirlwind", 17},
                                                        {"storm of vengeance", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
