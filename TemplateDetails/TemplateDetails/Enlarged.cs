using StatBlockCommon.Monster_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateDetails
{
    public class Enlarged : TemplateFoundation.TemplateFoundation
    {
        public Enlarged()
        {
            //not really a template but used to handle SB with enlarge person spell cast on it
            Name = "Enlarged";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
          

            return MonSB;
        }
    }
}
