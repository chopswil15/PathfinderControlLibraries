using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using CommonStatBlockInfo;
using TemplateFoundational;

namespace TemplateDetails
{
    public class Giant : TemplateFoundation
    {
        public Giant()
        {
            Name = "Giant";
            TemplateFoundationType = FoundationType.Simple;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", 3, true);

            return MonSB;
        }
    }
}
