using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TemplateFoundation;
using StatBlockCommon;
using CommonStatBlockInfo;
using StatBlockCommon.Monster_SB;

namespace TemplateDetails
{
    public class HalfFiend : TemplateFoundation.TemplateFoundation
    {
        public HalfFiend()
        {
            Name = "Half-Fiend";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", 1, true);

            if (MonSB.HDValue() <= 11)
            {
                TemplateCommon.AddDR(MonSB, "magic", 5);
            }
            else
            {
                TemplateCommon.AddDR(MonSB, "magic", 10);
            }

            TemplateCommon.AddResistance(MonSB, "cold ", 10);
            TemplateCommon.AddResistance(MonSB, "electricity ", 10);
            TemplateCommon.AddResistance(MonSB, "acid ", 10);
            TemplateCommon.AddResistance(MonSB, "fire ", 10);

            int CR_Hold;
            int.TryParse(MonSB.CR,out CR_Hold);
            if (CR_Hold <= 4)
            {
                MonSB.SR = ((CR_Hold - 1) + 11).ToString();
            }
            else if (CR_Hold <= 10)
            {
                MonSB.SR = ((CR_Hold - 2) + 11).ToString();
            }
            else
            {
                MonSB.SR = ((CR_Hold - 2) + 11).ToString();
            }

            return MonSB;
        }
    }
}
