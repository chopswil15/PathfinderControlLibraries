using MagicItemAbilityWrapper;
using OnGoing;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using System.Text;
using System.Threading.Tasks;
using PathfinderGlobals;

namespace StatBlockChecker
{
    public class ResistanceChecker : IResistanceChecker
    {
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private IEquipmentData _equipmentData;

        public ResistanceChecker(ISBCheckerBaseInput sbCheckerBaseInput, IEquipmentData equipmentData)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _equipmentData = equipmentData;
        }
        public void CheckResistanceValues()
        {
            string CheckName = "CheckResistanceValues";

            try
            {
                Dictionary<string, int> SB_Resistance = ParseResistance(_sbCheckerBaseInput.MonsterSB.Resist);
                if (_sbCheckerBaseInput.Race_Base.RaceSB == null)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "_sbCheckerBaseInput.Race_Base.RaceSB is null");
                    return;
                }
                Dictionary<string, int> Race_Resistance = ParseResistance(_sbCheckerBaseInput.Race_Base.RaceSB.Resist);

                Dictionary<string, int> MissingResistance = CompareValues(SB_Resistance, Race_Resistance);

                if (MissingResistance.Any())
                {
                    ComputeMagicItemResistance(MissingResistance);

                    if (_sbCheckerBaseInput.CharacterClasses.HasClass("barbarian") && _sbCheckerBaseInput.MonsterSBSearch.HasRagePower("energy resistance"))
                    {
                        int barbarianLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("barbarian");
                        int mod = barbarianLevel / 2;
                        if (mod == 0) mod = 1;
                        string power = _sbCheckerBaseInput.MonsterSBSearch.GetRagePower("energy resistance");
                        string energy = null;
                        if (power.Contains("acid")) energy = "acid";
                        if (power.Contains("cold")) energy = "cold";
                        if (power.Contains("electricity")) energy = "electricity";
                        if (power.Contains("fire")) energy = "fire";
                        if (power.Contains("sonic")) energy = "sonic";
                        CompareOneValues(new KeyValuePair<string, int>(energy, mod), MissingResistance);
                    }
                }


                if (_sbCheckerBaseInput.CharacterClasses.HasClass("oracle") && _sbCheckerBaseInput.MonsterSBSearch.HasMystery("flame"))
                {
                    if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("molten skin"))
                    {
                        int mod = 5;
                        int oracleLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("oracle");
                        if (oracleLevel >= 5) mod += 5;
                        if (oracleLevel >= 11) mod += 10;
                        CompareOneValues(new KeyValuePair<string, int>("fire", mod), MissingResistance);
                    }
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasTemplate("winter witch"))
                {
                    int witchLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("witch");
                    int modWitch = 0;

                    if (witchLevel >= 4) modWitch = 5;
                    if (witchLevel >= 9) modWitch = 10;
                    if (witchLevel >= 14) modWitch = 0;  //becomess immunity to cold

                    CompareOneValues(new KeyValuePair<string, int>("cold", modWitch), MissingResistance);
                }

                if (_sbCheckerBaseInput.CharacterClasses.HasClass("sorcerer"))
                {
                    int sorcererLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("sorcerer");
                    int modSorcerer = 0;

                    switch (_sbCheckerBaseInput.MonsterSBSearch.Bloodline)
                    {
                        case "abyssal":
                            //Demon Resistances
                            if (sorcererLevel >= 3) modSorcerer += 5;
                            if (sorcererLevel >= 9) modSorcerer += 5;
                            CompareOneValues(new KeyValuePair<string, int>("electricity", modSorcerer), MissingResistance);
                            break;
                        case "draconic (black)":
                            if (sorcererLevel >= 3) modSorcerer += 5;
                            if (sorcererLevel >= 9) modSorcerer += 5;
                            CompareOneValues(new KeyValuePair<string, int>("acid", modSorcerer), MissingResistance);
                            break;
                        case "elemental":
                            string energy = null;
                            if (_sbCheckerBaseInput.MonsterSBSearch.HasBloodline("air")) energy = "electricity";
                            if (_sbCheckerBaseInput.MonsterSBSearch.HasBloodline("water")) energy = "cold";

                            if (energy == null) _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Missing Bloodline Elemental Energy");
                            modSorcerer = sorcererLevel >= 3 ? 10 : 0;
                            if (sorcererLevel >= 9) modSorcerer += 10;

                            CompareOneValues(new KeyValuePair<string, int>(energy, modSorcerer), MissingResistance);
                            break;
                        case "elemental (fire)":
                            modSorcerer = sorcererLevel >= 3 ? 10 : 0;
                            if (sorcererLevel >= 9) modSorcerer += 10;

                            CompareOneValues(new KeyValuePair<string, int>("fire", modSorcerer), MissingResistance);

                            break;
                        case "elemental (water)":
                            modSorcerer = sorcererLevel >= 3 ? 10 : 0;
                            if (sorcererLevel >= 9) modSorcerer += 10;

                            CompareOneValues(new KeyValuePair<string, int>("cold", modSorcerer), MissingResistance);

                            break;
                        case "ghoul":
                            //Leathery Skin
                            if (sorcererLevel >= 3) modSorcerer += 5;
                            if (sorcererLevel >= 9) modSorcerer += 5;

                            CompareOneValues(new KeyValuePair<string, int>("cold", modSorcerer), MissingResistance);
                            break;
                        case "infernal":
                            modSorcerer = sorcererLevel >= 3 ? 5 : 0;
                            if (sorcererLevel >= 9) modSorcerer = 10;
                            CompareOneValues(new KeyValuePair<string, int>("fire", modSorcerer), MissingResistance);
                            break;
                        case "starsoul":
                            if (sorcererLevel >= 3) //Voidwalker
                            {
                                CompareOneValues(new KeyValuePair<string, int>("cold", 5), MissingResistance);
                                CompareOneValues(new KeyValuePair<string, int>("fire", 5), MissingResistance);
                            }
                            break;
                        case "stormborn":
                            if (sorcererLevel >= 3) //Voidwalker
                            {
                                CompareOneValues(new KeyValuePair<string, int>("electricity", 5), MissingResistance);
                                CompareOneValues(new KeyValuePair<string, int>("sonic", 5), MissingResistance);
                            }
                            break;
                        case "undead":
                            if (sorcererLevel >= 3) modSorcerer += 5;
                            if (sorcererLevel >= 9) modSorcerer += 5;

                            CompareOneValues(new KeyValuePair<string, int>("cold", modSorcerer), MissingResistance);
                            break;
                    }
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasDomain("Water"))
                {
                    //Cold Resistance (Ex)
                    int clericLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("cleric");
                    int levelUsed = 0;
                    int coldMod = 0;
                    if (clericLevel == 0)
                        levelUsed = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("druid");
                    else
                        levelUsed = clericLevel;

                    if (clericLevel >= 6) coldMod += 10;
                    if (clericLevel >= 12) coldMod += 10;
                    CompareOneValues(new KeyValuePair<string, int>("cold", coldMod), MissingResistance);
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasDomain("Fire"))
                {
                    //Cold Resistance (Ex)
                    int clericLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("cleric");
                    int levelUsed = 0;
                    int coldMod = 0;
                    if (clericLevel == 0)
                        levelUsed = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("druid");
                    else
                        levelUsed = clericLevel;

                    if (clericLevel >= 6) coldMod += 10;
                    if (clericLevel >= 12) coldMod += 10;
                    CompareOneValues(new KeyValuePair<string, int>("fire", coldMod), MissingResistance);
                }


                if (_sbCheckerBaseInput.CharacterClasses.HasClass("storm kindler"))
                {
                    int stormKindlerLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("storm kindler");
                    int modStormKindler = 5;

                    if (stormKindlerLevel >= 5) modStormKindler = 10;
                    if (stormKindlerLevel >= 9) modStormKindler = 20;
                    CompareOneValues(new KeyValuePair<string, int>("sonic", modStormKindler), MissingResistance);
                    CompareOneValues(new KeyValuePair<string, int>("electricity", modStormKindler), MissingResistance);
                }



                if (_sbCheckerBaseInput.MonsterSBSearch.Race().ToLower() == "kobold")
                {
                    if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Draconic Aspect (blue)"))
                    {
                        CompareOneValues(new KeyValuePair<string, int>("electricity", 5), MissingResistance);
                    }
                    else if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Draconic Aspect (black)"))
                    {
                        CompareOneValues(new KeyValuePair<string, int>("acid", 5), MissingResistance);
                    }
                    else if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Draconic Aspect (green)"))
                    {
                        CompareOneValues(new KeyValuePair<string, int>("acid", 5), MissingResistance);
                    }
                    else if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Draconic Aspect (red)"))
                    {
                        CompareOneValues(new KeyValuePair<string, int>("fire", 5), MissingResistance);
                    }
                    else if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Draconic Aspect (white)"))
                    {
                        CompareOneValues(new KeyValuePair<string, int>("cold", 5), MissingResistance);
                    }
                }

                //keep as last
                if (_sbCheckerBaseInput.CharacterClasses.HasClass("abjurer"))
                {
                    int abjurerLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("abjurer");
                    int modAbjurer = 0;

                    if (abjurerLevel >= 1) modAbjurer = 5;
                    if (abjurerLevel >= 11) modAbjurer = 10;
                    if (MissingResistance.Count == 1)
                    {
                        //aasume it's the missing one
                        string energy = MissingResistance.Keys.ElementAt(0);
                        CompareOneValues(new KeyValuePair<string, int>(energy, modAbjurer), MissingResistance);
                    }
                    else
                    {
                        //find matching energy value
                    }
                }

                if (MissingResistance.Any())
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Resistance Values are different - " + "{" + string.Join(",", MissingResistance.Select(kv => kv.Key.ToString() + "=" + kv.Value.ToString()).ToArray()) + "}");
                else
                    _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            catch (Exception ex)
            {
                throw new Exception(CheckName + "-----" + ex.Message);
            }
        }

        public void CheckDamageResistance()
        {
            string CheckName = "CheckDamageResistance";

            Dictionary<string, int> SB_DR = ParseDR(_sbCheckerBaseInput.MonsterSB.DR);
            if (_sbCheckerBaseInput.Race_Base.RaceSB == null)
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "_sbCheckerBaseInput.Race_Base.RaceSB is null");
                return;
            }
            Dictionary<string, int> Race_DR = ParseDR(_sbCheckerBaseInput.Race_Base.RaceSB.DR);

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("barbarian") && !_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("savage barbarian"))
            {
                int barbarianLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("barbarian");
                int DRValue = 0;
                if (barbarianLevel >= 7) DRValue++;
                if (barbarianLevel >= 10) DRValue++;
                if (barbarianLevel >= 13) DRValue++;
                if (barbarianLevel >= 16) DRValue++;
                if (barbarianLevel >= 19) DRValue++;

                string ragepowers = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("rage powers");
                if (ragepowers.Contains("increased damage reduction")) DRValue++;

                if (DRValue > 0) Race_DR = AddDRValue("-", DRValue, Race_DR);
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasTemplate("worm that walks"))
            {
                AddDRValue("-", 15, Race_DR);
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasBloodline("undead"))
            {
                int sorcererLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("sorcerer");
                int DRValue = 0;
                if (sorcererLevel >= 3) DRValue += 5;
                if (sorcererLevel >= 9) DRValue += 5;
                if (DRValue > 0) Race_DR = AddDRValue("lethal", DRValue, Race_DR);
            }

            if (_sbCheckerBaseInput.IndvSB != null)
            {
                List<OnGoingStatBlockModifier> Mods = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockMods();
                foreach (OnGoingStatBlockModifier mod in Mods)
                {
                    if (mod.ModType == OnGoingStatBlockModifier.StatBlockModifierTypes.DR)
                    {
                        string ModValue = mod.ConditionGroup;
                        int Pos = ModValue.IndexOf(PathfinderConstants.SPACE);
                        ModValue = ModValue.Substring(0, Pos);
                        Pos = ModValue.IndexOf("/");
                        string DR = ModValue.Substring(Pos + 1);
                        int Value = int.Parse(ModValue.Substring(0, Pos));
                        Race_DR = AddDRValue(DR, Value, Race_DR);
                    }
                }
            }

            Dictionary<string, int> MissingDR = CompareValues(SB_DR, Race_DR);

            if (MissingDR.Any())
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "DR Values are different - " + "{" + string.Join(", ", MissingDR.Select(kv => kv.Key.ToString() + "=" + kv.Value.ToString()).ToArray()) + "}");
            else
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
        }

        private void ComputeMagicItemResistance(Dictionary<string, int> MissingResistance)
        {
            foreach (MagicItemAbilitiesWrapper wrapper in _equipmentData.MagicItemAbilities)
            {
                if (wrapper != null)
                {
                    foreach (OnGoing.IOnGoing SBMods in wrapper.OnGoingStatBlockModifiers)
                    {
                        if (SBMods.OnGoingType == OnGoingType.StatBlock)
                        {
                            OnGoingStatBlockModifier Mod = (OnGoingStatBlockModifier)SBMods;
                            if (Mod.ModType == OnGoingStatBlockModifier.StatBlockModifierTypes.Resist)
                            {
                                string tempResist = null;


                                switch (Mod.SubType)
                                {
                                    case OnGoingStatBlockModifier.StatBlockModifierSubTypes.Resist_Acid:
                                        tempResist = "acid";
                                        break;
                                    case OnGoingStatBlockModifier.StatBlockModifierSubTypes.Resist_Cold:
                                        tempResist = "cold";
                                        break;
                                    case OnGoingStatBlockModifier.StatBlockModifierSubTypes.Resist_Electricity:
                                        tempResist = "electricity";
                                        break;
                                    case OnGoingStatBlockModifier.StatBlockModifierSubTypes.Resist_Fire:
                                        tempResist = "fire";
                                        break;
                                    case OnGoingStatBlockModifier.StatBlockModifierSubTypes.Resist_Sonic:
                                        tempResist = "sonic";
                                        break;
                                }
                                CompareOneValues(new KeyValuePair<string, int>(tempResist, Mod.Modifier), MissingResistance);
                            }
                        }
                    }
                }
            }
        }

        private void CompareOneValues(KeyValuePair<string, int> kvp, Dictionary<string, int> MissingDR)
        {
            int value;
            if (MissingDR.Any() && kvp.Key != null)
            {
                if (MissingDR.TryGetValue(kvp.Key, out value))
                {
                    if (kvp.Value == value)
                    {
                        MissingDR.Remove(kvp.Key);
                    }
                    else if (kvp.Value < value)
                    {
                        MissingDR[kvp.Key] = value - kvp.Value;
                    }
                    else // (kvp.Value > value)
                    {
                        MissingDR[kvp.Key] = kvp.Value - value;
                    }
                }
            }
        }

        private Dictionary<string, int> ParseResistance(string Resistance)
        {
            Resistance = Resistance.Replace(" and ", ",");
            Dictionary<string, int> tempDR = new Dictionary<string, int>();
            List<string> tempResist = Resistance.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
            tempResist = tempResist.Select(x => x.Trim()).ToList();

            foreach (string Resist in tempResist)
            {
                try
                {
                    int Pos = Resist.LastIndexOf(PathfinderConstants.SPACE);
                    int value = int.Parse(Resist.Substring(Pos));
                    string temp = Resist.Replace(value.ToString(), string.Empty).Trim();
                    tempDR.Add(temp, value);
                }
                catch
                {
                    continue;
                }
            }

            return tempDR;
        }

        private Dictionary<string, int> AddDRValue(string DR, int Value, Dictionary<string, int> DRValues)
        {
            int valueOut;
            if (DRValues.TryGetValue(DR, out valueOut))
                DRValues[DR] += Value;
            else
                DRValues.Add(DR, Value);

            return DRValues;
        }

        private Dictionary<string, int> CompareValues(Dictionary<string, int> SB_DR, Dictionary<string, int> Race_DR)
        {
            Dictionary<string, int> MissingDR = new Dictionary<string, int>();
            Dictionary<string, int> FoundDR = new Dictionary<string, int>();

            foreach (KeyValuePair<string, int> kvp in Race_DR)
            {
                int value;
                if (SB_DR.TryGetValue(kvp.Key, out value))
                {
                    if (kvp.Value == value)
                    {
                        FoundDR.Add(kvp.Key, kvp.Value);
                        continue;
                    }
                    if (kvp.Value < value)
                    {
                        FoundDR.Add(kvp.Key, value - kvp.Value);
                        MissingDR.Add(kvp.Key, value - kvp.Value);
                        continue;
                    }
                    if (kvp.Value > value)
                    {
                        FoundDR.Add(kvp.Key, value);
                        MissingDR.Add(kvp.Key, kvp.Value - value);
                    }
                }
                else
                {
                    MissingDR.Add(kvp.Key, kvp.Value);
                }
            }

            foreach (KeyValuePair<string, int> kvp in SB_DR)
            {
                int value;
                if (!Race_DR.TryGetValue(kvp.Key, out value))
                    MissingDR.Add(kvp.Key, kvp.Value);
            }

            return MissingDR;
        }

        private Dictionary<string, int> ParseDR(string damageResistanceString)
        {
            Dictionary<string, int> tempDR = new Dictionary<string, int>();

            List<string> holdDR;
            damageResistanceString = damageResistanceString.Replace(",", " and");

            if (damageResistanceString.Contains(" and"))
            {
                holdDR = damageResistanceString.Split(new string[] { "and" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            else
            {
                holdDR = damageResistanceString.Split(new string[] { "or" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            int holdValue = 0;


            foreach (string hold in holdDR)
            {
                string temp2 = hold;
                int Pos = temp2.IndexOf(PathfinderConstants.PAREN_LEFT);
                if (Pos > 0) temp2 = temp2.Substring(0, Pos);
                Pos = temp2.IndexOf("/");
                string holdTemp;
                if (Pos != -1 && holdValue == 0)
                {
                    string temp = temp2.Substring(0, Pos);
                    holdTemp = temp2.Replace(temp, string.Empty);
                    holdTemp = holdTemp.Replace("/", string.Empty).Trim();
                    if (holdValue == 0) holdValue = int.Parse(temp);
                }
                else
                {
                    holdTemp = hold;
                }
                tempDR.Add(holdTemp, holdValue);
            }

            return tempDR;
        }
    }
}
