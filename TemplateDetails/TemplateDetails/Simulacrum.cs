using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;

namespace TemplateDetails
{
    public class Simulacrum : TemplateFoundation.TemplateFoundation
    {
        //not really a template, just a way of handling "Simulacrum" in stat block
        public Simulacrum()
        {
            Name = "Simulacrum";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            return MonSB;
        }
    }
}
