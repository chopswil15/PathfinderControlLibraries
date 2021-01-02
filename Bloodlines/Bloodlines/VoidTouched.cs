using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skills;
using OnGoing;

namespace Bloodlines
{
    public class VoidTouched : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }        
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public VoidTouched()
        {
            Name = "Void-Touched";
            Description = "";
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
