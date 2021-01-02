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
    public class AdvancedHD : TemplateFoundation.TemplateFoundation
    {
        public AdvancedHD()
        {
            Name = "Advanced HD";
            TemplateFoundationType = FoundationType.Simple;
        }

        public override MonsterStatBlock ApplyAdvancedHDTemplate(MonsterStatBlock MonSB, string NewSizeValue, int HighCR, int LowCR)
        {
            if (NewSizeValue.Length > 0)
            {
                ComputeSizeChange(MonSB, NewSizeValue);
            }

            //natural armor change for CR increase
            List<int> HigherCR = new List<int> {0, 1, 2, 1, 2,1,1,1,1,2,1,1,2,1,1,1,1,1,1,1,2,2 }; //0 for 0th spot

            int diff = HighCR - LowCR - 1;

            int lowCRIndex = LowCR + 1;
            int highCRIndex = diff + lowCRIndex;
            int total = 0;

            for (int a = lowCRIndex; a <= highCRIndex; a++)
            {
                total += HigherCR[a];
            }

            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", total, true);

            return MonSB;
        }

        private void ComputeSizeChange(MonsterStatBlock MonSB, string NewSizeValue)
        {
            StatBlockInfo.SizeCategories RaceSize = StatBlockInfo.GetSizeEnum(MonSB.Size);
            StatBlockInfo.SizeCategories NewSize = StatBlockInfo.GetSizeEnum(NewSizeValue);

            int diff = NewSize - RaceSize;
            int naturalArmorChange = 0;
            int sign = 0;

            while (diff != 0)
            {
                if (diff < 0) //smaller
                {
                    RaceSize--;
                    sign = -1;
                }
                else //bigger
                {
                    RaceSize++;
                    sign = 1;
                }

                //apply new size changes
                switch (RaceSize)
                {
                    case StatBlockInfo.SizeCategories.Diminutive:
                    case StatBlockInfo.SizeCategories.Tiny:
                    case StatBlockInfo.SizeCategories.Small:
                    case StatBlockInfo.SizeCategories.Medium:
                        break;
                    case StatBlockInfo.SizeCategories.Large:
                        naturalArmorChange += 2 * sign;
                        break;
                    case StatBlockInfo.SizeCategories.Huge:
                        naturalArmorChange += 3 * sign;
                        break;
                    case StatBlockInfo.SizeCategories.Gargantuan:
                        naturalArmorChange += 4 * sign;
                        break;
                    case StatBlockInfo.SizeCategories.Colossal:
                        naturalArmorChange += 5 * sign;
                        break;
                }

                diff = NewSize - RaceSize;
            }

            if (naturalArmorChange > 0)
            {
                MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", naturalArmorChange, true);
            }
        }
    }
}
