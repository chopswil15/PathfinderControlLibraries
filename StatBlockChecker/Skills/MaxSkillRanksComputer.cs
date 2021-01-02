using StatBlockChecker.Parsers;
using CommonStatBlockInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockChecker.Skills
{
    public class MaxSkillRanksComputer : IMaxSkillRanksComputer
    {
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private IFavoredClassData _favoredClassData;
        public MaxSkillRanksComputer(ISBCheckerBaseInput sbCheckerBaseInput, IFavoredClassData favoredClassData)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _favoredClassData = favoredClassData;
        }

        public int ComputeMaxSkillRanks()
        {
            if (_sbCheckerBaseInput.MonsterSB.GetAbilityScoreValue(StatBlockInfo.INT) == 0) return 0;

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("animal companion"))
            {
                int animalCompanionMod = 0;
                int animalCompanionLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("animal companion");

                switch (animalCompanionLevel)
                {
                    case 1:
                        animalCompanionMod = 2;
                        break;
                    case 2:
                    case 3:
                        animalCompanionMod = 3;
                        break;
                    case 4:
                        animalCompanionMod = 4;
                        break;
                    case 5:
                        animalCompanionMod = 5;
                        break;
                    case 6:
                    case 7:
                        animalCompanionMod = 6;
                        break;
                    case 8:
                        animalCompanionMod = 7;
                        break;
                    case 9:
                        animalCompanionMod = 8;
                        break;
                    case 10:
                    case 11:
                        animalCompanionMod = 9;
                        break;
                    case 12:
                        animalCompanionMod = 10;
                        break;
                    case 13:
                        animalCompanionMod = 11;
                        break;
                    case 14:
                    case 15:
                        animalCompanionMod = 12;
                        break;
                    case 16:
                        animalCompanionMod = 13;
                        break;
                    case 17:
                        animalCompanionMod = 14;
                        break;
                    case 18:
                    case 19:
                        animalCompanionMod = 15;
                        break;
                    case 20:
                        animalCompanionMod = 16;
                        break;
                }

                return animalCompanionMod;
            }

            int RaceMod = 0;
            int Level = 1;
            int Base = 1;
            int ClassSkills = 0;
            int RaceSkills = 0;
            int IntModHold = _sbCheckerBaseInput.AbilityScores.IntMod;

            if (_sbCheckerBaseInput.MonsterSB.Class.Length > 0)
            {
                foreach (string name in _sbCheckerBaseInput.CharacterClasses.GetClassNames())
                {
                    if (name == "Fighter" && _sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("tactician"))
                    {
                        Base = 4;
                    }
                    else if (name == "Warpriest" && _sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("cult leader"))
                    {
                        Base = 4;
                    }
                    else if (name == "Rogue" && _sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("skulking slayer"))
                    {
                        Base = 6;
                    }
                    else
                    {
                        Base = _sbCheckerBaseInput.CharacterClasses.GetSkillRanksPerLevel(name);
                    }
                    Level = _sbCheckerBaseInput.CharacterClasses.FindClassLevel(name);

                    if (Base + IntModHold <= 0) //min 1 skill point per level
                    {
                        IntModHold = 0;
                        Base = 1;
                    }
                    ClassSkills += Level * (Base + IntModHold);
                }
            }

            int TotalLevels = _sbCheckerBaseInput.CharacterClasses.FindTotalClassLevels();
            IntModHold = _sbCheckerBaseInput.AbilityScores.IntMod;

            if (UseHDForSkills())
            {
                string raceName = _sbCheckerBaseInput.MonsterSB.IsBestiary ? _sbCheckerBaseInput.MonsterSB.name.ToLower() : _sbCheckerBaseInput.MonsterSB.Race.ToLower();

                if (raceName == "gearsman" || raceName == "gearsman robot")
                {
                    Base = 4;
                    Level = _sbCheckerBaseInput.MonsterSB.HDValue() - TotalLevels;
                    int Bonus = _sbCheckerBaseInput.MonsterSB.HDValue();
                    RaceSkills = (Level * (Base + IntModHold)) + Bonus;
                }
                else
                {
                    Base = _sbCheckerBaseInput.CreatureType.SkillRanksPerLevel;

                    Level = _sbCheckerBaseInput.MonsterSB.HDValue() - TotalLevels;

                    if (Base + IntModHold <= 0) //min 1 skill point per HD
                    {
                        IntModHold = 0;
                        Base = 1;
                    }
                    RaceSkills = Level * (Base + IntModHold);
                }
            }

            if (_sbCheckerBaseInput.Race_Base != null)
            {
                if (_sbCheckerBaseInput.Race_Base.Name() == "Human")
                {
                    RaceMod = 1;
                }
            }


            int Max = ClassSkills + RaceSkills + (RaceMod * TotalLevels) + (_favoredClassData.FavoredClassLevels - _favoredClassData.FavoredClassHP > 0 ? _favoredClassData.FavoredClassLevels - _favoredClassData.FavoredClassHP : 0); //ranks
            string info = "Skills Ranks: " + Max + " = " + ClassSkills.ToString() + " class skills ";
            if (RaceSkills > 0)
            {
                info += "+" + RaceSkills.ToString() + " race skills ";
            }
            if (RaceMod > 0)
            {
                info += "+" + TotalLevels.ToString() + " race mod ";
            }
            if (_favoredClassData.FavoredClassLevels - _favoredClassData.FavoredClassHP > 0)
            {
                info += "+" + (_favoredClassData.FavoredClassLevels - _favoredClassData.FavoredClassHP).ToString() + " Favored class";
            }

            _sbCheckerBaseInput.MessageXML.AddInfo(info);

            return Max;
        }

        private bool UseHDForSkills()
        {
            if (_sbCheckerBaseInput.Race_Base.UseRacialHD) return true;

            if (_sbCheckerBaseInput.MonsterSB.Environment.Length > 0 && ((_sbCheckerBaseInput.CreatureType.Name == "Humanoid" && _sbCheckerBaseInput.CharacterClasses.FindTotalClassLevels() == 0) || _sbCheckerBaseInput.CreatureType.Name != "Humanoid"))
            {
                return true;
            }

            if (_sbCheckerBaseInput.CreatureType.Name != "Humanoid") return true;

            return false;
        }
    }
}
