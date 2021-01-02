using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using CommonStatBlockInfo;
using TemplateFoundational;


namespace TemplateDetails
{
    public class WormThatWalks : TemplateFoundation
    {
        public WormThatWalks()
        {
            Name = "Worm That Walks";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            TemplateCommon.ChangeHD(MonSB, StatBlockInfo.HitDiceCategories.d8);

            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", 0, false);


            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "DiehardB");

            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Stealth");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Perception");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Sense Motive");

            return MonSB;
        }
    }
}
