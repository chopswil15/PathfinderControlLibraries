using CommonInterFacesDD;
using EquipmentBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using System.Text;
using System.Threading.Tasks;
using PathfinderGlobals;

namespace StatBlockChecker
{
    public class TotalArmorCheckPenaltyComputer
    {
        private SBCheckerBaseInput _sbCheckerBaseInput;
        private Dictionary<IEquipment, int> _armor;
        private SpellsData _spellsData;
        private ArmorClassData _armorClassData;

        public TotalArmorCheckPenaltyComputer(SBCheckerBaseInput sbCheckerBaseInput, Dictionary<IEquipment, int> armor,
                SpellsData spellsData, ArmorClassData armorClassData)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _armor = armor;
            _spellsData = spellsData;
            _armorClassData = armorClassData;
        }


        public void ComputeTotalArmorCheckPenalty()
        {
            _armorClassData.TotalArmorCheckPenalty = 0;
             _armorClassData.MaxDexMod = 0;
            Armor armor;
            string ACP_formula = string.Empty;

            foreach (KeyValuePair<IEquipment, int> kvp in _armor)
            {
                IEquipment hold = kvp.Key;
                if (hold.EquipmentType == EquipmentType.Armor) //shields are armor
                {
                    armor = (Armor)hold;
                    if (armor.armor_check_penalty.HasValue)
                    {
                        _armorClassData.TotalArmorCheckPenalty += Convert.ToInt32(armor.armor_check_penalty);

                        //if (armor.Masterwork)
                        //{
                        //    //ACP_formula += PathfinderConstants.SPACE + (armor.armor_check_penalty + 1).ToString() + " mwk " + armor.name;
                        //}
                        //else
                        {
                            ACP_formula += PathfinderConstants.SPACE + armor.armor_check_penalty.ToString() + PathfinderConstants.SPACE + armor.name;
                        }
                        if (armor.Masterwork && Convert.ToInt32(armor.armor_check_penalty) < 0 && armor.name != "elven chain" && !armor.Mithral)
                        {
                            _armorClassData.TotalArmorCheckPenalty++;
                            ACP_formula += " +1 masterwork (" + armor.name + PathfinderConstants.PAREN_RIGHT;
                        }
                         _armorClassData.MaxDexMod += Convert.ToInt32(armor.max_dex_bonus);
                    }
                }
            }
            // 0 means  _armorClassData.MaxDexMod will not be used, so a high number like 50 is used so it will be ignored
            if ( _armorClassData.MaxDexMod == 0)  _armorClassData.MaxDexMod = 50;

            int armorTraining = 0;
            if (_sbCheckerBaseInput.CharacterClasses.HasClass("fighter"))
            {
                //Armor Training
                int level = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("fighter");
                if (level >= 3)
                {
                    armorTraining++;
                    if ( _armorClassData.MaxDexMod > -1)
                    {
                         _armorClassData.MaxDexMod++;
                    }
                }
                if (level >= 7)
                {
                    armorTraining++;
                    if ( _armorClassData.MaxDexMod > -1)
                    {
                         _armorClassData.MaxDexMod++;
                    }
                }
                if (level >= 11)
                {
                    armorTraining++;
                    if ( _armorClassData.MaxDexMod > -1)
                    {
                         _armorClassData.MaxDexMod++;
                    }
                }
                if (level >= 15)
                {
                    armorTraining++;
                    if ( _armorClassData.MaxDexMod > -1)
                    {
                         _armorClassData.MaxDexMod++;
                    }
                }
                _armorClassData.TotalArmorCheckPenalty += armorTraining;
                ACP_formula += " +" + armorTraining.ToString() + " armor training";
            }
            else
            {
                if (_sbCheckerBaseInput.MonsterSB.SQ.Contains("armor training"))
                {
                    if (_sbCheckerBaseInput.MonsterSB.SQ.Contains(","))
                    {
                        List<string> temp = _sbCheckerBaseInput.MonsterSB.SQ.Split(',').ToList();
                    }
                    else
                    {
                        int AT = int.Parse(_sbCheckerBaseInput.MonsterSB.SQ.Replace("armor training", string.Empty));
                        _armorClassData.TotalArmorCheckPenalty += AT;
                        ACP_formula += " +" + AT.ToString() + " armor training";
                    }
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasTrait("Armor Expert"))
            {
                _armorClassData.TotalArmorCheckPenalty += 1;
                ACP_formula += " +1 Armor Expert";
            }

            if (_spellsData.BeforeCombatMagic.Contains("effortless armor")) ACP_formula += " Effortless Armor";

            if (_armorClassData.TotalArmorCheckPenalty > 0) _armorClassData.TotalArmorCheckPenalty = 0;

            _sbCheckerBaseInput.MessageXML.AddInfo("ACP " + _armorClassData.TotalArmorCheckPenalty.ToString() + " = " + ACP_formula);
        }
    }
}
