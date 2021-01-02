using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using Skills;

namespace Bloodlines
{
    public class Starsoul : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }        
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Starsoul()
        {
            Name = "Starsoul";
            Description = "You come from a line of stargazers and explorers who delved deeply into the darkness beyond the stars. In touching the void, the void touched them, and your mind, spirit, and body yearn to span the gulf between worlds.";
            ClassSkillName = SkillData.SkillNames.KnowledgeNature;
            BonusSpells = new Dictionary<string, int> { {"unseen servant",3}, 
                                                        {"glitterdust", 5},
                                                        {"blink", 7},
                                                        {"call lightning storm", 9},
                                                        {"overland flight", 11},
                                                        {"repulsion", 13},
                                                        {"reverse gravity", 15},
                                                        {"greater prying eyes", 17},
                                                        {"meteor swarm", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
