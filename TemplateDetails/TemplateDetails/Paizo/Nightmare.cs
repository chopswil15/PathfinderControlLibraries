using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using TemplateFoundational;


namespace TemplateDetails
{
    public class Nightmare : TemplateFoundation
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
