using CommonStatBlockInfo;
using FeatManager;
using PathfinderGlobals;
using StatBlockBusiness;
using StatBlockCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace StatBlockChecker.Parsers
{
    public class FeatParser
    {
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private FeatMaster _featManager;
        private IFeatStatBlockBusiness _featStatBlockBusiness;

        public FeatParser(ISBCheckerBaseInput sbCheckerBaseInput, FeatMaster featManager, IFeatStatBlockBusiness featStatBlockBusiness)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _featManager = featManager;
            _featStatBlockBusiness = featStatBlockBusiness;
        }

        public  void ParseFeats()
        {
            string workingFeat = null;

            try
            {
                bool found = false;
                string holdFeats = _sbCheckerBaseInput.MonsterSB.Feats;
                Utility.ParenCommaFix(ref holdFeats);
                List<string> tempFeats = holdFeats.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                string startFeatName = string.Empty;
                for (int a = tempFeats.Count - 1; a >= 0; a--)
                {
                    if (tempFeats[a].Contains("|"))
                    {
                        int Pos = tempFeats[a].IndexOf(PathfinderConstants.PAREN_LEFT);
                        startFeatName = tempFeats[a].Substring(0, Pos + 1).Trim();
                        holdFeats = tempFeats[a].Substring(Pos);
                        holdFeats = holdFeats.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                        holdFeats = holdFeats.Replace(PathfinderConstants.PAREN_LEFT, string.Empty).Trim();
                        List<string> subFeatsList = holdFeats.Split('|').ToList();
                        //1st one
                        tempFeats[a] = startFeatName + subFeatsList[0].Trim() + PathfinderConstants.PAREN_RIGHT;
                        subFeatsList.Remove(subFeatsList[0]);
                        foreach (string sub in subFeatsList)
                        {
                            tempFeats.Add(startFeatName + sub.Trim() + PathfinderConstants.PAREN_RIGHT);
                        }
                    }
                }

                for (int a = 0; a < tempFeats.Count; a++)
                {
                    found = false;
                    foreach (string script in _sbCheckerBaseInput.SourceSuperScripts)
                    {
                        if (tempFeats[a].EndsWith(script))
                        {
                            tempFeats[a] = tempFeats[a].Substring(0, tempFeats[a].Length - script.Length).Trim();
                            found = true;
                        }

                        if (tempFeats[a].Contains(script) && !found)
                        {
                            tempFeats[a] = tempFeats[a].Replace(script, string.Empty).Trim();
                            found = true;
                        }
                    }
                    if (tempFeats[a].EndsWith("M") && !found)
                    {
                        tempFeats[a] = tempFeats[a].Substring(0, tempFeats[a].Length - 1).Trim();
                    }
                }


                StatBlockInfo.ArmorProficiencies tempArmorPro = _sbCheckerBaseInput.CharacterClasses.GetAllKnownArmorProficiencies();
                if ((tempArmorPro & StatBlockInfo.ArmorProficiencies.Light) == StatBlockInfo.ArmorProficiencies.Light)
                {
                    tempFeats.Add("Light Armor Proficiency");
                }
                if ((tempArmorPro & StatBlockInfo.ArmorProficiencies.Medium) == StatBlockInfo.ArmorProficiencies.Medium)
                {
                    tempFeats.Add("Medium Armor Proficiency");
                }
                if ((tempArmorPro & StatBlockInfo.ArmorProficiencies.Heavy) == StatBlockInfo.ArmorProficiencies.Heavy)
                {
                    tempFeats.Add("Heavy Armor Proficiency");
                }
                StatBlockInfo.ShieldProficiencies tempShieldPro = _sbCheckerBaseInput.CharacterClasses.GetAllKnownShieldProficiencies();
                if ((tempShieldPro & StatBlockInfo.ShieldProficiencies.Shield) == StatBlockInfo.ShieldProficiencies.Shield)
                {
                    tempFeats.Add("Shield Proficiency");
                }
                if ((tempShieldPro & StatBlockInfo.ShieldProficiencies.Tower) == StatBlockInfo.ShieldProficiencies.Tower)
                {
                    tempFeats.Add("Tower Shield Proficiency");
                }
                StatBlockInfo.WeaponProficiencies tempWeaponPro = _sbCheckerBaseInput.CharacterClasses.GetAllKnownWeaponsProficiencies();
                if ((tempWeaponPro & StatBlockInfo.WeaponProficiencies.Simple) == StatBlockInfo.WeaponProficiencies.Simple)
                {
                    tempFeats.Add("Simple Weapon Proficiency");
                }
                if ((tempWeaponPro & StatBlockInfo.WeaponProficiencies.Martial) == StatBlockInfo.WeaponProficiencies.Martial)
                {
                    tempFeats.Add("Martial Weapon Proficiency");
                }
                if ((tempWeaponPro & StatBlockInfo.WeaponProficiencies.Exotic) == StatBlockInfo.WeaponProficiencies.Exotic)
                {
                    tempFeats.Add("Exotic Weapon Proficiency");
                }
               
                string hold = string.Empty;
                string selectedItem = string.Empty;
                bool bonusFeat;
                IFeatStatBlock featSB;
                List<string> FailedFeats = new List<string>();
                tempFeats.RemoveAll(p => p == string.Empty);
                tempFeats.RemoveAll(p => p == " *");
                tempFeats.RemoveAll(p => p == PathfinderConstants.SPACE);

                foreach (string tempFeat in tempFeats)
                {
                    hold = tempFeat.Trim();
                    workingFeat = hold;
                    // hold = hold.Replace("'", string.Empty);
                    bonusFeat = false;
                    if (hold.EndsWith("B"))
                    {
                        hold = hold.Substring(0, hold.Length - 1);
                        bonusFeat = true;
                    }
                    else if (hold.Contains("B "))
                    {
                        hold = hold.Replace("B ", PathfinderConstants.SPACE);
                        bonusFeat = true;
                    }
                    selectedItem = string.Empty;
                    int Pos = hold.IndexOf(PathfinderConstants.PAREN_LEFT);
                    if (Pos >= 0)
                    {
                        selectedItem = hold.Substring(Pos);
                        hold = hold.Replace(selectedItem, string.Empty).Trim();
                        selectedItem = Utility.RemoveParentheses(selectedItem);
                    }
                    hold = hold.Replace("**", string.Empty);
                    hold = hold.Replace("*", string.Empty);

                    if (hold.EndsWith("[M]"))
                    {
                        // hold = hold.Substring(0, hold.Length - 1);
                        hold = hold.Replace("[M]", string.Empty);
                        featSB = _featStatBlockBusiness.GetMythicFeatByName(hold);
                    }
                    else
                    {
                        featSB = _featStatBlockBusiness.GetFeatByName(hold);
                    }
                    if (featSB != null)
                    {
                        _featManager.AddFeatData(new FeatData(featSB.name, featSB.prerequisites, featSB.prerequisite_feats, bonusFeat, selectedItem, featSB.type, featSB.prerequisite_skills));
                    }
                    else
                    {
                        FailedFeats.Add(hold);
                    }
                }

                if (FailedFeats.Any())
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("ParseFeats", "Feats: " + string.Join(", ", FailedFeats.ToArray()) + " not defined");
                }

                //_featManager = new FeatMaster(temp);
            }
            catch (Exception ex)
            {
                if (_featManager == null)
                {
                    _featManager = new FeatMaster(new List<string>());
                }
                _sbCheckerBaseInput.MessageXML.AddFail("Parse Feats", "Working Feat : " + workingFeat + "   " + ex.Message);
            }
        }
    }
}
