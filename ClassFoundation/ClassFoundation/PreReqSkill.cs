using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skills;

namespace ClassFoundational
{
    public class PreReqSkill
    {
        public SkillData.SkillNames SkillName { get; set; }
        public string SubType { get; set; } //for craft and profession
        public int Value { get; set; }
    }
}
