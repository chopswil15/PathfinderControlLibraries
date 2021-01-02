using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using Skills;

namespace Bloodlines
{
    public class Aquatic : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Aquatic()
        {
            Name = "Aquatic";
            Description = "Your family traces its heritage back to the ocean depths, whether scions of undersea empires left in the wake of nomadic sea-tribes, or the spawn of creeping ichthyic infiltrators into remote seaside villages. The song of the sea hums in your blood, calling the waves and all those within to your command.";
            ClassSkillName = SkillData.SkillNames.Swim;
            BonusSpells = new Dictionary<string, int> { {"hydraulic push",3}, 
                                                        {"slipstream", 5},
                                                        {"aqueous orb", 7},
                                                        {"geyser", 9},
                                                        {"control water", 11},
                                                        {"beast shape IV", 13},
                                                        {"summon monster VII", 15},
                                                        {"seamantle", 17},
                                                        {"world wave", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
