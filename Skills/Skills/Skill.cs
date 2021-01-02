using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;

namespace Skills
{
    public class Skill
    {
        public string Name { get; set; }
        public bool Untrained { get; set; }
        public bool ArmorCheckPenalty { get; set; }
        public StatBlockInfo.AbilityName Ability { get; set; }
        public SkillData.SkillNames SkillName { get; set; }
        public bool IsKnowledge { get; set; }
    }
}
