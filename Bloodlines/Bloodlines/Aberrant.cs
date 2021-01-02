using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using Skills;

namespace Bloodlines
{
    public class Aberrant : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Aberrant()
        {
            Name = "Aberrant";
            Description = "There is a taint in your blood, one that is alien and bizarre. You tend to think in odd ways, approaching problems from an angle that most would not expect. Over time, this taint manifests itself in your physical form.";
            ClassSkillName = SkillData.SkillNames.KnowledgeEngineering;
            BonusSpells = new Dictionary<string, int> { {"enlarge person",3}, 
                                                        {"see invisibility", 5},
                                                        {"tongues", 7},
                                                        {"black tentacles", 9},
                                                        {"feeblemind", 11},
                                                        {"veil", 13},
                                                        {"plane shift", 15},
                                                        {"mind blank", 17},
                                                        {"shapechange", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
