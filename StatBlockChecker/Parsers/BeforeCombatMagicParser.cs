using CommonInterFacesDD;
using EquipmentBasic;
using OnGoing;
using PathfinderGlobals;
using StatBlockBusiness;
using StatBlockCommon;
using StatBlockCommon.Individual_SB;
using StatBlockCommon.MagicItem_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace StatBlockChecker.Parsers
{
    public class BeforeCombatMagicParser : IBeforeCombatMagicParser
    {
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private ISpellsData _spellsData;
        private Dictionary<IEquipment, int> _equipementRoster;
        private int _onGoingAttackMod;
        private IArmorClassData _armorClassData;
        private ISpellStatBlockBusiness _spellStatBlockBusiness;
        private IMagicItemStatBlockBusiness _magicItemStatBlockBusiness;

        public BeforeCombatMagicParser(ISBCheckerBaseInput sbCheckerBaseInput, ISpellsData spellsData,
              Dictionary<IEquipment, int> equipementRoster, ref int onGoingAttackMod, IArmorClassData armorClassData, 
              ISpellStatBlockBusiness spellStatBlockBusiness, IMagicItemStatBlockBusiness magicItemStatBlockBusiness)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _spellsData = spellsData;
            _onGoingAttackMod = onGoingAttackMod;
            _equipementRoster = equipementRoster;
            _armorClassData = armorClassData;
            _spellStatBlockBusiness = spellStatBlockBusiness;
            _magicItemStatBlockBusiness = magicItemStatBlockBusiness;
        }

        public void ApplyBeforeCombatMagic()
        {
            if (!_spellsData.BeforeCombatMagic.Any()) return;
            ISpellStatBlock spell = null;
            MagicItemStatBlock magicItem = null;
            _sbCheckerBaseInput.IndvSB.HD = _sbCheckerBaseInput.MonsterSB.HD;

            List<IndividualStatBlock_Combat> list = new List<IndividualStatBlock_Combat> { _sbCheckerBaseInput.IndvSB };
            Dictionary<string, int> Overrides = new Dictionary<string, int>();
            bool found = false;
            _spellsData.MagicInEffect = new List<string>();

            foreach (string magic in _spellsData.BeforeCombatMagic)
            {
                string search = magic;
                bool Potion = false;
                bool Oil = false;
                Oil = magic.Contains("oil");
                Potion =  magic.Contains("potion");
                search = Utility.SearchMod(search);
                spell = _spellStatBlockBusiness.GetSpellByName(search);
                if (spell != null && !Potion && !Oil)
                {
                    try
                    {
                        int CL = FindSpellCasterLevel(magic);
                        if (magic == "effortless armor")
                        {
                            int ACPmod = 1;
                            if (CL >= 5) ACPmod++;
                            if (CL >= 10) ACPmod++;
                            if (CL >= 15) ACPmod++;
                            if (CL >= 20) ACPmod++;
                            _armorClassData.TotalArmorCheckPenalty += ACPmod;
                            _sbCheckerBaseInput.MessageXML.AddInfo("effortless armor mod " + ACPmod.ToString());
                        }
                        _sbCheckerBaseInput.IndvSB.CastSpell(spell.name, CL, list);
                        found = true;
                        _spellsData.MagicInEffect.Add(spell.name + " CL: " + CL.ToString());
                    }
                    catch (Exception ex)
                    {
                        _sbCheckerBaseInput.MessageXML.AddFail("ApplyBeforeCombatMagic", "ApplyBeforeCombatMagic: Issue with " + magic + "-- " + ex.Message);
                    }
                }
                else
                {
                    magicItem = _magicItemStatBlockBusiness.GetMagicItemByName(magic);
                    if (magicItem != null)
                    {
                        found = true;
                    }
                    else
                    {
                        if (spell != null)
                        {
                            int CL = 0;
                            try
                            {
                                found = UseMagicalEquipment(ref spell, list, Overrides, magic, ref CL);
                            }
                            catch (Exception ex)
                            {
                                _sbCheckerBaseInput.MessageXML.AddInfo("Before Combat Magic: Issue with " + magic + " --" + ex.Message);
                            }
                            if (found)
                            {
                                _spellsData.MagicInEffect.Add(spell.name + " CL: " + CL.ToString());
                            }
                        }
                    }
                }

                if (!found)
                {
                    _sbCheckerBaseInput.MessageXML.AddInfo("Before Combat Magic: Issue with " + magic);
                }

                found = false;
            }


            if (_sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModsCount() > 0)
            {
                List<OnGoingStatBlockModifier> mods = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockMods();

                if (Overrides.Any())
                {
                    foreach (KeyValuePair<string, int> kvp in Overrides)
                    {
                        for (int a = mods.Count - 1; a > 0; a--)
                        {
                            if (mods[a].Name.ToLower().Contains(kvp.Key.ToString()))
                            {
                                mods[a].Modifier = kvp.Value;
                            }
                        }
                    }
                }

                foreach (OnGoingStatBlockModifier mod in mods)
                {
                    //if (mod.ModType == OnGoingStatBlockModifier.StatBlockModifierTypes.AC)
                    //{
                    //    ACMods_Computed.AddModEffect(mod);
                    //}
                    if (mod.ModType == OnGoingStatBlockModifier.StatBlockModifierTypes.Attack)
                    {
                        _onGoingAttackMod += mod.Modifier;
                    }
                }
            }

            List<OnGoingStatBlockModifier> onGoingMods = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockMods();

            //remove all ability mods since they have been already incorperated into the SB and we don't want to add them twice
            for (int a = onGoingMods.Count - 1; a >= 0; a--)
            {
                if (onGoingMods[a].ModType == OnGoingStatBlockModifier.StatBlockModifierTypes.Ability) onGoingMods.RemoveAt(a);
            }

            if (_spellsData.MagicInEffect.Any())
            {
                string temp = string.Join(", ", _spellsData.MagicInEffect.ToArray());
                _sbCheckerBaseInput.MessageXML.AddInfo("Magic In Effect: " + temp);
            }
        }

        private bool UseMagicalEquipment(ref ISpellStatBlock spell,
             List<IndividualStatBlock_Combat> list, Dictionary<string, int> Overrides, string magic, ref int CL)
        {
            bool found = false;

            foreach (KeyValuePair<IEquipment, int> kvp in _equipementRoster)
            {
                if (kvp.Key.ToString() == magic)
                {
                    switch (kvp.Key.EquipmentType)
                    {
                        case EquipmentType.Potion:
                            Potion potion = (Potion)kvp.Key;

                            if (potion.ValueOverride > 0)
                            {
                                Overrides.Add(potion.PotionOf, potion.ValueOverride);
                            }
                            string temp = Utility.SearchMod(potion.PotionOf);
                            spell = _spellStatBlockBusiness.GetSpellByName(temp);
                            if (spell != null)
                            {
                                try
                                {
                                    CL = potion.PotionCasterLevel(spell.GetSpellLevel());
                                    _sbCheckerBaseInput.IndvSB.CastSpell(spell.name, CL, list);
                                    found = true;
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("UseMagicalEquipment--" + ex.Message);
                                }
                            }

                            break;
                        case EquipmentType.Wand:
                            Wand wand = (Wand)kvp.Key;

                            spell = _spellStatBlockBusiness.GetSpellByName(wand.WandOf);
                            if (spell != null)
                            {
                                _sbCheckerBaseInput.IndvSB.CastSpell(spell.name, FindSpellCasterLevel(wand.WandOf), list);
                                found = true;
                            }

                            break;
                        case EquipmentType.Scroll:
                            Scroll scroll = (Scroll)kvp.Key;

                            spell = _spellStatBlockBusiness.GetSpellByName(scroll.ScrollOf);
                            if (spell != null)
                            {
                                CL = FindSpellCasterLevel(scroll.ScrollOf);
                                _sbCheckerBaseInput.IndvSB.CastSpell(spell.name, CL, list);
                                found = true;
                            }

                            break;
                        case EquipmentType.Oil:
                            Oil oil = (Oil)kvp.Key;
                            string temp2 = Utility.SearchMod(oil.OilOf);
                            spell = _spellStatBlockBusiness.GetSpellByName(temp2);
                            if (spell != null)
                            {
                                CL = oil.CasterLevel;
                                _sbCheckerBaseInput.IndvSB.CastSpell(spell.name, CL, list);
                                found = true;
                            }
                            break;
                    }
                }
                if (found) return found;
            }
            return found;
        }

        private int FindSpellCasterLevel(string spell)
        {
            if (!_spellsData.ClassSpells.Any() && !_spellsData.SLA.Any()) return -1;
            SpellList tempList = null;

            if (spell.Contains("self only"))
            {
                int pos = spell.IndexOf(PathfinderConstants.PAREN_LEFT);
                spell = spell.Substring(0, pos - 1);
            }

            foreach (KeyValuePair<string, SpellList> kvp in _spellsData.ClassSpells)
            {
                tempList = kvp.Value;
                if (tempList.SpellExists(spell))
                {
                    return tempList.CasterLevel;
                }
            }

            foreach (KeyValuePair<string, SpellList> kvp in _spellsData.SLA)
            {
                tempList = kvp.Value;
                if (tempList.SpellExists(spell))
                {
                    return tempList.CasterLevel;
                }
            }

            return 0;
        }
    }
}
