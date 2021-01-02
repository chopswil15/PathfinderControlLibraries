using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using TemplateFoundational;

namespace TemplateDetails
{
    public class Swarm  : TemplateFoundation
    {
        public Swarm()
        {
            Name = "Swarm";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {

            return MonSB;
        }
    }
}
