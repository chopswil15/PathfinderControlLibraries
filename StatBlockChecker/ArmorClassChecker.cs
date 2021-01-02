using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassManager;
using CommonStatBlockInfo;
using Utilities;
using OnGoing;

namespace StatBlockChecker
{
    public class ArmorClassChecker
    {
        private StatBlockMessageWrapper _messageXML;
        private MonSBSearch _monSBSearch;
        private ClassMaster CharacterClasses;
        private StatBlockInfo.ACMods ACMods_Computed;
        private StatBlockInfo.ACMods ACMods_SB;
        private int MaxDexMod;  //due to armor
        private int SizeMod;
        private int DodgeBonus;
        private string AC;
        private string Race;
        private string CR = Environment.NewLine;
        private List<OnGoingStatBlockModifier> _onGoingMods;
        private int HD;

        public ArmorClassChecker(StatBlockMessageWrapper _messageXML, MonSBSearch _monSBSearch, ClassMaster CharacterClasses,
                      StatBlockInfo.ACMods ACMods_Computed, int MaxDexMod, int SizeMod, int DodgeBonus, string AC,
                      StatBlockInfo.ACMods ACMods_SB, string Race, List<OnGoingStatBlockModifier> onGoingMods, int HD)
        {
            this._messageXML = _messageXML;
            this._monSBSearch = _monSBSearch;
            this.CharacterClasses = CharacterClasses;
            this.ACMods_Computed = ACMods_Computed;
            this.MaxDexMod = MaxDexMod;
            this.SizeMod = SizeMod;
            this.DodgeBonus = DodgeBonus;
            this.ACMods_SB = ACMods_SB;
            this.AC = AC;
            this.Race = Race;
            this.HD = HD;
            _onGoingMods = onGoingMods;
        }

        public void CheckACValue()
        {
            string CheckName = "AC Value";
            int FlatFootedMod = 0;
            int DexBonus = 0;
            int DexUsed = _monSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Dexterity);
            int DexMod = DexUsed;
            int Duelist = 0;
            int OnGoingMods = 0;
            string formula = string.Empty;

            if (CharacterClasses.HasClass("duelist")) //Canny Defense
            {
                int IntMod = _monSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Intelligence);
                if (IntMod > 0)
                {
                    int Mod = 0;
                    int DuelistLevel = CharacterClasses.FindClassLevel("duelist");
                    if (IntMod >= DuelistLevel)
                    {
                        Mod = DuelistLevel;
                    }
                    else
                    {
                        Mod = IntMod;
                    }
                    Duelist = Mod;
                }
            }

            if (MaxDexMod > -1) //negative number means no penalty
            {
                if (MaxDexMod < DexMod)
                {                    
                    DexUsed = MaxDexMod;
                    formula += " MaxDex: was " + MaxDexMod.ToString() + " even though Dex mod is " + (DexMod).ToString();
                   // Duelist = 0;
                }
            }

            if (DexMod > 0)
            {
                DexBonus = DexUsed;
            }

            if (_monSBSearch.HasFeat("Uncanny Dodge"))
            {
                FlatFootedMod = DexUsed;
            }

            switch (Race)
            {
                case "kasatha":
                    ACMods_Computed.Dodge += 2;
                    break;
                case "Svirfneblin":
                    ACMods_Computed.Dodge += 2;
                    break;
            }

            if (_monSBSearch.HasSQ("void form"))
            {
                ACMods_Computed.Deflection += HD / 4;
            }

            if(_monSBSearch.HasSubType("clockwork"))
            {
                 ACMods_Computed.Dodge += 2;
            }

            if (CharacterClasses.HasClass("sorcerer")) //Natural Armor Increase
            {
                if (_monSBSearch.HasBloodline("draconic"))
                {
                    int sorLevel = CharacterClasses.FindClassLevel("sorcerer");
                    if (sorLevel >= 3) ACMods_Computed.Natural += 1;
                    if (sorLevel >= 9) ACMods_Computed.Natural += 1;
                    if (sorLevel >= 15) ACMods_Computed.Natural += 2;
                }
            }

            if (CharacterClasses.HasClass("oracle"))
            {
                if (_monSBSearch.HasMystery("ancestor") && _monSBSearch.HasSQ("spirit shield"))
                {
                    int oracleLevel = CharacterClasses.FindClassLevel("oracle");
                    if (oracleLevel >= 1) ACMods_Computed.Armor += 4;
                    if (oracleLevel >= 7) ACMods_Computed.Armor += 2;
                    if (oracleLevel >= 11) ACMods_Computed.Armor += 2;
                    if (oracleLevel >= 15) ACMods_Computed.Armor += 2;
                    if (oracleLevel >= 19) ACMods_Computed.Armor += 2;
                }
            }

            if (CharacterClasses.HasClass("gunslinger")) //Nimble (Ex)
            {
                int gunslingerLevel = CharacterClasses.FindClassLevel("gunslinger");
                if (gunslingerLevel >= 2) ACMods_Computed.Dodge += 1;
                if (gunslingerLevel >= 6) ACMods_Computed.Dodge += 1;
                if (gunslingerLevel >= 10) ACMods_Computed.Dodge += 1;
                if (gunslingerLevel >= 14) ACMods_Computed.Dodge += 1;
                if (gunslingerLevel >= 18) ACMods_Computed.Dodge += 1;
            }

            if (CharacterClasses.HasClass("swashbuckler")) //Nimble (Ex)
            {
                int swashbucklerLevel = CharacterClasses.FindClassLevel("swashbuckler");
                if (swashbucklerLevel >= 3) ACMods_Computed.Dodge += 1;
                if (swashbucklerLevel >= 7) ACMods_Computed.Dodge += 1;
                if (swashbucklerLevel >= 11) ACMods_Computed.Dodge += 1;
                if (swashbucklerLevel >= 15) ACMods_Computed.Dodge += 1;
                if (swashbucklerLevel >= 19) ACMods_Computed.Dodge += 1;
            }

            if (CharacterClasses.HasClass("stalwart defender")) //AC Bonus (Ex)
            {
                int stalwartDefenderLevel = CharacterClasses.FindClassLevel("stalwart defender");
                if (stalwartDefenderLevel >= 1) ACMods_Computed.Dodge += 1;
                if (stalwartDefenderLevel >= 4) ACMods_Computed.Dodge += 1;
                if (stalwartDefenderLevel >= 7) ACMods_Computed.Dodge += 1;
                if (stalwartDefenderLevel >= 10) ACMods_Computed.Dodge += 1;
            }

            if (CharacterClasses.HasClass("ranger") && _monSBSearch.HasArchetype("shapeshifter"))
            {
                if (_monSBSearch.HasSQ("form of the dragon"))
                {
                    ACMods_Computed.Natural += 2;
                }
            }

            if (_monSBSearch.HasClassArchetype("staff magus")) //Quarterstaff Defense (Ex)
            {
                int magusLevel = CharacterClasses.FindClassLevel("magus");
               //TO DO
            }

            if (_monSBSearch.HasFeat("Aldori Dueling Mastery"))
            {
                ACMods_Computed.Shield += 2; //Aldori Dueling Mastery
            }

            if (CharacterClasses.HasClass("prophet of kalistrade"))
            {
                int level = CharacterClasses.FindClassLevel("prophet of kalistrade");
                //Auspicious Display --assumes the bling is present
                int mod = 1;
                if (level >= 4) mod++;
                if (level >= 7) mod++;
                if (level >= 10) mod++;
                ACMods_Computed.Dodge += mod;
            }

            //should be handled SBChecker.ParseACMods()
            //foreach (OnGoingStatBlockModifier mod in _onGoingMods)
            //{
            //    if (mod.ModType == OnGoingStatBlockModifier.StatBlockModifierTypes.AC)
            //    {
            //        OnGoingMods += mod.Modifier;
            //    }
            //}

            if (_monSBSearch.HasTemplate("worm that walks"))
            {
                int WisMod = _monSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Wisdom);
                if(WisMod < 2) WisMod = 2;

                ACMods_Computed.Insight += WisMod;
            }


            int OtherBonuses = ACMods_Computed.Enhancement + ACMods_Computed.Deflection + ACMods_Computed.Natural
                   + ACMods_Computed.Dodge + ACMods_Computed.Wis + ACMods_Computed.Defending + ACMods_Computed.Insight + SizeMod
                   + ACMods_Computed.Monk + Duelist + OnGoingMods;

            formula += " AC = 10 +" + ACMods_Computed.Armor.ToString() + " Armor +" + ACMods_Computed.Shield.ToString() + " Shield +"
                       + DexUsed.ToString() + " Dex Used +" + OtherBonuses.ToString() + " Other Bonuses +" + ACMods_Computed.Rage + " Rage +" + ACMods_Computed.BloodRage + " Bloodrage";

            if (OtherBonuses != 0)
            {
                formula += " Other Bonuses =";
                if (ACMods_Computed.Enhancement != 0)
                {
                    formula += " +" + ACMods_Computed.Enhancement.ToString() + " Enhancement";
                }
                if (ACMods_Computed.Deflection != 0)
                {
                    formula += " +" + ACMods_Computed.Deflection.ToString() + " Deflection";
                }
                if (ACMods_Computed.Natural != 0)
                {
                    formula += " +" + ACMods_Computed.Natural.ToString() + " Natural";
                }
                if (ACMods_Computed.Dodge != 0)
                {
                    formula += " +" + ACMods_Computed.Dodge.ToString() + " Dodge";
                }
                if (ACMods_Computed.Wis != 0)
                {
                    formula += " +" + ACMods_Computed.Wis.ToString() + " Wis";
                }
                if (ACMods_Computed.Defending != 0)
                {
                    formula += " +" + ACMods_Computed.Defending.ToString() + " defending";
                }
                formula += Utility.GetStringValue(SizeMod) + " SizeMod";
                if (ACMods_Computed.Monk != 0)
                {
                    formula += " +" + ACMods_Computed.Monk.ToString() + " Monk";
                }
                if (Duelist != 0)
                {
                    formula += " +" + Duelist.ToString() + " Duelist";
                }
                if (OnGoingMods != 0)
                {
                    formula += " +" + OnGoingMods.ToString() + " OnGoingMods";
                }
                if (ACMods_Computed.Insight != 0)
                {
                    formula += " +" + ACMods_Computed.Insight.ToString() + " Insight";
                }
            }

            int CompAC = 10 + ACMods_Computed.Armor + ACMods_Computed.Shield + DexUsed + OtherBonuses + ACMods_Computed.Rage + ACMods_Computed.BloodRage;
            int CompTouchAC = 10 + DexUsed + SizeMod + ACMods_Computed.Deflection + ACMods_Computed.Defending + DodgeBonus + ACMods_Computed.Rage + ACMods_Computed.BloodRage + ACMods_Computed.Wis + ACMods_Computed.Monk + Duelist + ACMods_Computed.Insight;
            int CompFlatFoootedAC = CompAC - DexBonus + FlatFootedMod - DodgeBonus - Duelist;

            int AC_SB = 0;
            int Touch_SB = 0;
            int Flatfooted_SB = 0;
            if (AC.Contains(CR)) //2nd AC block
            {
                int Pos5 = AC.IndexOf(CR);
                AC = AC.Substring(0, Pos5).Trim();
                Pos5 = AC.IndexOf(")");
                AC = AC.Substring(Pos5 + 1).Trim();
            }
            List<string> ACList_SB = AC.Split(',').ToList();
            if (ACList_SB[0].IndexOf("(") >= 0)
            {
                ACList_SB[0] = ACList_SB[0].Substring(0, ACList_SB[0].IndexOf("(")).Trim();
            }
            try
            {
                AC_SB = Convert.ToInt32(ACList_SB[0]);
            }
            catch (Exception ex)
            {
                _messageXML.AddFail(CheckName, ex.Message);
                return;
            }
            string temp = ACList_SB[1].Replace("touch", string.Empty);
            try
            {
                Touch_SB = Convert.ToInt32(temp);
            }
            catch
            {
                _messageXML.AddFail(CheckName, "Issue parsing touch AC");
                return;
            }
            temp = ACList_SB[2].Replace("flat-footed", string.Empty);
            try
            {
                Flatfooted_SB = Convert.ToInt32(temp);
            }
            catch
            {
                _messageXML.AddFail(CheckName, "Issue parsing flat-footed AC");
                return;
            }

            if (CompAC == AC_SB && CompTouchAC == Touch_SB && CompFlatFoootedAC == Flatfooted_SB)
            {
                _messageXML.AddPass(CheckName,formula);
                return;
            }
            if (CompAC != AC_SB)
            {
                _messageXML.AddFail(CheckName, CompAC.ToString() + " " + ACMods_Computed.ToString(), AC_SB.ToString(), formula);
            }
            if (CompTouchAC != Touch_SB)
            {
                _messageXML.AddFail(CheckName + " Touch ", CompTouchAC.ToString() + " " + ACMods_Computed.ToString(), Touch_SB.ToString(),formula);
            }
            if (CompFlatFoootedAC != Flatfooted_SB)
            {
                _messageXML.AddFail(CheckName + " Flat-Footed ", CompFlatFoootedAC.ToString() + " " + ACMods_Computed.ToString(), Flatfooted_SB.ToString(), formula);
            }
        }

        public void CheckACMath()
        {
            string CheckName = "AC Math";
            int ComputedAC = ACMods_SB.ComputeAC();
            string formula = ACMods_SB.ComputeACFormula();

            if (CharacterClasses.HasClass("monk"))
            {
                //int MonkLevel = CharacterClasses.FindClassLevel("monk");
                //ComputedAC += MonkLevel / 4;
            }
            else if (_monSBSearch.HasGear("monk's robe"))
            {
                ComputedAC += 1; //monk level 5 ac bonus
            }

            //inlcude in Dex mod
            //if (CharacterClasses.HasClass("duelist")) //Canny Defense
            //{
            //    int IntMod = _monSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Intelligence);
            //    if (IntMod > 0)
            //    {
            //        int Mod = 0;
            //        int DuelistLevel = CharacterClasses.FindClassLevel("duelist");
            //        if (IntMod >= DuelistLevel)
            //        {
            //            Mod = DuelistLevel;
            //        }
            //        else
            //        {
            //            Mod = IntMod;
            //        }
            //        ComputedAC += Mod;
            //    }
            //}

            if (_monSBSearch.HasClassArchetype("daredevil"))
            {
                int BardLevel = CharacterClasses.FindClassLevel("bard");
            }

            string temp = AC;
            if (AC.Contains(CR)) //2nd AC block
            {
                int Pos2 = AC.IndexOf(CR);
                temp = temp.Substring(0, Pos2).Trim();
                Pos2 = temp.IndexOf(")");
                temp = temp.Substring(Pos2 +1).Trim();
            }
            try
            {
                temp = temp.Substring(0, temp.IndexOf(","));
                temp = temp.Replace("AC", string.Empty);
            }
            catch (Exception ex)
            {
                throw new Exception("CheckACMath-- missing comma " + ex.Message);
            }
            int Pos = temp.IndexOf("(");
            if (Pos >= 0)
            {
                temp = temp.Substring(0, Pos).Trim();
            }
            try
            {
                int SB_AC = Convert.ToInt32(temp);

                if (SB_AC != ComputedAC)
                {
                    _messageXML.AddFail(CheckName, ComputedAC.ToString(), SB_AC.ToString(), formula);
                }
                else
                {
                    _messageXML.AddPass(CheckName);
                }
            }
            catch (Exception ex)
            {
                _messageXML.AddFail(CheckName, ex.Message);
            }
        }
    }
}
