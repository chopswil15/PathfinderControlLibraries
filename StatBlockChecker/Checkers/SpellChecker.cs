using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using StatBlockCommon;
using ClassManager;
using Utilities;
using OnGoing;
using StatBlockCommon.Monster_SB;
using StatBlockCommon.Individual_SB;
using PathfinderGlobals;
using StatBlockBusiness;

namespace StatBlockChecker
{
    public class SpellChecker
    {
        private StatBlockMessageWrapper _messageXML;
        private ClassMaster _characterClasses;
        private Dictionary<string, SpellList> _classSpells;
        private MonsterStatBlock _monsterSB;
        private MonSBSearch _monSBSearch;
        private Dictionary<string, SpellList> _sla;
        private IndividualStatBlock_Combat _indvSB;
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private ISpellStatBlockBusiness _spellStatBlockBusiness;

        public SpellChecker(ISBCheckerBaseInput sbCheckerBaseInput, Dictionary<string, SpellList> classSpells,
            Dictionary<string, SpellList> SLA, ISpellStatBlockBusiness spellStatBlockBusiness)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _messageXML = _sbCheckerBaseInput.MessageXML;
            _characterClasses = _sbCheckerBaseInput.CharacterClasses;
            _classSpells = classSpells;
            _monSBSearch = _sbCheckerBaseInput.MonsterSBSearch;
            _sla = SLA;
            _monsterSB = _sbCheckerBaseInput.MonsterSB;
            _indvSB = _sbCheckerBaseInput.IndvSB;
            _spellStatBlockBusiness = spellStatBlockBusiness;
        }

        public static void CheckEmpowerSpellLikeAbilityMonster(int casterLevel, SpellData spellData, ISpellStatBlock spell,
              StatBlockMessageWrapper _messageXML)
        {
            if ((spellData.metaMagicPowers & StatBlockInfo.MetaMagicPowers.empowered) == StatBlockInfo.MetaMagicPowers.empowered)
            {
                int MinCasterLevelNeeded = -1;
                switch (spell.SLA_Level)
                {
                    case 0:
                        MinCasterLevelNeeded = 4;
                        break;
                    case 1:
                        MinCasterLevelNeeded = 6;
                        break;
                    case 2:
                        MinCasterLevelNeeded = 8;
                        break;
                    case 3:
                        MinCasterLevelNeeded = 10;
                        break;
                    case 4:
                        MinCasterLevelNeeded = 12;
                        break;
                    case 5:
                        MinCasterLevelNeeded = 14;
                        break;
                    case 6:
                        MinCasterLevelNeeded = 16;
                        break;
                    case 7:
                        MinCasterLevelNeeded = 18;
                        break;
                    case 8:
                        MinCasterLevelNeeded = 20;
                        break;
                }

                if (casterLevel >= MinCasterLevelNeeded)
                {
                    _messageXML.AddPass("Empower spell-Like Ability-" + spellData.Name);
                }
                else
                {
                    _messageXML.AddFail("Empower spell-Like Ability-" + spellData.Name, MinCasterLevelNeeded.ToString(), casterLevel.ToString());
                }
            }
        }

        public static void CheckQuickenSpellLikeAbilityMonster(int casterLevel, SpellData spellData, ISpellStatBlock spell, StatBlockMessageWrapper _messageXML)
        {
            if ((spellData.metaMagicPowers & StatBlockInfo.MetaMagicPowers.quickened) == StatBlockInfo.MetaMagicPowers.quickened)
            {
                int MinCasterLevelNeeded = -1;
                switch (spell.SLA_Level)
                {
                    case 0:
                        MinCasterLevelNeeded = 8;
                        break;
                    case 1:
                        MinCasterLevelNeeded = 10;
                        break;
                    case 2:
                        MinCasterLevelNeeded = 12;
                        break;
                    case 3:
                        MinCasterLevelNeeded = 14;
                        break;
                    case 4:
                        MinCasterLevelNeeded = 16;
                        break;
                    case 5:
                        MinCasterLevelNeeded = 18;
                        break;
                    case 6:
                        MinCasterLevelNeeded = 20;
                        break;
                }

                if (casterLevel >= MinCasterLevelNeeded)
                {
                    _messageXML.AddPass("Quicken spell-Like Ability-" + spellData.Name);
                }
                else
                {
                    _messageXML.AddFail("Quicken spell-Like Ability-" + spellData.Name, MinCasterLevelNeeded.ToString(), casterLevel.ToString());
                }
            }
        }

        public void CheckSpellDC(bool isGnome)
        {
            List<string> classNames = _characterClasses.GetClassNames();
            SpellList SL;
            string formula = string.Empty;
            Dictionary<string, SpellList> classSpellsClone = new Dictionary<string, SpellList>(_classSpells);

            foreach (string className in classNames)
            {
                formula = string.Empty;
                if (_characterClasses.CanClassCastSpells(className.ToLower()))
                {
                    if (classSpellsClone.ContainsKey(className))
                    {
                        SL = CkeckClassSpellDCs(isGnome, ref formula, className);
                        classSpellsClone.Remove(className);
                    }
                }
            }

            if (!classNames.Any() && classSpellsClone.Any() && _monsterSB.SpellsPrepared.Length > 0)
            {
                string temp = _monsterSB.SpellsPrepared;
                temp = temp.Substring(0, temp.IndexOf(PathfinderConstants.SPACE));
                if (classSpellsClone.ContainsKey(temp))
                {
                    SL = _classSpells[temp];
                    List<SpellData> listOfSpells = SL.ListOfSpells;
                    int abilityBonus = StatBlockInfo.GetAbilityModifier(_monsterSB.GetAbilityScoreValue(_characterClasses.GetSpellBonusAbility(temp)));

                    foreach (SpellData spellData in listOfSpells)
                    {
                        if (spellData.DC > 0)
                        {
                            int computedDC = 10 + spellData.Level + abilityBonus;
                            formula = "10 +" + spellData.Level.ToString() + " spell level +" + abilityBonus.ToString() + " ability bonus";
                            if (computedDC == spellData.DC)
                            {
                                _messageXML.AddPass("Spell DC-" + spellData.Name);
                            }
                            else
                            {
                                _messageXML.AddFail("Spell DC-" + spellData.Name, computedDC.ToString(), spellData.DC.ToString(), formula);
                            }
                        }
                    }
                    classSpellsClone.Remove(temp);
                }
            }

            if (!classNames.Any() && classSpellsClone.Any() && _monsterSB.SpellsKnown.Length > 0)
            {
                string temp = _monsterSB.SpellsKnown;
                temp = temp.Substring(0, temp.IndexOf(PathfinderConstants.SPACE));
                if (classSpellsClone.ContainsKey(temp))
                {
                    SL = _classSpells[temp];
                    List<SpellData> listOfSpells = SL.ListOfSpells;
                    if (temp == "Spells") temp = "Sorcerer";
                    int abilityBonus = StatBlockInfo.GetAbilityModifier(_monsterSB.GetAbilityScoreValue(_characterClasses.GetSpellBonusAbility(temp)));

                    foreach (SpellData spellData in listOfSpells)
                    {
                        if (spellData.DC > 0)
                        {
                            int computedDC = 10 + spellData.Level + abilityBonus;
                            formula = "10 +" + spellData.Level.ToString() + " spell level +" + abilityBonus.ToString() + " ability bonus";
                            if (computedDC == spellData.DC)
                            {
                                _messageXML.AddPass("Spell DC-" + spellData.Name);
                            }
                            else
                            {
                                _messageXML.AddFail("Spell DC-" + spellData.Name, computedDC.ToString(), spellData.DC.ToString(), formula);
                            }
                        }
                    }
                }
                classSpellsClone.Remove(temp);
            }

            if (classSpellsClone.Any())
            {
                _messageXML.AddFail("Spell DC-", " Can't find Spell Block(s) for these classes: " + string.Join(", ", classSpellsClone.Keys.ToArray()));
            }
        }

        private SpellList CkeckClassSpellDCs(bool isGnome, ref string formula, string name)
        {
            SpellList SL;
            {
                SL = _classSpells[name];
                List<SpellData> listOfSpells = SL.ListOfSpells;
                int abilityScoreValue = _monsterSB.GetAbilityScoreValue(_characterClasses.GetSpellBonusAbility(name));
                OnGoingStatBlockModifier.StatBlockModifierSubTypes subType = Utility.GetOnGoingAbilitySubTypeFromString(_characterClasses.GetSpellBonusAbility(name));
                abilityScoreValue += _indvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.Ability, subType);
                int abilityBonus = StatBlockInfo.GetAbilityModifier(abilityScoreValue);

                int bonus = 0;

                foreach (SpellData spellData in listOfSpells)
                {
                    try
                    {
                        string temp = spellData.Name.Replace("†", string.Empty);
                        List<string> schoolList;

                        temp = Utility.RemoveSuperScripts(temp);

                        string search = Utility.SearchMod(temp);
                        if (search == "empty slot") continue;
                        ISpellStatBlock spell = _spellStatBlockBusiness.GetSpellByName(search);
                        bonus = 0;

                        if (spell == null)
                        {
                            _messageXML.AddFail("Spell DC", "Missing spell: " + search);
                        }
                        else
                        {
                            if (_monSBSearch.HasSpellFocusFeat(out schoolList))
                            {
                                if (schoolList.Contains(spell.school)) bonus++;
                                List<string> schoolList2;
                                if (_monSBSearch.HasGreaterSpellFocusFeat(out schoolList2))
                                {
                                    if (schoolList2.Contains(spell.school)) bonus++;
                                }
                                if (SLA_SaveFail(spellData, spell))
                                {
                                    _messageXML.AddFail("SLA Save-" + spellData.Name, spell.saving_throw, spellData.DC.ToString());
                                }
                            }

                            if (_monSBSearch.HasElementalSkillFocusFeat(out schoolList))
                            {
                                if (schoolList.Contains(spell.school)) bonus++;
                                List<string> schoolList2;
                                if (_monSBSearch.HasGreaterElementalSkillFocusFeat(out schoolList2))
                                {
                                    if (schoolList2.Contains(spell.school)) bonus++;
                                }
                                if (SLA_SaveFail(spellData, spell))
                                {
                                    _messageXML.AddFail("SLA Save-" + spellData.Name, spell.saving_throw, spellData.DC.ToString());
                                }
                            }

                            if (spellData.DC > 0)
                            {
                                ComputeSpellDC(isGnome, ref formula, name, abilityBonus, ref bonus, spellData, spell);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _messageXML.AddFail("CheckSpellDC--" + spellData.Name, ex.Message);
                    }
                }
            }

            return SL;
        }

        private void ComputeSpellDC(bool isGnome, ref string formula, string name, int abilityBonus,
            ref int Bonus, SpellData SD, ISpellStatBlock Spell)
        {
            int bloodline = 0;
            int gnomeBonus = 0;

            int spellClassLevel = Spell.GetSpellLevelByClass(name, _monSBSearch.HasCurse("haunted"), _monSBSearch.HasFeat("Shade of the Uskwood"), _monSBSearch.HasArchetype("Razmiran priest"));
            if (SD.Domain) spellClassLevel = SD.Level;
            // if (spellClassLevel < 0 && SD.Domain) spellClassLevel = SD.Level; 
            if (spellClassLevel < 0 && _monSBSearch.HasBloodline())
            {
                Dictionary<string, int> bloodlineBonusSpells = _characterClasses.GetBloodlineBonusSpells();
                if (bloodlineBonusSpells.ContainsKey(Spell.name.ToLower()))
                {
                    spellClassLevel = SD.Level;
                }
            }

            if (spellClassLevel < 0 && _monSBSearch.HasMystery())
            {
                Dictionary<string, int> mysteryBonusSpells = _characterClasses.GetMysteryBonusSpells();
                if (mysteryBonusSpells.ContainsKey(Spell.name.ToLower()))
                {
                    spellClassLevel = SD.Level;
                }
            }
            if (spellClassLevel < 0 && _monSBSearch.HasPatron())
            {
                List<string> patronBonusSpells = _characterClasses.GetPatronBonusSpells();
                if (patronBonusSpells.Contains(Spell.name.ToLower()))
                {
                    spellClassLevel = SD.Level;
                }
            }
            if (isGnome && Spell.school.Contains("illusion"))
            {
                gnomeBonus = 1;
            }
            if (_monSBSearch.HasBloodline("arcane"))
            {
                int sorcererLevel = _characterClasses.FindClassLevel("sorcerer");
                if (sorcererLevel >= 15)
                {
                    string schoolPower = _monSBSearch.GetSQ("school power");
                    schoolPower = schoolPower.Replace("spells)", string.Empty).Trim();
                    int Pos = schoolPower.LastIndexOf(PathfinderConstants.SPACE);
                    schoolPower = schoolPower.Substring(Pos).Trim();
                    if (Spell.school.Contains(schoolPower)) bloodline += 2;
                }
            }
            if (_monSBSearch.HasBloodline("fey"))
            {
                if (Spell.subschool.Contains("compulsion")) bloodline += 2;
            }
            if (_monSBSearch.HasBloodline("stormborn"))
            {
                if (Spell.descriptor.Contains("electricity") || Spell.descriptor.Contains("sonic")) bloodline += 1;
            }
            if (_monSBSearch.HasBloodline("infernal"))
            {
                if (Spell.subschool.Contains("charm")) bloodline += 2;
            }
            if (_monSBSearch.HasClassArchetype("winter witch"))
            {
                if (Spell.descriptor.Contains("cold")) Bonus += 1;
            }
            if (_monSBSearch.Race() == "kitsune" && Spell.school.Contains("enchantment"))
            {
                Bonus += 1;
            }
            if (_monSBSearch.HasSubType("sahkil") && (Spell.school.Contains("emotion") || Spell.school.Contains("fear")))
            {
                Bonus += 2;
            }

            List<string> School;
            if (_monSBSearch.HasElementalSkillFocusFeat(out School))
            {

                if (School.Contains(Spell.descriptor)) Bonus++;
                List<string> School2;
                if (_monSBSearch.HasGreaterElementalSkillFocusFeat(out School2))
                {
                    if (School2.Contains(Spell.descriptor)) Bonus++;
                }
            }

            int computedDC = 10 + spellClassLevel + abilityBonus + Bonus + bloodline + gnomeBonus;
            formula = "10 +" + spellClassLevel.ToString() + " spell class level +" + abilityBonus.ToString() + " ability bonus";
            if (bloodline != 0) formula += " +" + bloodline.ToString() + " bloodline";
            if (gnomeBonus != 0) formula += " +" + gnomeBonus.ToString() + " gnome magic";
            if (Bonus != 0) formula += " +" + Bonus.ToString() + " skill/spell focus bonus";

            if (computedDC == SD.DC)
            {
                _messageXML.AddPass("Spell DC-" + SD.Name);
            }
            else
            {
                _messageXML.AddFail("Spell DC-" + SD.Name, computedDC.ToString(), SD.DC.ToString(), formula);
            }
        }

        private static bool SLA_SaveFail(SpellData SD, ISpellStatBlock Spell)
        {
            var savingThrowLower = Spell.saving_throw.ToLower();
            return SD.DC == -1 && !savingThrowLower.Contains("none") && !savingThrowLower.Contains("harmless")
                && Spell.saving_throw.Length > 0 && !savingThrowLower.Contains("see text") && !savingThrowLower.Contains("negates")
                && !savingThrowLower.Contains("disbelief");
        }

        public void CheckSpellsLevels()
        {
            List<string> classNames = _characterClasses.GetClassNames();
            List<int> levels = new List<int>();
            List<int> levelFrequency = new List<int>();
            SpellList spellList;
            int overloadLevel = 0;
            int overloadClassLevel = 0;

            foreach (string name in classNames)
            {
                if (_characterClasses.CanClassCastSpells(name.ToLower()))
                {
                    spellList = null;
                    if (_classSpells.ContainsKey(name))
                    {
                        spellList = GetClassSpellsByClassName(name);
                    }

                    if (spellList != null)
                    {
                        int casterLevel = spellList.CasterLevel;
                        int classLevel = _characterClasses.FindClassLevel(name);
                        switch (name.ToLower())
                        {
                            case "antipaladin":
                                classLevel -= 3;
                                break;
                            case "paladin":
                                classLevel -= 3;
                                break;
                            case "ranger":
                                classLevel -= 3;
                                break;
                        }

                        overloadLevel = 0;
                        _characterClasses.GetSpellOverLoadsForPrestigeClasses(out overloadLevel, out overloadClassLevel);  
                        classLevel += overloadLevel;

                        if (casterLevel != classLevel)
                        {
                            _messageXML.AddFail("Caster Level", "Character level not equal to caster level for " + name);
                        }

                        GetSpellLevels(ref levels, name, overloadLevel);
                        levelFrequency = _characterClasses.GetClassSpellsPerDay(name.ToLower(), overloadLevel);
                    }
                    string SpellBonusAbility = _characterClasses.GetSpellBonusAbility(name.ToLower());
                    int SpellBonusAbilityValue = _monsterSB.GetAbilityScoreValue(SpellBonusAbility);
                    bool IsInquisitor = name == "Inquisitor";
                    List<int> SpellBonus = StatBlockInfo.GetSpellBonus(SpellBonusAbilityValue, IsInquisitor);

                    switch (name)
                    {
                        case "Bard":
                        case "Skald":
                        case "Sorcerer":
                        case "Summoner":
                        case "Inquisitor":
                        case "Red Mantis Assassin":
                        case "Arcanist":
                            SpellBonus = new List<int>() { 0 };
                            break;
                        case "Oracle": //cure or inflict bonus                    
                            SpellBonus = new List<int> { 0, 1, 1, 1, 1, 1, 1, 1, 1 };
                            break;
                    }


                    if (levels != null)
                    {
                        if (spellList != null)
                        {
                            CheckSpellCount(levels, levelFrequency, spellList, name, SpellBonusAbility, SpellBonusAbilityValue, SpellBonus, overloadClassLevel, IsInquisitor);
                            CheckBonusSpells(spellList, name, _characterClasses.FindClassLevel(name));
                        }
                        else if (levels.Any() && SpellBonusAbilityValue <= 10)
                        {
                            _messageXML.AddInfo("Lacks Ability Score to cast spells with " + SpellBonusAbility + PathfinderConstants.SPACE + SpellBonusAbilityValue.ToString());
                        }
                    }
                    else
                    {
                        _messageXML.AddFail("CheckSpellsLevels", "Failed to load GetSpellLevels() for " + name);
                    }
                }
            }
        }

         

        private void GetSpellLevels(ref List<int> Levels, string name, int overloadLevel)
        {
            if (overloadLevel == 0)
            {
                Levels = _characterClasses.GetClassSpellLevels(name.ToLower());
                if (_monSBSearch.HasArchetype("kensai"))
                {
                    for (int a = 0; a <= Levels.Count - 1; a++)
                    {
                        Levels[a] -= 1;
                    }
                }
            }
            else
            {
                Levels = _characterClasses.GetClassSpellLevels(name.ToLower(), overloadLevel);
            }

            if (_monSBSearch.HasArchetype("spellslinger)")) Levels[0] = 0; //no cantrips for spellslinger
        }

        private void CheckSpellCount(List<int> Levels, List<int> LevelFrequency, SpellList SL, string name, string SpellBonusAbility,
                 int SpellBonusAbilityValue, List<int> SpellBonus, int overloadClassLevel, bool IsInquisitor)
        {
            int BloodlineLevels = _characterClasses.GetClassBloodlineSpellLevels(name, overloadClassLevel);
            int MysteryLevels = _characterClasses.GetClassMysterySpellLevels(name);
         //   int BloatMageLevels = _characterClasses.FindClassLevel("BloatMage");
            int NewArcanaLevels = 0;
            int DomainSpellCount = _characterClasses.GetDomainSpellCount(name.ToLower());
            List<int> OracleCurseSpells = new List<int>() { 0 };
            int ExpandedArcanaCount = 0;
            if (_monSBSearch.HasFeat("Expanded Arcana")) ExpandedArcanaCount = _monSBSearch.FeatItemCount("Expanded Arcana");

            if (_monSBSearch.HasCurse("haunted"))
            {
                int oracleLevel = _characterClasses.FindClassLevel("oracle");
                OracleCurseSpells = new List<int> { 2 };//mage hand and ghost sound 0th
                if (oracleLevel >= 5) OracleCurseSpells.AddRange(new List<int> { 0, 2 }); //levitate and minor image 2nd level
                if (oracleLevel >= 10) OracleCurseSpells.AddRange(new List<int> { 0, 0, 1 }); // telekinesis 5th
                if (oracleLevel >= 15) OracleCurseSpells.AddRange(new List<int> { 0, 1 }); //  reverse gravity 7th
            }

            if (_monSBSearch.HasSQ("new arcana"))
            {
                int sorcererLevel = _characterClasses.FindClassLevel("sorcerer");
                if (sorcererLevel >= 9) NewArcanaLevels++;
                if (sorcererLevel >= 13) NewArcanaLevels++;
                if (sorcererLevel >= 17) NewArcanaLevels++;
            }

            List<SpellData> ListOfSpells = SL.ListOfSpells;
            int SBLevelCount = 0;
            string formula;

            for (int SpellLevelIndex = 0; SpellLevelIndex <= Levels.Count - 1; SpellLevelIndex++)
            {
                SBLevelCount = 0;
                string freq = null;

                ComputeSpellFrequency(ListOfSpells, ref SBLevelCount, SpellLevelIndex, ref freq);

                if (freq != null)
                {
                    int tempFreq = int.Parse(freq);
                    List<int> FrequencySpellBonus = StatBlockInfo.GetSpellBonus(SpellBonusAbilityValue, IsInquisitor);
                    int bonus = FrequencySpellBonus.Count - 1 >= SpellLevelIndex ? FrequencySpellBonus[SpellLevelIndex] : 0;
                    if (LevelFrequency.Any())
                    {
                        if (LevelFrequency[SpellLevelIndex] + bonus == tempFreq)
                        {
                            _messageXML.AddPass("CheckSpellCount--Spell Frequency Level =" + SpellLevelIndex.ToString());
                        }
                        else
                        {
                            string formua = "Base Freq " + LevelFrequency[SpellLevelIndex].ToString();
                            if (bonus > 0) formua += " + " + SpellBonusAbility + " Bonus " + bonus.ToString();
                            _messageXML.AddFail("CheckSpellCount--Spell Frequency Level =" + SpellLevelIndex.ToString(), (LevelFrequency[SpellLevelIndex] + bonus).ToString(), freq, formua);
                        }
                    }
                }

                int ClassLevelCount = Levels[SpellLevelIndex];
                formula = "Level Base: " + ClassLevelCount.ToString();

                if (SpellBonus.Count - 1 >= SpellLevelIndex)
                {
                    ClassLevelCount += SpellBonus[SpellLevelIndex];
                    if (name == "Oracle")
                    {
                        formula += " + " + SpellBonus[SpellLevelIndex].ToString() + " cure/inflict Bonus";
                    }
                    else
                    {
                        formula += " + " + SpellBonus[SpellLevelIndex].ToString() + " Spell Bonus";
                    }
                }
                if (name == "Oracle" && OracleCurseSpells.Count - 1 >= SpellLevelIndex)
                {
                    ClassLevelCount += OracleCurseSpells[SpellLevelIndex];
                    formula += " + " + OracleCurseSpells[SpellLevelIndex].ToString() + " Oracle Curse Spells";
                }

                if (ClassLevelCount < SBLevelCount)
                {
                    if (BloodlineLevels > 0) //assume bloodline spells are diff
                    {
                        int diff = SBLevelCount - ClassLevelCount;
                        ClassLevelCount++;
                        BloodlineLevels--;
                        formula += " + 1 Bloodline Spell";
                        //if (diff <= BloodlineLevels)
                        //{
                        //    ClassLevelCount += diff;
                        //    BloodlineLevels -= diff;
                        //}
                    }

                    if (MysteryLevels > 0) //assume mystery spells are diff
                    {
                        int diff = SBLevelCount - ClassLevelCount;
                        if (diff <= MysteryLevels)
                        {
                            ClassLevelCount += diff;
                            MysteryLevels -= diff;
                            formula += " + " + diff.ToString() + " Mystery Levels";
                        }
                    }

                    if (DomainSpellCount > 0) //assume domain spells are diff
                    {
                        int diff = SBLevelCount - ClassLevelCount;
                        if (diff <= DomainSpellCount)
                        {
                            ClassLevelCount += diff;
                            DomainSpellCount -= diff;
                            formula += " + " + diff.ToString() + " Domain Spell Count";
                        }
                    }

                    if (NewArcanaLevels > 0) //assume New Arcana spells are diff
                    {
                        int diff = SBLevelCount - ClassLevelCount;
                        if (diff <= NewArcanaLevels)
                        {
                            ClassLevelCount += diff;
                            NewArcanaLevels -= diff;
                            formula += " + " + diff.ToString() + " New Arcana Levels";
                        }
                    }
                    if (SBLevelCount - ClassLevelCount > 0 && ExpandedArcanaCount > 0)
                    {
                        int diff = SBLevelCount - ClassLevelCount;
                        ClassLevelCount += diff;
                        ExpandedArcanaCount--;
                        formula += " + 1 Expanded Arcana Levels";
                    }
                }


                if (ClassLevelCount == SBLevelCount && ClassLevelCount != 0)
                {
                    if (SpellLevelIndex + 10 > SpellBonusAbilityValue)
                    {
                        _messageXML.AddFail("Spell Ability Score", "Lacks Ability Score to cast Level " + SpellLevelIndex.ToString() + " spells with "
                            + SpellBonusAbility + PathfinderConstants.SPACE + SpellBonusAbilityValue.ToString() + ", but has spells");
                    }
                    else
                    {
                        _messageXML.AddPass("Spell Count-" + name + " Level: " + SpellLevelIndex.ToString(), ClassLevelCount.ToString() + PathfinderConstants.SPACE + SBLevelCount.ToString() + PathfinderConstants.SPACE + formula);
                    }
                }
                else
                {
                    if (SpellLevelIndex + 10 > SpellBonusAbilityValue)
                    {
                        _messageXML.AddInfo("Lacks Ability Score to cast Level " + SpellLevelIndex.ToString() + " spells with " + SpellBonusAbility + PathfinderConstants.SPACE + SpellBonusAbilityValue.ToString());
                    }
                    else if (SBLevelCount != ClassLevelCount)
                    {
                        _messageXML.AddFail("Spell Count-" + name + " Level: " + SpellLevelIndex.ToString(), ClassLevelCount.ToString(), SBLevelCount.ToString() + PathfinderConstants.SPACE + formula);
                    }
                }
            }
        }

        private void ComputeSpellFrequency(List<SpellData> listOfSpells, ref int sbLevelCount, int spellLevelIndex, ref string freq)
        {
            int Pos = -1;
            foreach (SpellData SD in listOfSpells)
            {
                if (SD.Level == spellLevelIndex)
                {
                    if (SD.Frequency.Length > 0)
                    {
                        freq = SD.Frequency;
                        Pos = freq.IndexOf("/");
                        if (Pos >= 0)
                        {
                            freq = freq.Substring(0, Pos);
                        }
                        try
                        {
                            sbLevelCount += Convert.ToInt32(freq) / SD.Count;
                        }
                        catch (DivideByZeroException ex)
                        {
                            _messageXML.AddFail("CheckSpellCount", "Divide by zero error - Level " + SD.Level.ToString());
                        }
                        catch
                        {
                            _messageXML.AddFail("CheckSpellCount", "Int convert error - Level " + SD.Level.ToString());
                        }
                    }
                    else
                    {
                        sbLevelCount += SD.Count;
                    }
                }
            }
        }

        private SpellList GetClassSpellsByClassName(string name)
        {
            return _classSpells[name];
        }

        public void CheckSLA(bool isGnome)
        {
            if (!_sla.Any()) return;
            SpellList SL = null;
            int AbilityBonus = 0;
            string AbilityBonusName = StatBlockInfo.CHA;
            string formulaConcentration = string.Empty;

            foreach (KeyValuePair<string, SpellList> sla in _sla)
            {
                SL = sla.Value;
                if (sla.Key == "Domain" || sla.Key == "Qinggong Monk")
                {
                    AbilityBonus = StatBlockInfo.GetAbilityModifier(_monsterSB.GetAbilityScoreValue(StatBlockInfo.AbilityName.Wisdom));
                    AbilityBonusName = StatBlockInfo.WIS;
                    formulaConcentration = SL.CasterLevel.ToString() + " Caster Level +" + AbilityBonus.ToString() + " Wisdom modifier";
                }
                else if (sla.Key == "Rogue" || sla.Key.Contains("Evoker") || sla.Key.Contains("Enchanter") || sla.Key.Contains("Arcane") || sla.Key.Contains("Diviner") || sla.Key.Contains("Conjurer"))
                {
                    AbilityBonus = StatBlockInfo.GetAbilityModifier(_monsterSB.GetAbilityScoreValue(StatBlockInfo.AbilityName.Intelligence));
                    AbilityBonusName = StatBlockInfo.INT;
                    formulaConcentration = SL.CasterLevel.ToString() + " Caster Level +" + AbilityBonus.ToString() + " Intelligence modifier";
                }
                else
                {
                    AbilityBonus = StatBlockInfo.GetAbilityModifier(_monsterSB.GetAbilityScoreValue(StatBlockInfo.AbilityName.Charisma));
                    formulaConcentration = SL.CasterLevel.ToString() + " Caster Level +" + AbilityBonus.ToString() + " Charisma modifier";
                }

                int Other = 0;
                if (_monSBSearch.Race() == "Spriggan")
                {
                    Other++;
                    formulaConcentration += " +1 Spriggan Magic";
                }
                int computedConcentration = SL.CasterLevel + AbilityBonus + Other;
                if (SL.Concentration == computedConcentration)
                {
                    _messageXML.AddPass("SLA Concentration-" + SL.Source);
                }
                else
                {
                    _messageXML.AddFail("SLA Concentration-" + SL.Source, computedConcentration.ToString(), SL.Concentration.ToString(), formulaConcentration);
                }

                List<SpellData> ListOfSpells = SL.ListOfSpells;
                int CasterLevel = SL.CasterLevel;
                int gnomeBonus = 0;

                foreach (SpellData SD in ListOfSpells)
                {
                    string search = Utility.SearchMod(SD.Name);
                    ISpellStatBlock Spell = _spellStatBlockBusiness.GetSpellByName(search);
                    if (Spell != null)
                    {
                        CheckQuickenSpellLikeAbilityMonster(CasterLevel, SD, Spell, _messageXML);
                        CheckEmpowerSpellLikeAbilityMonster(CasterLevel, SD, Spell, _messageXML);
                        if (SD.DC > 0)
                        {
                            gnomeBonus = 0;
                            if (isGnome && Spell.school.Contains("illusion")) gnomeBonus = 1;
                            int computedDC = 10 + (Spell.SLA_Level ?? -1) + AbilityBonus + gnomeBonus;
                            string formula = "10 +" + (Spell.SLA_Level ?? -1).ToString() + " Spell.SLA_Level +" + AbilityBonus.ToString() + " AbilityBonus (" + AbilityBonusName + PathfinderConstants.PAREN_RIGHT;
                            if (gnomeBonus != 0) formula += " +" + gnomeBonus.ToString() + " gnome magic";
                            if (_monSBSearch.Race() == "Spriggan") computedDC++;  //Spriggan Magic
                            if (_monSBSearch.HasSubType("sahkil") && (Spell.descriptor.Contains("emotion") || Spell.descriptor.Contains("fear")))
                            {
                                computedDC += 2;
                            }

                            if (computedDC == SD.DC)
                            {
                                _messageXML.AddPass("SLA DC-" + SD.Name);
                            }
                            else
                            {
                                _messageXML.AddFail("SLA DC-" + SD.Name, computedDC.ToString(), SD.DC.ToString(), formula);
                            }
                        }
                        if (SLA_SaveFail(SD, Spell))
                        {
                            _messageXML.AddFail("SLA Save-" + SD.Name, Spell.saving_throw, SD.DC.ToString());
                        }
                    }
                    else
                    {
                        _messageXML.AddInfo("SLA: Missing Spell-" + search);
                    }
                }
            }
        }


        private void CheckBonusSpells(SpellList SL, string name, int casterLevel)
        {
            string checkName = "CheckBonusSpells";

            Dictionary<string, int> MysteryBonusSpells = _characterClasses.GetMysteryBonusSpells();
            if (MysteryBonusSpells != null && MysteryBonusSpells.Any())
            {
                foreach (KeyValuePair<string, int> kvp in MysteryBonusSpells)
                {
                    if (!SL.SpellExists(kvp.Key))
                    {
                        _messageXML.AddFail(checkName, "Missing bonus Mystery Spell from spell list -" + kvp.Key);
                    }
                }
            }

            List<string> PatronBonusSpells = _characterClasses.GetPatronBonusSpells();
            if (PatronBonusSpells != null && PatronBonusSpells.Any())
            {
                foreach (string spell in PatronBonusSpells)
                {
                    if (!SL.SpellExists(spell))
                    {
                        _messageXML.AddFail(checkName, "Missing bonus Patron Spell from spell list -" + spell);
                    }
                }
            }

            Dictionary<string, int> BloodlineBonusSpells = _characterClasses.GetBloodlineBonusSpells();
            if (BloodlineBonusSpells != null && BloodlineBonusSpells.Any())
            {
                foreach (KeyValuePair<string, int> kvp in BloodlineBonusSpells)
                {
                    if (!SL.SpellExists(kvp.Key) && casterLevel >= kvp.Value)
                    {
                        _messageXML.AddFail(checkName, "Missing bonus Bloodline Spell from spell list -" + kvp.Key);
                    }
                }
            }
        }
    }
}
