using OnGoing;
using Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bloodlines
{
    public class DeepEarth : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public DeepEarth()
        {
            Name = "Deep Earth";
            Description = "";
            ClassSkillName = SkillData.SkillNames.KnowledgeEngineering;
            BonusSpells = new Dictionary<string, int> { {"expeditious excavation",3},
                                                        {"darkvision", 5},
                                                        {"shifting sand", 7},
                                                        {"stoneskin", 9},
                                                        {"spike stones", 11},
                                                        {"stone tell", 13},
                                                        {"repel metal or stone", 15},
                                                        {"earthquake", 17},
                                                        {"clashing rocks", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
