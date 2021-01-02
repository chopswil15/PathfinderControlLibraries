using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using CommonStatBlockInfo;

namespace TemplateDetails
{
    public class HalfCelestial: TemplateFoundation.TemplateFoundation
    {
        public HalfCelestial()
        {
            Name = "Half-Celestial";
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

            TemplateCommon.AddResistance(MonSB, "acid ", 10);
            TemplateCommon.AddResistance(MonSB, "cold ", 10);
            TemplateCommon.AddResistance(MonSB, "electricity ", 10);

             

            return MonSB;
        }
    }
}
