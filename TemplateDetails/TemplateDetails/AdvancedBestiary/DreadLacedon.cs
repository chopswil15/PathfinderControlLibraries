using StatBlockCommon.Monster_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateFoundational;

namespace TemplateDetails
{
    public class DreadLacedon : TemplateFoundation
    {
        public DreadLacedon()
        {
            Name = "Dread Lacedon";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {

            return MonSB;
        }
    }
}
