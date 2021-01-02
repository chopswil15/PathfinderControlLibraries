using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using PathfinderGlobals;
using Utilities;

namespace StatBlockChecker
{
    public class CombatManeuversChecker : ICombatManeuversChecker
    {
        private StatBlockInfo.SizeCategories SizeCat;
        private int SizeMod;
        private int OnGoing;
        private string BaseAtk;
        private bool HasMonk;
        private string CMBString;
        private string formulaOnGoing;
        private int TotalHD;
        private string AC_Mods;
        private int DodgeBonus;
        private string CMDString;
        private ISBCheckerBaseInput _sbCheckerBaseInput;

        public CombatManeuversChecker(ISBCheckerBaseInput sbCheckerBaseInput, ICombatManeuversCheckerInput combatManeuversCheckerInput)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            SizeCat = combatManeuversCheckerInput.SizeData.SizeCat;
            BaseAtk = _sbCheckerBaseInput.MonsterSB.BaseAtk;
            HasMonk = _sbCheckerBaseInput.CharacterClasses.HasClass("monk");
            CMBString = _sbCheckerBaseInput.MonsterSB.CMB;
            SizeMod = combatManeuversCheckerInput.SizeData.SizeMod;
            OnGoing = combatManeuversCheckerInput.OnGoing;
            formulaOnGoing = combatManeuversCheckerInput.Formula;
            TotalHD = combatManeuversCheckerInput.TotalHD;
            AC_Mods = _sbCheckerBaseInput.MonsterSB.AC_Mods;
            DodgeBonus = combatManeuversCheckerInput.DodgeBonus;
            CMDString = _sbCheckerBaseInput.MonsterSB.CMD;
        }

        public void CheckCMB()
        {
            string CheckName = "CMB";
            int CMB = 0;
            AbilityScores.AbilityScores.AbilityName abilityName = AbilityScores.AbilityScores.AbilityName.Strength;


            if (SizeCat <= StatBlockInfo.SizeCategories.Tiny)
            {
                abilityName = AbilityScores.AbilityScores.AbilityName.Dexterity;
            }
            else if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Agile Maneuvers"))
            {
                abilityName = AbilityScores.AbilityScores.AbilityName.Dexterity;
            }
            else if (_sbCheckerBaseInput.MonsterSBSearch.HasDefensiveAbility("incorporeal"))
            {
                abilityName = AbilityScores.AbilityScores.AbilityName.Dexterity;
            }

            int AbilityMod = StatBlockInfo.GetAbilityModifier(_sbCheckerBaseInput.MonsterSBSearch.GetAbilityScoreValue(abilityName));
            // int BAB = Convert.ToInt32(Utility.GetNonParenValue(BaseAtk));
            int BAB = _sbCheckerBaseInput.CharacterClasses.GetBABValue() + (_sbCheckerBaseInput.CharacterClasses.HasClass("animal companion") ? 0 : _sbCheckerBaseInput.Race_Base.RaceBAB());

            //was taken out before, added back in 11/8/2014
            if (HasMonk)
            {
                int MonkLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Monk");
                if (MonkLevel >= 3)
                {
                    BAB = MonkLevel + _sbCheckerBaseInput.CharacterClasses.GetNonMonkBABValue() + _sbCheckerBaseInput.Race_Base.RaceBAB();
                }
            }

            CMB = BAB + (SizeMod * -1) + AbilityMod + OnGoing;
            string formula = BAB.ToString() + " BAB " + CommonMethods.GetPlusSign(SizeMod * -1) + (SizeMod * -1).ToString() + " SizeMod +" + AbilityMod.ToString() + PathfinderConstants.SPACE + abilityName.ToString() + " AbilityMod";
            if (OnGoing != 0)
            {
                formula += "+" + OnGoing.ToString() + " (" + formulaOnGoing + ") OnGoingAttackMod";
            }

            _sbCheckerBaseInput.MessageXML.AddInfo("Base BAB: " + (BAB + (SizeMod) + AbilityMod).ToString());

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSubType("swarm"))
            {
                CMB = 0;
                formula = "0 swarm";
            }


            string holdCMB = Utility.GetNonParenValue(CMBString);
            if (holdCMB == "-") holdCMB = "0";
            if (CMB == Convert.ToInt32(holdCMB))
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName, formula);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, CMB.ToString(), holdCMB, formula);
            }
        }

        public void CheckCMD()
        {
            string CheckName = "CMD";
            int MonkBonus = 0;
            int MonkLevel = 0;
            int WisBonus = 0;
            int StrModUsed = _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Strength);

            int BAB = Convert.ToInt32(Utility.GetNonParenValue(BaseAtk));

            if (HasMonk)
            {
                MonkBonus += _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Wisdom);
                MonkLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Monk");
                if (MonkLevel >= 4) MonkBonus += 1;
                if (MonkLevel >= 8) MonkBonus += 1;
                if (MonkLevel >= 12) MonkBonus += 1;
                if (MonkLevel >= 16) MonkBonus += 1;
                if (MonkLevel >= 20) MonkBonus += 1;
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("psychic") && _sbCheckerBaseInput.MonsterSBSearch.MonSB.PsychicDiscipline == "self-perfection")
            {
                WisBonus = _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Wisdom);
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Defensive Combat Training"))
            {
                BAB = TotalHD;
            }

            //Certain AC bonuses improve CMD
            int ACBonus = GetACModValue("deflection"); //until we can parse magic items fully
            ACBonus += GetACModValue("sacred");
            ACBonus += GetACModValue("profane");
            ACBonus += GetACModValue("circumstance");
            ACBonus += GetACModValue("morale");
            ACBonus += GetACModValue("insight");

            int DexMod = _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Dexterity);

            int CMD = 10 + BAB + StrModUsed + DexMod + (SizeMod * -1) + DodgeBonus + MonkBonus + ACBonus + WisBonus;

            string formula = "10 +" + BAB.ToString() + " BAB +" + StrModUsed.ToString() + " StrModUsed +"
                   + DexMod.ToString() + " DexMod +" + (SizeMod * -1).ToString() + " SizeMod";
            if (DodgeBonus != 0)
            {
                formula += " +" + DodgeBonus.ToString() + " DodgeBonus";
            }
            if (MonkBonus != 0)
            {
                formula += " +" + MonkBonus.ToString() + " MonkBonus";
            }
            if (ACBonus != 0)
            {
                formula += " +" + ACBonus.ToString() + " ACBonus";
            }
            if (WisBonus != 0)
            {
                formula += " +" + WisBonus.ToString() + " WisBonus";
            }

            //shown in CMD mod
            // if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Improved Grapple"))
            //{
            //    CMD += 2;
            //    calculation += " + 2";
            //    formula += " + Improved Grapple";
            //}

            //all AC penalties affect CMD
            int negativeACMods = GetAllNegativeACMods();
            if (negativeACMods != 0)
            {
                CMD += GetAllNegativeACMods();
                formula += negativeACMods + " negative AC mods";
            }

            if (SizeMod < 0)
            {
                CMD += SizeMod * -1; // don't count more than once
            }
            if (DexMod < 0)
            {
                CMD += DexMod * -1; // don't count more than once
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSubType("swarm"))
            {
                CMD = 0;
                formula = "+0 swarm";
            }

            string holdCMD = Utility.GetNonParenValue(CMDString);
            if (holdCMD == "-") holdCMD = "0";


            if (CMD == Convert.ToInt32(holdCMD))
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, CMD.ToString(), holdCMD, formula);
            }
        }

        private int GetAllNegativeACMods()
        {
            int Sum = 0;
            int Pos = 0;
            string temp = string.Empty;

            try
            {
                temp = AC_Mods.Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
                temp = temp.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                if (temp.Contains(PathfinderConstants.PAREN_RIGHT))
                {
                    int Pos2 = temp.IndexOf(PathfinderConstants.PAREN_RIGHT);
                    temp = temp.Substring(0, Pos);
                }
                List<string> Mods = temp.Split(',').ToList();
                foreach (string mod in Mods)
                {
                    if (mod.Contains("-"))
                    {
                        temp = mod.Trim();
                        Pos = temp.IndexOf(PathfinderConstants.SPACE);
                        Sum += Convert.ToInt32(temp.Substring(0, Pos));
                    }
                }
            }
            catch
            {
                _sbCheckerBaseInput.MessageXML.AddFail("GetAllNegativeACMods", "AC Mods Formatting Error");
            }

            return Sum;
        }

        private int GetACModValue(string Mod)
        {
            string holdMods = AC_Mods;
            if (holdMods.Length == 0) return 0;

            int Pos = holdMods.IndexOf(PathfinderConstants.PAREN_RIGHT);
            holdMods = holdMods.Substring(0, Pos);
            Pos = holdMods.IndexOf("|");
            if (Pos != -1)
            {
                holdMods = holdMods.Substring(0, Pos).Trim();
            }

            if (holdMods.Contains(Mod))
            {
                holdMods = holdMods.Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
                holdMods = holdMods.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                List<string> Mods = holdMods.Split(',').ToList();
                foreach (string mod in Mods)
                {
                    if (mod.Contains(Mod))
                    {
                        string hold = mod.Replace(Mod, string.Empty).Trim();
                        if (hold.IndexOf(PathfinderConstants.SPACE) > 0)
                        {
                            hold = hold.Substring(0, hold.IndexOf(PathfinderConstants.SPACE));
                        }
                        return Convert.ToInt32(hold);
                    }
                }
            }
            return 0;
        }
    }

    public class CombatManeuversCheckerInput : ICombatManeuversCheckerInput
    {
        public ISizeData SizeData { get; set; }
        public int OnGoing { get; set; }
        public string Formula { get; set; }
        public int TotalHD { get; set; }
        public int DodgeBonus { get; set; }
    }
}
