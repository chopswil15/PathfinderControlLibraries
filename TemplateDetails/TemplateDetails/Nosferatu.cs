using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using CommonStatBlockInfo;

namespace TemplateDetails
{
    public class Nosferatu : TemplateFoundation.TemplateFoundation
    {
        public Nosferatu()
        {
            Name = "Nosferatu";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", 8, true);

            TemplateCommon.ChangeHD(MonSB, StatBlockInfo.HitDiceCategories.d8);

            TemplateCommon.AddDR(MonSB, "wood and piercing", 5);

            TemplateCommon.AddResistance(MonSB, "cold ", 10);
            TemplateCommon.AddResistance(MonSB, "electricity ", 10);
            TemplateCommon.AddResistance(MonSB, "sonic ", 10);

            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Perception");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Sense Motive");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Stealth");

            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "AlertnessB");
            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "Improved InitiativeB");
            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "Lightning ReflexesB");
            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "Skill Focus()B");
            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "Skill Focus()B");

            return MonSB;
        }
    }
}
