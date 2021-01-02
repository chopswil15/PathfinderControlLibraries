using System;
using System.Collections.Generic;
using System.Linq;

using CommonInterFacesDD;
using OnGoing;
using StatBlockCommon;
using Utilities;

using StatBlockCommon.Monster_SB;
using PathfinderDomains;
using PathfinderGlobals;
using StatBlockBusiness;

namespace StatBlockChecker
{
    public class MonSBSearch : IMonSBSearch
    {
        private List<string> Feats;
        private List<string> MythicFeats;
        private Dictionary<string, int> FeatCount;
        private List<string> Skills;
        private List<string> Traits;
        private List<string> Hexes;
        private List<string> SpecialAttacks;
        private List<string> SQ;
        private List<string> Archetypes;
        private string _specialAttacks;
        private string _defensiveAbilities;
        private string _SQ;
        private string _gear;
        private string _domains;
        private string _templatesApplied;
        private Dictionary<string, int> _WT;
        private Dictionary<IEquipment, int> _armor;
        private string _classArchetypes;
        private List<OnGoingStatBlockModifier> _onGoingModifers;
        private string _ragePowers;
        private string _subType;
        private string _type;
        private AbilityScores.AbilityScores _abilityValues;
        private string _race;
        private string _bloodline;
        private string _mystery;
        private bool _onlyClassHitdice;
        private bool _isMythic;
        private int _mythicRank;
        private int _mythicTier;
        private string _partron;
        private MonsterStatBlock _MonSB;
        private bool _isBestirarySB;
        private bool _featsHaveParens;
        private IFeatStatBlockBusiness _featStatBlockBusiness;

        public Dictionary<IEquipment, int> Armor
        {
            set
            {
                _armor = value;
            }
        }

        public string Name { get; private set; }

        public MonSBSearch(MonsterStatBlock MonSB, Dictionary<string, int> WT,
               Dictionary<IEquipment, int> Armor, List<OnGoingStatBlockModifier> OnGoingModifers, IFeatStatBlockBusiness featStatBlockBusiness)
        {
            _featStatBlockBusiness = featStatBlockBusiness;
            ParseFeats(MonSB.Feats);
            ParseTraits(MonSB.Traits);
            ParseSkills(MonSB.Skills);
            ParseSpecialAttacks(MonSB.SpecialAttacks);
            ParseArchetypes(MonSB.ClassArchetypes);

            _specialAttacks = MonSB.SpecialAttacks;
            ParseHexes();

            _defensiveAbilities = MonSB.DefensiveAbilities;
            _WT = WT;
            _gear = MonSB.Gear + ", " + MonSB.OtherGear;
            _templatesApplied = MonSB.TemplatesApplied;
            _armor = Armor;
            _classArchetypes = MonSB.ClassArchetypes.ToLower();
            _SQ = MonSB.SQ;
            Utility.ParenCommaFix(ref _SQ);
            SQ = _SQ.Split(',').ToList();
            _onGoingModifers = OnGoingModifers;
            _domains = MonSB.SpellDomains;
            _subType = MonSB.SubType;
            _type = MonSB.Type;
            _abilityValues = new AbilityScores.AbilityScores(MonSB.AbilityScores);
            _race = MonSB.Race;
            _bloodline = MonSB.Bloodline;
            _mystery = MonSB.Mystery.Replace("*", string.Empty);
            int Pos = _mystery.IndexOf(PathfinderConstants.PAREN_LEFT);
            if (Pos > 0)
            {
                _mystery = _mystery.Substring(0, Pos).Trim();
            }


            _bloodline = Utility.RemoveSuperScripts(_bloodline);
            _mystery = Utility.RemoveSuperScripts(_mystery);


            _bloodline = _bloodline.ToLower();
            _onlyClassHitdice = !MonSB.DontUseRacialHD;
            _isMythic = MonSB.Mythic;
            _mythicRank = MonSB.MR;
            _mythicTier = MonSB.MT;
            _partron = MonSB.Patron;
            Name = MonSB.name;
            _isBestirarySB = MonSB.Environment.Length > 0 ? true : false;
            this._MonSB = MonSB;
        }

        public MonsterStatBlock MonSB
        {
            get
            {
                return _MonSB;
            }
        }

        public bool IsBestirarySB
        {
            get
            {
                return _isBestirarySB;
            }
        }

        public bool IsMythic
        {
            get
            {
                return _isMythic;
            }
        }

        public int MythicRank
        {
            get
            {
                return _mythicRank;
            }
        }

        public int MythicTier
        {
            get
            {
                return _mythicTier;
            }
        }

        public int MythicValue
        {
            get
            {
                int value = _mythicTier;
                if (value == 0) value = _mythicRank;
                return value;
            }
        }

        public string Bloodline
        {
            get
            {
                return _bloodline;
            }
        }

        public string Mystery
        {
            get
            {
                return _mystery;
            }
        }

        #region private

        private void ParseArchetypes(string archetypes)
        {
            Archetypes = archetypes.Split(',').ToList();

            for (int a = 0; a <= Archetypes.Count() - 1; a++)
            {
                Archetypes[a] = Utility.RemoveSuperScripts(Archetypes[a]);
            }
        }

        private void ParseSkills(string skills)
        {
            Skills = skills.Split(',').ToList();
        }

        private void ParseHexes()
        {
            Hexes = new List<string>();
            if (!_specialAttacks.Contains("hexes")) return;

            string hold = string.Empty;
            foreach (string attack in SpecialAttacks)
            {
                if (attack.Contains("hex"))
                {
                    hold = attack;
                    break;
                }
            }
            hold = hold.Replace("|", ",");
            hold = hold.Replace("hexes", string.Empty);
            hold = Utility.RemoveParentheses(hold);
            BracketCommaFix(ref hold);
            Hexes = hold.Split(',').ToList();
        }

        private void ParseSpecialAttacks(string specialAttacks)
        {
            ParenCommaFix(ref specialAttacks);
            SpecialAttacks = specialAttacks.Replace("*", string.Empty).Split(',').ToList();
            SpecialAttacks.RemoveAll(x => x== string.Empty);
            _ragePowers = string.Empty;

            int Count = SpecialAttacks.Count - 1;
            for (int a = Count; a >= 0; a--)
            {
                SpecialAttacks[a] = SpecialAttacks[a].Trim();
                if (SpecialAttacks[a].IndexOf("rage power") >= 0)
                {
                    ParseRagePowers(SpecialAttacks[a]);
                }
            }
        }

        private void ParseRagePowers(string ragePowers)
        {
            ragePowers = ragePowers.Replace("rage powers (", string.Empty);
            _ragePowers = ragePowers.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Trim();
        }

        private void ParenCommaFix(ref string Block)
        {
            int LeftParenPos = Block.IndexOf(PathfinderConstants.PAREN_LEFT);
            if (LeftParenPos == -1) return;
            int RightParenPos = Block.IndexOf(PathfinderConstants.PAREN_RIGHT);
            int CommaPos = Block.IndexOf(",", LeftParenPos);


            string temp = Block;
            string hold = string.Empty;
            while (LeftParenPos >= 0)
            {
                if ((CommaPos > LeftParenPos) && (CommaPos < RightParenPos))
                {
                    temp = Block.Substring(LeftParenPos, RightParenPos - LeftParenPos);
                    hold = temp.Replace(",", "|");
                    Block = Block.Replace(temp, hold);
                }
                LeftParenPos = Block.IndexOf(PathfinderConstants.PAREN_LEFT, LeftParenPos + 1);

                if (LeftParenPos >= 0)
                {
                    RightParenPos = Block.IndexOf(PathfinderConstants.PAREN_RIGHT, LeftParenPos);
                    CommaPos = Block.IndexOf(",", LeftParenPos);
                }
            }
        }

        private void BracketCommaFix(ref string Block)
        {
            int LeftParenPos = Block.IndexOf("[");
            if (LeftParenPos == -1) return;
            int RightParenPos = Block.IndexOf("]");
            int CommaPos = Block.IndexOf(",", LeftParenPos);


            string temp = Block;
            string hold = string.Empty;
            while (LeftParenPos >= 0)
            {
                if ((CommaPos > LeftParenPos) && (CommaPos < RightParenPos))
                {
                    temp = Block.Substring(LeftParenPos, RightParenPos - LeftParenPos);
                    hold = temp.Replace(",", "|");
                    Block = Block.Replace(temp, hold);
                }
                LeftParenPos = Block.IndexOf("[", LeftParenPos + 1);

                if (LeftParenPos >= 0)
                {
                    RightParenPos = Block.IndexOf("]", LeftParenPos);
                    CommaPos = Block.IndexOf(",", LeftParenPos);
                }
            }
        }


        private void ParseFeats(string feats)
        {
            MythicFeats = new List<string>();
            Feats = feats.Replace("*", string.Empty).Split(',').ToList();
            Feats.RemoveAll(x => x== string.Empty);
            Feats.Remove(PathfinderConstants.SPACE);

            FeatCount = new Dictionary<string, int>();

            int Count = Feats.Count - 1;
            for (int a = Count; a >= 0; a--)
            {
                if (Feats[a].EndsWith("B")) Feats[a] = Feats[a].Substring(0, Feats[a].Length - 1);

                if (Feats[a].Contains(PathfinderConstants.PAREN_LEFT))
                {
                    _featsHaveParens = true;
                    int Pos = Feats[a].IndexOf(PathfinderConstants.PAREN_LEFT);
                    string temp = Feats[a].Substring(Pos);
                    temp = Utility.RemoveParentheses(temp);
                    int value;
                    int.TryParse(temp, out value);
                    if (value > 0)
                    {
                        Feats[a] = Feats[a].Replace(temp, string.Empty);
                        temp = Utility.RemoveParentheses(temp);
                        Feats[a] = Utility.RemoveParentheses(Feats[a]);
                        Feats[a] = Utility.RemoveSuperScripts(Feats[a]);
                        FeatCount.Add(Feats[a].Trim(), value);
                    }
                    else if (temp.Contains("see "))
                    {
                        Feats[a] = Feats[a].Replace(temp, string.Empty);
                    }
                }

                Feats[a] = Utility.RemoveSuperScripts(Feats[a]);
                if (Feats[a].EndsWith("[M]") || Feats[a].Contains("[M] "))
                {
                    string temp = Feats[a].Replace("[M] ", PathfinderConstants.SPACE);
                    if (temp.EndsWith("[M]"))
                    {
                        temp = temp.Substring(0, temp.Length - 3);
                    }
                    temp = temp.Trim();
                    string tempOld = temp;
                    MythicFeats.Add(temp);
                    if (temp.Contains(PathfinderConstants.PAREN_LEFT))
                    {
                        _featsHaveParens = true;
                        int Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
                        temp = temp.Substring(0, Pos).Trim();
                    }
                    IFeatStatBlock tempFeat = _featStatBlockBusiness.GetMythicFeatByName(temp);
                    if (tempFeat != null && tempFeat.prerequisite_feats == temp)
                        Feats[a] = tempOld;
                    else
                        Feats[a] = string.Empty;

                    Feats.RemoveAll(x => x== string.Empty);
                }
                else
                    Feats[a] = Feats[a].Trim();

            }
        }

        private void ParseTraits(string traits)
        {
            if (string.IsNullOrEmpty(traits))
            {
                Traits = new List<string>();
                return;
            }
            Traits = traits.Replace("*", string.Empty).Split(',').ToList();

            Traits.RemoveAll(x => x== string.Empty);
            Traits.Remove(PathfinderConstants.SPACE);

            int Count = Traits.Count - 1;

            for (int a = Count; a >= 0; a--)
            {
                Traits[a] = Utility.RemoveSuperScripts(Traits[a]).ToLower();
            }
        }

        #endregion private

        public string Race()
        {
            return _race;
        }

        public void UpdateAbilityScore(AbilityScores.AbilityScores AbilityValues)
        {
            _abilityValues = AbilityValues;
        }

        public bool HasOnlyClassHitdice()
        {
            return _onlyClassHitdice;
        }

        public bool HasArchetype(string Archetype)
        {
            return Archetypes.Contains(Archetype.ToLower());
        }

        public List<string> GetArchetypes()
        {
            return Archetypes;
        }

        public int GetAbilityScoreValue(AbilityScores.AbilityScores.AbilityName abilityName)
        {
            return _abilityValues.GetAbilityScoreValue(abilityName);
        }

        public bool HasCavalierOrder(string Order)
        {
            return HasSQ(Order);
        }

        public bool HasPatron()
        {
            return _partron.Length > 0 ? true : false;
        }

        public int GetAbilityMod(AbilityScores.AbilityScores.AbilityName abilityName)
        {
            switch (abilityName)
            {
                case AbilityScores.AbilityScores.AbilityName.Charisma:
                    return _abilityValues.ChaMod;
                case AbilityScores.AbilityScores.AbilityName.Constitution:
                    return _abilityValues.ConMod;
                case AbilityScores.AbilityScores.AbilityName.Dexterity:
                    return _abilityValues.DexMod;
                case AbilityScores.AbilityScores.AbilityName.Intelligence:
                    return _abilityValues.IntMod;
                case AbilityScores.AbilityScores.AbilityName.Strength:
                    return _abilityValues.StrMod;
                case AbilityScores.AbilityScores.AbilityName.Wisdom:
                    return _abilityValues.WisMod;
                default:
                    return -100;
            }
        }

        public int GetBaseAbilityMod(AbilityScores.AbilityScores.AbilityName abilityName)
        {
            switch (abilityName)
            {
                case AbilityScores.AbilityScores.AbilityName.Charisma:
                    return _abilityValues.ChaBaseMod;
                case AbilityScores.AbilityScores.AbilityName.Constitution:
                    return _abilityValues.ConBaseMod;
                case AbilityScores.AbilityScores.AbilityName.Dexterity:
                    return _abilityValues.DexBaseMod;
                case AbilityScores.AbilityScores.AbilityName.Intelligence:
                    return _abilityValues.IntBaseMod;
                case AbilityScores.AbilityScores.AbilityName.Strength:
                    return _abilityValues.StrBaseMod;
                case AbilityScores.AbilityScores.AbilityName.Wisdom:
                    return _abilityValues.WisBaseMod;
                default:
                    return -100;
            }
        }

        public bool HasSubType(string subType)
        {
            return _subType.Contains(subType);
        }


        public bool HasType(string type)
        {
            return _type.Contains(type);
        }

        public bool HasSpellDomians()
        {
            return _domains.Length > 0 ? true : false;
        }

        public bool HasSpellDomain(string domain)
        {
            return _domains.Contains(domain);
        }

        public bool HasBloodline(string bloodlineName)
        {
            return _bloodline.Contains(bloodlineName.ToLower());
        }

        public bool HasBloodline()
        {
            return _bloodline.Length > 0;
        }

        public bool HasMystery()
        {
            return _mystery.Length > 0;
        }

        public bool HasMystery(string mysteryName)
        {
            return _mystery.Contains(mysteryName);
        }

        public bool HasFamiliar()
        {
            if (_SQ.Contains("familiar)")) return true;
            if (_SQ.Contains("familiar (")) return true;
            if (_SQ.Contains("familiar tatto")) return true;
            if (_SQ.Contains("familiar") && !_SQ.Contains("familiarity")) return true;
            if (_SQ.Contains("arcane bond"))
            {
                string bond = GetSQ("arcane bond");
                bond = bond.Replace("arcane bond", string.Empty);
                int Pos = bond.IndexOf("named");
                if (Pos > 0) bond = bond.Substring(0, Pos);
                bond = Utility.RemoveParentheses(bond);
                List<string> familarList = CommonMethods.GetWizardFamiliarList();
                if (familarList.Contains(bond)) return true;
            }

            return false;
        }

        public string FindFamiliarString(bool HasWitchClass)
        {
            if (HasFamiliar())
            {
                string familairString = string.Empty;
                foreach (string sqItem in SQ)
                {
                    if (sqItem.Contains("familiar") && !sqItem.Contains("familiarity") && !sqItem.Contains("mythic familiar") && !sqItem.Contains("scry on familiar"))
                    {
                        familairString = sqItem;
                        break;
                    }
                }

                if (familairString.Length == 0)
                {
                    if (_SQ.Contains("arcane bond"))
                    {
                        string bond = GetSQ("arcane bond");
                        bond = bond.Replace("arcane bond", string.Empty);
                        int Pos = bond.IndexOf("named");
                        if (Pos > 0) bond = bond.Substring(0, Pos);
                        bond = Utility.RemoveParentheses(bond);
                        List<string> familarList = CommonMethods.GetWizardFamiliarList();
                        if (familarList.Contains(bond))
                            familairString = bond;
                    }
                }

                List<string> familiars = HasWitchClass ? CommonMethods.GetWitchFamiliarList() : CommonMethods.GetWizardFamiliarList();

                foreach (string familiar in familiars)
                {
                    if (familairString.Contains(familiar)) return familiar;
                }
            }

            return string.Empty;
        }

        public int SkillValue(string skillName)
        {
            int value = -100;
            foreach (string skill in Skills)
            {
                if (skill.IndexOf(skillName) >= 0)
                {
                    string temp = skill.Replace(skillName, string.Empty);
                    int Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
                    if (Pos >= 0)
                    {
                        temp = temp.Substring(0, Pos);
                    }
                    int.TryParse(temp, out value);
                    return value;
                }
            }

            return value;
        }

        public bool HasSkill(string skillName)
        {
            foreach (string skill in Skills)
            {
                if (skill.Contains(skillName))
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasCompanion()
        {
            string temp = GetSQ("companion");
            if (temp.Length > 0) return true;
            return false;
        }

        public bool HasDomain()
        {
            if (_domains.Length > 0) return true;
            return false;
        }

        public bool HasDomain(string domainName)
        {
            if (_domains.Contains(domainName)) return true;
            return false;
        }

        public bool HasTemplate(string TemplateName)
        {
            if (_templatesApplied.Length == 0) return false;
            if (_templatesApplied.Contains(TemplateName.ToLower())) return true;
            return false;
        }

        public int TemplateCount()
        {
            if (_templatesApplied.Length == 0) return 0;
            List<string> count = _templatesApplied.Split('|').ToList();
            return count.Count;
        }

        public bool HasHex(string hexString)
        {
            foreach (string hex in Hexes)
            {
                if (hex.Contains(hexString)) return true;
            }
            return false;
        }

        public bool HasAnyClassArchetypes()
        {
            return !string.IsNullOrEmpty(_classArchetypes);
        }

        public bool HasClassArchetype(string ArchetypeName)
        {
            return _classArchetypes.Contains(ArchetypeName.ToLower());
        }

        public int GetWeaponsTrainingModifier(string weaponName, ref string formula)
        {
            int mod = 0;
            weaponName = weaponName.ToLower();
            Dictionary<string, int> pairs = new Dictionary<string, int>();
            string tempFormula = string.Empty;

            foreach (KeyValuePair<string, int> kvp in _WT)
            {
                if (kvp.Key.IndexOf(weaponName) >= 0)
                {
                    pairs.Add(" +" + kvp.Value.ToString() + " weapon training", kvp.Value);
                    //mod += kvp.Value;
                    tempFormula = " +" + kvp.Value.ToString() + " weapon training";
                }
            }

            if (pairs.Count == 1)
            {
                formula += tempFormula;
                return pairs.FirstOrDefault().Value;
            }

            int max = 0;
            string hold = string.Empty;
            foreach (KeyValuePair<string, int> kvp in pairs)
            {
                if (kvp.Value > max)
                {
                    max = kvp.Value;
                    hold = kvp.Key;
                }
            }
            mod += max;
            formula += hold;
            return mod;
        }

        public bool HasShield()
        {
            foreach (KeyValuePair<IEquipment, int> kvp in _armor)
            {
                IEquipment hold = kvp.Key;
                armor armorTemp = hold as armor;
                if (armorTemp != null)
                {
                    if (armorTemp.category == "shield")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool HasSQ(string sq)
        {
            return _SQ.Contains(sq);
        }

        public bool HasDefensiveAbility(string defensiveAbility)
        {
            if (_defensiveAbilities.Contains(defensiveAbility)) return true;
            return false;
        }

        public bool HasSpecialAttackGeneral(string specialAttackName)
        {
            if (_specialAttacks.Contains(specialAttackName)) return true;
            return false;
        }

        public bool HasRagePower(string ragePower)
        {
            if (_ragePowers.Contains(ragePower)) return true;
            return false;
        }

        public bool HasSpecialAttack(string specialAttackName)
        {
            foreach (string SA in SpecialAttacks)
            {
                if (SA.Contains(specialAttackName)) return true;
            }

            return false;
        }

        public bool HasDeed(string deedName)
        {
            if (HasSpecialAttack(deedName)) return true;
            if (HasSQ(deedName)) return true;
            return false;
        }

        public List<string> GetDeeds()
        {
            string SBDeeds = GetSQ("deeds");
            if (SBDeeds.Length == 0)
            {
                SBDeeds = GetSpecialAttack("deeds");
            }
            SBDeeds = SBDeeds.Replace("deeds (", string.Empty);
            SBDeeds = SBDeeds.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Trim();

            return SBDeeds.Split('|').Select(s => s.Trim()).ToList();
        }

        public string GetRagePower(string ragePower)
        {
            List<string> ragePowerList = _ragePowers.Split('|').ToList();
            return ragePowerList.Where(x => x.Contains(ragePower)).FirstOrDefault();
        }

        public string GetSpecialAttack(string specialAttackName)
        {
            foreach (string SA in SpecialAttacks)
            {
                if (SA.Contains(specialAttackName) && SA.IndexOf(specialAttackName) == 0)
                {
                    return SA;
                }
            }

            return string.Empty;
        }

        public string GetSQ(string sqName)
        {
            if (_SQ.Contains(sqName))
            {
                foreach (string sq in SQ)
                {
                    if (sq.Contains(sqName)) return sq;
                }
            }

            return string.Empty;
        }

        public bool HasCurse(string curseName)
        {
            string curse = GetSQ("oracle's curse");
            if (curse.Length == 0) return false;

            return curse.Contains(curseName);
        }

        private bool CheckNaturalWeaponWeponFocus(List<string> FeatList, string FeatName)
        {
            if (FeatName == "Weapon Focus (pincers)")
            {
                FeatName = "Weapon Focus (pincer)";
                if (FeatList.Contains(FeatName)) return true;
            }
            if (FeatName == "Weapon Focus (claw)")
            {
                FeatName = "Weapon Focus (claws)";
                if (FeatList.Contains(FeatName)) return true;
            }
            if (FeatName == "Weapon Focus (tentacle)")
            {
                FeatName = "Weapon Focus (tentacles)";
                if (FeatList.Contains(FeatName)) return true;
            }
            if (FeatName.Contains("composite "))
            {
                FeatName = FeatName.Replace("composite ", string.Empty).Trim();
                if (FeatList.Contains(FeatName)) return true;
            }
            return false;
        }

        public bool HasMythicFeat(string FeatName)
        {
            if (MythicFeats.Contains(FeatName)) return true;
            if (CheckNaturalWeaponWeponFocus(MythicFeats, FeatName)) return true;
            return false;
        }


        public bool HasFeat(string featName)
        {
            if (Feats.Contains(featName)) return true;
            if (Feats.Contains(featName + "B")) return true;
            if (CheckNaturalWeaponWeponFocus(Feats, featName)) return true;
            if (_featsHaveParens)
            {
                foreach (var feat in Feats)
                {
                    if (feat.Contains(featName)) return true;
                }
            }
            return false;
        }

        public int FeatItemCount(string FeatName)
        {
            int count = 0;
            FeatCount.TryGetValue(FeatName, out count);
            if (count > 0) return count;
            if (HasFeat(FeatName)) return 1;
            return 0;
        }

        public bool HasSpellFocusFeat(out List<string> School)
        {
            School = new List<string>();
            string holdSchool = string.Empty;

            foreach (string temp in Feats)
            {
                if (temp.Contains("Spell Focus"))
                {
                    int Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
                    if (Pos == -1)
                    {
                        Pos = temp.Length;
                        throw new Exception("HasSpellFocusFeat-Missing paren");
                    }
                    holdSchool = temp.Substring(Pos);
                    holdSchool = Utility.RemoveParentheses(holdSchool);

                    if (!School.Contains(holdSchool)) School.Add(holdSchool);
                }
            }

            if (School.Any()) return true;
            return false;
        }

        public bool HasGreaterSpellFocusFeat(out List<string> School)
        {
            School = new List<string>();
            string holdSchool = string.Empty;

            foreach (string temp in Feats)
            {
                if (temp.Contains("Greater Spell Focus"))
                {
                    int Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
                    holdSchool = temp.Substring(Pos);
                    holdSchool = Utility.RemoveParentheses(holdSchool);
                    if (!School.Contains(holdSchool)) School.Add(holdSchool);
                }
            }
            if (School.Any()) return true;
            return false;
        }


        public bool HasElementalSkillFocusFeat(out List<string> School)
        {
            School = new List<string>();
            string holdSchool = string.Empty;

            foreach (string temp in Feats)
            {
                if (temp.Contains("Elemental Focus"))
                {
                    int Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
                    if (Pos == -1)
                    {
                        Pos = temp.Length;
                        throw new Exception("HasElementalSkillFocusFeat-Missing paren");
                    }
                    holdSchool = temp.Substring(Pos);
                    holdSchool = Utility.RemoveParentheses(holdSchool);

                    if (!School.Contains(holdSchool)) School.Add(holdSchool);
                }
            }

            return (School.Any());
        }

        public bool HasGreaterElementalSkillFocusFeat(out List<string> School)
        {
            School = new List<string>();
            string holdSchool = string.Empty;

            foreach (string temp in Feats)
            {
                if (temp.Contains("Greater Elemental Focus"))
                {
                    int Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
                    holdSchool = temp.Substring(Pos);
                    holdSchool = Utility.RemoveParentheses(holdSchool);
                    if (!School.Contains(holdSchool)) School.Add(holdSchool);
                }
            }

            return (School.Any());
        }


        public bool HasTrait(string TraitName)
        {
            return (Traits.Contains(TraitName.ToLower()));
        }

        public bool HasGear(string GearName)
        {
            return (_gear.Contains(GearName));
        }

        public string GetGearString(string GearName)
        {
            if (!HasGear(GearName)) return string.Empty;

            try
            {
                List<string> gearItems = _gear.Split(',').ToList();
                gearItems.RemoveAll(x => x== string.Empty);
                foreach (string item in gearItems)
                {
                    if (item.Contains(GearName)) return item;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception("GetGearString - " + ex.Message);
            }
        }


        public int GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes Type,
                                               OnGoingStatBlockModifier.StatBlockModifierSubTypes SubType, ref string formula)
        {
            int modValue = 0;
            foreach (OnGoingStatBlockModifier mod in _onGoingModifers)
            {
                if (mod.ModType == Type && mod.SubType == SubType && mod.ConditionGroup == string.Empty)
                {
                    modValue += mod.Modifier;
                    formula += " +" + mod.Modifier.ToString() + PathfinderConstants.SPACE + mod.Name;
                    //calculation += " +" + mod.Modifier.ToString();
                }
            }

            return modValue;
        }

        public bool HasArmor()
        {
            return !_armor.Any() ? false : true;
        }

        public bool HasLightArmor()
        {
            EquipmentBasic.Armor armor;
            foreach (KeyValuePair<IEquipment, int> kvp in _armor)
            {
                IEquipment hold = kvp.Key;
                if (hold.EquipmentType == EquipmentType.Armor) //shields are armor
                {
                    armor = (EquipmentBasic.Armor)hold;
                    if (armor.category == "light armor")
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

