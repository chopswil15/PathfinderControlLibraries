
using OnGoing;
using RaceManager;
using StatBlockCommon;
using StatBlockCommon.Monster_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using System.Text;
using System.Threading.Tasks;
using PathfinderGlobals;
using StatBlockBusiness;
using StatBlockParsing;
using CreatureTypeFoundational;

namespace StatBlockChecker.Parsers
{
    public class RaceParser : IRaceParser
    {
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private CreatureTypeFoundation _creatureType;
        private IMonsterStatBlockBusiness _monsterStatBlockBusiness;

        public RaceParser(ISBCheckerBaseInput sbCheckerBaseInput, IMonsterStatBlockBusiness monsterStatBlockBusiness)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _creatureType = _sbCheckerBaseInput.CreatureType;
            _monsterStatBlockBusiness = monsterStatBlockBusiness;
        }

        public void ParseRace()
        {
            _sbCheckerBaseInput.Race_Base = null;
            string tempRace = _sbCheckerBaseInput.MonsterSB.Race;
            if (tempRace.Length == 0) tempRace = _sbCheckerBaseInput.MonsterSB.name;
            if (tempRace.Contains(PathfinderConstants.PAREN_LEFT))
            {
                int Pos = tempRace.IndexOf(PathfinderConstants.PAREN_LEFT);
                tempRace = tempRace.Substring(0, Pos).Trim();
            }

            if (tempRace.Length > 0)
            {
                if (tempRace.Contains(" tiefling"))
                {
                    int Pos = tempRace.IndexOf(" tiefling");
                    string temp = tempRace.Substring(0, Pos);
                    tempRace = tempRace.Replace(temp, string.Empty).Trim();
                }
                if (tempRace.Contains("Weakened")) tempRace = tempRace.Replace("Weakened", string.Empty).Trim();

                List<string> baseRaceNames = CommonMethods.GetBaseRaceNames();
                foreach (string brn in baseRaceNames)
                {
                    if (tempRace.EndsWith(brn)) tempRace = tempRace.Replace(brn, string.Empty).Trim();
                }

                RaceManager.RaceMaster RM = new RaceMaster();
                _sbCheckerBaseInput.Race_Base = new RaceBase(RM.ParceRace(tempRace), _creatureType, tempRace, _monsterStatBlockBusiness);
                if (_sbCheckerBaseInput.Race_Base.RaceBaseType == RaceBase.RaceType.None)
                {
                    if (_sbCheckerBaseInput.MonsterSB.Environment.Length == 0)
                    {
                        MonsterStatBlock Mon_SB = _monsterStatBlockBusiness.GetBestiaryMonsterByNamePathfinderDefault(tempRace);

                        _sbCheckerBaseInput.Race_Base = new RaceBase(Mon_SB, _creatureType, false, _sbCheckerBaseInput.IndvSB, string.Empty, _monsterStatBlockBusiness);
                        if (Mon_SB == null)
                        {
                            if (tempRace.Contains("Animal Companion") || tempRace.Contains("animal companion") ||
                                tempRace.Contains(" Tier") || tempRace.Contains(" familiar"))
                            {
                                tempRace = tempRace.Replace("Animal Companion", string.Empty);
                                tempRace = tempRace.Replace("animal companion", string.Empty);
                                int Pos = tempRace.IndexOf("Tier");
                                if (Pos > 0)
                                {
                                    tempRace = tempRace.Substring(0, Pos).Trim();
                                }
                                tempRace = tempRace.Replace("familiar", string.Empty);
                                Mon_SB = _monsterStatBlockBusiness.GetBestiaryMonsterByNamePathfinderDefault(tempRace);
                                _sbCheckerBaseInput.Race_Base = new RaceBase(Mon_SB, _creatureType, false, _sbCheckerBaseInput.IndvSB, string.Empty, _monsterStatBlockBusiness);
                                if (Mon_SB == null)
                                {
                                    _sbCheckerBaseInput.MessageXML.AddFail("Missing Familiar/Companion Race", tempRace);
                                }
                            }
                            else if (tempRace.Contains("elemental"))
                            {
                                if (!tempRace.Contains(_sbCheckerBaseInput.MonsterSB.Size))
                                {
                                    tempRace = _sbCheckerBaseInput.MonsterSB.Size + PathfinderConstants.SPACE + tempRace;
                                }
                                Mon_SB = _monsterStatBlockBusiness.GetBestiaryMonsterByNamePathfinderDefault(tempRace);
                                _sbCheckerBaseInput.Race_Base = new RaceBase(Mon_SB, _creatureType, false, _sbCheckerBaseInput.IndvSB, string.Empty, _monsterStatBlockBusiness);
                                if (Mon_SB == null)
                                {
                                    _sbCheckerBaseInput.MessageXML.AddFail("Missing Race", tempRace);
                                }
                            }
                            else if (tempRace.ToLower().Contains("graven guardian"))
                            {
                                tempRace = tempRace.Substring(0, "graven guardian".Length);
                                Mon_SB = _monsterStatBlockBusiness.GetBestiaryMonsterByNamePathfinderDefault(tempRace);
                                _sbCheckerBaseInput.Race_Base = new RaceBase(Mon_SB, _creatureType, false, _sbCheckerBaseInput.IndvSB, string.Empty, _monsterStatBlockBusiness);
                                if (Mon_SB == null)
                                {
                                    _sbCheckerBaseInput.MessageXML.AddFail("Missing Race", tempRace);
                                }

                            }
                            else
                            {
                                List<string> tempCommonTempaltes = CommonMethods.GetCommonTemplates();
                                string hold = string.Empty;
                                bool Found = false;
                                foreach (string common in tempCommonTempaltes)
                                {
                                    if (tempRace.ToLower().Contains(common.ToLower()))
                                    {
                                        hold = tempRace.ToLower().Replace(common.ToLower(), string.Empty).Trim();
                                        Mon_SB = _monsterStatBlockBusiness.GetBestiaryMonsterByNamePathfinderDefault(hold);
                                        if (Mon_SB != null)
                                        {
                                            Found = true;
                                            _sbCheckerBaseInput.MonsterSB.Race = _sbCheckerBaseInput.MonsterSB.Race.Replace(common.ToLower(), string.Empty).Trim();
                                            _sbCheckerBaseInput.MonsterSB.TemplatesApplied += common.ToLower() + "|";
                                            break;
                                        }
                                    }
                                }
                                //if (!Found && CharacterClasses.HasClass("eidolon"))
                                //{
                                //    //find eidolon base
                                //}
                                //else 
                                if (!Found) _sbCheckerBaseInput.MessageXML.AddFail("Missing Race", tempRace);
                            }
                        }
                        else
                        {
                            if (Mon_SB.DontUseRacialHD)
                            {
                                _sbCheckerBaseInput.MessageXML.AddFail("Missing Race, no race data", tempRace);
                            }
                        }
                    }

                    if (_sbCheckerBaseInput.Race_Base.RaceBaseType == RaceBase.RaceType.None) //Bestiary SB
                    {
                        _sbCheckerBaseInput.Race_Base = new RaceBase(_sbCheckerBaseInput.MonsterSB, _creatureType, true, _sbCheckerBaseInput.IndvSB, string.Empty, _monsterStatBlockBusiness);
                    }
                }
            }
            else
            {
                throw new Exception("ParceRace: Both Race and Name are empty");
            }

            if (_sbCheckerBaseInput.Race_Base.RaceSB == null) _sbCheckerBaseInput.MessageXML.AddFail("ParseRace", "Missing Race, no Bestiary Race entry for " + tempRace);

            List<OnGoingStatBlockModifier> mods = _sbCheckerBaseInput.Race_Base.RacialOnGoingMods();

            foreach (OnGoingStatBlockModifier mod in mods)
            {
                _sbCheckerBaseInput.IndvSB.AddOnGoingStatBlockMod(mod);
            }
        }
    }
}
