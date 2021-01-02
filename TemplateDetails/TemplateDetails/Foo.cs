using StatBlockCommon.Monster_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateDetails
{
    public class Foo : TemplateFoundation.TemplateFoundation
    {
        public Foo()
        {
            Name = "Foo";
            TemplateFoundationType = FoundationType.Inherited;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {

            return MonSB;
        }
    }
}
