using StatBlockChecker.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockChecker.Checkers
{
    public class FavoredClassChecker : IFavoredClassChecker
    {
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private IFavoredClassData _favoredClassData;
        private IHitDiceHitPointData _hitDiceHitPointData;

        public FavoredClassChecker(ISBCheckerBaseInput sbCheckerBaseInput, IFavoredClassData favoredClassData,
            IHitDiceHitPointData hitDiceHitPointData)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _favoredClassData = favoredClassData;
            _hitDiceHitPointData = hitDiceHitPointData;
        }

        public void CheckFavoredClassHP()
        {
            if (_sbCheckerBaseInput.MonsterSB.Class.Length == 0 || _sbCheckerBaseInput.CharacterClasses.HasClass("animal companion")) return;

            string CheckName = "CheckFavoredClassHP";
            int tempHDModifier = _hitDiceHitPointData.HDModifier;
            bool PassFavored = true;

            int rageModDiff;
            int rageHPDiff = 0;
            string ragepowers = _sbCheckerBaseInput.MonsterSBSearch.GetSpecialAttack("rage powers");
            if (!string.IsNullOrEmpty(ragepowers))
            {
                //remove rage hit points
                rageModDiff = _sbCheckerBaseInput.AbilityScores.ConBaseMod - _sbCheckerBaseInput.AbilityScores.ConMod;
                rageHPDiff = rageModDiff * _sbCheckerBaseInput.MonsterSB.HDValue();
            }

            _favoredClassData.FavoredClassHP = Math.Abs(_hitDiceHitPointData.MaxHPMod - tempHDModifier - rageHPDiff);// MaxHPMod - tempHDModifier; // Math.Abs(MaxHPMod - tempHDModifier);

            if (_hitDiceHitPointData.MaxHPMod < tempHDModifier && _hitDiceHitPointData.MaxFalseLife > 0)
            {
                _favoredClassData.FavoredClassHP -= _hitDiceHitPointData.FalseLife;
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFamiliar() && _sbCheckerBaseInput.MonsterSBSearch.FindFamiliarString(_sbCheckerBaseInput.CharacterClasses.HasClass("witch")) == "toad")
            {
                _favoredClassData.FavoredClassHP -= 3; // +3 hp for toad
            }


            if (_favoredClassData.FavoredClassHP > _sbCheckerBaseInput.CharacterClasses.FindAllClassLevels())
            {
                _favoredClassData.FavoredClassHP += _hitDiceHitPointData.MaxFalseLife;
                if (_favoredClassData.FavoredClassHP > _sbCheckerBaseInput.CharacterClasses.FindAllClassLevels())
                {
                    PassFavored = false;
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "FavoredClassHP  > Class HD: " + _favoredClassData.FavoredClassHP.ToString() + " > " + _sbCheckerBaseInput.CharacterClasses.FindAllClassLevels().ToString());
                }
            }
            if (_favoredClassData.FavoredClassHP > _favoredClassData.FavoredClassLevels)
            {
                PassFavored = false;
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Favored Class Points " + _favoredClassData.FavoredClassLevels.ToString() + " is less than Favored Class Points Devoted to HP " + _favoredClassData.FavoredClassHP.ToString());
            }
            if (PassFavored)
            {
                _hitDiceHitPointData.MaxHPMod += _favoredClassData.FavoredClassHP;
                _hitDiceHitPointData.MaxHPModFormula += " + " + _favoredClassData.FavoredClassHP.ToString() + " Favored Class HP";
            }

            if (_favoredClassData.FavoredClass.Length > 0)
            {
                string hold = _favoredClassData.FavoredClass;
                if (_favoredClassData.FavoredClass2nd.Length > 0)
                    hold += ", " + _favoredClassData.FavoredClass2nd;

                _sbCheckerBaseInput.MessageXML.AddInfo("Favored Class: " + hold);
                _sbCheckerBaseInput.MessageXML.AddInfo("Favored Class Points: " + _favoredClassData.FavoredClassLevels);
                _sbCheckerBaseInput.MessageXML.AddInfo("Favored Class Points Devoted to HP: " + _favoredClassData.FavoredClassHP);
            }
            if (PassFavored) _hitDiceHitPointData.HDModifier = _hitDiceHitPointData.MaxHPMod;

            if (_hitDiceHitPointData.MaxHPMod == tempHDModifier)
                _sbCheckerBaseInput.MessageXML.AddPass("HD Modifier Computed", _hitDiceHitPointData.MaxHPModFormula);
            else
                _sbCheckerBaseInput.MessageXML.AddFail("HD Modifier Computed", _hitDiceHitPointData.MaxHPMod.ToString(), tempHDModifier.ToString(), _hitDiceHitPointData.MaxHPModFormula);
        }
    }
}
