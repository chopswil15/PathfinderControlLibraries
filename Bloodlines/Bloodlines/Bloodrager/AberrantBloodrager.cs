using OnGoing;
using Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bloodlines
{
    public class AberrantBloodrager : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public AberrantBloodrager()
        {
            Name = "Aberrant (Bloodrager)";
            Description = "There is a taint in your blood, one that is alien and bizarre. You tend to think in odd ways, approaching problems from an angle that most would not expect. Over time, this taint manifests itself in your physical form.";
            ClassSkillName = SkillData.SkillNames.KnowledgeEngineering;
            BonusSpells = new Dictionary<string, int> { {"enlarge person",7},
                                                        {"see invisibility", 10},
                                                        {"displacement ", 13},
                                                        {"black tentacles", 16}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
