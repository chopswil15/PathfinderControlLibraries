using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;

namespace TemplateDetails
{
    public class Awakened : TemplateFoundation.TemplateFoundation
    {
        public Awakened()
        {
            Name = "Awakened"; 
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {


            return MonSB;
        }
    }
}
