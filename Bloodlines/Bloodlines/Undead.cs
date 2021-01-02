using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using Skills;

namespace Bloodlines
{
    public class Undead : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
       public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Undead()
        {
            Name = "Undead";
            Description = "The taint of the grave runs through your family. Perhaps one of your ancestors became a powerful lich or vampire, or maybe you were born dead before suddenly returning to life. Either way, the forces of death move through you and touch your every action.";
            ClassSkillName = SkillData.SkillNames.KnowledgeReligion;
            BonusSpells = new Dictionary<string, int> { {"chill touch",3}, 
                                                        {"false life", 5},
                                                        {"vampiric touch", 7},
                                                        {"animate dead", 9},
                                                        {"waves of fatigue", 11},
                                                        {"undeath to death", 13},
                                                        {"finger of death", 15},
                                                        {"horrid wilting", 17},
                                                        {"energy drain", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }

    }
}
