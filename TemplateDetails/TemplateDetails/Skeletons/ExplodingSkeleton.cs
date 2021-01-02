using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using CommonStatBlockInfo;
using TemplateFoundational;


namespace TemplateDetails
{
    public class ExplodingSkeleton : TemplateFoundation
    {
        public ExplodingSkeleton()
        {
            Name = "Exploding Skeleton";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB, List<string> StackableTemplates)
        {
           MonSB = ApplyTemplate(MonSB); //base skeleteton

          

            return MonSB;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            return TemplateCommon.AppyBaseSkeletonTemplate(MonSB);       
        }
    }
}
