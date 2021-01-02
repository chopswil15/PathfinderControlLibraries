using OnGoing;
using Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bloodlines
{
    public class Verminous : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Verminous()
        {
            Name = "Verminous";
            Description = "";
            ClassSkillName = SkillData.SkillNames.Survival;
            BonusSpells = new Dictionary<string, int> { {"endure elements",3},
                                                        {"summon swarm", 5},
                                                        {"poison", 7},
                                                        {"giant vermin", 9},
                                                        {"insect plague", 11},
                                                        {"harm", 13},
                                                        {"creeping doom", 15},
                                                        {"symbol of insanity", 17},
                                                        {"antipathy", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
