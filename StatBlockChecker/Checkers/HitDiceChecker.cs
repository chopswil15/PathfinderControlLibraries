using ClassManager;
using CommonStatBlockInfo;
using CommonStrings;
using PathfinderGlobals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace StatBlockChecker
{
    public class HitDiceChecker : IHitDiceChecker
    {
        private List<StatBlockInfo.HDBlockInfo> _hitDiceBlockInfo;
        private ISBCheckerBaseInput _sbCheckerBaseInput;

        public HitDiceChecker(ISBCheckerBaseInput sbCheckerBaseInput)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _hitDiceBlockInfo = new List<StatBlockInfo.HDBlockInfo>();
        }
        public IHitDiceCheckerOutput CheckHDValue()
        {
            HitDiceCheckerOutput output = new HitDiceCheckerOutput();
            try
            {
                string CheckName = "HD";
                string holdHD = _sbCheckerBaseInput.MonsterSB.HD;
                holdHD = holdHD.Replace(PathfinderConstants.PAREN_LEFT, string.Empty).Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Replace(" plus ", "+");
                int Pos = holdHD.IndexOf("temporary");
                if (Pos != -1)
                {
                    Pos = holdHD.LastIndexOf("+", Pos);
                    holdHD = holdHD.Substring(0, Pos).Trim();
                }

                Pos = holdHD.LastIndexOf("+");
                int Pos2 = holdHD.LastIndexOf("d");
                output.HDModifier = _sbCheckerBaseInput.MonsterSB.HDMod();
                string racialHD = _sbCheckerBaseInput.CharacterClasses.HasClass("animal companion") || _sbCheckerBaseInput.CharacterClasses.HasClass("eidolon")
                         ? string.Empty : _sbCheckerBaseInput.Race_Base.RacialHD();
                racialHD = HandleAnimatedObject(racialHD);

                if (Pos >= 0 && Pos2 < Pos)
                {
                    //string temp = holdHD.Substring(Pos);

                    //  _hdModifier = Convert.ToInt32(temp);
                    // holdHD = holdHD.Replace(temp, string.Empty).Trim();
                    holdHD = holdHD.Substring(0, Pos);
                }


                List<string> hitDiceBlocks = FindHDBlocks(holdHD);
                
                StatBlockInfo.HDBlockInfo racialHDInfo = new StatBlockInfo.HDBlockInfo();
                int Count = 0;
                bool Found = false;
                bool Pass = true;

                HandleHDBlocks(hitDiceBlocks);

                if (racialHD.Length > 0)
                {
                    racialHDInfo.ParseHDBlock(racialHD);
                }

                output.TotalHd = 0;
                foreach (StatBlockInfo.HDBlockInfo tempBlock in _hitDiceBlockInfo)
                {
                    output.TotalHd += tempBlock.Multiplier;
                    if (racialHD.Length > 0 && tempBlock.HDType != racialHDInfo.HDType && tempBlock.Multiplier != racialHDInfo.Multiplier)
                    {
                        output.TotalHdNonRacial += tempBlock.Multiplier;
                    }
                }


                List<string> foundClassesList = new List<string>();
                StatBlockInfo.HDBlockInfo tempHDInfo = new StatBlockInfo.HDBlockInfo();
                List<ClassWrapper> tempCharacterClasses = new List<ClassWrapper>(_sbCheckerBaseInput.CharacterClasses.Classes);
                HandleHdInfos(ref tempHDInfo, ref Count, ref Found, foundClassesList, tempCharacterClasses);

                tempHDInfo = HandleMythic(tempHDInfo, foundClassesList, tempCharacterClasses);

                int hdMultiplier = 0;
                if (racialHD.Length > 0)
                {
                    foreach (StatBlockInfo.HDBlockInfo tempBlock in _hitDiceBlockInfo)
                    {
                        hdMultiplier = tempBlock.Multiplier;
                        if (racialHDInfo.HDType == tempBlock.HDType)
                        {
                            hdMultiplier -= racialHDInfo.Multiplier;
                        }
                        if (hdMultiplier != 0)
                        {
                            _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Wrong HD for " + tempBlock.HDType.ToString());
                            Pass = false;
                        }
                    }
                }

                if (foundClassesList.Count != _sbCheckerBaseInput.CharacterClasses.Classes.Count)
                {
                    string hold = string.Empty;
                    foreach (ClassWrapper wrapper in tempCharacterClasses)
                    {
                        hold += wrapper.Name + ",";
                    }
                    hold = hold.Substring(0, hold.Length - 1);
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Missing HD for Classes " + hold);
                    Pass = false;
                }


                if (Pass)
                {
                    _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
                }
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("CheckHDValue", ex.Message);
            }

            return output;
        }

        private string HandleAnimatedObject(string RacialHD)
        {
            if (_sbCheckerBaseInput.MonsterSB.Race.ToLower() == "animated object")
            {
                switch (StatBlockInfo.GetSizeEnum(_sbCheckerBaseInput.MonsterSB.Size))
                {
                    case StatBlockInfo.SizeCategories.Tiny:
                        RacialHD = "1d10";
                        break;
                    case StatBlockInfo.SizeCategories.Small:
                        RacialHD = "2d10+10";
                        break;
                    case StatBlockInfo.SizeCategories.Medium:
                        RacialHD = "3d10+20";
                        break;
                    case StatBlockInfo.SizeCategories.Large:
                        RacialHD = "4d10+30";
                        break;
                    case StatBlockInfo.SizeCategories.Huge:
                        RacialHD = "7d10+40";
                        break;
                    case StatBlockInfo.SizeCategories.Gargantuan:
                        RacialHD = "10d10+60";
                        break;
                    case StatBlockInfo.SizeCategories.Colossal:
                        RacialHD = "13d10+80";
                        break;
                }
            }

            return RacialHD;
        }

        private StatBlockInfo.HDBlockInfo HandleMythic(StatBlockInfo.HDBlockInfo tempHDInfo, List<string> FoundClasses, List<ClassWrapper> tempCharacterClasses)
        {
            if (_sbCheckerBaseInput.MonsterSBSearch.IsMythic && tempCharacterClasses.Any())
            {
                tempHDInfo = new StatBlockInfo.HDBlockInfo();
                tempHDInfo.HDType = StatBlockInfo.HitDiceCategories.None;
                for (int a = 0; a < _sbCheckerBaseInput.CharacterClasses.Classes.Count; a++)
                {
                    ClassWrapper wrapper = _sbCheckerBaseInput.CharacterClasses.Classes[a];

                    if (tempHDInfo.HDType == _sbCheckerBaseInput.CharacterClasses.GetClassHDType(wrapper.Name))
                    {
                        FoundClasses.Add(wrapper.Name);
                        tempCharacterClasses.Remove(wrapper);
                    }
                }
            }

            return tempHDInfo;
        }

        private void HandleHdInfos(ref StatBlockInfo.HDBlockInfo tempHDInfo, ref int Count, ref bool Found, List<string> FoundClasses, List<ClassWrapper> tempCharacterClasses)
        {
            for (int a = 0; a < _hitDiceBlockInfo.Count; a++)
            {
                tempHDInfo = _hitDiceBlockInfo[a];
                foreach (ClassWrapper wrapper in _sbCheckerBaseInput.CharacterClasses.Classes)
                {
                    if (tempHDInfo.HDType == _sbCheckerBaseInput.CharacterClasses.GetClassHDType(wrapper.Name))
                    {
                        if (wrapper.Name == "Eidolon")
                        {
                            tempHDInfo.Multiplier -= StatBlockInfo.ComputeEidolonHD(wrapper.Level);
                        }
                        else
                        {
                            tempHDInfo.Multiplier -= wrapper.Level;
                        }
                        Found = true;
                        FoundClasses.Add(wrapper.Name);
                        tempCharacterClasses.Remove(wrapper);
                    }
                }
                if (Found)
                {
                    Found = false;
                    _hitDiceBlockInfo[Count] = tempHDInfo;
                }
                Count++;
            }
        }

        private void HandleHDBlocks(List<string> HDBlocks)
        {
            StatBlockInfo.HDBlockInfo tempHDInfo = new StatBlockInfo.HDBlockInfo();
            foreach (string oneBlock in HDBlocks)
            {
                tempHDInfo.ParseHDBlock(oneBlock);
                bool HDTypeFound = false;
                StatBlockInfo.HDBlockInfo HoldHD2 = new StatBlockInfo.HDBlockInfo();
                foreach (StatBlockInfo.HDBlockInfo tempBlock in _hitDiceBlockInfo)
                {
                    if (tempBlock.HDType == tempHDInfo.HDType)  //combine like HD types, i.e. 3d10+7d10 -> 10d10
                    {
                        HoldHD2 = tempBlock;
                        tempHDInfo.Multiplier += tempBlock.Multiplier;
                        tempHDInfo.Modifier += tempBlock.Modifier;
                        HDTypeFound = true;
                        break;
                    }
                }
                if (HDTypeFound) _hitDiceBlockInfo.Remove(HoldHD2);
                _hitDiceBlockInfo.Add(tempHDInfo);
            }
        }

        private static List<string> FindHDBlocks(string HoldHD)
        {
            List<string> HDBlocks = new List<string>();
            List<string> blockParts = HoldHD.Split('+').ToList();

            foreach(var part in blockParts)
            {
                if (part.Contains("d"))
                {
                    HDBlocks.Add(part.Trim());
                }
                else
                {
                    HDBlocks[HDBlocks.Count - 1] += "+" + part.Trim();
                }

            }

            return HDBlocks;
        }

        //    private static List<string> FindHDBlocks(string HoldHD)
        //{
        //    List<string> HDBlocks = new List<string>();
        //    string temp = string.Empty;
        //    int Pos = 0;
        //    int Pos2 = 0;
        //    int DPos = 0;
        //    int Count = 0;
        //    Pos = HoldHD.IndexOf(";");
        //    if (Pos >= 0)
        //    {
        //        temp = HoldHD.Substring(0, Pos + 1);
        //        HoldHD = HoldHD.Replace(temp, string.Empty);
        //    }
        //    Pos = HoldHD.IndexOf(",");
        //    if (Pos >= 0)
        //    {
        //        temp = HoldHD.Substring(0, Pos + 1);
        //        HoldHD = HoldHD.Replace(temp, string.Empty);
        //    }
        //    HoldHD = Utility.RemoveParentheses(HoldHD);
        //    int HD_TypeCount = HoldHD.Length - HoldHD.Replace("d", string.Empty).Length;

        //    for (int a = 1; a <= HD_TypeCount; a++)
        //    {
        //        DPos = HoldHD.IndexOf("d");
        //        Pos = HoldHD.IndexOf("+", DPos);
        //        if (Pos == -1) // just one HD, no plus
        //        {
        //            HDBlocks.Add(HoldHD);
        //        }
        //        else
        //        {
        //            Pos2 = HoldHD.IndexOf("d", DPos + 1); //next d
        //            Pos = HoldHD.IndexOf("+", DPos);
        //            if (Pos2 == -1) // no more HD
        //            {
        //                HDBlocks.Add(HoldHD);
        //            }
        //            else
        //            {
        //                temp = HoldHD.Substring(Pos, Pos2 - Pos);
        //                Count = temp.Length - temp.Replace("+", string.Empty).Length;
        //                if (Count > 1) //mod of HD, 2d10 +5 + 2d8
        //                {
        //                    Pos = HoldHD.IndexOf("+", Pos + 1); //next plus
        //                    temp = HoldHD.Substring(0, Pos);
        //                    HDBlocks.Add(temp);
        //                    HoldHD = HoldHD.Replace(temp, string.Empty).Trim();
        //                    Pos = HoldHD.IndexOf("+");
        //                    if (Pos == 0) //remove leading "+"
        //                    {
        //                        HoldHD = HoldHD.Substring(1);
        //                    }
        //                }
        //                else //no mod, 2d10 + 2d8
        //                {
        //                    temp = HoldHD.Substring(0, Pos);
        //                    HDBlocks.Add(temp);
        //                    HoldHD = HoldHD.ReplaceFirst(temp, string.Empty).Trim();
        //                    Pos = HoldHD.IndexOf("+");
        //                    if (Pos == 0) //remove leading "+"
        //                    {
        //                        HoldHD = HoldHD.Substring(1);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return HDBlocks;
        //}
    }

    public class HitDiceCheckerOutput : IHitDiceCheckerOutput
    {
        public int HDModifier { get; set; }
        public int TotalHd { get; set; }
        public int TotalHdNonRacial { get; set; }
    }
}
