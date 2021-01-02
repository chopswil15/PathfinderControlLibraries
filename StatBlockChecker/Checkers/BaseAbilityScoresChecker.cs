using CommonStatBlockInfo;
using OnGoing;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using System.Text;
using System.Threading.Tasks;
using PathfinderGlobals;

namespace StatBlockChecker.Checkers
{
    public class BaseAbilityScoresChecker : IBaseAbilityScoresChecker
    {
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private IArmorClassData _armorClassData;

        public BaseAbilityScoresChecker(ISBCheckerBaseInput sbCheckerBaseInput, IArmorClassData armorClassData)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _armorClassData = armorClassData;
        }

        public void CheckAbilityBaseScores(int hdModifier)
        {
            string CheckName = "Ability Base Scores";
            int origConMod = -10;
            int origHDMod = -100;
            if (_sbCheckerBaseInput.MonsterSB.BaseStatistics.Length == 0)
            {
                if (_sbCheckerBaseInput.CharacterClasses.HasClass("barbarian") && _armorClassData.ACMods_SB.Rage != 0)//must account for rage
                {
                    int holdScore = _sbCheckerBaseInput.MonsterSB.GetAbilityScoreValue(StatBlockInfo.STR);
                    _sbCheckerBaseInput.AbilityScores.UpdateAbilityScoreBaseValue(AbilityScores.AbilityScores.AbilityName.Strength, holdScore - 4);
                    holdScore = _sbCheckerBaseInput.MonsterSB.GetAbilityScoreValue(StatBlockInfo.CON);
                    _sbCheckerBaseInput.AbilityScores.UpdateAbilityScoreBaseValue(AbilityScores.AbilityScores.AbilityName.Constitution, holdScore - 4);
                    _sbCheckerBaseInput.MonsterSBSearch.UpdateAbilityScore(_sbCheckerBaseInput.AbilityScores);
                }
                return;
            }

            string BaseStatistics = _sbCheckerBaseInput.MonsterSB.BaseStatistics;
            List<string> AbilityNames = new List<string> { "Str ", "Dex ", "Con ", "Int ", "Wis ", "Cha " };
            int Pos = 0;
            string AbilityStartName = string.Empty;
            bool HasAC = BaseStatistics.Contains("AC ");

            foreach (string name in AbilityNames)
            {
                Pos = BaseStatistics.IndexOf(name);
                if (HasAC)
                {
                    int ACPos = BaseStatistics.IndexOf("AC ");
                    ACPos = BaseStatistics.IndexOf(PathfinderConstants.PAREN_RIGHT, ACPos);
                    if (ACPos != -1)
                    {
                        Pos = BaseStatistics.IndexOf(name, ACPos);
                    }
                }
                AbilityStartName = name;
                if (Pos >= 0) break;
            }

            if (Pos == -1) return; //no ability base statisitcs
            int Pos3 = BaseStatistics.IndexOf("hp ");
            //   if (Pos3 > Pos) return;
            int Pos2 = BaseStatistics.IndexOf(";", Pos);
            if (Pos2 == -1)
            {
                Pos2 = BaseStatistics.IndexOf(".", Pos);
            }
            if (Pos2 == -1)
            {
                Pos2 = BaseStatistics.Length;
            }

            List<string> BaseBlock = BaseStatistics.Substring(Pos, Pos2 - Pos).Split(',').ToList();

            int Str = _sbCheckerBaseInput.MonsterSB.GetAbilityScoreValue(StatBlockInfo.STR);
            int Int = _sbCheckerBaseInput.MonsterSB.GetAbilityScoreValue(StatBlockInfo.INT);
            int Wis = _sbCheckerBaseInput.MonsterSB.GetAbilityScoreValue(StatBlockInfo.WIS);
            int Dex = _sbCheckerBaseInput.MonsterSB.GetAbilityScoreValue(StatBlockInfo.DEX);
            int Con = _sbCheckerBaseInput.MonsterSB.GetAbilityScoreValue(StatBlockInfo.CON);
            int Cha = _sbCheckerBaseInput.MonsterSB.GetAbilityScoreValue(StatBlockInfo.CHA);

            int StrBase = Str;
            int IntBase = Int;
            int WisBase = Wis;
            int DexBase = Dex;
            int ConBase = Con;
            int ChaBase = Cha;

            string temp = string.Empty;
            int hold = 0;

            for (int a = 0; a <= BaseBlock.Count - 1; a++)
            {
                BaseBlock[a] = BaseBlock[a].Trim();
            }
            foreach (string ability in BaseBlock)
            {
                try
                {
                    Pos = ability.IndexOf(PathfinderConstants.SPACE);
                    temp = ability.Substring(0, Pos + 1);
                    if (!AbilityNames.Contains(temp)) return;
                    int.TryParse(ability.Replace(temp, string.Empty).Trim(), out hold);
                    if (hold > 0)
                    {
                        switch (temp.Trim())
                        {
                            case StatBlockInfo.STR:
                                StrBase = hold;
                                _sbCheckerBaseInput.AbilityScores.UpdateAbilityScoreBaseValue(AbilityScores.AbilityScores.AbilityName.Strength, hold);
                                break;
                            case StatBlockInfo.INT:
                                IntBase = hold;
                                _sbCheckerBaseInput.AbilityScores.UpdateAbilityScoreBaseValue(AbilityScores.AbilityScores.AbilityName.Intelligence, hold);
                                break;
                            case StatBlockInfo.WIS:
                                WisBase = hold;
                                _sbCheckerBaseInput.AbilityScores.UpdateAbilityScoreBaseValue(AbilityScores.AbilityScores.AbilityName.Wisdom, hold);
                                break;
                            case StatBlockInfo.DEX:
                                DexBase = hold;
                                _sbCheckerBaseInput.AbilityScores.UpdateAbilityScoreBaseValue(AbilityScores.AbilityScores.AbilityName.Dexterity, hold);
                                break;
                            case StatBlockInfo.CON:
                                ConBase = hold;
                                _sbCheckerBaseInput.AbilityScores.UpdateAbilityScoreBaseValue(AbilityScores.AbilityScores.AbilityName.Constitution, hold);
                                break;
                            case StatBlockInfo.CHA:
                                ChaBase = hold;
                                _sbCheckerBaseInput.AbilityScores.UpdateAbilityScoreBaseValue(AbilityScores.AbilityScores.AbilityName.Charisma, hold);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(CheckName + " Issue with ability base " + ability + " --" + ex.Message);
                }
            }

            _sbCheckerBaseInput.MonsterSBSearch.UpdateAbilityScore(_sbCheckerBaseInput.AbilityScores);
            origConMod = StatBlockInfo.GetAbilityModifier(ConBase);

            int HPBase = 0;
            try
            {
                Pos = BaseStatistics.IndexOf("hp ");
                if (Pos != -1)
                {
                    Pos2 = BaseStatistics.IndexOf(";", Pos);
                    if (Pos2 == -1) Pos2 = BaseStatistics.IndexOf(".", Pos);
                    if (Pos2 == -1) Pos2 = BaseStatistics.Length;
                    temp = BaseStatistics.Substring(Pos, Pos2 - Pos);
                    temp = temp.Replace("hp no temporary hit points", "0");
                    Pos = temp.IndexOf(",");
                    if (Pos != -1) temp = temp.Substring(0, Pos);
                    Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
                    if (Pos > 0) temp = temp.Substring(0, Pos).Trim();
                    temp = temp.Replace("each", string.Empty);
                    HPBase = int.Parse(temp.Replace("hp", string.Empty));
                    int HPDiff = int.Parse(_sbCheckerBaseInput.MonsterSB.HP) - HPBase;
                    origHDMod = hdModifier - HPDiff;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(CheckName + " Issue with hp --" + ex.Message);
            }

            int tempMod = 0;
            int RageMod = 0;
            int mutagenBonusStr = 0;
            int mutagenBonusDex = 0;
            int mutagenBonusCon = 0;
            int mutagenPenaltyInt = 0;  //Str
            int mutagenPenaltyWis = 0;  //Dex
            int mutagenPenaltyCha = 0; //Con

            if (_sbCheckerBaseInput.IndvSB != null)
                tempMod = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                          OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Str);

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("barbarian") && _armorClassData.ACMods_SB.Rage != 0) RageMod = 4;

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("alchemist"))  //mutagen mods
            {
                if ((StrBase - Str) == -4)
                {
                    mutagenBonusStr = 4;
                    mutagenPenaltyInt = -2;
                }
                if ((DexBase - Dex) == -4)
                {
                    mutagenBonusDex = 4;
                    mutagenPenaltyWis = -2;
                }
                if ((ConBase - Con) == -4)
                {
                    mutagenBonusCon = 4;
                    mutagenPenaltyCha = -2;
                }
            }

            if (Math.Abs(StrBase + RageMod - Str + mutagenBonusStr) != tempMod)
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName + " Str", (StrBase + tempMod).ToString(), (Str).ToString());

            tempMod = 0;
            if (_sbCheckerBaseInput.IndvSB != null)
                tempMod = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                          OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Int);

            if (Math.Abs(IntBase - Int + mutagenPenaltyInt) != tempMod)
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName + " Int", (IntBase + tempMod).ToString(), (Int).ToString());


            tempMod = 0;
            if (_sbCheckerBaseInput.IndvSB != null)
                tempMod = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                          OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Wis);

            if (Math.Abs(WisBase - Wis + mutagenPenaltyWis) != tempMod)
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName + " Wis", (WisBase + tempMod).ToString(), (Wis).ToString());

            tempMod = 0;
            if (_sbCheckerBaseInput.IndvSB != null)
                tempMod = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                          OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Dex);

            if (Math.Abs(DexBase - Dex + mutagenBonusDex) != tempMod)
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName + " Dex", (DexBase + tempMod).ToString(), (Dex).ToString());

            tempMod = 0;
            if (_sbCheckerBaseInput.IndvSB != null)
                tempMod = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                         OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Con);

            if (Math.Abs(ConBase + RageMod - Con + mutagenBonusCon) != tempMod)
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName + " Con", (ConBase + tempMod).ToString(), (Con).ToString());

            tempMod = 0;
            if (_sbCheckerBaseInput.IndvSB != null)
                tempMod = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.Ability,
                         OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Cha);

            if (Math.Abs(ChaBase - Cha + mutagenPenaltyCha) != tempMod)
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName + " Cha", (ChaBase + tempMod).ToString(), (Cha).ToString());
        }
    }
}
