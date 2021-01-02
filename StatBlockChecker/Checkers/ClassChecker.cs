using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassManager;
using Utilities;

using PathfinderGlobals;
using ClassFoundational;

namespace StatBlockChecker
{
    public class ClassChecker : IClassChecker
    {
        private ISBCheckerBaseInput _sbCheckerBaseInput;

        public ClassChecker(ISBCheckerBaseInput sbCheckerBaseInput)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
        }

        public void CheckClasses()
        {
            if (_sbCheckerBaseInput.CharacterClasses.ClassCount() == 0) return;

            CheckClassAlignments();
            CheckRaceArchetypes();

            List<string> ClassNamesList = _sbCheckerBaseInput.CharacterClasses.GetClassNames();
            List<CheckClassError> CheckClassErrors = null;

            foreach (string ClassName in ClassNamesList)
            {
                try
                {
                    CheckClassErrors = _sbCheckerBaseInput.CharacterClasses.CheckClass(ClassName.ToLower());
                    foreach (CheckClassError error in CheckClassErrors)
                    {
                        if (error.IsPass)
                        {
                            _sbCheckerBaseInput.MessageXML.AddPass(error.CheckName);
                        }
                        else
                        {
                            _sbCheckerBaseInput.MessageXML.AddFail(error.CheckName, error.StatBlockValue, error.ComputedValue);
                        }
                    }
                }
                catch
                {
                    switch (ClassName.ToLower())
                    {
                        case "alchemist":
                            int alchemistLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("alchemist");
                            CheckDiscoveryCount(alchemistLevel);
                            break;
                        case "assassin":
                            int assassinLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("assassin");
                            CheckDeathAttack(assassinLevel);
                            CheckTrueDeath(assassinLevel);
                            break;
                        case "barbarian":
                            int barbarianLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("barbarian");
                            CheckRage(barbarianLevel);
                            break;
                        case "bard":
                            int bardLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("bard");
                            CheckBardicPerformamce(bardLevel);
                            break;
                        case "cleric":
                            int clericLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("cleric");
                            CheckChannelEnergyCleric(clericLevel);
                            break;
                        case "druid":
                            CheckDomainOrCompanion();
                            break;
                        case "gunslinger":
                            int gunslingerLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("gunslinger");
                            CheckDeeds(gunslingerLevel);
                            CheckSpecificDeeds(gunslingerLevel);
                            CheckGritPoints(gunslingerLevel);
                            break;
                        case "fighter":
                            int fighterLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("fighter");
                            CheckFighterWillMod(fighterLevel);
                            break;
                        case "inquisitor":
                            CheckInquisition();
                            break;
                        case "investigator":
                            CheckInvestigatorTalents();
                            break;
                        case "loremaster":
                            int loremasterLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("loremaster");
                            CheckSecrets(loremasterLevel);
                            break;
                        case "medium":
                            int mediumLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("medium");
                            CheckSpiritBonus(mediumLevel);
                            break;
                        case "mesmerist":
                            int mesmeristLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("mesmerist");
                            CheckMesmeristTrick(mesmeristLevel);
                            break;
                        case "monk":
                            int MonkLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("monk");
                            CheckMonkImmunities(MonkLevel);
                            CheckStunningFist(MonkLevel);
                            CheckKiPool(MonkLevel);
                            CheckMonkSR(MonkLevel);
                            break;
                        case "ninja":
                            int ninjaLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("ninja");
                            CheckNinjaTricks(ninjaLevel);
                            CheckSneakAttack(0);  //rogue level
                            break;
                        case "paladin":
                            int paladinLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("paladin");
                            CheckPaladinImmunities(paladinLevel);
                            CheckLayOnHands(paladinLevel);
                            CheckMercies(paladinLevel);
                            CheckSmiteEvil(paladinLevel);
                            CheckChannelEnergyPaladin(paladinLevel);
                            CheckPaladinAuras(paladinLevel);
                            break;
                        case "antipaladin":
                            int antipaladinLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("antipaladin");
                            CheckCruelties(antipaladinLevel);
                            break;
                        case "ranger":
                            int rangerLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("ranger");
                            if (_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("urban ranger"))
                            {
                                CheckFavoredCommunity(rangerLevel);
                            }
                            else
                            {
                                CheckFavoredTerrain(rangerLevel);
                            }
                            if (_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("skirmisher"))
                            {
                                CheckHuntersTrick(rangerLevel);
                            }
                            CheckFavoredEnemy(rangerLevel);
                            break;
                        case "rogue":
                            int rogueLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("rogue");
                            CheckRogueTalents(rogueLevel);
                            if (_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("knife master"))
                            {
                                CheckSneakStab(rogueLevel);
                            }
                            else
                            {
                                CheckSneakAttack(rogueLevel);
                            }
                            break;
                        case "slayer":
                            int slayerLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("slayer");
                            CheckSlayerTalents(slayerLevel);
                            break;
                        case "warpriest":
                            int warpriestLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("warpriest");
                            CheckBlessings(warpriestLevel);
                            break;
                        default:
                            _sbCheckerBaseInput.MessageXML.AddFail("CheckClasses", "No check defined for Class " + ClassName);
                            break;
                    }
                }
            }
        }

        private void CheckSlayerTalents(int slayerLevel)
        {
            string CheckName = "CheckSlayerTalents";

            if (slayerLevel < 2) return;

            string slayetTelentsSB = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("slayer talents");
            slayetTelentsSB = slayetTelentsSB.Replace("slayer talents", string.Empty);
            slayetTelentsSB = Utility.RemoveParentheses(slayetTelentsSB);
            List<string> slayetTelentsSBList = slayetTelentsSB.Split('|').ToList();

            int slayerTalentCount = 0;
            if (slayerLevel >= 2) slayerTalentCount++;
            if (slayerLevel >= 4) slayerTalentCount++;
            if (slayerLevel >= 6) slayerTalentCount++;
            if (slayerLevel >= 8) slayerTalentCount++;
            if (slayerLevel >= 10) slayerTalentCount++;
            if (slayerLevel >= 12) slayerTalentCount++;
            if (slayerLevel >= 14) slayerTalentCount++;
            if (slayerLevel >= 16) slayerTalentCount++;
            if (slayerLevel >= 18) slayerTalentCount++;
            if (slayerLevel >= 20) slayerTalentCount++;

            if (slayetTelentsSBList.Count() == slayerTalentCount)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, slayerTalentCount.ToString(), slayetTelentsSBList.Count().ToString());
            }
        }

        private void CheckSpecificDeeds(int gunslingerLevel)
        {
            string CheckName = "CheckSpecificDeeds";

            List<string> SBDeedList = _sbCheckerBaseInput.MonsterSBSearch.GetDeeds();
            Dictionary<string, int> DeedsByLevel = CommonMethods.GetDeedsByLevel();

            if (_sbCheckerBaseInput.MonsterSBSearch.HasAnyClassArchetypes())
            {
                if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("musket master"))
                {
                    if (gunslingerLevel >= 1)
                    {
                        if (!_sbCheckerBaseInput.MonsterSBSearch.HasDeed("steady aim")) _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Musket Master missing steady aim deed");
                        SBDeedList.Remove("steady aim");
                        if (_sbCheckerBaseInput.MonsterSBSearch.HasDeed("dodge")) _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Musket Master can't have dodge deed");
                    }
                    if (gunslingerLevel >= 3)
                    {
                        if (!_sbCheckerBaseInput.MonsterSBSearch.HasDeed("fast musket")) _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Musket Master missing fast musket deed");
                        SBDeedList.Remove("fast musket");
                        if (_sbCheckerBaseInput.MonsterSBSearch.HasDeed("utility shot")) _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Musket Master can't have utility shot deed");
                    }
                }
            }

            foreach (string deed in SBDeedList)
            {
                if (DeedsByLevel.ContainsKey(deed))
                {
                    if (DeedsByLevel[deed] > gunslingerLevel) _sbCheckerBaseInput.MessageXML.AddFail(CheckName, deed + " exceeds min class level of " + DeedsByLevel[deed].ToString());
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, deed + " doesn't exist in deed list");
                }
            }
        }

        private void CheckInvestigatorTalents()
        {
            string CheckName = "Investigator Talents";

            int investigatorLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("investigator");

            if (investigatorLevel < 3) return;

            string talents = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("investigator talents");
            talents = Utility.RemoveParentheses(talents);

            List<string> talentList = talents.Split(',').ToList();

            int talentCountSB = 3;
            investigatorLevel -= 3;
            investigatorLevel = (investigatorLevel / 2);
            talentCountSB += investigatorLevel;

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Extra Investigator Talent")) talentCountSB++;

            if (talentCountSB != talentList.Count)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, talentCountSB.ToString(), talentList.Count.ToString());
            }
        }

        private void CheckSpiritBonus(int mediumLevel)
        {
            string CheckName = "Spirit Bonus";

            int spiritValue = 1;

            if (mediumLevel >= 4) spiritValue++;
            if (mediumLevel >= 8) spiritValue++;
            if (mediumLevel >= 12) spiritValue++;
            if (mediumLevel >= 16) spiritValue++;
            if (mediumLevel >= 20) spiritValue++;

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Spirit Focus")) spiritValue++;

            string spiritSB = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("spirit bonus").Replace("spirit bonus", string.Empty);
            spiritSB = spiritSB.Replace("*", string.Empty);

            int spiritSBValue = int.Parse(spiritSB);

            if (spiritValue == spiritSBValue)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, spiritSBValue.ToString(), spiritValue.ToString());
            }
        }

        private void CheckGritPoints(int gunslingerLevel)
        {
            string CheckName = "Grit Count";

            int gritValue = _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Wisdom);

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Extra Grit")) gritValue += 2;

            string SBValue = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("grit");
            if (SBValue.Length == 0)
            {
                SBValue = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("grit");
            }
            SBValue = SBValue.Replace("grit (", string.Empty);
            SBValue = SBValue.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Trim();

            int gritSB = int.Parse(SBValue);

            if (gritValue == gritSB)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, gritSB.ToString(), gritValue.ToString());
            }
        }

        private void CheckDeeds(int gunslingerLevel)
        {
            string CheckName = "Deeds Count";

            int deedCount = 3;
            if (gunslingerLevel >= 3) deedCount += 3;
            if (gunslingerLevel >= 7) deedCount += 3;
            if (gunslingerLevel >= 11) deedCount += 3;
            if (gunslingerLevel >= 15) deedCount += 3;
            if (gunslingerLevel >= 19) deedCount += 3;

            List<string> deedsList = _sbCheckerBaseInput.MonsterSBSearch.GetDeeds();

            if (deedsList.Count() == deedCount)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, deedCount.ToString(), deedsList.Count().ToString());
            }
        }

        private void CheckMesmeristTrick(int mesmeristLevel)
        {
            string CheckName = "Mesmerist Trick Count";

            int Trickcount = 1;
            if (mesmeristLevel >= 2) Trickcount++;
            if (mesmeristLevel >= 4) Trickcount++;
            if (mesmeristLevel >= 6) Trickcount++;
            if (mesmeristLevel >= 8) Trickcount++;
            if (mesmeristLevel >= 10) Trickcount++;
            if (mesmeristLevel >= 12) Trickcount++;
            if (mesmeristLevel >= 14) Trickcount++;
            if (mesmeristLevel >= 16) Trickcount++;
            if (mesmeristLevel >= 18) Trickcount++;
            if (mesmeristLevel >= 20) Trickcount++;

            string SBValue = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("mesmerist trick");
            SBValue = SBValue.Replace("mesmerist tricks", string.Empty);
            SBValue = SBValue.Replace("mesmerist trick", string.Empty);
            int Pos = SBValue.IndexOf("/day");

            string TrickFreqSB = SBValue.Substring(0, Pos + 5).Trim(); ;
            SBValue = SBValue.Replace(TrickFreqSB, string.Empty);
            SBValue = Utility.RemoveParentheses(SBValue);

            List<string> trickList = SBValue.Split('|').ToList();

            if (trickList.Count() == Trickcount)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, Trickcount.ToString(), trickList.Count().ToString());
            }

            CheckName = "Mesmerist Trick Freq";

            TrickFreqSB = TrickFreqSB.Replace("/day", string.Empty);
            int freq = int.Parse(TrickFreqSB);

            int freqValue = mesmeristLevel / 2;
            if (freqValue == 0) freqValue = 1;
            int ChaMod = _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Charisma);
            freqValue += ChaMod;
            string formula = freqValue.ToString() + " = " + mesmeristLevel.ToString() + "/2 + " + ChaMod.ToString() + " Cha mod";

            if (freq == freqValue)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, freqValue.ToString(), freq.ToString(), formula);
            }
        }

        private void CheckNinjaTricks(int ninjaLevel)
        {
            string CheckName = "Ninja Trick Count";

            int Trickcount = 0;

            if (ninjaLevel >= 2) Trickcount++;
            if (ninjaLevel >= 4) Trickcount++;
            if (ninjaLevel >= 6) Trickcount++;
            if (ninjaLevel >= 8) Trickcount++;
            if (ninjaLevel >= 10) Trickcount++;
            if (ninjaLevel >= 12) Trickcount++;
            if (ninjaLevel >= 14) Trickcount++;
            if (ninjaLevel >= 16) Trickcount++;
            if (ninjaLevel >= 18) Trickcount++;
            if (ninjaLevel >= 20) Trickcount++;

            string SBValue = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("ninja tricks");
            SBValue = SBValue.Replace("ninja tricks (", string.Empty);
            SBValue = SBValue.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Trim();
            List<string> TrickList = SBValue.Split('|').ToList();
            TrickList.RemoveAll(x => x == string.Empty);

            if (TrickList.Count() > Trickcount && _sbCheckerBaseInput.MonsterSBSearch.HasFeat("Extra Rogue Talent")) Trickcount++; //assume Extra Rogue Talent gave you a ninja trick

            if (TrickList.Count() == Trickcount)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, Trickcount.ToString(), TrickList.Count().ToString());
            }
        }

        private void CheckBlessings(int warpriestLevel)
        {
            string CheckName = "Blessing Frequency";

            int BlessingCount = 3 + (warpriestLevel / 2);

            string BlessingSB = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("blessings");
            int Pos = BlessingSB.IndexOf("/day");
            BlessingSB = BlessingSB.Substring(0, Pos);
            BlessingSB = BlessingSB.Replace("blessings", string.Empty);

            int BlessingValue = int.Parse(BlessingSB);

            if (BlessingValue == BlessingCount)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, BlessingCount.ToString(), BlessingValue.ToString());
            }
        }

        private void CheckInquisition()
        {
            string CheckName = "Check Inquisition";

            if (!_sbCheckerBaseInput.MonsterSB.SpellDomains.Contains("inquisition")) return;

            string Inquisition = _sbCheckerBaseInput.MonsterSB.SpellDomains.Replace("inquisition", string.Empty).Trim();

            Inquisition = Utility.RemoveSuperScripts(Inquisition);

            List<string> InquisitionList = CommonMethods.GetInquisitions();

            if (InquisitionList.Contains(Inquisition))
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Inquisition " + Inquisition + " not in Inquisition list");
            }
        }

        private void CheckDiscoveryCount(int alchemistLevel)
        {
            string CheckName = "Discoveries Count";
            int discoveryCount = 0;

            if (alchemistLevel >= 2) discoveryCount++;
            if (alchemistLevel >= 4) discoveryCount++;
            if (alchemistLevel >= 6) discoveryCount++;
            if (alchemistLevel >= 8) discoveryCount++;
            if (alchemistLevel >= 10) discoveryCount++;
            if (alchemistLevel >= 12) discoveryCount++;
            if (alchemistLevel >= 14) discoveryCount++;
            if (alchemistLevel >= 16) discoveryCount++;
            if (alchemistLevel >= 18) discoveryCount++;

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Extra Discovery")) discoveryCount++;

            string discoveries = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("discoveries");
            if (string.IsNullOrEmpty(discoveries))
            {
                discoveries = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("discovery");
            }

            discoveries = discoveries.Replace("discoveries (", string.Empty);
            discoveries = discoveries.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Trim();
            List<string> discoveriesList = discoveries.Split('|').ToList();
            discoveriesList.RemoveAll(x => x == string.Empty);


            if (discoveriesList.Count == discoveryCount)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, discoveryCount.ToString(), discoveriesList.Count.ToString());
            }
        }

        private void CheckHuntersTrick(int rangerLevel)
        {
            string CheckName = "Hunter's Trick Count";
            int huntersTrickCount = 0;

            if (rangerLevel >= 5) huntersTrickCount++;
            if (rangerLevel >= 7) huntersTrickCount++;
            if (rangerLevel >= 9) huntersTrickCount++;
            if (rangerLevel >= 11) huntersTrickCount++;
            if (rangerLevel >= 13) huntersTrickCount++;
            if (rangerLevel >= 15) huntersTrickCount++;
            if (rangerLevel >= 17) huntersTrickCount++;
            if (rangerLevel >= 19) huntersTrickCount++;

            string huntersTrick = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("hunter's trick");
            huntersTrick = huntersTrick.Replace("hunter's trick (", string.Empty);
            huntersTrick = huntersTrick.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Trim();
            List<string> huntersTrickList = huntersTrick.Split('|').ToList();
            huntersTrickList.RemoveAll(x => x == string.Empty);

            if (huntersTrickList.Count == huntersTrickCount)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, huntersTrickCount.ToString(), huntersTrickList.Count.ToString());
            }
        }

        private void CheckFighterWillMod(int fighterLevel)
        {
            string CheckName = "Fighter Will Mod";
            int mod = 0;

            if (fighterLevel >= 2) mod++;
            if (fighterLevel >= 6) mod++;
            if (fighterLevel >= 10) mod++;
            if (fighterLevel >= 14) mod++;
            if (fighterLevel >= 18) mod++;

            int willModSB = 0;
            string WillSB = _sbCheckerBaseInput.MonsterSB.Will;
            if (WillSB.Contains(PathfinderConstants.PAREN_LEFT) && WillSB.Contains("fear"))
            {
                int pos = WillSB.IndexOf(PathfinderConstants.PAREN_LEFT);
                WillSB = WillSB.Substring(pos);
                pos = WillSB.IndexOf("vs");
                WillSB = WillSB.Substring(0, pos);
                WillSB = WillSB.Replace(PathfinderConstants.PAREN_LEFT, string.Empty).Trim();
                willModSB = int.Parse(WillSB);
            }
            else
            {
                WillSB = _sbCheckerBaseInput.MonsterSB.Save_Mods;
                if (WillSB.Contains("fear"))
                {
                    int pos = WillSB.IndexOf("vs");
                    WillSB = WillSB.Substring(0, pos);
                    WillSB = WillSB.Replace(PathfinderConstants.PAREN_LEFT, string.Empty).Trim();
                    willModSB = int.Parse(WillSB);
                }
            }

            if (willModSB == mod)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, mod.ToString(), willModSB.ToString());
            }
        }

        private void CheckCruelties(int antipaladinLevel)
        {
            if (antipaladinLevel <= 2) return;
            string CheckName = "Cruelty Count";

            int crueltyCount = 1;
            if (antipaladinLevel >= 6) crueltyCount++;
            if (antipaladinLevel >= 9) crueltyCount++;
            if (antipaladinLevel >= 12) crueltyCount++;
            if (antipaladinLevel >= 15) crueltyCount++;
            if (antipaladinLevel >= 18) crueltyCount++;
            //if ( _sbCheckerBaseInput.MonsterSBSearch.HasFeat("Extra Mercy")) crueltyCount++;

            string crueltySB = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("cruelty");
            if (crueltySB.Length == 0) crueltySB = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("cruelties");
            crueltySB = crueltySB.Replace("cruelty", string.Empty).Replace("cruelties", string.Empty)
                .Replace(PathfinderConstants.PAREN_LEFT, string.Empty).Replace(PathfinderConstants.SPACE, string.Empty)
                .Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Trim();
            List<string> cruelties = crueltySB.Split('|').ToList();
            cruelties.RemoveAll(x => x == string.Empty);

            if (cruelties.Count == crueltyCount)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, crueltyCount.ToString(), cruelties.Count.ToString());
            }


            CheckName = "Cruelty Allowed";
            List<string> crueltiesAllowed = new List<string> { "fatigued", "shaken", "sickened" };
            if (antipaladinLevel >= 6) crueltiesAllowed.AddRange(new List<string> { "dazed", "diseased", "staggered" });
            if (antipaladinLevel >= 9) crueltiesAllowed.AddRange(new List<string> { "cursed", "exhausted", "frightened", "nauseated", "poisoned" });
            if (antipaladinLevel >= 12) crueltiesAllowed.AddRange(new List<string> { "blinded", "deafened", "paralyzed", "stunned" });

            foreach (string cruelty in cruelties)
            {
                if (!crueltiesAllowed.Contains(cruelty)) _sbCheckerBaseInput.MessageXML.AddFail(CheckName, cruelty + " is not allowed at level " + antipaladinLevel.ToString());
            }

            CheckName = "Cruelty PreReqs";

            if (cruelties.Contains("exhausted") && !cruelties.Contains("fatigued")) _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "cruelty exhausted needs fatigued as prereq.");
            if (cruelties.Contains("frightened") && !cruelties.Contains("shaken")) _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "cruelty frightened needs shaken as prereq.");
            if (cruelties.Contains("nauseated") && !cruelties.Contains("sickened")) _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "cruelty nauseated needs sickened as prereq.");
        }

        private void CheckFavoredCommunity(int rangerLevel)
        {

        }

        private void CheckRaceArchetypes()
        {
            if (_sbCheckerBaseInput.MonsterSB.ClassArchetypes.Length == 0) return;

            Dictionary<string, string> RaceArchetypes = new Dictionary<string, string>{
                                           { "Exarch","dwarf"}, {"forgemaster","dwarf"}, {"stonelord","dwarf"},
                            {"Ancient lorekeeper","elf"}, {"spell dancer","elf"}, {"spellbinder","elf"},
                            {"treesinger","elf"}, {"Experimental gunsmith","gnome"}, {"prankster","gnome"},
                            {"saboteur","gnome"}, {"Bramble brewer","half-elf"}, {"bonded witch","half-elf"},
                            {"wild caller","half-elf"}, {"wild shadow","half-elf"}, {"Blood god disciple","half-orc"},
                            {"hateful rager","half-orc"},{"redeemer","half-orc"},{"skulking slayer","half-orc"},
                            {"Community guardian","halfling"},{"filcher","halfling"},{"order of the paw","halfling"},
                            {"underfoot adept","halfling"}, {"Buccaneer","human"},  {"feral child","human"},
                            {"imperious bloodline","human"},{"wanderer","human"}, {"Purifier","asimar"},
                            {"tranquil guardian","asimar"}, {"Cat burglar","catfolk"},{"nimble guardian","catfolk"},
                            {"Cruoromancer","dhampir"}, {"kinslayer","dhampir"}, {"Cavern sniper","drow"},
                            {"demonic apostle","drow"}, {"Dusk stalker","fetchling"},{"shadow caller","fetchling"},
                            {"Feral gnasher","goblin"}, {"fire bomber","goblin"},{"Fell rider","hobgoblin"},
                            {"ironskin monk","hobgoblin"},{"Immolator","ifrit"}, {"wishcrafter","ifrit"},
                            {"Bushwhacker","kobold"},{"kobold sorcerer","kobold"}, {"Dirty fighter","orc"},
                            {"scarred witch doctor","orc"},{"Shaitan binder","oread"}, {"student of stone","oread"},
                            {"Gulch gunner","ratfolk"}, {"plague bringer","ratfolk"}, {"Sky druid","sylph"},
                            {"wind listener","sylph"}, {"Shigenjo","tengu"},{"swordmaster","tengu"},
                            {"Fiend flayer","tiefling"},{"fiendish vessel","tiefling"},{"Undine adept","undine"},
                            {"watersinger","undine"},    {"Dreamweaver","changeling"}, {"Gray disciple","duergar"},
                            {"Eldritch raider","gillmen"},  {"Bogborn alchemist","grippli"},  {"Kitsune trickster","kitsune"},
                            {"Wave warden","merfolk"},  {"Naga aspirant","nagaji"}, {"Reincarnated oracle","samsaran"},
                            {"Airborne ambusher","strix"}, {"Elemental knight","sulis"}, {"Deep bomber","svirfneblin"},
                            {"Treetop monk","vanara"}, {"Deadly courtesan","vishkanya"}, {"Shadow puppeteer","wayang"},
                                {"Charger","centaur"}  ,  {"Cyclopean Seer","cyclops"}       };


            List<string> CA = _sbCheckerBaseInput.MonsterSB.ClassArchetypes.Split('|').ToList();
            CA.RemoveAll(x => x == string.Empty);

            bool success = true;
            foreach (string archetype in CA)
            {
                if (RaceArchetypes.ContainsKey(archetype))
                {
                    if (_sbCheckerBaseInput.MonsterSB.Race.ToLower() != RaceArchetypes[archetype])
                    {
                        if (_sbCheckerBaseInput.MonsterSB.Race.ToLower() == "drow noble" && archetype == "demonic apostle") continue;
                        _sbCheckerBaseInput.MessageXML.AddFail("CheckRaceArchetypes", "Incorrect Race for Archetype " + archetype);
                        success = false;
                    }
                }
            }

            if (success) _sbCheckerBaseInput.MessageXML.AddPass("CheckRaceArchetypes");
        }


        private void CheckClassAlignments()
        {
            List<ClassWrapper> classes = _sbCheckerBaseInput.CharacterClasses.Classes;
            List<string> alignments;
            bool goodCheck = true;

            foreach (ClassWrapper oneClass in classes)
            {
                alignments = _sbCheckerBaseInput.CharacterClasses.GetClassAlignments(oneClass.Name);
                if (oneClass.Name == "Antipaladin" && _sbCheckerBaseInput.MonsterSBSearch.HasArchetype("tyrant"))
                {
                    alignments.Remove("CE");
                    alignments.Add("LE");
                }
                if (!alignments.Contains(_sbCheckerBaseInput.MonsterSB.Alignment))
                {
                    goodCheck = false;
                    _sbCheckerBaseInput.MessageXML.AddFail("CheckClassAlignments", "Incorrect alignment for " + oneClass.Name + " Current Alignment: " + _sbCheckerBaseInput.MonsterSB.Alignment + " Possible Aligment(s): " + string.Join(",", alignments.ToArray()));
                }
            }

            if (goodCheck)
            {
                _sbCheckerBaseInput.MessageXML.AddPass("CheckClassAlignments");
            }
        }

        private void CheckDomainOrCompanion()
        {
            string CheckName = "Domain Or Companion";

            bool foundOne = false;
            if (_sbCheckerBaseInput.MonsterSBSearch.HasDomain())
            {
                foundOne = true;
            }
            else if (_sbCheckerBaseInput.MonsterSBSearch.HasCompanion())
            {
                foundOne = true;
            }

            if (foundOne)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Druid has to have either domain or companion, missing");
            }

        }

        private void CheckMonkSR(int monkLevel)
        {
            if (monkLevel <= 12) return;

            string CheckName = "Monk SR";

            int SR = monkLevel + 10;
            string formula = "10 +" + monkLevel.ToString() + " monk level";

            string SRString = _sbCheckerBaseInput.MonsterSB.SR;
            int SR_SB = int.Parse(SRString);

            if (SR <= SR_SB)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, SR.ToString(), SR_SB.ToString(), formula);
            }
        }

        private void CheckRage(int barbarianLevel)
        {
            string CheckName = "Rage Frequency";

            if (_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("Mad Dog")) barbarianLevel -= 3;

            int conMod = _sbCheckerBaseInput.MonsterSBSearch.GetBaseAbilityMod(AbilityScores.AbilityScores.AbilityName.Constitution);
            int rageValue = 4 + conMod + ((barbarianLevel - 1) * 2);

            string formula = "4 base +" + conMod.ToString() + " Con Mod +" + ((barbarianLevel - 1) * 2).ToString() + " barbarianLevel (level > 2 * 2)";
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Extra Rage"))
            {
                rageValue += 6;
                formula += " +6 Extra Rage";
            }

            string rageName = "rage";
            if (barbarianLevel >= 11) rageName = "greater rage";

            string rageString = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack(rageName);
            rageString = rageString.Replace(rageName, string.Empty);
            if (rageString.Contains("rounds per day"))
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Has rounds per day instead of rounds/day");
                rageString = rageString.Replace("rounds per day", string.Empty);
            }
            rageString = rageString.Replace("rounds/day", string.Empty);
            rageString = rageString.Replace("rounds/ day", string.Empty);
            rageString = Utility.RemoveParentheses(rageString);

            int rageSB = -500;
            try
            {
                rageSB = int.Parse(rageString);
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "int parse: " + ex.Message);
            }

            if (rageValue == rageSB)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, rageValue.ToString(), rageSB.ToString(), formula);
            }

        }

        private void CheckKiPool(int MonkLevel)
        {
            string CheckName = "Ki Pool";
            string SQ = _sbCheckerBaseInput.MonsterSB.SQ;
            if (MonkLevel < 4) return; //no ki until 4th level

            if (!SQ.Contains("ki pool"))
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Missing Ki Pool value in SQ");
                return;
            }

            int poolValue = (MonkLevel / 2) + _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Wisdom);
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Extra Ki")) poolValue += 2;

            int Pos = SQ.IndexOf("ki pool");
            int Pos2 = SQ.IndexOf("point", Pos);
            string temp = SQ.Substring(Pos, Pos2 - Pos);
            temp = temp.Replace("ki pool", string.Empty);
            temp = temp.Replace(PathfinderConstants.PAREN_LEFT, string.Empty).Trim();
            int SBValue = int.Parse(temp);

            if (poolValue == SBValue)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, poolValue.ToString(), SBValue.ToString());
            }
        }


        private void CheckPaladinAuras(int paladinLevel)
        {
            if (paladinLevel <= 2) return;
            string CheckName = "Paladin Auras";

            string aura = _sbCheckerBaseInput.MonsterSB.Aura;

            if (!aura.Contains("courage")) _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Paladin has aura of courge due to Aura of Courage");

            if (paladinLevel >= 8)
            {
                if (!aura.Contains("resolve")) _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Paladin has aura of courge due to Aura of Resolve");
            }

            if (paladinLevel >= 11)
            {
                if (!aura.Contains("justice")) _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Paladin has aura of courge due to Aura of Justice");
            }
        }

        private void CheckBardicPerformamce(int bardLevel)
        {
            if (_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("archaeologist")) return;
            string CheckName = "Bardic Performamce Frequency";

            int ChaMod = _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Charisma);
            int performanceValue = 4 + ChaMod + (bardLevel - 1) * 2;
            string formula = "4 +" + ChaMod.ToString() + " Cha Mod +" + ((bardLevel - 1) * 2).ToString() + " (bardLevel above 1: " + (bardLevel - 1).ToString() + " * 2)";
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Extra Performance"))
            {
                performanceValue += 6;
                formula += " +6 Extra Performance";
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("pathfinder chronicler"))
            {
                int pathfinderchroniclerLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("pathfinder chronicler");
                int mod = (pathfinderchroniclerLevel - 2) * 2;
                performanceValue += mod;
                formula += " +" + mod.ToString() + " pathfinder chronicler";
            }


            string performanceString = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("bardic performance");
            int Pos = performanceString.IndexOf(PathfinderConstants.PAREN_LEFT);
            if (Pos != -1) performanceString = performanceString.Substring(0, Pos);
            performanceString = performanceString.Replace("bardic performance", string.Empty);
            performanceString = performanceString.Replace("rounds/day", string.Empty).Trim();
            performanceString = performanceString.Replace("rounds/ day", string.Empty).Trim();
            if (performanceString.Length == 0)
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Issue with bardic performance formatting --" + _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("bardic performance"));
                return;
            }
            int performanceSB = int.Parse(performanceString);

            if (performanceSB == performanceValue)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, performanceValue.ToString() + " rounds/day", performanceSB.ToString() + " rounds/day", formula);
            }
        }

        private void CheckChannelEnergyCleric(int clericLevel)
        {
            string CheckName = "Cleric Channel Energy Damage";

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("holy vindicator"))
            {
                int holyVindicatorLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("holy vindicator");
                clericLevel += holyVindicatorLevel;
            }

            try
            {
                string channelEnergy = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("channel positive energy");
                if (channelEnergy.Length == 0) channelEnergy = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("channel negative energy");
                if (channelEnergy.Length == 0) channelEnergy = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("channel energy");
                if (channelEnergy.Length == 0) channelEnergy = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("variant channeling");
                string damage = string.Empty;
                string freq = string.Empty;
                string DC = string.Empty;
                int DC_Value = 0;
                int Pos = 0;
                int Pos2 = 0;

                if (string.IsNullOrEmpty(channelEnergy))
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "No Channel Energy string found");
                    return;
                }

                if (channelEnergy.Contains("d6"))
                {
                    //old way 4/day (DC 17, 5d6)
                    //frog god way (3d6| DC 20, 10/day)
                    //othe frog gods way 6/day (5d6| DC 18)
                    channelEnergy = channelEnergy.Replace("channel energy", string.Empty).Replace("channel positive energy", string.Empty)
                        .Replace("variant channeling", string.Empty).Replace("channel negative energy", string.Empty).Trim();

                    bool frogGod = channelEnergy.Contains("d6|");

                    if (frogGod)
                    {
                        var items = channelEnergy.Split('|');
                        switch (items.Count())
                        {
                            case 3:
                                damage = items[0].Replace(PathfinderConstants.PAREN_LEFT, string.Empty).Trim();
                                freq = items[2].Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Trim();
                                DC = items[1].Replace("DC", string.Empty).Trim();
                                DC_Value = int.Parse(DC);
                                break;
                            case 2:
                                DC = items[1].Replace("DC", string.Empty).Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Trim();
                                DC_Value = int.Parse(DC);
                                items = items[0].Split(' ');
                                freq = items[0];
                                damage = items[1].Replace(PathfinderConstants.PAREN_LEFT, string.Empty).Trim();
                                break;
                        }
                    }
                    else
                    {
                        Pos = channelEnergy.IndexOf("/day");
                        freq = channelEnergy.Substring(0, Pos);
                        Pos2 = channelEnergy.IndexOf(PathfinderConstants.PAREN_LEFT);
                        channelEnergy = channelEnergy.Substring(Pos2);
                        Pos = channelEnergy.IndexOf("|");
                        damage = channelEnergy.Substring(Pos + 1);
                        DC = channelEnergy.Substring(0, Pos);
                        DC = DC.Replace("DC", string.Empty).Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
                        DC_Value = int.Parse(DC);
                    }
                }
                else
                {
                    //new way
                    Pos = channelEnergy.IndexOf("DC");
                    Pos2 = channelEnergy.IndexOf("|", Pos);
                    if (Pos2 == -1) Pos2 = channelEnergy.Length;
                    DC = channelEnergy.Substring(Pos, Pos2 - Pos);
                    DC = DC.Replace("DC", string.Empty).Replace("channel positive energy", string.Empty)
                        .Replace("variant channeling", string.Empty).Replace("channel negative energy", string.Empty).Trim();
                    Pos = DC.IndexOf(PathfinderConstants.SPACE);
                    freq = DC.Substring(0, Pos);
                    DC = DC.Replace(freq, string.Empty).Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
                    DC_Value = int.Parse(DC);
                    Pos = channelEnergy.IndexOf("|");
                    damage = channelEnergy.Substring(Pos + 1);
                }

                if (clericLevel == 20)
                {
                    if (damage.Contains("60 points"))
                    {
                        _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
                    }
                    else
                    {
                        _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "20th level cleric has max channel energy of 60 points");
                    }
                }
                else
                {
                    damage = damage.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Replace("d6", string.Empty);
                    Pos = damage.IndexOf("[");
                    if (Pos >= 0) damage = damage.Substring(0, Pos);
                    int damageSB = int.Parse(damage);


                    int damageValue = 1;
                    if (clericLevel >= 3) damageValue++;
                    if (clericLevel >= 5) damageValue++;
                    if (clericLevel >= 7) damageValue++;
                    if (clericLevel >= 9) damageValue++;
                    if (clericLevel >= 11) damageValue++;
                    if (clericLevel >= 13) damageValue++;
                    if (clericLevel >= 15) damageValue++;
                    if (clericLevel >= 17) damageValue++;
                    if (clericLevel >= 19) damageValue++;

                    if (damageValue == damageSB)
                    {
                        _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
                    }
                    else
                    {
                        _sbCheckerBaseInput.MessageXML.AddFail(CheckName, damageValue.ToString() + "d6", damageSB.ToString() + "d6");
                    }
                }

                CheckName = "Cleric Channel Energy Frequency";
                freq = freq.Replace("/day", string.Empty);
                int freqSB = int.Parse(freq);
                int ChaMod = _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Charisma);
                int freqValue = 3 + ChaMod;
                string formula = "3 +" + ChaMod + " Cha Mod";
                if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Extra Channel"))
                {
                    freqValue += 2;
                    formula += " +2 Extra Channel";
                }

                if (freqSB == freqValue)
                {
                    _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, freqValue.ToString() + "/day", freqSB.ToString() + "/day", formula);
                }

                CheckName = "Cleric Channel Energy DC";
                int SB_DC = 10 + (clericLevel / 2) + ChaMod;
                formula = "10 + " + (clericLevel / 2).ToString() + " (clericLevel / 2) + " + ChaMod.ToString() + "ChaMod";

                if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Improved Channel"))
                {
                    SB_DC += 2;
                    formula += " +2 Improved Channel";
                }

                if (SB_DC == DC_Value)
                {
                    _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, SB_DC.ToString(), DC_Value.ToString(), formula);
                }
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, ex.Message);
            }
        }

        private void CheckStunningFist(int monkLevel)
        {
            if (_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("hungry ghost monk")) return;
            try
            {
                string CheckName = "Stunning Fist Count";
                int otherClassLevels = _sbCheckerBaseInput.CharacterClasses.FindAllClassLevelsExcludingClass("monk");

                int freq = monkLevel + (otherClassLevels / 4);
                string formula = monkLevel.ToString() + " monk level +" + (otherClassLevels / 4).ToString() + " other class levels /4";

                string stunningFist = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("stunning fist");
                stunningFist = stunningFist.Replace("stunning fist", string.Empty);
                if (stunningFist.Length == 0)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Issue with string length = 0");
                    return;
                }
                stunningFist = Utility.RemoveParentheses(stunningFist);
                int Pos = stunningFist.IndexOf("|");
                string stunningFistDC = stunningFist.Substring(Pos + 1);
                stunningFist = stunningFist.Substring(0, Pos);
                stunningFist = stunningFist.Replace("/day", string.Empty);
                int feqSB = int.Parse(stunningFist);

                if (feqSB == freq)
                {
                    _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, freq.ToString(), feqSB.ToString(), formula);
                }

                CheckName = "Stunning Fist DC";
                stunningFistDC = stunningFistDC.Replace("DC", string.Empty).Trim();
                int DC = 10 + _sbCheckerBaseInput.CharacterClasses.FindAllClassLevels() / 2 + _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Wisdom);
                formula = "10 +" + (_sbCheckerBaseInput.CharacterClasses.FindAllClassLevels() / 2).ToString() + " total class levels/2 +" + _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Wisdom).ToString() + " wis mod";
                int DC_SB = int.Parse(stunningFistDC);

                if (DC_SB == DC)
                {
                    _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, DC.ToString(), DC_SB.ToString(), formula);
                }
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("CheckStunningFist", ex.Message);
            }

        }

        private void CheckTrueDeath(int assassinLevel)
        {
            try
            {
                string CheckName = "True Death DC";
                if (assassinLevel < 4) return;
                int DC = 15 + assassinLevel;
                string formula = "15 +" + assassinLevel.ToString() + " assassin level";

                string trueDeath = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("true death");
                trueDeath = trueDeath.Replace("true death", string.Empty);
                trueDeath = trueDeath.Replace("DC", string.Empty);
                trueDeath = Utility.RemoveParentheses(trueDeath);
                int trueDeathSB;
                int.TryParse(trueDeath, out trueDeathSB);

                if (trueDeathSB == DC)
                {
                    _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, DC.ToString(), trueDeathSB.ToString(), formula);
                }
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("CheckTrueDeath", ex.Message);
            }
        }

        private void CheckDeathAttack(int assassinLevel)
        {
            string CheckName = "Death Attack";
            int deathDC = 10 + assassinLevel + _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Intelligence);

            string deathDCString = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("death attack");
            deathDCString = deathDCString.Replace("death attack", string.Empty);
            deathDCString = Utility.RemoveParentheses(deathDCString);
            deathDCString = deathDCString.Replace("DC", string.Empty).Trim();
            int deathDC_SB = int.Parse(deathDCString);

            if (deathDC_SB == deathDC)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, deathDC.ToString(), deathDC_SB.ToString());
            }
        }

        private void CheckChannelEnergyPaladin(int paladinLevel)
        {
            string CheckName = "Channel Energy Damage";
            if (paladinLevel < 4) return;

            string channelEnergy = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("channel positive energy");
            if (channelEnergy.Length == 0)
            {
                throw new Exception("CheckChannelEnergyPaladin: Missing channel positive energy from Special Attack");
            }
            int Pos = channelEnergy.IndexOf("|");
            string DC = channelEnergy.Substring(0, Pos);
            DC = DC.Replace("DC", string.Empty);
            DC = DC.Replace("channel positive energy", string.Empty);
            DC = DC.Replace(PathfinderConstants.PAREN_LEFT, string.Empty).Trim();
            if (DC.Contains("/day"))
            {
                Pos = DC.IndexOf(PathfinderConstants.SPACE);
                DC = DC.Substring(Pos);
            }
            int DC_Value = int.Parse(DC);
            string damage = channelEnergy.Substring(Pos + 1);

            if (paladinLevel == 20)
            {
                if (damage.Contains("60 points"))
                {
                    _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "20th level paladin has max channel energy of 60 points");
                }
            }
            else
            {
                damage = damage.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                damage = damage.Replace("d6", string.Empty);
                if (damage.Contains("|"))
                {
                    Pos = damage.IndexOf("|");
                    damage = damage.Substring(Pos + 1);
                }
                int damageValue = int.Parse(damage);


                int damgeSB = 1;
                if (paladinLevel >= 3) damgeSB++;
                if (paladinLevel >= 5) damgeSB++;
                if (paladinLevel >= 7) damgeSB++;
                if (paladinLevel >= 9) damgeSB++;
                if (paladinLevel >= 11) damgeSB++;
                if (paladinLevel >= 13) damgeSB++;
                if (paladinLevel >= 15) damgeSB++;
                if (paladinLevel >= 17) damgeSB++;
                if (paladinLevel >= 19) damgeSB++;

                if (damgeSB == damageValue)
                {
                    _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, damageValue.ToString() + "d6", damgeSB.ToString() + "d6");
                }
            }


            CheckName = "Channel Energy DC";
            int SB_DC = 10 + (paladinLevel / 2) + _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Charisma);

            if (SB_DC == DC_Value)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, DC_Value.ToString(), SB_DC.ToString());
            }
        }


        private void CheckSmiteEvil(int paladinLevel)
        {
            string CheckName = "Smite Evil Count";
            try
            {
                string smiteEvil = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("smite evil");
                smiteEvil = smiteEvil.Replace("smite evil", string.Empty);
                int Pos = smiteEvil.IndexOf(PathfinderConstants.PAREN_LEFT);
                smiteEvil = smiteEvil.Substring(0, Pos).Trim();
                smiteEvil = smiteEvil.Replace("/day", string.Empty);
                int smiteEvilSB = int.Parse(smiteEvil);


                int smiteEvilCount = 1;
                if (paladinLevel >= 4) smiteEvilCount++;
                if (paladinLevel >= 7) smiteEvilCount++;
                if (paladinLevel >= 10) smiteEvilCount++;
                if (paladinLevel >= 13) smiteEvilCount++;
                if (paladinLevel >= 16) smiteEvilCount++;
                if (paladinLevel >= 19) smiteEvilCount++;

                if (smiteEvilCount == smiteEvilSB)
                {
                    _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, smiteEvilCount.ToString(), smiteEvilSB.ToString());
                }
            }
            catch
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Not properly formatted");
            }
        }

        private int GetAspisAgentMult()
        {
            if (!_sbCheckerBaseInput.CharacterClasses.HasClass("aspis agent")) return 0;
            int AspisAgentLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("aspis agent");
            int count = 0;
            if (AspisAgentLevel >= 3) count++;
            if (AspisAgentLevel >= 6) count++;
            if (AspisAgentLevel >= 9) count++;
            return count;
        }

        private int GetBellflowerTillerMult()
        {
            if (!_sbCheckerBaseInput.CharacterClasses.HasClass("bellflower tiller")) return 0;
            int AspisAgentLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("bellflower tiller");
            int count = 0;
            if (AspisAgentLevel >= 3) count++;
            if (AspisAgentLevel >= 6) count++;
            if (AspisAgentLevel >= 9) count++;
            return count;
        }

        private int GetArcaneTricksterMult()
        {
            if (!_sbCheckerBaseInput.CharacterClasses.HasClass("arcane trickster")) return 0;
            int ATLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("arcane trickster");
            return ATLevel / 2;
        }

        private int GetAssassinMult()
        {
            if (!_sbCheckerBaseInput.CharacterClasses.HasClass("assassin")) return 0;
            int assassinLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("assassin");
            return ((assassinLevel - 1) / 2) + 1;
        }

        private int GetRedMantisAssassinMult()
        {
            if (!_sbCheckerBaseInput.CharacterClasses.HasClass("red mantis assassin")) return 0;
            int RMALevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("red mantis assassin");
            return (RMALevel / 2);
        }

        private int GetNinjaMult()
        {
            if (!_sbCheckerBaseInput.CharacterClasses.HasClass("ninja")) return 0;
            int ninjaLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("ninja");
            return ((ninjaLevel - 1) / 2) + 1;
        }


        private void CheckSneakStab(int rogueLevel)
        {
            string CheckName = "Sneak Stab Damage";

            int multiplier = (rogueLevel / 2);
            if (rogueLevel % 2 != 0) multiplier++;
            multiplier += GetArcaneTricksterMult();
            multiplier += GetAssassinMult();
            multiplier += GetAspisAgentMult();
            multiplier += GetRedMantisAssassinMult();
            multiplier += GetNinjaMult();
            multiplier += GetBellflowerTillerMult();

            string sneakAttack = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("sneak stab");
            sneakAttack = sneakAttack.Replace("sneak stab", string.Empty).Trim();
            int Pos = sneakAttack.IndexOf("d");
            int mult;

            try
            {
                mult = int.Parse(sneakAttack.Substring(0, Pos));
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("CheckSneakStab", "Issue with Multiplier-- " + sneakAttack + PathfinderConstants.SPACE + ex.Message);
                return;
            }

            if (mult == multiplier)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, multiplier.ToString() + "d6", mult.ToString() + "d6");
            }
        }

        private void CheckSneakAttack(int rogueLevel)
        {
            string CheckName = "Sneak Attack Damage";

            int multiplier = (rogueLevel / 2);
            if (rogueLevel % 2 != 0) multiplier++;
            multiplier += GetArcaneTricksterMult();
            multiplier += GetAssassinMult();
            multiplier += GetAspisAgentMult();
            multiplier += GetRedMantisAssassinMult();
            multiplier += GetNinjaMult();

            string sneakAttack = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("sneak attack");
            sneakAttack = sneakAttack.Replace("sneak attack", string.Empty).Trim();
            int Pos = sneakAttack.IndexOf("d");
            int mult;

            try
            {
                mult = int.Parse(sneakAttack.Substring(0, Pos));
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("CheckSneakAttack", "Issue with Multiplier-- " + sneakAttack + PathfinderConstants.SPACE + ex.Message);
                return;
            }

            if (mult == multiplier)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, multiplier.ToString() + "d6", mult.ToString() + "d6");
            }

        }

        private void CheckFavoredEnemy(int rangerLevel)
        {
            if (_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("guide") || _sbCheckerBaseInput.MonsterSBSearch.HasArchetype("hooded champion")) return;
            string CheckName = "Favored Enemy Count";
            int favoredEnemyCount = 0;

            if (rangerLevel >= 1) favoredEnemyCount++;
            if (rangerLevel >= 5) favoredEnemyCount++;
            if (rangerLevel >= 10) favoredEnemyCount++;
            if (rangerLevel >= 15) favoredEnemyCount++;
            if (rangerLevel >= 20) favoredEnemyCount++;

            int favoredEnemyPoints = (2 * favoredEnemyCount) + (2 * (favoredEnemyCount - 1));

            string favoredEnemy = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("favored enemy");
            if (string.IsNullOrEmpty(favoredEnemy))
            {
                favoredEnemy = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("favored enemies");
                favoredEnemy = favoredEnemy.Replace("favored enemies (", string.Empty);
            }
            else
            {
                favoredEnemy = favoredEnemy.Replace("favored enemy (", string.Empty);
            }
            favoredEnemy = favoredEnemy.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Trim();
            List<string> favoredEnemyList = favoredEnemy.Split('|').ToList();
            favoredEnemyList.RemoveAll(x => x == string.Empty);

            if (favoredEnemyList.Count == favoredEnemyCount)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, favoredEnemyCount.ToString(), favoredEnemyList.Count.ToString());
            }

            CheckName = "Favored Enemy Values";
            int favoredEnemyPoints_Computed = 0;

            foreach (string FE in favoredEnemyList)
            {
                string temp = FE;
                int Pos = temp.IndexOf("+");
                if (Pos == -1)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, FE + " missing points");
                    return;
                }

                temp = temp.Substring(Pos);
                int value;
                int.TryParse(temp, out value);
                if (value == 0)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, FE + " value equals 0");
                }
                if (value % 2 != 0)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, FE + "not a multiple of 2");
                }
                favoredEnemyPoints_Computed += value;
            }

            if (favoredEnemyPoints_Computed == favoredEnemyPoints)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, favoredEnemyPoints.ToString(), favoredEnemyPoints_Computed.ToString());
            }
        }

        private void CheckSecrets(int loremasterLevel)
        {
            string CheckName = "Secret Count";

            int secretCount = 1;
            if (loremasterLevel >= 3) secretCount++;
            if (loremasterLevel >= 5) secretCount++;
            if (loremasterLevel >= 7) secretCount++;
            if (loremasterLevel >= 9) secretCount++;

            string secretsString = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("secrets");
            secretsString = secretsString.Replace("secrets", string.Empty);
            secretsString = Utility.RemoveParentheses(secretsString);
            List<string> Secrets = secretsString.Split('|').ToList();

            if (Secrets.Count == secretCount)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, secretCount.ToString(), Secrets.Count.ToString());
            }
        }


        private void CheckRogueTalents(int rogueLevel)
        {
            string CheckName = "Rogue Talents Count";
            string formula = string.Empty;

            if (rogueLevel == 1) return;
            if (_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("Sharper")) return;

            int talentCount = rogueLevel / 2;
            formula = "base:" + talentCount.ToString();

            if (_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("underground chemist") && rogueLevel >= 4)
            {
                talentCount--; //Precise Splash Weapons (Ex)
                formula += " -1 underground chemist";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Extra Rogue Talent"))
            {
                int count = _sbCheckerBaseInput.MonsterSBSearch.FeatItemCount("Extra Rogue Talent");
                talentCount += count;
                formula += " +" + count.ToString() + "  Extra Rogue Talent(s)";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("pirate") && rogueLevel >= 2)
            {
                talentCount--;
                formula += " -1 pirate";
            }
            if (_sbCheckerBaseInput.CharacterClasses.HasClass("shadowdancer"))
            {
                int shadowdancerLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("shadowdancer");
                int mod = 0;
                if (shadowdancerLevel >= 3) mod++;
                if (shadowdancerLevel >= 6) mod++;
                if (shadowdancerLevel >= 9) mod++;
                talentCount += mod;
                formula += " +" + mod.ToString() + "  shadowdancer";
            }
            string rogueTalents = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("rogue talents");
            if (rogueTalents.Length == 0)
            {
                rogueTalents = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("rogue talent");
                rogueTalents = rogueTalents.Replace("rogue talent (", string.Empty);
            }
            rogueTalents = rogueTalents.Replace("rogue talents (", string.Empty)
                .Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Trim();
            if (rogueTalents.Contains("skill mastery ["))
            {
                //only one talent not many
                int Start = rogueTalents.IndexOf("[");
                int Stop = rogueTalents.IndexOf("]", Start + 1);
                string tempRemove = rogueTalents.Substring(Start, Stop - Start);
                rogueTalents = rogueTalents.Replace(tempRemove, string.Empty);
            }
            List<string> rogueTalentsList = rogueTalents.Split('|').ToList();
            rogueTalentsList.RemoveAll(x => x == string.Empty);

            if (rogueTalentsList.Count == talentCount)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName, formula);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, talentCount.ToString(), rogueTalentsList.Count.ToString(), formula);
            }
        }

        private void CheckFavoredTerrain(int rangerLevel)
        {
            string CheckName = "Favored Terrain Count";
            int favoredTerrainCount = 0;

            if (rangerLevel >= 3) favoredTerrainCount++;
            if (rangerLevel >= 8) favoredTerrainCount++;
            if (rangerLevel >= 13) favoredTerrainCount++;
            if (rangerLevel >= 18) favoredTerrainCount++;

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("horizon walker"))
            {
                int horizonWalkerLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("horizon walker");
                if (horizonWalkerLevel >= 1) favoredTerrainCount++;
                if (horizonWalkerLevel >= 2) favoredTerrainCount++;
                if (horizonWalkerLevel >= 4) favoredTerrainCount++;
                if (horizonWalkerLevel >= 5) favoredTerrainCount++;
                if (horizonWalkerLevel >= 7) favoredTerrainCount++;
                if (horizonWalkerLevel >= 8) favoredTerrainCount++;
                if (horizonWalkerLevel >= 10) favoredTerrainCount++;
            }

            int favoredTerrainPoints = (2 * favoredTerrainCount) + (2 * (favoredTerrainCount - 1));

            string favoredTerrain = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("favored terrain");
            favoredTerrain = favoredTerrain.Replace("favored terrain (", string.Empty).Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Trim();
            List<string> favoredTerrainList = favoredTerrain.Split('|').ToList();
            favoredTerrainList.RemoveAll(x => x == string.Empty);

            if (favoredTerrainList.Count == favoredTerrainCount)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, favoredTerrainCount.ToString(), favoredTerrainList.Count.ToString());
            }

            if (rangerLevel < 3) return; //no FT until 3rd

            CheckName = "Favored Terrain Values";
            int favoredTerrainPoints_Computed = 0;

            foreach (string FT in favoredTerrainList)
            {
                string temp = FT;
                if (!temp.Contains("+"))
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, FT + " has no numeric value");
                    return;
                }
                int Pos = temp.IndexOf("+");
                temp = temp.Substring(Pos).Replace("*", string.Empty);
                int value = int.Parse(temp);
                if (value % 2 != 0)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, FT + " not a multiple of 2");
                }
                favoredTerrainPoints_Computed += value;
            }

            if (favoredTerrainPoints_Computed == favoredTerrainPoints)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, favoredTerrainPoints_Computed.ToString(), favoredTerrainPoints.ToString());
            }
        }

        private void CheckMercies(int paladinLevel)
        {
            if (paladinLevel <= 2) return;
            string CheckName = "Mercy Count";

            int mercyCount = 1;
            if (paladinLevel >= 6) mercyCount++;
            if (paladinLevel >= 9) mercyCount++;
            if (paladinLevel >= 12) mercyCount++;
            if (paladinLevel >= 15) mercyCount++;
            if (paladinLevel >= 18) mercyCount++;
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Extra Mercy")) mercyCount++;

            string mercySB = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("mercy");
            if (mercySB.Length == 0) mercySB = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("mercies");
            mercySB = mercySB.Replace("mercy", string.Empty);
            mercySB = mercySB.Replace("mercies", string.Empty);
            mercySB = mercySB.Replace(PathfinderConstants.PAREN_LEFT, string.Empty).Replace(PathfinderConstants.SPACE, string.Empty)
                .Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Trim();
            List<string> mercies = mercySB.Split('|').ToList();

            if (mercies.Count == mercyCount)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, mercyCount.ToString(), mercies.Count.ToString());
            }


            CheckName = "Mercy Allowed";
            List<string> merciesAllowed = new List<string> { "fatigued", "shaken", "sickened" };
            if (paladinLevel >= 6) merciesAllowed.AddRange(new List<string> { "dazed", "diseased", "staggered" });
            if (paladinLevel >= 9) merciesAllowed.AddRange(new List<string> { "cursed", "exhausted", "frightened", "nauseated", "poisoned" });
            if (paladinLevel >= 12) merciesAllowed.AddRange(new List<string> { "blinded", "deafened", "paralyzed", "stunned" });

            foreach (string mercy in mercies)
            {
                if (!merciesAllowed.Contains(mercy)) _sbCheckerBaseInput.MessageXML.AddFail(CheckName, mercy + " is not allowed at level " + paladinLevel.ToString());
            }

            CheckName = "Mercy PreReqs";

            if (mercies.Contains("exhausted") && !mercies.Contains("fatigued")) _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "mercy exhausted needs fatigued as prereq.");
            if (mercies.Contains("frightened") && !mercies.Contains("shaken")) _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "mercy frightened needs shaken as prereq.");
            if (mercies.Contains("nauseated") && !mercies.Contains("sickened")) _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "mercy nauseated needs sickened as prereq.");
        }

        private void CheckLayOnHands(int paladinLevel)
        {
            if (paladinLevel <= 1) return;
            string CheckName = "Lay On Hands Count";

            int LOH = (paladinLevel / 2) + _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Charisma);

            string SB_LOH = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("lay on hands");
            if (SB_LOH.Contains("|")) //lay on hands (2d6, 5/day)
            {
                int Pos = SB_LOH.IndexOf("|");
                SB_LOH = SB_LOH.Substring(Pos + 1);
            }
            else //lay on hands 3/day (1d6)
            {
                int Pos = SB_LOH.IndexOf(PathfinderConstants.PAREN_LEFT);
                SB_LOH = SB_LOH.Substring(Pos);
                SB_LOH = SB_LOH.Replace("lay on hands", string.Empty);
            }
            SB_LOH = SB_LOH.Replace("/day", string.Empty);
            SB_LOH = Utility.RemoveParentheses(SB_LOH);
            int LOHCount = int.Parse(SB_LOH);
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Extra Lay on Hands")) LOH += 2;

            if (LOHCount == LOH)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, LOH.ToString(), LOHCount.ToString());
            }


            CheckName = "Lay On Hands Damage";
            SB_LOH = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("lay on hands");

            if (paladinLevel == 20)
            {
                if (SB_LOH.Contains("60 points"))
                {
                    _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "20th level paladin has max lay on hands of 60 points");
                }
            }
            else
            {
                if (SB_LOH.Contains("|")) //lay on hands (2d6, 5/day)
                {
                    int Pos = SB_LOH.IndexOf("|");
                    SB_LOH = SB_LOH.Substring(0, Pos);
                }
                else //lay on hands 3/day (1d6)
                {
                    int Pos = SB_LOH.IndexOf(PathfinderConstants.PAREN_LEFT);
                    SB_LOH = SB_LOH.Substring(Pos + 1);
                    SB_LOH = SB_LOH.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                }
                SB_LOH = SB_LOH.Replace("lay on hands", string.Empty).Replace("d6", string.Empty)
                    .Replace(PathfinderConstants.PAREN_LEFT, string.Empty).Trim();

                int damageSB = int.Parse(SB_LOH);
                int damage = paladinLevel / 2;

                if (damageSB == damage)
                {
                    _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, damage.ToString(), damageSB.ToString());
                }
            }
        }

        private void CheckPaladinImmunities(int paladinLevel)
        {
            string immuneHold = _sbCheckerBaseInput.MonsterSB.Immune;
            if (paladinLevel >= 3)
            {
                if (!immuneHold.Contains("fear")) _sbCheckerBaseInput.MessageXML.AddFail("Immune", "Paladin supposed to have immune fear due to Aura of Courage");
                if (!immuneHold.Contains("disease")) _sbCheckerBaseInput.MessageXML.AddFail("Immune", "Paladin supposed to have immune disease due to Divine Health");
            }
            if (paladinLevel >= 8)
            {
                if (!immuneHold.Contains("charm")) _sbCheckerBaseInput.MessageXML.AddFail("Immune", "Paladin supposed to have immune disease due to Aura of Resolve");
            }
            if (paladinLevel >= 17)
            {
                if (!immuneHold.Contains("compulsion")) _sbCheckerBaseInput.MessageXML.AddFail("Immune", "Paladin supposed to have immune compulsion due to Aura of Righteousness");
            }
        }

        private void CheckMonkImmunities(int monkLevel)
        {
            string immuneHold = _sbCheckerBaseInput.MonsterSB.Immune;
            if (monkLevel >= 5)
            {
                if (!immuneHold.Contains("disease") && !_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("hungry ghost monk")) _sbCheckerBaseInput.MessageXML.AddFail("Immune", "Monk supposed to have immune disease due to Purity of Body");
            }
            if (monkLevel >= 11)
            {
                if (!immuneHold.Contains("poison")) _sbCheckerBaseInput.MessageXML.AddFail("Immune", "Monk supposed to have immune poison due to Diamond Body");
            }
        }
    }
}
