using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using OnGoing;
using PathfinderGlobals;

namespace StatBlockChecker
{
    public class ArmorClassChecker
    {
        private StatBlockInfo.ACMods ACMods_SB;
        private int SizeMod;
        private int DodgeBonus;
        private string AC;
        private string Race;
        private string CR = Environment.NewLine;
        private List<OnGoingStatBlockModifier> _onGoingMods;
        private int HD;
        private SBCheckerBaseInput _sbCheckerBaseInput;
        private IArmorClassData _armorClassData;


        public ArmorClassChecker(SBCheckerBaseInput sbCheckerBaseInput,  IArmorClassData armorClassData, int SizeMod, int DodgeBonus)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            this.SizeMod = SizeMod;
            this.DodgeBonus = DodgeBonus;
            ACMods_SB = armorClassData.ACMods_SB;
            AC = _sbCheckerBaseInput.MonsterSB.AC;
            Race = _sbCheckerBaseInput.Race_Base.Name();
            HD = _sbCheckerBaseInput.Race_Base.RaceSB.HDValue();
            _onGoingMods = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockMods();
            _armorClassData = armorClassData;
        }

        public void CheckACValue()
        {
            string CheckName = "AC Value";
            int dexBonus = 0, duelist = 0, onGoingMods = 0, flatFootedMod = 0;
            int dexUsed = _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Dexterity);
            int dexMod = dexUsed;
            string formula = string.Empty;
            StatBlockInfo.ACMods acMods_Computed = new StatBlockInfo.ACMods();

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("duelist")) //Canny Defense
            {
                int IntMod = _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Intelligence);
                if (IntMod > 0)
                {
                    int Mod;
                    int DuelistLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("duelist");
                    Mod =  IntMod >= DuelistLevel ?   Mod = DuelistLevel :   Mod = IntMod;
                    duelist = Mod;
                }
            }

            if (_armorClassData.MaxDexMod > -1) //negative number means no penalty
            {
                if (_armorClassData.MaxDexMod < dexMod)
                {                    
                    dexUsed = _armorClassData.MaxDexMod;
                    formula += " MaxDex: was " + _armorClassData.MaxDexMod.ToString() + " even though Dex mod is " + (dexMod).ToString();
                   // duelist = 0;
                }
            }

            if (dexMod > 0) dexBonus = dexUsed;

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Uncanny Dodge"))  flatFootedMod = dexUsed;

            switch (Race)
            {
                case "kasatha":
                    acMods_Computed.Dodge += 2;
                    break;
                case "Svirfneblin":
                    acMods_Computed.Dodge += 2;
                    break;
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("void form"))  acMods_Computed.Deflection += HD / 4;
            if(_sbCheckerBaseInput.MonsterSBSearch.HasSubType("clockwork")) acMods_Computed.Dodge += 2;
            
            if (_sbCheckerBaseInput.CharacterClasses.HasClass("sorcerer")) //Natural Armor Increase
            {
                if (_sbCheckerBaseInput.MonsterSBSearch.HasBloodline("draconic"))
                {
                    int sorLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("sorcerer");
                    if (sorLevel >= 3) acMods_Computed.Natural += 1;
                    if (sorLevel >= 9) acMods_Computed.Natural += 1;
                    if (sorLevel >= 15) acMods_Computed.Natural += 2;
                }
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("oracle"))
            {
                if (_sbCheckerBaseInput.MonsterSBSearch.HasMystery("ancestor") && _sbCheckerBaseInput.MonsterSBSearch.HasSQ("spirit shield"))
                {
                    int oracleLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("oracle");
                    if (oracleLevel >= 1) acMods_Computed.Armor += 4;
                    if (oracleLevel >= 7) acMods_Computed.Armor += 2;
                    if (oracleLevel >= 11) acMods_Computed.Armor += 2;
                    if (oracleLevel >= 15) acMods_Computed.Armor += 2;
                    if (oracleLevel >= 19) acMods_Computed.Armor += 2;
                }
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("gunslinger")) //Nimble (Ex)
            {
                int gunslingerLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("gunslinger");
                if (gunslingerLevel >= 2) acMods_Computed.Dodge += 1;
                if (gunslingerLevel >= 6) acMods_Computed.Dodge += 1;
                if (gunslingerLevel >= 10) acMods_Computed.Dodge += 1;
                if (gunslingerLevel >= 14) acMods_Computed.Dodge += 1;
                if (gunslingerLevel >= 18) acMods_Computed.Dodge += 1;
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("swashbuckler")) //Nimble (Ex)
            {
                int swashbucklerLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("swashbuckler");
                if (swashbucklerLevel >= 3) acMods_Computed.Dodge += 1;
                if (swashbucklerLevel >= 7) acMods_Computed.Dodge += 1;
                if (swashbucklerLevel >= 11) acMods_Computed.Dodge += 1;
                if (swashbucklerLevel >= 15) acMods_Computed.Dodge += 1;
                if (swashbucklerLevel >= 19) acMods_Computed.Dodge += 1;
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("stalwart defender")) //AC Bonus (Ex)
            {
                int stalwartDefenderLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("stalwart defender");
                if (stalwartDefenderLevel >= 1) acMods_Computed.Dodge += 1;
                if (stalwartDefenderLevel >= 4) acMods_Computed.Dodge += 1;
                if (stalwartDefenderLevel >= 7) acMods_Computed.Dodge += 1;
                if (stalwartDefenderLevel >= 10) acMods_Computed.Dodge += 1;
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("ranger") && _sbCheckerBaseInput.MonsterSBSearch.HasArchetype("shapeshifter"))
            {
                if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("form of the dragon"))
                {
                    acMods_Computed.Natural += 2;
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("staff magus")) //Quarterstaff Defense (Ex)
            {
                int magusLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("magus");
               //TO DO
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Aldori Dueling Mastery"))
            {
                acMods_Computed.Shield += 2; //Aldori Dueling Mastery
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("prophet of kalistrade"))
            {
                int level = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("prophet of kalistrade");
                //Auspicious Display --assumes the bling is present
                int mod = 1;
                if (level >= 4) mod++;
                if (level >= 7) mod++;
                if (level >= 10) mod++;
                acMods_Computed.Dodge += mod;
            }

            //should be handled SBChecker.ParseACMods()
            //foreach (OnGoingStatBlockModifier mod in _onGoingMods)
            //{
            //    if (mod.ModType == OnGoingStatBlockModifier.StatBlockModifierTypes.AC)
            //    {
            //        onGoingMods += mod.Modifier;
            //    }
            //}

            if (_sbCheckerBaseInput.MonsterSBSearch.HasTemplate("worm that walks"))
            {
                int WisMod = _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Wisdom);
                if(WisMod < 2) WisMod = 2;

                acMods_Computed.Insight += WisMod;
            }

            acMods_Computed.Natural += _armorClassData.ACMods_SB.Natural;
            acMods_Computed.Armor += _armorClassData.ACMods_Computed.Armor;
            acMods_Computed.Shield += _armorClassData.ACMods_Computed.Shield;
            acMods_Computed.Deflection += _armorClassData.ACMods_SB.Deflection;
            acMods_Computed.Dodge += _armorClassData.ACMods_SB.Dodge;


            int OtherBonuses = acMods_Computed.Enhancement + acMods_Computed.Deflection + acMods_Computed.Natural
                   + acMods_Computed.Dodge + acMods_Computed.Wis + acMods_Computed.Defending + acMods_Computed.Insight + SizeMod
                   + acMods_Computed.Monk + duelist + onGoingMods;

            formula += " AC = 10 +" + acMods_Computed.Armor.ToString() + " Armor +" + acMods_Computed.Shield.ToString() + " Shield +"
                       + dexUsed.ToString() + " Dex Used +" + OtherBonuses.ToString() + " Other Bonuses +" + acMods_Computed.Rage + " Rage +" + acMods_Computed.BloodRage + " Bloodrage";

            if (OtherBonuses != 0)
            {
                formula += " Other Bonuses =";
                if (acMods_Computed.Enhancement != 0) formula += " +" + acMods_Computed.Enhancement.ToString() + " Enhancement";
                if (acMods_Computed.Deflection != 0)  formula += " +" + acMods_Computed.Deflection.ToString() + " Deflection";
                if (acMods_Computed.Natural != 0) formula += " +" + acMods_Computed.Natural.ToString() + " Natural";
                if (acMods_Computed.Dodge != 0) formula += " +" + acMods_Computed.Dodge.ToString() + " Dodge";
                if (acMods_Computed.Wis != 0) formula += " +" + acMods_Computed.Wis.ToString() + " Wis";
                if (acMods_Computed.Defending != 0)  formula += " +" + acMods_Computed.Defending.ToString() + " defending";
                formula += CommonMethods.GetStringValue(SizeMod) + " SizeMod";
                if (acMods_Computed.Monk != 0)  formula += " +" + acMods_Computed.Monk.ToString() + " Monk";
                if (duelist != 0)  formula += " +" + duelist.ToString() + " duelist";
                if (onGoingMods != 0) formula += " +" + onGoingMods.ToString() + " onGoingMods";
                if (acMods_Computed.Insight != 0) formula += " +" + acMods_Computed.Insight.ToString() + " Insight";
            }

            int CompAC = 10 + acMods_Computed.Armor + acMods_Computed.Shield + dexUsed + OtherBonuses + acMods_Computed.Rage + acMods_Computed.BloodRage;
            int CompTouchAC = 10 + dexUsed + SizeMod + acMods_Computed.Deflection + acMods_Computed.Defending + DodgeBonus + acMods_Computed.Rage + acMods_Computed.BloodRage + acMods_Computed.Wis + acMods_Computed.Monk + duelist + acMods_Computed.Insight;
            int CompFlatFoootedAC = CompAC - dexBonus + flatFootedMod - DodgeBonus - duelist;

            int AC_SB;
            int Touch_SB;
            int Flatfooted_SB;
            if (AC.Contains(CR)) //2nd AC block
            {
                int Pos5 = AC.IndexOf(CR);
                AC = AC.Substring(0, Pos5).Trim();
                Pos5 = AC.IndexOf(PathfinderConstants.PAREN_RIGHT);
                AC = AC.Substring(Pos5 + 1).Trim();
            }
            List<string> ACList_SB = AC.Split(',').ToList();
            if (ACList_SB[0].Contains(PathfinderConstants.PAREN_LEFT))
            {
                ACList_SB[0] = ACList_SB[0].Substring(0, ACList_SB[0].IndexOf(PathfinderConstants.PAREN_LEFT)).Trim();
            }
            try
            {
                AC_SB = Convert.ToInt32(ACList_SB[0]);
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, ex.Message);
                return;
            }
            string temp = ACList_SB[1].Replace("touch", string.Empty);
            try
            {
                Touch_SB = Convert.ToInt32(temp);
            }
            catch
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Issue parsing touch AC");
                return;
            }
            temp = ACList_SB[2].Replace("flat-footed", string.Empty);
            try
            {
                Flatfooted_SB = Convert.ToInt32(temp);
            }
            catch
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Issue parsing flat-footed AC");
                return;
            }

            if (CompAC == AC_SB && CompTouchAC == Touch_SB && CompFlatFoootedAC == Flatfooted_SB)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName,formula);
                return;
            }
            if (CompAC != AC_SB)
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, CompAC.ToString() + PathfinderConstants.SPACE + acMods_Computed.ToString(), AC_SB.ToString(), formula);
            }
            if (CompTouchAC != Touch_SB)
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName + " Touch ", PathfinderConstants.ASCII_10 +  CompTouchAC.ToString() + PathfinderConstants.SPACE + acMods_Computed.ToString(), Touch_SB.ToString(),formula);
            }
            if (CompFlatFoootedAC != Flatfooted_SB)
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName + " Flat-Footed ", CompFlatFoootedAC.ToString() + PathfinderConstants.SPACE + acMods_Computed.ToString(), Flatfooted_SB.ToString(), formula);
            }

            _armorClassData.ACMods_Computed = acMods_Computed;
        }

        public void CheckACMath()
        {
            string CheckName = "AC Math";
            int ComputedAC = ACMods_SB.ComputeAC();
            string formula = ACMods_SB.ComputeACFormula();

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("monk"))
            {
                //int MonkLevel = CharacterClasses.FindClassLevel("monk");
                //ComputedAC += MonkLevel / 4;
            }
            else if (_sbCheckerBaseInput.MonsterSBSearch.HasGear("monk's robe"))
            {
                ComputedAC += 1; //monk level 5 ac bonus
            }

            //inlcude in Dex mod
            //if (CharacterClasses.HasClass("duelist")) //Canny Defense
            //{
            //    int IntMod = _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Intelligence);
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

            if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("daredevil"))
            {
                int BardLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("bard");
            }

            string temp = AC;
            if (AC.Contains(CR)) //2nd AC block
            {
                int Pos2 = AC.IndexOf(CR);
                temp = temp.Substring(0, Pos2).Trim();
                Pos2 = temp.IndexOf(PathfinderConstants.PAREN_RIGHT);
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
            int Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
            if (Pos >= 0)
            {
                temp = temp.Substring(0, Pos).Trim();
            }
            try
            {
                int SB_AC = Convert.ToInt32(temp);

                if (SB_AC != ComputedAC)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, ComputedAC.ToString(), SB_AC.ToString(), formula);
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
                }
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, ex.Message);
            }
        }
    }
}
