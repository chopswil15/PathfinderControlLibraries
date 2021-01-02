using PathfinderGlobals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace StatBlockChecker.Checkers
{
    public class InitModValueChecker : IInitModValueChecker
    {
        private ISBCheckerBaseInput _sbCheckerBaseInput;

        public InitModValueChecker(ISBCheckerBaseInput sbCheckerBaseInput)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
        }

        public void CheckInitModValue()
        {
            string CheckName = "Init Mod";
            int intMod = _sbCheckerBaseInput.AbilityScores.DexMod;
            string holdInit = Utility.GetNonParenValue(_sbCheckerBaseInput.MonsterSB.Init);
            holdInit = holdInit.Replace("M", string.Empty);
            string formula = "+" + _sbCheckerBaseInput.AbilityScores.DexMod.ToString() + " Dex Mod";

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Improved Initiative"))
            {
                intMod += 4;
                formula += " +4 Improved Initiative";
            }
            if (_sbCheckerBaseInput.MonsterSBSearch.IsMythic)
            {
                if (_sbCheckerBaseInput.MonsterSBSearch.HasMythicFeat("Improved Initiative"))
                {
                    int mythicValue = _sbCheckerBaseInput.MonsterSBSearch.MythicValue;
                    intMod += mythicValue;
                    formula += " +" + mythicValue.ToString() + " Mythic Improved Initiative";
                }
                if (_sbCheckerBaseInput.MonsterSBSearch.HasSpecialAttack("amazing initiative"))
                {
                    int mythicValue = _sbCheckerBaseInput.MonsterSBSearch.MythicValue;
                    intMod += mythicValue;
                    formula += " +" + mythicValue.ToString() + " amazing initiative";
                }
            }
            if (_sbCheckerBaseInput.MonsterSBSearch.HasTrait("Reactionary"))
            {
                intMod += 2;
                formula += " +2 Reactionary";
            }
            if (_sbCheckerBaseInput.MonsterSBSearch.HasTrait("Elven Reflexes"))
            {
                intMod += 2;
                formula += " +2 Elven Reflexes";
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("diviner"))
            {
                int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("diviner") / 2;
                intMod += Mod2 == 0 ? 1 : Mod2;
                formula += " +" + (Mod2 == 0 ? 1 : Mod2).ToString() + " diviner";
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("shieldmarshal"))
            {
                int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("shieldmarshal") / 2;
                intMod += Mod2 == 0 ? 1 : Mod2;
                formula += " +" + (Mod2 == 0 ? 1 : Mod2).ToString() + " shieldmarshal";
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("Inquisitor"))
            {
                int InquistorLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Inquisitor");
                if (InquistorLevel >= 2)
                {
                    intMod += _sbCheckerBaseInput.AbilityScores.WisMod;
                    formula += " +" + (_sbCheckerBaseInput.AbilityScores.WisMod).ToString() + " Inquisitor Wisdom Mod";
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Aldori Dueling Mastery"))
            {
                intMod += 2;
                formula += " +2 Aldori Dueling Mastery";
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("gunslinger"))
            {
                int gunslingerLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("gunslinger");
                if (gunslingerLevel >= 3)
                {
                    if (_sbCheckerBaseInput.MonsterSBSearch.HasDeed("gunslinger initiative"))
                    {
                        string SA = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("grit");
                        if (SA.Length > 0)
                        {
                            SA = SA.Replace("grit (", string.Empty);
                            SA = SA.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                            int grit = int.Parse(SA);
                            if (grit > 0)
                            {
                                intMod += 2;
                                formula += " +2 gunslinger initiative";
                            }
                        }
                    }
                }
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("swashbuckler") && _sbCheckerBaseInput.MonsterSBSearch.HasDeed("swashbuckler initiative"))
            {
                intMod += 2;
                formula += " +2 Swashbuckler Initiative"; //asumes 1 panache points exists
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("duelist")) //Improved Reaction
            {
                int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("duelist");
                int tempMod = 0;
                if (Mod2 >= 2)
                {
                    tempMod += 2;
                }
                if (Mod2 >= 8)
                {
                    tempMod += 2;
                }
                intMod += tempMod;
                formula += " +" + tempMod.ToString() + " duelist";
            }

            #region ClassArchetypes

            if (_sbCheckerBaseInput.MonsterSBSearch.HasAnyClassArchetypes())
            {
                if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("tactician"))
                {
                    int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("fighter");
                    int Mod = 0;

                    if (Mod2 >= 2) Mod++;
                    if (Mod2 >= 6) Mod++;
                    if (Mod2 >= 10) Mod++;
                    if (Mod2 >= 14) Mod++;
                    if (Mod2 >= 18) Mod++;

                    intMod += Mod;
                    formula += " +" + Mod.ToString() + " tactician";
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("hooded champion"))
                {
                    int rangerLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("ranger");

                    if (rangerLevel >= 3)
                    {
                        intMod += 2;
                        formula += " +2 hooded champion’s initiative";
                    }
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("deep walker"))
                {
                    int rangerLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("ranger");

                    if (rangerLevel >= 3)
                    {
                        intMod += 2;
                        formula += " +2 Deep Knowledge";
                    }
                }

                #endregion ClassArchetypes

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFamiliar())
            {
                int witchLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("witch");
                string familiar = _sbCheckerBaseInput.MonsterSBSearch.FindFamiliarString(witchLevel > 0);
                if (familiar == "scorpion")
                {
                    intMod += 4;
                    formula += " +4 scorpion";
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("otherworldly insight"))
            {
                intMod += 10;
                formula += " +10 otherworldly insight";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFamiliar() && _sbCheckerBaseInput.MonsterSBSearch.FindFamiliarString(_sbCheckerBaseInput.CharacterClasses.HasClass("witch")) == "compsognathus")
            {
                intMod += 4;
                formula += " +4 compsognathus Familiar";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.Name.ToLower().Contains("time dragon"))
            {
                string name = _sbCheckerBaseInput.MonsterSBSearch.Name.ToLower().Replace("time dragon", string.Empty).Trim();
                int timeDragonAgeMod = 0;
                string ModName = string.Empty;

                switch (name)
                {
                    case "wyrmling":
                        timeDragonAgeMod = 1;
                        ModName = " +1 wyrmling time dragon";
                        break;
                    case "very young":
                        timeDragonAgeMod = 2;
                        ModName = " +2 very young time dragon";
                        break;
                    case "young":
                        timeDragonAgeMod = 3;
                        ModName = " +3 young time dragon";
                        break;
                    case "juvenile":
                        timeDragonAgeMod = 4;
                        ModName = " +4 juvenile time dragon";
                        break;
                    case "young adult":
                        timeDragonAgeMod = 5;
                        ModName = " +5 young adult time dragon";
                        break;
                    case "adult":
                        timeDragonAgeMod = 6;
                        ModName = " +6 adult time dragon";
                        break;
                    case "mature":
                        timeDragonAgeMod = 7;
                        ModName = " +7 mature time dragon";
                        break;
                    case "old":
                        timeDragonAgeMod = 8;
                        ModName = " +8 old time dragon";
                        break;
                    case "very old":
                        timeDragonAgeMod = 9;
                        ModName = " +9 very old time dragon";
                        break;
                    case "ancient":
                        timeDragonAgeMod = 10;
                        ModName = " +10 ancient time dragon";
                        break;
                    case "wyrm":
                        timeDragonAgeMod = 11;
                        ModName = " +11 wyrm time dragon";
                        break;
                    case "great wyrm":
                        timeDragonAgeMod = 12;
                        ModName = " +12 great wyrm time dragon";
                        break;
                }
                intMod += timeDragonAgeMod;
                formula += ModName;
            }

            int compInt = -1000;

            try
            {
                compInt = Convert.ToInt32(holdInit);
            }
            catch
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Parse issue with holdInit value = " + holdInit);
                return;
            }

            if (intMod == compInt)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, intMod.ToString(), holdInit, formula);
            }
        }
        }
    }
}
