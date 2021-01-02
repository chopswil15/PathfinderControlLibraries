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
    public class Ghost : TemplateFoundation.TemplateFoundation
    {
        public Ghost()
        {
            Name = "Ghost";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            int ChaValue = MonSB.GetAbilityScoreValue(StatBlockInfo.CHA) + 4;
            int ChaMod = StatBlockInfo.GetAbilityModifier(ChaValue);
            TemplateCommon.ChangeHD(MonSB, StatBlockInfo.HitDiceCategories.d8);
            // MonSB.AbilitiyScores
           // MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "deflection", ChaMod, true);
            return MonSB;
        }
    }
}
