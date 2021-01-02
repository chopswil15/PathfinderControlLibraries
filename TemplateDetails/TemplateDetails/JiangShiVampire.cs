using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using CommonStatBlockInfo;

namespace TemplateDetails
{
    public class JiangShiVampire: TemplateFoundation.TemplateFoundation
    {
        public JiangShiVampire()
        {
            Name = "Jiang- Shi Vampire";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", 2, true);

            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "AlertnessB");
            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "DodgeB");
            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "MobilityB");
            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "Skill Focus (Acrobatics)B");
            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "Spring AttackB");

            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Acrobatics");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Perception");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Stealth");

            TemplateCommon.AddDR(MonSB, "magic and slashing", 10);

            TemplateCommon.AddResistance(MonSB, "cold ", 20);

            return MonSB;
        }
    }
}
