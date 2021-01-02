using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using TemplateFoundational;

namespace TemplateDetails
{
    public class NaturalWererat : TemplateFoundation
    {
        public NaturalWererat()
        {
            Name = "Natural Wererat"; 
            TemplateFoundationType = FoundationType.Inherited;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            TemplateCommon.AddDR(MonSB, "silver", 10);

            return MonSB;
        }
    }
}
