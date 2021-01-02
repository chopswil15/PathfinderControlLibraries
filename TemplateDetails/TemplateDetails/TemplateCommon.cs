using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon;
using CommonStatBlockInfo;
using StatBlockCommon.Monster_SB;
using Utilities;

namespace TemplateDetails
{
    public static class TemplateCommon
    {
        public static void ChangeHD(MonsterStatBlock MonSB, StatBlockInfo.HitDiceCategories NewHDCategory)
        {
            if (!MonSB.DontUseRacialHD)
            {
                StatBlockInfo.HDBlockInfo tempHDInfo = new StatBlockInfo.HDBlockInfo();
                tempHDInfo.ParseHDBlock(MonSB.HD);
                tempHDInfo.HDType = NewHDCategory;
                tempHDInfo.Modifier = 0;
                MonSB.HD = tempHDInfo.ToString();
            }
        }


        public static void AddDR(MonsterStatBlock MonSB, string Type, int Value)
        {
            if (MonSB.DR.Contains(Type))
            {

            }
            else
            {
                if (MonSB.DR.Length == 0)
                    MonSB.DR = Value.ToString() + "/" + Type;
                else
                    MonSB.DR = MonSB.DR + "," + Value.ToString() + "/" + Type;
            }
        }

        public static void AddResistance(MonsterStatBlock MonSB, string Type, int Value)
        {
            if (MonSB.Resist.Contains(Type))
            {
                List<string> ResistList = MonSB.Resist.Split(',').ToList();
                for (int i = 0; i < ResistList.Count; i++)
                {
                    if (ResistList[i].Contains(Type))
                    {
                        string temp = ResistList[i].Replace(Type, string.Empty).Trim();
                        int NewValue = int.Parse(temp);
                        NewValue += Value;
                        ResistList[i] = Type + Utility.SPACE + NewValue.ToString();
                    }
                }
            }
            else
            {
                if (MonSB.Resist.Length == 0)
                    MonSB.Resist = Type + Utility.SPACE + Value.ToString();
                else
                    MonSB.Resist = MonSB.Resist + "," + Type + Utility.SPACE + Value.ToString();
            }
        }


        public static MonsterStatBlock AppyBaseSkeletonTemplate(MonsterStatBlock MonSB)
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

            temp = (HD / 2); ///Will
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

            TemplateCommon.AddDR(MonSB, "bludgeoning", 5);

            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", Convert.ToInt32(temp), false);


            MonSB.Skills = string.Empty;
            MonSB.Feats = "Improved Initiative";

            return MonSB;
        }

        public static MonsterStatBlock AppyBaseZombieTemplate(MonsterStatBlock MonSB)
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

            TemplateCommon.AddDR(MonSB, "slashing", 5);

            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", Convert.ToInt32(temp), false);

            MonSB.Skills = string.Empty;
            MonSB.Feats = "ToughnessB";

            return MonSB;
        }
    }
}
