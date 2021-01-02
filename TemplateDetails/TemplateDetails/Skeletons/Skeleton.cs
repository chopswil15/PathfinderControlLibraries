using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TemplateFoundational;
using StatBlockCommon;
using CommonStatBlockInfo;
using StatBlockCommon.Monster_SB;

namespace TemplateDetails
{
    public class Skeleton : TemplateFoundation
    {
        public Skeleton()
        {
            Name = "Skeleton";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB, List<string> StackableTemplates)
        {
           MonSB = ApplyTemplate(MonSB); //base skeleteton

            

            foreach(string template in StackableTemplates)
            {
                switch (template.ToLower())
                {
                    case "acid":
                        break;
                    case "bloody":
                        break;
                    case "electric":
                        break;
                }
            }

            return MonSB;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            return TemplateCommon.AppyBaseSkeletonTemplate(MonSB);
        }
    }
}
