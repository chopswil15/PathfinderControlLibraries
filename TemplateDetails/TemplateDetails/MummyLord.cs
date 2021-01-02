using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using CommonStatBlockInfo;

namespace TemplateDetails
{
    public class MummyLord : TemplateFoundation.TemplateFoundation
    {
        public MummyLord()
        {
            Name = "Mummy Lord";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            TemplateCommon.ChangeHD(MonSB, StatBlockInfo.HitDiceCategories.d8);

            TemplateCommon.AddDR(MonSB, "-", 10);

            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "ToughnessB");

            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Intimidate");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Sense Motive");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Stealth");

            return MonSB;
        }
    }
}
