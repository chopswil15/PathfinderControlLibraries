using StatBlockChecker.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using System.Text;
using System.Threading.Tasks;
using PathfinderGlobals;

namespace StatBlockChecker.Checkers
{
    public class HDModifierChecker : IHDModifierChecker
    {
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private IFavoredClassData _favoredClassData;
        private IHitDiceHitPointData _hitDiceHitPointData;
        private AbilityScores.AbilityScores _abilityScores;

        public HDModifierChecker(ISBCheckerBaseInput sbCheckerBaseInput, IFavoredClassData favoredClassData,
            IHitDiceHitPointData hitDiceHitPointData)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _favoredClassData = favoredClassData;
            _hitDiceHitPointData = hitDiceHitPointData;
            _abilityScores = _sbCheckerBaseInput.AbilityScores;
        }

        public void CheckHDModifier()
        {
            string CheckName = "HD Modifier";
            int modUsed = _abilityScores.ConMod; //_abilityScores.ConBaseMod; Bear's Endurance use new ConMod?
            int tempHDModifier = _hitDiceHitPointData.HDModifier;

            if (_sbCheckerBaseInput.MonsterSBSearch.HasTemplate("ghost"))
            {
                modUsed = _abilityScores.ChaMod;
            }
            else if (_sbCheckerBaseInput.MonsterSB.Type.Contains("undead"))
            {
                modUsed = _abilityScores.ChaMod;
            }


            //if (OrigConMod != -10)
            //{
            //    modUsed = OrigConMod;
            //}

            _hitDiceHitPointData.MaxHPMod = (modUsed * _hitDiceHitPointData.TotalHD);
            _hitDiceHitPointData.MaxHPModFormula = " + " + _hitDiceHitPointData.MaxHPMod.ToString() + " (" + modUsed.ToString() + " ability mod * " + _hitDiceHitPointData.TotalHD.ToString() + " HD)";

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Toughness"))
            {
                int ToughnessMod = 0;
                if (_hitDiceHitPointData.TotalHD <= 3)
                    ToughnessMod = 3;
                else
                    ToughnessMod += _hitDiceHitPointData.TotalHD;

                _hitDiceHitPointData.MaxHPModFormula += " +" + ToughnessMod.ToString() + " Toughness";
                _hitDiceHitPointData.MaxHPMod += ToughnessMod;
            }

            _hitDiceHitPointData.MaxFalseLife = 0;
            _hitDiceHitPointData.FalseLife = 0;
            foreach (string effect in _sbCheckerBaseInput.MagicInEffect)
            {
                if (effect.Contains("False Life"))
                {
                    try
                    {
                        FalseLifeHitPoints(effect);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(CheckName + "--False Life effect:" + effect + "  " + ex.Message);
                    }

                    break;
                }
            }

            _hitDiceHitPointData.MaxHPModFormula = _hitDiceHitPointData.MaxHPMod.ToString() + " = " + _hitDiceHitPointData.MaxHPModFormula;
        }

        private void FalseLifeHitPoints(string effect)
        {
            bool greater = effect.Contains("Greater");
            string temp2 = effect.Replace("False Life, Greater", string.Empty);
            temp2 = temp2.Replace("False Life", string.Empty);
            temp2 = temp2.Replace("CL:", string.Empty).Trim();
            int CL = int.Parse(temp2);
            if (greater)
            {
                _hitDiceHitPointData.MaxFalseLife = 20;
            }
            else
            {
                _hitDiceHitPointData.MaxFalseLife = 10; //1d10 + CL but flase life has max of 10
            }
            _hitDiceHitPointData.MaxHPModFormula += " +" + _hitDiceHitPointData.MaxFalseLife.ToString() + " MaxFalseLife";
            _sbCheckerBaseInput.MessageXML.AddInfo("False Life: Max Value " + _hitDiceHitPointData.MaxFalseLife);

            string baseStat = _sbCheckerBaseInput.MonsterSB.BaseStatistics;
            int Pos = baseStat.IndexOf("hp ");
            if (Pos >= 0)
            {
                temp2 = baseStat.Substring(Pos);
                temp2 = temp2.Replace("hp ", string.Empty);
                if (temp2.Contains(";"))
                {
                    Pos = temp2.IndexOf(";");
                    temp2 = temp2.Substring(0, Pos);
                }
                if (temp2.Contains(PathfinderConstants.PAREN_LEFT))
                {
                    Pos = temp2.IndexOf(PathfinderConstants.PAREN_LEFT);
                    temp2 = temp2.Substring(0, Pos);
                }
                temp2 = temp2.Replace(".", string.Empty);
                int holdHP = int.Parse(temp2);
                _hitDiceHitPointData.FalseLife = int.Parse(_sbCheckerBaseInput.MonsterSB.HP) - holdHP;
            }
        }
    }
}
