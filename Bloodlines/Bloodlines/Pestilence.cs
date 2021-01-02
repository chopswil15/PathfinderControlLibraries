using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using Skills;

namespace Bloodlines
{
    public class Pestilence : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Pestilence()
        {
            Name = "Pestilence";
            Description = "You were born during the height of a great magical plague, to a mother suffering from an eldritch disease, or you suffered an eldritch pox as a child, such that your very soul now carries a blight of pestilence within it.";
            ClassSkillName = SkillData.SkillNames.Heal;
            BonusSpells = new Dictionary<string, int> { {"charm animal",3}, 
                                                        {"summon swarm", 5},
                                                        {"contagion", 7},
                                                        {"repel vermin", 9},
                                                        {"insect plague", 11},
                                                        {"eyebite", 13},
                                                        {"creeping doom", 15},
                                                        {"horrid wilting", 17},
                                                        {"power word", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
