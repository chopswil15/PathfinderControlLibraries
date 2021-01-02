using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using TemplateFoundational;

namespace TemplateDetails
{
    public class HalfSuccubus : TemplateFoundation
    {
        public HalfSuccubus()
        {
            Name = "Half-Succubus"; //Demon-Possessed Creature
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {


            return MonSB;
        }
    }
}
