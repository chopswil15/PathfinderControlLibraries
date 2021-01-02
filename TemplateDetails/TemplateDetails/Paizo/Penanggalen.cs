using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using CommonStatBlockInfo;
using TemplateFoundational;


namespace TemplateDetails
{
    public class Penanggalen : TemplateFoundation
    {
        public Penanggalen()
        {
            Name = "Penanggalen";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", 6, true);
            TemplateCommon.ChangeHD(MonSB, StatBlockInfo.HitDiceCategories.d8);

            TemplateCommon.AddDR(MonSB, "silver and slashing", 5);
            TemplateCommon.AddResistance(MonSB, "cold ", 10);
            TemplateCommon.AddResistance(MonSB, "fire ", 10);
            
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Bluff");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Fly");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Perception");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Sense Motive");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Stealth");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Knowledge (arcana)");

            return MonSB;
        }
    }
}
