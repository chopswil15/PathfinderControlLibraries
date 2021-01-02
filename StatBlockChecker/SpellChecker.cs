using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using StatBlockCommon;
using ClassManager;
using Utilities;
using OnGoing;
using D_D_Common;
using StatBlockCommon.Monster_SB;
using StatBlockCommon.Spell_SB;
using StatBlockCommon.Individual_SB;

namespace StatBlockChecker
{
    public class SpellChecker
    {
        private StatBlockMessageWrapper _messageXML;
        private ClassMaster CharacterClasses;
        private Dictionary<string, SpellList> ClassSpells;
        private MonsterStatBlock MonSB;
        private MonSBSearch _monSBSearch;
        private Dictionary<string, SpellList> SLA;
        private IndividualStatBlock_Combat _indvSB;

        public SpellChecker(StatBlockMessageWrapper _messageXML, ClassMaster CharacterClasses, IndividualStatBlock_Combat _indvSB,
               Dictionary<string, SpellList> ClassSpells, MonSBSearch _monSBSearch, MonsterStatBlock MonSB, Dictionary<string, SpellList> SLA)
        {
            this._messageXML = _messageXML;
            this.CharacterClasses = CharacterClasses;
            this.ClassSpells = ClassSpells;
            this._monSBSearch = _monSBSearch;
            this.SLA = SLA;
            this.MonSB = MonSB;
            this._indvSB = _indvSB;
        }

        public static void CheckEmpowerSpellLikeAbilityMonster(int CasterLevel, SpellData SD, ISpellStatBlock Spell, StatBlockMessageWrapper _messageXML)
        {
            if ((SD.metaMagicPowers & StatBlockInfo.MetaMagicPowers.empowered) == StatBlockInfo.MetaMagicPowers.empowered)
            {
                int MinCasterLevelNeeded = -1;
                switch (Spell.SLA_Level)
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

                if (CasterLevel >= MinCasterLevelNeeded)
                {
                    _messageXML.AddPass("Empower Spell-Like Ability-" + SD.Name);
                }
                else
                {
                    _messageXML.AddFail("Empower Spell-Like Ability-" + SD.Name, MinCasterLevelNeeded.ToString(), CasterLevel.ToString());
                }
            }
        }

        public static void CheckQuickenSpellLikeAbilityMonster(int CasterLevel, SpellData SD, ISpellStatBlock Spell, StatBlockMessageWrapper _messageXML)
        {
            if ((SD.metaMagicPowers & StatBlockInfo.MetaMagicPowers.quickened) == StatBlockInfo.MetaMagicPowers.quickened)
            {
                int MinCasterLevelNeeded = -1;
                switch (Spell.SLA_Level)
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

                if (CasterLevel >= MinCasterLevelNeeded)
                {
                    _messageXML.AddPass("Quicken Spell-Like Ability-" + SD.Name);
                }
                else
                {
                    _messageXML.AddFail("Quicken Spell-Like Ability-" + SD.Name, MinCasterLevelNeeded.ToString(), CasterLevel.ToString());
                }
            }
        }        

        public void CheckSpellDC(bool IsGnome)
        {
            List<string> Names = CharacterClasses.GetClassNames();
            SpellList SL = null;
            string formula = string.Empty;

            foreach (string name in Names)
            {
                formula = string.Empty;
                if (CharacterClasses.CanClassCastSpells(name.ToLower()))
                {
                    if (ClassSpells.ContainsKey(name))
                    {
                        SL = ClassSpells[name];
                        List<SpellData> ListOfSpells = SL.ListOfSpells;
                        int AbilityScore = MonSB.GetAbilityScoreValue(CharacterClasses.GetSpellBonusAbility(name));
                        OnGoingStatBlockModifier.StatBlockModifierSubTypes subType = Utility.GetOnGoingAbilitySubTypeFromString(CharacterClasses.GetSpellBonusAbility(name));
                        AbilityScore += _indvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.Ability, subType);           
                        int AbilityBonus = StatBlockInfo.GetAbilityModifier(AbilityScore);
                       
                        int Bonus = 0; 

                        foreach (SpellData SD in ListOfSpells)
                        {
                            try
                            {
                                string Temp = SD.Name;
                                Temp = Temp.Replace("†", string.Empty);
                                List<string> School;
                                
                                Temp = Utility.RemoveSuperScripts(Temp);
                                
                                string search = Utility.SearchMod(Temp);
                                if (search == "empty slot") continue;
                                ISpellStatBlock Spell = SpellStatBlock.GetSpellByName(search);
                                Bonus = 0;


                                if (Spell == null)
                                {
                                    _messageXML.AddFail("Spell DC", "Missing spell: " + search);
                                }
                                else
                                {
                                    if (_monSBSearch.HasSpellFocusFeat(out School))
                                    {

                                        if (School.Contains(Spell.school)) Bonus++;
                                        List<string> School2;
                                        if (_monSBSearch.HasGreaterSpellFocusFeat(out School2))
                                        {
                                            if (School2.Contains(Spell.school)) Bonus++;
                                        }
                                        if (SLA_SaveFail(SD, Spell))
                                        {
                                            _messageXML.AddFail("SLA Save-" + SD.Name, Spell.saving_throw, SD.DC.ToString());
                                        }
                                    }

                                    if (_monSBSearch.HasElementalSkillFocusFeat(out School))
                                    {

                                        if (School.Contains(Spell.school)) Bonus++;
                                        List<string> School2;
                                        if (_monSBSearch.HasGreaterElementalSkillFocusFeat(out School2))
                                        {
                                            if (School2.Contains(Spell.school)) Bonus++;
                                        }
                                        if (SLA_SaveFail(SD, Spell))
                                        {
                                            _messageXML.AddFail("SLA Save-" + SD.Name, Spell.saving_throw, SD.DC.ToString());
                                        }
                                    }

                                    if (SD.DC > 0)
                                    {
                                        ComputeSpellDC(IsGnome, ref formula, name, AbilityBonus, ref Bonus, SD, Spell);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _messageXML.AddFail("CheckSpellDC--" + SD.Name, ex.Message);
                            }
                        }
                    }
                }
            }

            if (!Names.Any() && MonSB.SpellsPrepared.Length > 0)
            {
                string temp = MonSB.SpellsPrepared;
                temp = temp.Substring(0, temp.IndexOf(" "));
                if (ClassSpells.ContainsKey(temp))
                {
                    SL = ClassSpells[temp];
                    List<SpellData> ListOfSpells = SL.ListOfSpells;
                    int AbilityBonus = StatBlockInfo.GetAbilityModifier(MonSB.GetAbilityScoreValue(CharacterClasses.GetSpellBonusAbility(temp)));

                    foreach (SpellData SD in ListOfSpells)
                    {
                        if (SD.DC > 0)
                        {
                            int computedDC = 10 + SD.Level + AbilityBonus;
                            formula = "10 +" + SD.Level.ToString() + " spell level +" + AbilityBonus.ToString() + " ability bonus";                           
                            if (computedDC == SD.DC)
                            {
                                _messageXML.AddPass("Spell DC-" + SD.Name);
                            }
                            else
                            {
                                _messageXML.AddFail("Spell DC-" + SD.Name, computedDC.ToString(), SD.DC.ToString(), formula);
                            }
                        }
                    }
                }
            }

            if (!Names.Any() && MonSB.SpellsKnown.Length > 0)
            {
                string temp = MonSB.SpellsKnown;
                temp = temp.Substring(0, temp.IndexOf(" "));
                if (ClassSpells.ContainsKey(temp))
                {
                    SL = ClassSpells[temp];
                    List<SpellData> ListOfSpells = SL.ListOfSpells;
                    if (temp == "Spells") temp = "Sorcerer";
                    int AbilityBonus = StatBlockInfo.GetAbilityModifier(MonSB.GetAbilityScoreValue(CharacterClasses.GetSpellBonusAbility(temp)));

                    foreach (SpellData SD in ListOfSpells)
                    {
                        if (SD.DC > 0)
                        {
                            int computedDC = 10 + SD.Level + AbilityBonus;
                            formula = "10 +" + SD.Level.ToString() + " spell level +" + AbilityBonus.ToString() + " ability bonus";
                            if (computedDC == SD.DC)
                            {
                                _messageXML.AddPass("Spell DC-" + SD.Name);
                            }
                            else
                            {
                                _messageXML.AddFail("Spell DC-" + SD.Name, computedDC.ToString(), SD.DC.ToString(), formula);
                            }
                        }
                    }
                }
            }
        }

        private void ComputeSpellDC(bool IsGnome, ref string formula, string name, int AbilityBonus, ref int Bonus, SpellData SD, ISpellStatBlock Spell)
        {            
            int Bloodline = 0;
            int gnomeBonus = 0;

            int spellClassLevel = Spell.GetSpellLevelByClass(name, _monSBSearch.HasCurse("haunted"), _monSBSearch.HasFeat("Shade of the Uskwood"), _monSBSearch.HasArchetype("Razmiran priest"));
            if (SD.Domain) spellClassLevel = SD.Level;
            // if (spellClassLevel < 0 && SD.Domain) spellClassLevel = SD.Level; 
            if (spellClassLevel < 0 && _monSBSearch.HasBloodline())
            {
                Dictionary<string, int> bloodlineBonusSpells = CharacterClasses.GetBloodlineBonusSpells();
                if (bloodlineBonusSpells.ContainsKey(Spell.name.ToLower()))
                {
                    spellClassLevel = SD.Level;
                }
            }

            if (spellClassLevel < 0 && _monSBSearch.HasMystery())
            {
                Dictionary<string, int> mysteryBonusSpells = CharacterClasses.GetMysteryBonusSpells();
                if (mysteryBonusSpells.ContainsKey(Spell.name.ToLower()))
                {
                    spellClassLevel = SD.Level;
                }
            }
            if (spellClassLevel < 0 && _monSBSearch.HasPatron())
            {
                List<string> patronBonusSpells = CharacterClasses.GetPatronBonusSpells();
                if (patronBonusSpells.Contains(Spell.name.ToLower()))
                {
                    spellClassLevel = SD.Level;
                }
            }
            if (IsGnome && Spell.school.Contains("illusion"))
            {
                gnomeBonus = 1;
            }
            if (_monSBSearch.HasBloodline("arcane"))
            {
                int sorcererLevel = CharacterClasses.FindClassLevel("sorcerer");
                if (sorcererLevel >= 15)
                {
                    string schoolPower = _monSBSearch.GetSQ("school power");
                    schoolPower = schoolPower.Replace("spells)", string.Empty).Trim();
                    int Pos = schoolPower.LastIndexOf(" ");
                    schoolPower = schoolPower.Substring(Pos).Trim();
                    if (Spell.school.Contains(schoolPower)) Bloodline += 2;
                }
            }
            if (_monSBSearch.HasBloodline("fey"))
            {
                if (Spell.subschool.Contains("compulsion")) Bloodline += 2;
            }
            if (_monSBSearch.HasBloodline("stormborn"))
            {
                if (Spell.descriptor.Contains("electricity") || Spell.descriptor.Contains("sonic")) Bloodline += 1;
            }
            if (_monSBSearch.HasBloodline("infernal"))
            {
                if (Spell.subschool.Contains("charm")) Bloodline += 2;
            }
            if (_monSBSearch.HasClassArchetype("winter witch"))
            {
                if (Spell.descriptor.Contains("cold")) Bonus += 1;
            }
            if (_monSBSearch.Race() == "kitsune" && Spell.school.Contains("enchantment"))
            {
                Bonus += 1;
            }
            if(_monSBSearch.HasSubType("sahkil") && (Spell.school.Contains("emotion") || Spell.school.Contains("fear")))
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

            int computedDC = 10 + spellClassLevel + AbilityBonus + Bonus + Bloodline + gnomeBonus;
            formula = "10 +" + spellClassLevel.ToString() + " spell class level +" + AbilityBonus.ToString() + " ability bonus";
            if (Bloodline != 0) formula += " +" + Bloodline.ToString() + " bloodline";
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
            return SD.DC == -1 && !Spell.saving_throw.ToLower().Contains("none") && !Spell.saving_throw.ToLower().Contains("harmless")
                && Spell.saving_throw.Length > 0 && !Spell.saving_throw.ToLower().Contains("see text") && !Spell.saving_throw.ToLower().Contains("negates")
                && !Spell.saving_throw.ToLower().Contains("disbelief");
        }

        public void CheckSpellsLevels()
        {
            List<string> Names = CharacterClasses.GetClassNames();
            List<int> Levels = new List<int>();
            List<int> LevelFrequency = new List<int>();
            SpellList SL = null;
            string spellsHold = string.Empty;
            string spellLevelHold = string.Empty;
            string CR = string.Empty;
            string SpellBlock = string.Empty;
            int overloadLevel = 0;
            int overloadClassLevel = 0;

            foreach (string name in Names)
            {
                if (CharacterClasses.CanClassCastSpells(name.ToLower()))
                {
                    SL = null;
                    if (ClassSpells.ContainsKey(name))
                    {
                        SL = GetClassSpellsByClassName(name);
                    }

                    if (SL != null)
                    {
                        int casterLevel = SL.CasterLevel;
                        int classLevel = CharacterClasses.FindClassLevel(name);
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
                        CharacterClasses.GetSpellOverLoadsForPrestigeClasses(out overloadLevel, out overloadClassLevel);
                        GetSpellOverLoads(ref overloadLevel, ref overloadClassLevel, casterLevel, classLevel);                       

                        classLevel += overloadLevel;

                        if (casterLevel != classLevel)
                        {
                            _messageXML.AddFail("Caster Level", "Character level not equal to caster level for " + name);
                        }

                        GetSpellLevels(ref Levels, name, overloadLevel);
                        LevelFrequency = CharacterClasses.GetClassSpellsPerDay(name.ToLower(), overloadLevel);
                    }
                    string SpellBonusAbility = CharacterClasses.GetSpellBonusAbility(name.ToLower());
                    int SpellBonusAbilityValue = MonSB.GetAbilityScoreValue(SpellBonusAbility);
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


                    if (Levels != null)
                    {
                        if (SL != null)
                        {
                            CheckSpellCount(Levels, LevelFrequency, SL, name, SpellBonusAbility, SpellBonusAbilityValue, SpellBonus, overloadClassLevel, IsInquisitor);
                            CheckBonusSpells(SL, name, CharacterClasses.FindClassLevel(name));
                        }
                        else if (Levels.Any() && SpellBonusAbilityValue <= 10)
                        {
                            _messageXML.AddInfo("Lacks Ability Score to cast spells with " + SpellBonusAbility + " " + SpellBonusAbilityValue.ToString());
                        }
                    }
                    else
                    {
                        _messageXML.AddFail("CheckSpellsLevels", "Failed to load GetSpellLevels() for " + name);
                    }
                }
            }
        }

        private void GetSpellOverLoads(ref int overloadLevel, ref int overloadClassLevel, int casterLevel, int classLevel)
        {
            //TODO: move these to public override void GetSpellOverLoads in class definition, e.g.  Demoniac
            if (CharacterClasses.HasClass("arcane archer") && classLevel < casterLevel)
            {
                int archerLevels = CharacterClasses.FindClassLevel("arcane archer");
                overloadClassLevel += archerLevels;
                overloadLevel += GetOverloadPrestige(archerLevels);
            }

            if (CharacterClasses.HasClass("dragon disciple") && classLevel < casterLevel)
            {
                int discipleLevels = CharacterClasses.FindClassLevel("dragon disciple");
                overloadClassLevel += discipleLevels;
                overloadLevel += GetOverloadPrestige(discipleLevels);
            }

            if (CharacterClasses.HasClass("blackfire adept") && classLevel < casterLevel)
            {
                int blackfireAdeptLevels = CharacterClasses.FindClassLevel("blackfire adept");
                overloadClassLevel += blackfireAdeptLevels;
                overloadLevel += GetOverloadPrestige(blackfireAdeptLevels);
            }

            if (CharacterClasses.HasClass("arclord of nex") && classLevel < casterLevel)
            {
                int temp = CharacterClasses.FindClassLevel("arclord of nex");
                overloadClassLevel += temp;
                overloadLevel += temp;
            }

            if (CharacterClasses.HasClass("eldritch knight") && classLevel < casterLevel)
            {
                int eldritchKnightLevels = CharacterClasses.FindClassLevel("eldritch knight");
                overloadClassLevel += eldritchKnightLevels;
                if (eldritchKnightLevels >= 2)
                {
                    overloadLevel += eldritchKnightLevels - 1;
                }
            }

            if (CharacterClasses.HasClass("riftwarden") && classLevel < casterLevel)
            {
                int riftWardenLevels = CharacterClasses.FindClassLevel("riftwarden");
                overloadClassLevel += riftWardenLevels;
                overloadLevel += GetOverloadPrestige(riftWardenLevels);
            }
            if (CharacterClasses.HasClass("razmiran priest") && classLevel < casterLevel)
            {
                int razmiranPriestLevels = CharacterClasses.FindClassLevel("razmiran priest");
                overloadClassLevel += razmiranPriestLevels;
                overloadLevel += GetOverloadPrestige(razmiranPriestLevels);
            }

            if (CharacterClasses.HasClass("gray gardener") && classLevel < casterLevel)
            {
                int grayGardenerLevels = CharacterClasses.FindClassLevel("gray gardener");
                overloadClassLevel += grayGardenerLevels;
                overloadLevel += GetOverloadPrestige(grayGardenerLevels);
            }

            if (CharacterClasses.HasClass("cyphermage"))
            {
                int temp = CharacterClasses.FindClassLevel("cyphermage");
                overloadClassLevel += temp;
                overloadLevel += temp;
            }
            if (CharacterClasses.HasClass("Harrower"))
            {
                int temp = CharacterClasses.FindClassLevel("Harrower");
                overloadClassLevel += temp;
                overloadLevel += temp;
            }
            if (CharacterClasses.HasClass("hellknight signifer"))
            {
                int temp = CharacterClasses.FindClassLevel("hellknight");
                overloadClassLevel += temp;
                overloadLevel += temp;
            }
            if (CharacterClasses.HasClass("magaambyan arcanist"))
            {
                int temp = CharacterClasses.FindClassLevel("magaambyan arcanist");
                overloadClassLevel += temp;
                overloadLevel += temp;
            }
            if (CharacterClasses.HasClass("Winter Witch"))
            {
                int temp = CharacterClasses.FindClassLevel("Winter Witch");
                overloadClassLevel += temp;
                overloadLevel += temp - 1;
            }
            if (CharacterClasses.HasClass("BloatMage"))
            {
                int temp = CharacterClasses.FindClassLevel("BloatMage");
                overloadClassLevel += temp;
                overloadLevel += temp;
            }
            if (CharacterClasses.HasClass("loremaster"))
            {
                int temp = CharacterClasses.FindClassLevel("loremaster");
                overloadClassLevel += temp;
                overloadLevel += temp;
            }
            if (CharacterClasses.HasClass("mystic theurge"))
            {
                int temp = CharacterClasses.FindClassLevel("mystic theurge");
                overloadClassLevel += temp;
                overloadLevel += temp;
            }
            if (CharacterClasses.HasClass("arcane trickster"))
            {
                int temp = CharacterClasses.FindClassLevel("arcane trickster");
                overloadClassLevel += temp;
                overloadLevel += temp;
            }
            //if (CharacterClasses.HasClass("demoniac"))
            //{
            //    int temp = CharacterClasses.FindClassLevel("demoniac");
            //    overloadClassLevel += temp;
            //    overloadLevel += temp - 1;
            //}
            if (CharacterClasses.HasClass("technomancer"))
            {
                int temp = CharacterClasses.FindClassLevel("technomancer");
                overloadClassLevel += temp;
                overloadLevel += temp - 1;
            }
            if (CharacterClasses.HasClass("exalted"))
            {
                int temp = CharacterClasses.FindClassLevel("exalted");
                overloadClassLevel += temp;
                overloadLevel += temp;
            }
            if (CharacterClasses.HasClass("ZealotOfOrcus"))
            {
                int temp = CharacterClasses.FindClassLevel("ZealotOfOrcus");
                overloadClassLevel += temp;
                overloadLevel += GetOverloadPrestige(temp);
            }
            if (CharacterClasses.HasClass("archwizard"))
            {
                int temp = CharacterClasses.FindClassLevel("BloatMage");
                overloadClassLevel += temp;
                overloadLevel += temp;
            }
            //if (CharacterClasses.HasClass("umbral court agent"))
            //{
            //    int temp = CharacterClasses.FindClassLevel("umbral court agent");                
            //    overloadClassLevel += GetOverloadPrestige(temp);
            //    overloadLevel += GetOverloadPrestige(temp);
            //}
        }

        private static int GetOverloadPrestige(int classLevels)
        {
            int overloadLevel = 0;
            if (classLevels >= 2) overloadLevel++;
            if (classLevels >= 3) overloadLevel++;
            if (classLevels >= 4) overloadLevel++;
            if (classLevels >= 6) overloadLevel++;
            if (classLevels >= 7) overloadLevel++;
            if (classLevels >= 8) overloadLevel++;
            if (classLevels >= 10) overloadLevel++;
            return overloadLevel;
        }

        private void GetSpellLevels(ref List<int> Levels, string name, int overloadLevel)
        {
            if (overloadLevel == 0)
            {
                Levels = CharacterClasses.GetClassSpellLevels(name.ToLower());
                if (_monSBSearch.HasArchetype("kensai"))
                {
                    for (int a = 0; a<=Levels.Count - 1; a++)
                    {
                        Levels[a] -= 1;
                    }
                }
            }
            else
            {
                Levels = CharacterClasses.GetClassSpellLevels(name.ToLower(), overloadLevel);
            }

            if (_monSBSearch.HasArchetype("spellslinger)")) Levels[0] = 0; //no cantrips for spellslinger
        }

        private void CheckSpellCount(List<int> Levels, List<int> LevelFrequency, SpellList SL, string name, string SpellBonusAbility,
                 int SpellBonusAbilityValue, List<int> SpellBonus, int overloadClassLevel, bool IsInquisitor)
        {
            int BloodlineLevels = CharacterClasses.GetClassBloodlineSpellLevels(name, overloadClassLevel);
            int MysteryLevels = CharacterClasses.GetClassMysterySpellLevels(name);
            int BloatMageLevels = CharacterClasses.FindClassLevel("BloatMage");
            int NewArcanaLevels = 0;            
            int DomainSpellCount = CharacterClasses.GetDomainSpellCount(name.ToLower());
            List<int> OracleCurseSpells = new List<int>() { 0 };
            int ExpandedArcanaCount = 0;
            if(_monSBSearch.HasFeat("Expanded Arcana"))
            {
               ExpandedArcanaCount = _monSBSearch.FeatItemCount("Expanded Arcana");
            }

            if (_monSBSearch.HasCurse("haunted"))
            {
                int oracleLevel = CharacterClasses.FindClassLevel("oracle");
                OracleCurseSpells = new List<int> { 2 };//mage hand and ghost sound 0th
                if (oracleLevel >= 5) OracleCurseSpells.AddRange(new List<int> {  0, 2 }); //levitate and minor image 2nd level
                if (oracleLevel >= 10) OracleCurseSpells.AddRange(new List<int> {0,0,1}); // telekinesis 5th
                if (oracleLevel >= 15) OracleCurseSpells.AddRange(new List<int> { 0, 1}); //  reverse gravity 7th
            }

            if (_monSBSearch.HasSQ("new arcana"))
            {
                int sorcererLevel = CharacterClasses.FindClassLevel("sorcerer");
                if (sorcererLevel >= 9) NewArcanaLevels++;
                if (sorcererLevel >= 13) NewArcanaLevels++;
                if (sorcererLevel >= 17) NewArcanaLevels++;
            }

            List<SpellData> ListOfSpells = SL.ListOfSpells;
            int SBLevelCount = 0;
            string formula = null;

            for (int SpellLevelIndex = 0; SpellLevelIndex <= Levels.Count - 1; SpellLevelIndex++)
            {
                SBLevelCount = 0;
                formula = null;
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
                            + SpellBonusAbility + " " + SpellBonusAbilityValue.ToString() + ", but has spells");
                    }
                    else 
                    {
                        _messageXML.AddPass("Spell Count-" + name + " Level: " + SpellLevelIndex.ToString(), ClassLevelCount.ToString() + " " + SBLevelCount.ToString() + " " + formula);
                    }
                }
                else
                {
                    if (SpellLevelIndex + 10 > SpellBonusAbilityValue)
                    {
                        _messageXML.AddInfo("Lacks Ability Score to cast Level " + SpellLevelIndex.ToString() + " spells with " + SpellBonusAbility + " " + SpellBonusAbilityValue.ToString());
                    }
                    else if (SBLevelCount != ClassLevelCount)
                    {
                        _messageXML.AddFail("Spell Count-" + name + " Level: " + SpellLevelIndex.ToString(), ClassLevelCount.ToString(), SBLevelCount.ToString() + " " + formula);
                    }
                }
            }
        }

        private void ComputeSpellFrequency(List<SpellData> ListOfSpells, ref int SBLevelCount, int SpellLevelIndex, ref string freq)
        {
            int Pos = -1;
            foreach (SpellData SD in ListOfSpells)
            {
                if (SD.Level == SpellLevelIndex)
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
                            SBLevelCount += Convert.ToInt32(freq) / SD.Count;
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
                        SBLevelCount += SD.Count;
                    }
                }
            }
        }

        private SpellList GetClassSpellsByClassName(string name)
        {            
            return ClassSpells[name];           
        }

        public void CheckSLA(bool IsGnome)
        {
            if (!SLA.Any()) return;
            SpellList SL = null;
            int AbilityBonus = 0;
            string AbilityBonusName = "Cha";
            string formulaConcentration = string.Empty;

            foreach (KeyValuePair<string, SpellList> sla in SLA)
            {
                SL = sla.Value;
                if (sla.Key == "Domain" || sla.Key == "Qinggong Monk")
                {
                    AbilityBonus = StatBlockInfo.GetAbilityModifier(MonSB.GetAbilityScoreValue(StatBlockInfo.AbilityName.Wisdom));
                    AbilityBonusName = "Wis";
                    formulaConcentration = SL.CasterLevel.ToString() + " Caster Level +" + AbilityBonus.ToString() + " Wisdom modifier";
                }
                else if (sla.Key == "Rogue" || sla.Key.Contains("Evoker") || sla.Key.Contains("Enchanter") || sla.Key.Contains("Arcane") || sla.Key.Contains("Diviner") || sla.Key.Contains("Conjurer"))
                {
                    AbilityBonus = StatBlockInfo.GetAbilityModifier(MonSB.GetAbilityScoreValue(StatBlockInfo.AbilityName.Intelligence));
                    AbilityBonusName = "Int";
                    formulaConcentration = SL.CasterLevel.ToString() + " Caster Level +" + AbilityBonus.ToString() + " Intelligence modifier";
                }
                else
                {
                    AbilityBonus = StatBlockInfo.GetAbilityModifier(MonSB.GetAbilityScoreValue(StatBlockInfo.AbilityName.Charisma));
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
                    ISpellStatBlock Spell = SpellStatBlock.GetSpellByName(search);
                    if (Spell != null)
                    {
                        SpellChecker.CheckQuickenSpellLikeAbilityMonster(CasterLevel, SD, Spell, _messageXML);
                        SpellChecker.CheckEmpowerSpellLikeAbilityMonster(CasterLevel, SD, Spell, _messageXML);
                        if (SD.DC > 0)
                        {
                            gnomeBonus = 0;
                            if (IsGnome && Spell.school.Contains("illusion")) gnomeBonus = 1;
                            int computedDC = 10 + (Spell.SLA_Level ?? -1) + AbilityBonus + gnomeBonus;
                            string formula = "10 +" + (Spell.SLA_Level ?? -1).ToString() + " Spell.SLA_Level +" + AbilityBonus.ToString() + " AbilityBonus (" + AbilityBonusName + ")";
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

            Dictionary<string, int> MysteryBonusSpells = CharacterClasses.GetMysteryBonusSpells();
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

            List<string> PatronBonusSpells = CharacterClasses.GetPatronBonusSpells();
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

            Dictionary<string, int> BloodlineBonusSpells = CharacterClasses.GetBloodlineBonusSpells();
            if (BloodlineBonusSpells != null && BloodlineBonusSpells.Any())
            {
                foreach (KeyValuePair<string, int> kvp in BloodlineBonusSpells)
                {
                    if (!SL.SpellExists(kvp.Key) && casterLevel>= kvp.Value)
                    {
                        _messageXML.AddFail(checkName, "Missing bonus Bloodline Spell from spell list -" + kvp.Key);
                    }
                }
            }
        }
    }
}
