using StatBlockCommon.Monster_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateFoundational;

namespace TemplateDetails
{
    public class AfflictedWereassassinbug : TemplateFoundation
    {
        public AfflictedWereassassinbug()
        {
            Name = "Afflicted Wereassassin Bug";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            if (!MonSB.AlternateNameForm.Contains("Human Form"))
            {
                TemplateCommon.AddDR(MonSB, "silver", 10);
            }

            return MonSB;
        }
    }
}
