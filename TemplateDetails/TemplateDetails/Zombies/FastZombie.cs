using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using CommonStatBlockInfo;
using TemplateFoundational;


namespace TemplateDetails
{
    public class FastZombie : TemplateFoundation
    {
        public FastZombie()
        {
            Name = "Fast Zombie";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            StatBlockInfo.HDBlockInfo tempHDInfo = new StatBlockInfo.HDBlockInfo();
            tempHDInfo.ParseHDBlock(MonSB.HD);
            tempHDInfo.HDType = StatBlockInfo.HitDiceCategories.d8; //keeps HD, change to d8
            tempHDInfo.Modifier = 0;
            MonSB.HD = tempHDInfo.ToString();

            double HD = Convert.ToInt32(tempHDInfo.Multiplier);
            double temp = (HD * 3) / 4;
            MonSB.BaseAtk = "+" + Math.Floor(temp).ToString();

            temp = HD / 3; //Fort
            MonSB.Fort = "+" + Math.Floor(temp).ToString();

            temp = HD / 3; ///Ref
            MonSB.Ref = "+" + Math.Floor(temp).ToString();

            temp = HD / 2; ///Will
            MonSB.Will = "+" + (Math.Floor(temp) + 2).ToString();

            switch (StatBlockInfo.GetSizeEnum(MonSB.Size))
            {
                case StatBlockInfo.SizeCategories.Tiny:
                    temp = 0;
                    break;
                case StatBlockInfo.SizeCategories.Small:
                    temp = 1;
                    break;
                case StatBlockInfo.SizeCategories.Medium:
                    temp = 2;
                    break;
                case StatBlockInfo.SizeCategories.Large:
                    temp = 3;
                    break;
                case StatBlockInfo.SizeCategories.Huge:
                    temp = 4;
                    break;
                case StatBlockInfo.SizeCategories.Gargantuan:
                    temp = 7;
                    break;
                case StatBlockInfo.SizeCategories.Colossal:
                    temp = 11;
                    break;
                default:
                    temp = 0;
                    break;
            }

            TemplateCommon.ChangeHD(MonSB, StatBlockInfo.HitDiceCategories.d8);

            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", Convert.ToInt32(temp), false);

            MonSB.Skills = string.Empty;
            MonSB.Feats = "ToughnessB";

            return MonSB;
        }
    }
}
