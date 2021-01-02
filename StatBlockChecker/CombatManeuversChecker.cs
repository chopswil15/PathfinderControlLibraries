using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using Utilities;
using OnGoing;
using ClassManager;
using StatBlockCommon;

namespace StatBlockChecker
{
    public class CombatManeuversChecker
    {
        private StatBlockMessageWrapper _messageXML;
        private MonSBSearch _monSBSearch;
        private ClassMaster CharacterClasses;
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
        private RaceBase Race_Base;

        public CombatManeuversChecker(StatBlockMessageWrapper _messageXML, MonSBSearch _monSBSearch, ClassMaster CharacterClasses, StatBlockInfo.SizeCategories SizeCat,
              int SizeMod, int OnGoing, string BaseAtk, bool HasMonk, string CMBString, string formula, int TotalHD,
              string AC_Mods, int DodgeBonus, string CMDString, RaceBase raceBase)
        {
            this._messageXML = _messageXML;
            this._monSBSearch = _monSBSearch;
            this.CharacterClasses = CharacterClasses; 
            this.SizeCat = SizeCat;            
            this.BaseAtk = BaseAtk;
            this.HasMonk = HasMonk;
            this.CMBString = CMBString;
            this.SizeMod = SizeMod;
            this.OnGoing = OnGoing;
            this.formulaOnGoing = formula;
            this.TotalHD = TotalHD;
            this.AC_Mods = AC_Mods;
            this.DodgeBonus = DodgeBonus;
            this.CMDString = CMDString;
            Race_Base = raceBase;
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
            else if (_monSBSearch.HasFeat("Agile Maneuvers"))
            {
                abilityName = AbilityScores.AbilityScores.AbilityName.Dexterity;
            }
            else if (_monSBSearch.HasDefensiveAbility("incorporeal"))
            {
                abilityName = AbilityScores.AbilityScores.AbilityName.Dexterity;
            }

            int AbilityMod = StatBlockInfo.GetAbilityModifier(_monSBSearch.GetAbilityScoreValue(abilityName));
           // int BAB = Convert.ToInt32(Utility.GetNonParenValue(BaseAtk));
            int BAB = CharacterClasses.GetBABValue() + (CharacterClasses.HasClass("animal companion") ? 0 : Race_Base.RaceBAB());

            //was taken out before, added back in 11/8/2014
            if (HasMonk)
            {
                int MonkLevel = CharacterClasses.FindClassLevel("Monk");
                if (MonkLevel >= 3)
                {
                    BAB = MonkLevel + CharacterClasses.GetNonMonkBABValue() + Race_Base.RaceBAB();
                }
            }

            CMB = BAB + (SizeMod * -1) + AbilityMod + OnGoing;
            string formula = BAB.ToString() + " BAB " + Utility.GetPlusSign(SizeMod * -1) + (SizeMod * -1).ToString() + " SizeMod +" + AbilityMod.ToString() + " " + abilityName.ToString() + " AbilityMod";
            if (OnGoing != 0)
            {
                formula += "+" + OnGoing.ToString() + " (" + formulaOnGoing + ") OnGoingAttackMod";
            }

            _messageXML.AddInfo("Base BAB: " + (BAB + (SizeMod) + AbilityMod).ToString());

            if (_monSBSearch.HasSubType("swarm"))
            {
                CMB = 0;
                formula = "0 swarm";
            }


            string holdCMB = Utility.GetNonParenValue(CMBString);
            if (holdCMB == "-") holdCMB = "0";
            if (CMB == Convert.ToInt32(holdCMB))
            {
                _messageXML.AddPass(CheckName,formula);
            }
            else
            {
                _messageXML.AddFail(CheckName, CMB.ToString(), holdCMB, formula);
            }
        }

        public void CheckCMD()
        {
            string CheckName = "CMD";
            int MonkBonus = 0;
            int MonkLevel = 0;
            int WisBonus = 0;
            int StrModUsed =  _monSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Strength);

            int BAB = Convert.ToInt32(Utility.GetNonParenValue(BaseAtk));

            if (HasMonk)
            {
                MonkBonus += _monSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Wisdom);
                MonkLevel = CharacterClasses.FindClassLevel("Monk");
                if (MonkLevel >= 4) MonkBonus += 1;
                if (MonkLevel >= 8) MonkBonus += 1;
                if (MonkLevel >= 12) MonkBonus += 1;
                if (MonkLevel >= 16) MonkBonus += 1;
                if (MonkLevel >= 20) MonkBonus += 1;
            }

            if (CharacterClasses.HasClass("psychic") && _monSBSearch.MonSB.PsychicDiscipline == "self-perfection")
            {
                WisBonus = _monSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Wisdom);
            }

            if (_monSBSearch.HasFeat("Defensive Combat Training"))
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

            int DexMod = _monSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Dexterity);

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
            // if (_monSBSearch.HasFeat("Improved Grapple"))
            //{
            //    CMD += 2;
            //    calculation += " + 2";
            //    formula += " + Improved Grapple";
            //}

            //all AC penalties affect CMD
            int negativeACMods = GetAllNegativeACMods();
            if(negativeACMods != 0)
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

            if (_monSBSearch.HasSubType("swarm"))
            {
                CMD = 0;
                formula = "+0 swarm";
            }

            string holdCMD = Utility.GetNonParenValue(CMDString);
            if (holdCMD == "-") holdCMD = "0";


            if (CMD == Convert.ToInt32(holdCMD))
            {
                _messageXML.AddPass(CheckName);
            }
            else
            {
                _messageXML.AddFail(CheckName, CMD.ToString(), holdCMD, formula);
            }
        }

        private int GetAllNegativeACMods()
        {
            int Sum = 0;
            int Pos = 0;
            string temp = string.Empty;

            try
            {
                temp = AC_Mods.Replace("(", string.Empty);
                temp = temp.Replace(")", string.Empty);
                if(temp.Contains(")"))
                {
                    int Pos2 = temp.IndexOf(")");
                    temp = temp.Substring(0, Pos);
                }
                List<string> Mods = temp.Split(',').ToList<string>();
                foreach (string mod in Mods)
                {
                    if (mod.IndexOf("-") >= 0)
                    {
                        temp = mod.Trim();
                        Pos = temp.IndexOf(" ");
                        Sum += Convert.ToInt32(temp.Substring(0, Pos));
                    }
                }
            }
            catch
            {
                _messageXML.AddFail("GetAllNegativeACMods", "AC Mods Formatting Error");
            }

            return Sum;
        }

        private int GetACModValue(string Mod)
        {
            string holdMods = AC_Mods;
            if (holdMods.Length == 0) return 0;

            int Pos = holdMods.IndexOf(")");
            holdMods = holdMods.Substring(0, Pos);
            Pos = holdMods.IndexOf("|");
            if (Pos != -1)
            {
                holdMods = holdMods.Substring(0, Pos).Trim();
            }

            if (holdMods.IndexOf(Mod) >= 0)
            {
                holdMods = holdMods.Replace("(", string.Empty);
                holdMods = holdMods.Replace(")", string.Empty);
                List<string> Mods = holdMods.Split(',').ToList<string>();
                foreach (string mod in Mods)
                {
                    if (mod.IndexOf(Mod) >= 0)
                    {
                        string hold = mod.Replace(Mod, string.Empty).Trim();
                        if (hold.IndexOf(" ") > 0)
                        {
                            hold = hold.Substring(0, hold.IndexOf(" "));
                        }
                        return Convert.ToInt32(hold);
                    }
                }
            }
            return 0;
        }
    }
}
