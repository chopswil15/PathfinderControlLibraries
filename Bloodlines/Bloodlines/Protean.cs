using OnGoing;
using Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bloodlines
{
    public class Protean : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Protean()
        {
            Name = "Protean";
            Description = "";
            ClassSkillName = SkillData.SkillNames.KnowledgePlanes;
            BonusSpells = new Dictionary<string, int> { {"entropic shield",3},
                                                        {"blur", 5},
                                                        {"gaseous form", 7},
                                                        {"confusion", 9},
                                                        {"major creation", 11},
                                                        {"disintegrate", 13},
                                                        {"greater polymorph", 15},
                                                        {"polymorph any object", 17},
                                                        {"shapechange", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}

