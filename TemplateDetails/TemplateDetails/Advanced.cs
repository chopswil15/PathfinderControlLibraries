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
    public class Advanced : TemplateFoundation.TemplateFoundation
    {
        public Advanced()
        {
            Name = "Advanced";
            TemplateFoundationType = FoundationType.Simple;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", 2, true);
            return MonSB;
        }
    }
}
