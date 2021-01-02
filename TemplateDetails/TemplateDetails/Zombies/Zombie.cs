using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using CommonStatBlockInfo;
using TemplateFoundational;


namespace TemplateDetails
{
    public class Zombie : TemplateFoundation
    {
        public Zombie()
        {
            Name = "Zombie";
            TemplateFoundationType = FoundationType.Acquired;
        }        

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB, List<string> StackableTemplates)
        {
            MonSB = ApplyTemplate(MonSB); //base zombie

            foreach (string template in StackableTemplates)
            {
                switch (template.ToLower())
                {
                    case "fast":
                        break;
                    case "plague":
                        break;
                    case "void":
                        break;
                    case "yellow musk":
                        break;
                    case "juju":
                        break;
                    case "relentless":
                        break;
                    case "brain-eating":
                        break;
                    case "hungry":
                        break;
                }
            }

            return MonSB;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            return TemplateCommon.AppyBaseZombieTemplate(MonSB);
        }
    }
}
