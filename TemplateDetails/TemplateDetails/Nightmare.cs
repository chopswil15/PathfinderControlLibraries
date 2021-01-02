using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;

namespace TemplateDetails
{
    public class Nightmare : TemplateFoundation.TemplateFoundation
    {
        public Nightmare()
        {
            Name = "Nightmare";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            TemplateCommon.AddDR(MonSB, "good or silver", 5);

            return MonSB;
        }
    }
}
