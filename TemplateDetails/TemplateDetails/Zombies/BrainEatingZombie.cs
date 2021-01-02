using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using TemplateFoundational;


namespace TemplateDetails
{
    public class BrainEatingZombie : TemplateFoundation
    {
        public BrainEatingZombie()
        {
            Name = "Brain-Eating Zombie";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {

            return MonSB;
        }
    }
}
