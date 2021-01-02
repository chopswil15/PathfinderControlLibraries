using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using CommonStatBlockInfo;
using TemplateFoundational;


namespace TemplateDetails
{
    public class Mummified : TemplateFoundation
    {
        public Mummified()
        {
            Name = "Mummified";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            TemplateCommon.ChangeHD(MonSB, StatBlockInfo.HitDiceCategories.d8);

            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", 4, true);

            TemplateCommon.AddDR(MonSB, "-", 5);

            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+4 Stealth");

            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "ToughnessB");
            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, " Improved Natural AttackB");

            return MonSB;
        }
    
    }
}
