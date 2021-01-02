using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using Skills;

namespace Bloodlines
{
    public class Draconic : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Draconic()
        {
            Name = "Draconic";
            Description = "Your family has always been skilled in the eldritch art of magic. While many of your relatives were accomplished wizards, your powers developed without the need for study and practice.";
            ClassSkillName = SkillData.SkillNames.Perception;
            BonusSpells = new Dictionary<string, int> { {"mage armor",3}, 
                                                        {"resist energy", 5},
                                                        {"fly", 7},
                                                        {"fear", 9},
                                                        {"spell resistance", 11},
                                                        {"form of the dragon I", 13},
                                                        {"form of the dragon II", 15},
                                                        {"form of the dragon III", 17},
                                                        {"wish", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }

    }
}
