using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using CommonStatBlockInfo;

namespace TemplateDetails
{
    public class BrokenSoul  : TemplateFoundation.TemplateFoundation
    {
        public BrokenSoul()
        {
            Name = "Broke Soul";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", 4, true);

            TemplateCommon.AddDR(MonSB, "-", 5);
            
            TemplateCommon.AddResistance(MonSB, "cold ", 5);
            TemplateCommon.AddResistance(MonSB, "electricity ", 5);
            TemplateCommon.AddResistance(MonSB, "acid ", 5);
            TemplateCommon.AddResistance(MonSB, "fire ", 5);
            TemplateCommon.AddResistance(MonSB, "sonic ", 5);

            return MonSB;
        }
    }
}
