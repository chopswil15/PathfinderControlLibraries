using StatBlockCommon.Monster_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateDetails
{
    public class Petitioner : TemplateFoundation.TemplateFoundation
    {
        public Petitioner()
        {
            Name = "Petitioner";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            return MonSB;
        }    
    }
}
