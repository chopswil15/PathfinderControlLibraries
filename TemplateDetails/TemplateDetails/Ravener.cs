using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using CommonStatBlockInfo;

namespace TemplateDetails
{
    public class Ravener : TemplateFoundation.TemplateFoundation
    {
        public Ravener()
        {
            Name = "Ravener";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            TemplateCommon.ChangeHD(MonSB, StatBlockInfo.HitDiceCategories.d8);

            int ACMod = StatBlockInfo.GetAbilityModifier(MonSB.GetAbilityScoreValue(StatBlockInfo.AbilityName.Charisma)) / 2;
            if (ACMod < 1) ACMod = 1;

            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "deflection", ACMod, true);

            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Perception");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Intimidate");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Stealth");


            return MonSB;
        }
    }
}
