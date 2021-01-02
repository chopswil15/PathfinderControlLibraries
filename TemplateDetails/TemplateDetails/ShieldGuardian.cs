using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;

namespace TemplateDetails
{
    public class ShieldGuardian : TemplateFoundation.TemplateFoundation
    {
        public ShieldGuardian()
        {
            Name = "Shield Guardian";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            return MonSB;
        }
    }
}
