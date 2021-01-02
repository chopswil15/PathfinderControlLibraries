using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using StatBlockCommon;
using StatBlockCommon.Monster_SB;

namespace TemplateDetails
{
    public class Ogrekin : TemplateFoundation.TemplateFoundation
    {
        public Ogrekin()
        {
            Name = "Ogrekin";
            TemplateFoundationType = FoundationType.Inherited;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", 3, true);

            return MonSB;
        }
    }
}
