using StatBlockCommon.Monster_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateDetails
{
    public class Divine : TemplateFoundation.TemplateFoundation
    {
        public Divine()
        {
            Name = "Divine";
            TemplateFoundationType = FoundationType.Simple;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {

            return MonSB;
        }
    }
}
