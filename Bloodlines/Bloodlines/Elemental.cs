using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using Skills;

namespace Bloodlines
{
    public class Elemental : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Elemental()
        {
            Name = "Elemental";
            Description = "The power of the elements resides in you, and at times you can hardly control its fury. This influence comes from an elemental outsider in your family history or a time when you or your relatives were exposed to a powerful elemental force.";
            ClassSkillName = SkillData.SkillNames.KnowledgePlanes;
            BonusSpells = new Dictionary<string, int> { {"burning hands",3}, 
                                                        {"scorching ray", 5},
                                                        {"protection from energy", 7},
                                                        {"elemental body I", 9},
                                                        {"elemental body II", 11},
                                                        {"elemental body III", 13},
                                                        {"elemental body IV", 15},
                                                        {"summon monster VIII", 17},
                                                        {"elemental swarm", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
