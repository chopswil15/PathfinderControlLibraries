using StatBlockCommon.Monster_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateFoundational;

namespace TemplateDetails
{
    public class DreadGhast : TemplateFoundation
    {
        public DreadGhast()
        {
            Name = "Dread Ghast";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {

            return MonSB;
        }
    }
}
