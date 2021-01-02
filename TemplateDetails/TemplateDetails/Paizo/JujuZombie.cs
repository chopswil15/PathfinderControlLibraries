using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TemplateFoundational;
using StatBlockCommon;
using CommonStatBlockInfo;
using StatBlockCommon.Monster_SB;

namespace TemplateDetails
{
    public class JujuZombie : TemplateFoundation
    {
        public JujuZombie()
        {
            Name = "Juju Zombie";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            TemplateCommon.ChangeHD(MonSB, StatBlockInfo.HitDiceCategories.d8);

            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", 3, true);

            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Climb");

            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "Improved InitiativeB");
            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "ToughnessB");

            TemplateCommon.AddDR(MonSB, "magic and slashing", 5);

            TemplateCommon.AddResistance(MonSB, "fire ", 10);

            return MonSB;
        }
    }
}
