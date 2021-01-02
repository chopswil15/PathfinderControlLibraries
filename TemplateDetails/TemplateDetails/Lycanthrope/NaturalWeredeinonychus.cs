using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using TemplateFoundational;

namespace TemplateDetails
{
    public class NaturalWeredeinonychus : TemplateFoundation
    {
        public NaturalWeredeinonychus()
        {
            Name = "Natural Weredeinonychus"; 
            TemplateFoundationType = FoundationType.Inherited;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            TemplateCommon.AddDR(MonSB, "silver", 10);

            return MonSB;
        }
    }
}
