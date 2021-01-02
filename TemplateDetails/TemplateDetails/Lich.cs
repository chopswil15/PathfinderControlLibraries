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
    public class Lich : TemplateFoundation.TemplateFoundation
    {
        public Lich()
        {
            Name = "Lich";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {

            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", 5, true);
            
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Perception");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Sense Motive");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Stealth");

            TemplateCommon.AddDR(MonSB, "bludgeoning and magic", 15);

            return MonSB;
        }
    }
}
