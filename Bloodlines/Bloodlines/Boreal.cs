using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using Skills;

namespace Bloodlines
{
    public class Boreal : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Boreal()
        {
            Name = "Boreal";
            Description = "Descended from inhabitants of the lands of ice and snow, you count among your ancestors giant-kin, troll-born, and frost-rimed spirits. Their savage and raw energies flow down through generations to infuse you to the marrow with the chill of the polar wind, crackling auroras, and the long winter’s night.";
            ClassSkillName = SkillData.SkillNames.Survival;
            BonusSpells = new Dictionary<string, int> { {"enlarge person",3}, 
                                                        {"rage", 5},
                                                        {"elemental aura", 7},
                                                        {"wall of ice", 9},
                                                        {"cone of cold", 11},
                                                        {"transformation", 13},
                                                        {"giant form I", 15},
                                                        {"polar ray", 17},
                                                        {"meteor swarm", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
