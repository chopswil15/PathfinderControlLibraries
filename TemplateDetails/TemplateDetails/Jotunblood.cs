using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonStatBlockInfo;
using StatBlockCommon;
using StatBlockCommon.Monster_SB;

namespace TemplateDetails
{
    public class Jotunblood : TemplateFoundation.TemplateFoundation
    {
        public Jotunblood()
        {
            Name = "Jotunblood";
            TemplateFoundationType = FoundationType.Inherited;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            string size = MonSB.Size;
            int naturalMod = 0;

            switch (size)
            {
                case "Large":
                    naturalMod = 6;
                    break;
                case "Huge":
                    naturalMod = 7;
                    break;
                case "Gargantuan":
                    naturalMod = 8;
                    break;
                case "Colossal":
                    naturalMod = 9;
                    break;
            }

            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", naturalMod, true);

            return MonSB;
        }
    }
}
