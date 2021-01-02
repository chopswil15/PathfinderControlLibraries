using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using CommonStatBlockInfo;
using TemplateFoundational;


namespace TemplateDetails
{
    public class SkeletalChampion : TemplateFoundation
    {
        public SkeletalChampion()
        {
            Name = "Skeletal Champion";
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
                case StatBlockInfo.SizeCategories.Large:
                    temp = 2;
                    break;
                case StatBlockInfo.SizeCategories.Huge:
                    temp = 3;
                    break;
                case StatBlockInfo.SizeCategories.Gargantuan:
                    temp = 6;
                    break;
                case StatBlockInfo.SizeCategories.Colossal:
                    temp = 10;
                    break;
                default:
                    temp = 0;
                    break;
            }


            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", Convert.ToInt32(temp), false);

            TemplateCommon.AddDR(MonSB, "bludgeoning", 5);
            
            return MonSB;
        }
    }
}
