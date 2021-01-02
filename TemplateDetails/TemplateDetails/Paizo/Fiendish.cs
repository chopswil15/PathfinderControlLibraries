using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using CommonStatBlockInfo;
using TemplateFoundational;

namespace TemplateDetails
{
    public class Fiendish : TemplateFoundation
    {
        public Fiendish()
        {
            Name = "Fiendish";
            TemplateFoundationType = FoundationType.Simple;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            int HDValue = MonSB.HDValue();

            if (HDValue < 5)
            {
                TemplateCommon.AddResistance(MonSB, "cold ", 5);
                TemplateCommon.AddResistance(MonSB, "fire ", 5);
            }
            else if (HDValue >= 5 && HDValue <= 10)
            {
                TemplateCommon.AddDR(MonSB, "good", 5);
                TemplateCommon.AddResistance(MonSB, "cold ", 10);
                TemplateCommon.AddResistance(MonSB, "fire ", 10);
            }
            else if (HDValue >= 11)
            {
                TemplateCommon.AddDR(MonSB, "good", 10);
                TemplateCommon.AddResistance(MonSB, "cold ", 15);
                TemplateCommon.AddResistance(MonSB, "fire ", 15);
            }

            int CR_Hold;
            int.TryParse(MonSB.CR, out CR_Hold);
            MonSB.SR = (CR_Hold + 5).ToString();

            return MonSB;
        }
    }
}
