using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using CommonStatBlockInfo;

namespace TemplateDetails
{
    public class Graveknight : TemplateFoundation.TemplateFoundation
    {
        public Graveknight()
        {
            Name = "Graveknight";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", 4, true);

            TemplateCommon.ChangeHD(MonSB, StatBlockInfo.HitDiceCategories.d8);

            TemplateCommon.AddDR(MonSB, "magic", 10);

            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Perception");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Intimidatee");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Ride");

            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "Mounted CombatB");
            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "Improved InitiativeB");
            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "Ride-By AttackB");
            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "ToughnessB");

            return MonSB;
        }
    }
}
