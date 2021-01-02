using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatBlockCommon.Monster_SB;

namespace TemplateDetails
{
    public class Ghoulish : TemplateFoundation.TemplateFoundation
    {
        public Ghoulish()
        {
            Name = "Ghoulish";
            TemplateFoundationType = FoundationType.Simple;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {            

            return MonSB;
        }
    }
}
