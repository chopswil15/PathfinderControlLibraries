using StatBlockCommon.Monster_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateDetails
{
    public class Degenerate : TemplateFoundation.TemplateFoundation
    {
        public Degenerate()
        {
            Name = "Degenerate";
            TemplateFoundationType = FoundationType.Simple;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
           
            return MonSB;
        }
   
    }
}
