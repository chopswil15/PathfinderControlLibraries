using CommonStatBlockInfo;
using System;
using Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathfinderGlobals;

namespace StatBlockChecker.Checkers
{
    public class SpaceReachChecker : ISpaceReachChecker
    {
        private SBCheckerBaseInput _sbCheckerBaseInput;

        public SpaceReachChecker(SBCheckerBaseInput sbCheckerBaseInput)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
        }

        public void CheckSpaceReach()
        {
            string CheckName = "CheckSpaceReach";
            StatBlockInfo.SizeCategories monsterSize = StatBlockInfo.GetSizeEnum(_sbCheckerBaseInput.MonsterSB.Size);
            string reachTemp = _sbCheckerBaseInput.MonsterSB.Reach.Replace("ft.", string.Empty);
            int Pos = reachTemp.IndexOf(PathfinderConstants.PAREN_LEFT);
            if (Pos > 0)
            {
                reachTemp = reachTemp.Substring(0, Pos).Trim();
            }
            string spaceTemp = _sbCheckerBaseInput.MonsterSB.Space.Replace("ft.", string.Empty);
            spaceTemp = spaceTemp.Replace("feet", string.Empty);

            bool spaceHalf = false;
            ReplaceHalfString(ref spaceTemp, ref spaceHalf);
            spaceTemp = spaceTemp.Trim();
            if (spaceTemp.Length == 0) spaceTemp = "0";
            bool reachHalf = false;
            ReplaceHalfString(ref reachTemp, ref reachHalf);
            int ReachSB = int.Parse(reachTemp);
            int SpaceSB = 0;

            try
            {
                SpaceSB = int.Parse(spaceTemp);
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Failure converting spaceTemp to int");
            }

            if (_sbCheckerBaseInput.MonsterSB.SubType.Contains("swarm"))
            {
                if (ReachSB == 0)
                {
                    _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Swarm Reach isn't 0");
                }
            }
            else
            {
                bool SpaceGood = false;
                bool ReachGood = false;
                double ReachComputed = -100;
                double SpaceComputed = -100;

                switch (monsterSize)
                {
                    case StatBlockInfo.SizeCategories.Colossal:
                        SpaceComputed = 30;
                        if (SpaceComputed == SpaceSB) SpaceGood = true;
                        if (ReachSB == 30)
                        {
                            ReachComputed = 30;
                            ReachGood = true;
                        }
                        if (ReachSB == 20)
                        {
                            ReachComputed = 20;
                            ReachGood = true;
                        }
                        break;
                    case StatBlockInfo.SizeCategories.Gargantuan:
                        SpaceComputed = 20;
                        if (SpaceComputed == SpaceSB) SpaceGood = true;
                        if (ReachSB == 20)
                        {
                            ReachComputed = 20;
                            ReachGood = true;
                        }
                        if (ReachSB == 15)
                        {
                            ReachComputed = 15;
                            ReachGood = true;
                        }
                        break;
                    case StatBlockInfo.SizeCategories.Huge:
                        SpaceComputed = 15;
                        if (SpaceComputed == SpaceSB) SpaceGood = true;
                        if (ReachSB == 15)
                        {
                            ReachComputed = 15;
                            ReachGood = true;
                        }
                        if (ReachSB == 10)
                        {
                            ReachComputed = 10;
                            ReachGood = true;
                        }
                        break;
                    case StatBlockInfo.SizeCategories.Large:
                        SpaceComputed = 10;
                        if (SpaceComputed == SpaceSB) SpaceGood = true;
                        if (ReachSB == 10)
                        {
                            ReachComputed = 10;
                            ReachGood = true;
                        }
                        if (ReachSB == 5)
                        {
                            ReachComputed = 5;
                            ReachGood = true;
                        }
                        break;
                    case StatBlockInfo.SizeCategories.Medium:
                        SpaceComputed = 5;
                        ReachComputed = 5;
                        if (ReachComputed == ReachSB) ReachGood = true;
                        if (SpaceComputed == SpaceSB) SpaceGood = true;
                        break;
                    case StatBlockInfo.SizeCategories.Small:
                        SpaceComputed = 5;
                        ReachComputed = 5;
                        if (ReachComputed == ReachSB) ReachGood = true;
                        if (SpaceComputed == SpaceSB) SpaceGood = true;
                        break;
                    case StatBlockInfo.SizeCategories.Tiny:
                        SpaceComputed = 2;
                        ReachComputed = 0;
                        if (SpaceSB == SpaceComputed && spaceHalf) SpaceGood = true;
                        if (ReachComputed == ReachSB) ReachGood = true;
                        break;
                    case StatBlockInfo.SizeCategories.Diminutive:
                        SpaceComputed = 2;
                        ReachComputed = 0;
                        if (ReachComputed == ReachSB) ReachGood = true;
                        if (SpaceComputed == SpaceSB) SpaceGood = true;
                        break;
                    case StatBlockInfo.SizeCategories.Fine:
                        if (ReachSB == 0)
                            ReachGood = true;
                        else
                            ReachComputed = 0;
                        break;
                }

                if (SpaceGood && ReachGood)
                {
                    _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
                    return;
                }

                if (!SpaceGood)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, SpaceComputed.ToString(), SpaceSB.ToString());
                }
                if (!ReachGood)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, ReachComputed.ToString(), ReachSB.ToString());
                }
            }
        }

        private static void ReplaceHalfString(ref string spaceTemp, ref bool spaceHalf)
        {
            if (spaceTemp.Contains("-1/2"))
            {
                spaceHalf = true;
                spaceTemp = spaceTemp.Replace("-1/2", string.Empty);
            }
            else if (spaceTemp.Contains(" 1/2"))
            {
                spaceHalf = true;
                spaceTemp = spaceTemp.Replace(" 1/2", string.Empty);
            }
            else if (spaceTemp.Contains("1/2"))
            {
                spaceHalf = true;
                spaceTemp = spaceTemp.Replace("1/2", string.Empty);
            }
        }
    }
}
