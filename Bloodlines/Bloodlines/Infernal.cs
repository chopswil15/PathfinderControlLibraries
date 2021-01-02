using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using Skills;

namespace Bloodlines
{
    public class Infernal : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Infernal()
        {
            Name = "Infernal";
            Description = "Somewhere in your family's history, a relative made a deal with a devil, and that pact has influenced your family line ever since. In you, it manifests in direct and obvious ways, granting you powers and abilities. While your fate is still your own, you can't help but wonder if your ultimate reward is bound to the Pit.";
            ClassSkillName = SkillData.SkillNames.Diplomacy;
            BonusSpells = new Dictionary<string, int> { {"protection from good",3}, 
                                                        {"scorching ray", 5},
                                                        {"suggestion", 7},
                                                        {"charm monster", 9},
                                                        {"dominate person", 11},
                                                        {"planar binding", 13},
                                                        {"greater teleport", 15},
                                                        {"power word stun", 17},
                                                        {"meteor swarm", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }

    }
}
