using System;
using System.Collections.Generic;
using System.Linq;

using StatBlockCommon;
using ClassManager;
using CommonStatBlockInfo;
using Skills;
using OnGoing;
using System.Xml;
using FeatManager;
using Utilities;
using StatBlockCommon.Monster_SB;
using StatBlockCommon.Individual_SB;
using StatBlockChecker.Checkers;
using StatBlockChecker.Parsers;
using EquipmentBusiness;
using PathfinderGlobals;
using StatBlockBusiness;
using StatBlockParsing;

namespace StatBlockChecker
{
    public class SBChecker
    {                
        private FeatMaster _featManager;
        private int _dodgeBonus;    
        private int _onGoingAttackMod = 0;        
        private SkillsChecker _skillsChecker;
        private int _acDefendingMod = 0;
        private SBCheckerBaseInput _sbCheckerBaseInput;
        private FavoredClassData _favoredClassData;
        private HitDiceHitPointData _hitDiceHitPointData;
        private SpellsData _spellsData;
        private EquipmentData _equipmentData;
        private ArmorClassData _armorClassData;
        private SizeData _sizeData;

        private ISpellStatBlockBusiness _spellStatBlockBusiness;
        private IMonsterStatBlockBusiness _monsterStatBlockBusiness;
        private IMagicItemStatBlockBusiness _magicItemStatBlockBusiness;
        private IFeatStatBlockBusiness _featStatBlockBusiness;
        private INaturalWeaponBusiness _naturalWeaponBusiness;
        private IWeaponBusiness _weaponBusiness;
        private IArmorBusiness _armorBusiness;
        private IEquipmentGoodsBusiness _equipmentGoodsBusiness;

        public List<SkillCalculation> SkillCalculations { get; private set; } = new List<SkillCalculation>();

        public SBChecker(MonsterStatBlock SB)
        {
            _spellsData = new SpellsData();
            _equipmentData = new EquipmentData();
            _armorClassData = new ArmorClassData();
            _sizeData = new SizeData();
            _favoredClassData = new FavoredClassData();
            _hitDiceHitPointData = new HitDiceHitPointData();
            _sbCheckerBaseInput = new SBCheckerBaseInput();

            _spellStatBlockBusiness = new SpellStatBlockBusiness();
            _monsterStatBlockBusiness = new MonsterStatBlockBusiness();
            _magicItemStatBlockBusiness = new MagicItemStatBlockBusiness();
            _featStatBlockBusiness = new FeatStatBlockBusiness();
            _naturalWeaponBusiness = new NaturalWeaponBusiness();
            _weaponBusiness = new WeaponBusiness();
            _armorBusiness = new ArmorBusiness();
            _equipmentGoodsBusiness = new EquipmentGoodsBusiness();

            _sbCheckerBaseInput.MonsterSB = SB;
            _sbCheckerBaseInput.MagicInEffect = new List<string>();
            _sbCheckerBaseInput.MessageXML = new StatBlockMessageWrapper();

            _sbCheckerBaseInput.SourceSuperScripts = Utility.GetSuperScripts();
            _sbCheckerBaseInput.IndvSB = new IndividualStatBlock_Combat();

            var methodName = "ParseSubTypes";
            try
            {
                ParseSubTypes();
                methodName = "ParseBeforeCombat";
                ParseBeforeCombat();
                methodName = "ParseSLA";
                ParseSLA();
                methodName = "ParseConstantSpells";
                ParseConstantSpells();
                methodName = "FindAbilityBonuses";
                FindAbilityBonuses();
                methodName = "ParseCreatureType";
                ParseCreatureType();
                methodName = "ParseRace";
                ParseRace();
                methodName = "ComputeSizeMod";
                ComputeSizeMod();
                methodName = "ParseClasses";
                ParseClasses();
                methodName = "ParseFeats";
                ParseFeats();
                methodName = "ParseWeaponsTraining";
                ParseWeaponsTraining();
                _sbCheckerBaseInput.MonsterSBSearch = new MonSBSearch(_sbCheckerBaseInput.MonsterSB, _equipmentData.WeaponsTrainingData,
                         _equipmentData.Armor, _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockMods(), _featStatBlockBusiness);

                methodName = "ParseEquipment";
                ParseEquipment();

                methodName = "ComputeTotalArmorCheckPenalty";
                ComputeTotalArmorCheckPenalty();
                methodName = "ComputeDodgeBonus";
                ComputeDodgeBonus();
                // ParseSLA();
                methodName = "ParseClassSpells";
                ParseClassSpells();
                methodName = "CheckAlignment";
                CheckAlignment();
            }
            catch(Exception ex)
            {
                throw new Exception("SBChecker " + methodName + ":" + ex.Message);
            }
        }

        public XmlDocument MessageXML
        {
            get { return _sbCheckerBaseInput.MessageXML.MessageXML; }
        }

        public List<MessageInfo> MessageInfos
        {
            get { return _sbCheckerBaseInput.MessageXML.MessageInfos; }
        }

        public void CheckStatBlock()
        {
            var methodName = "ApplyBeforeCombatMagic";
            try
            {
                //Basic
                ApplyBeforeCombatMagic();
                methodName = "CheckSenses";
                CheckSenses();
                methodName = "CheckResistance";
                CheckResistance();
                methodName = "CheckAura";
                CheckAura();
                methodName = "CheckTemplates";
                CheckTemplates();
                methodName = "ParseACMods";
                ParseACMods();
                // CheckACMath();
                methodName = "CheckSizeMod";
                CheckSizeMod();
                methodName = "CheckInitModValue";
                CheckInitModValue();
                methodName = "CheckXP_CR";
                CheckXP_CR();
                methodName = "CheckAbilityBaseScores";
                CheckAbilityBaseScores();

                //Defense
                methodName = "CheckSaves";
                CheckSaves();
                methodName = "CheckImmune";
                CheckImmune();
                methodName = "CheckSR";
                CheckSR();
                methodName = "CheckClasses";
                CheckClasses();
                methodName = "CheckSpaceReach";
                CheckSpaceReach();

                //Offense
                methodName = "ParseWeaponsTraining";
                ParseWeaponsTraining();
                methodName = "CheckMeleeWeapons";
                CheckMeleeWeapons();
                methodName = "CheckRangedWeapons";
                CheckRangedWeapons();
                methodName = "CheckSpells";
                CheckSpells(true);
                //CheckACValue();
                methodName = "CheckAC";
                CheckAC();
                methodName = "CheckSpecialAttacks";
                CheckSpecialAttacks();
                methodName = "CheckBABPreReqs";
                CheckBABPreReqs();

                //Statistics
                methodName = "CheckAbilityScoreMods";
                CheckAbilityScoreMods();
                methodName = "CheckHDValue";
                CheckHDValue();
                //   CheckAbilityBaseScores();
                methodName = "CheckHDModifier";
                CheckHDModifier();
                methodName = "FindFavoredClass";
                FindFavoredClass();
                methodName = "CheckFavoredClassHP";
                CheckFavoredClassHP();
                methodName = "CheckBaseAttack";
                CheckBaseAttack();
                methodName = "CheckCombatManeuvers";
                CheckCombatManeuvers();
                methodName = "CheckLanguages";
                CheckLanguages();
                methodName = "CheckDamageResistance";
                CheckDamageResistance();
                methodName = "CheckResistanceValues";
                CheckResistanceValues();                
                //  CheckCMD();
                methodName = "InitSkillsChecker";
                InitSkillsChecker();
                methodName = "CheckSkillMath";
                _skillsChecker.CheckSkillMath();
                _sbCheckerBaseInput.SkillsValues = _skillsChecker.GetSkillsValues();
                SkillCalculations = _skillsChecker.SkillCalculationsList;
                methodName = "CheckFeats";
                CheckFeats();
                methodName = "CheckLanguageCount";
                CheckLanguageCount();
                methodName = "CheckSQ";
                CheckSQ();
            }
            catch(Exception ex)
            {
                throw new Exception("CheckStatBlock " + methodName + ": " +ex.Message);
            }
        }

        private void CheckBABPreReqs()
        {
            foreach (ClassWrapper wrapper in _sbCheckerBaseInput.CharacterClasses.Classes)
            {
                if (wrapper.ClassInstance.IsPrestigeClass && wrapper.ClassInstance.PrestigeBABMin != -1)
                {
                    if (wrapper.ClassInstance.PrestigeBABMin > Convert.ToInt32(Utility.GetNonParenValue(_sbCheckerBaseInput.MonsterSB.BaseAtk)))
                    {
                        _sbCheckerBaseInput.MessageXML.AddFail("CheckBABPreReqs",wrapper.ClassInstance.Name + " does not meet min BAB of " 
                            + wrapper.ClassInstance.PrestigeBABMin.ToString() + " has BAB of " + Utility.GetNonParenValue(_sbCheckerBaseInput.MonsterSB.BaseAtk));
                    }
                }
            }
        }        

        public void CheckBestiaryStatBlock()
        {  
            //Basic
            CheckBAB();
            CheckSenses();
            CheckResistance();
            CheckAura();
            CheckTemplates();
            ParseACMods();
          //  CheckACMath();
            CheckSizeMod();
            CheckInitModValue();
            CheckXP_CR();

            //Defense   
            CheckSaves();
            CheckImmune();
            CheckSR();
            CheckClasses();
            CheckSpaceReach();

            //Offense
            CheckMeleeWeapons();
            CheckRangedWeapons();
            CheckSpells(false);
            CheckAC();

            //Statistics
            CheckHDValue();
            CheckHDModifier();
            FindFavoredClass();
            CheckFavoredClassHP();
            CheckCombatManeuvers();
            //CheckCMD();            
            InitSkillsChecker();
            _skillsChecker.CheckSkillMath();
            _sbCheckerBaseInput.SkillsValues = _skillsChecker.GetSkillsValues();
            SkillCalculations = _skillsChecker.SkillCalculationsList;
            CheckFeats();
            CheckPoison();
            CheckDisease();
        }
        
        private void InitSkillsChecker()
        {
            _skillsChecker = new SkillsChecker(_sbCheckerBaseInput, _equipmentData,
                   _spellsData, _favoredClassData,_armorClassData, _sizeData);
        }

        private void FindAbilityBonuses()
        {
            try
            {
                _sbCheckerBaseInput.AbilityScores = new AbilityScores.AbilityScores(_sbCheckerBaseInput.MonsterSB.AbilityScores);
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("FindAbilityBonuses", ex.Message);
            }
        }       

        private void CheckAlignment()
        {
            List<string> tempAlign = _sbCheckerBaseInput.MonsterSB.Alignment.Split('/').ToList();
            foreach (string align in tempAlign)
            {
                if (!(CommonMethods.GetAlignments().Contains(align.Trim())))
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("CheckAlignment", "Invalid alignment value: " + align);
                }
            }
        }

        private void CheckClasses()
        {
            ClassChecker checker = new ClassChecker(_sbCheckerBaseInput);
            try
            {
                checker.CheckClasses();
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("CheckClasses" , ex.Message);
            }
        }

        private void CheckSpaceReach()
        {
            SpaceReachChecker spaceReachChecker = new SpaceReachChecker(_sbCheckerBaseInput);
            spaceReachChecker.CheckSpaceReach();
        }       

        private void CheckSR()
        {
            string CheckName = "SR Check";

            if (_sbCheckerBaseInput.Race_Base.RaceSB == null)
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "RaceSB is null");
                return;
            }

            int SB_SR;
            int.TryParse(_sbCheckerBaseInput.MonsterSB.SR, out SB_SR);
            int Computed_SR;
            int.TryParse( _sbCheckerBaseInput.Race_Base.RaceSB.SR, out Computed_SR);
            string formula = "Race SR + " + Computed_SR.ToString();

            if (_sbCheckerBaseInput.CharacterClasses.ClassCount() > 0)
            {
                switch ( _sbCheckerBaseInput.Race_Base.RaceSB.name)
                {
                    case "Serpentfolk":
                    case "Degenerate Serpentfolk":
                        Computed_SR = _sbCheckerBaseInput.MonsterSB.HDValue() + 10;
                        formula = "Advacned Serpentfolk(HD + 10) " + _sbCheckerBaseInput.MonsterSB.HDValue().ToString() + " + 10 ";
                        break;
                    case "Drow":
                        Computed_SR = 6 + _sbCheckerBaseInput.CharacterClasses.FindAllClassLevels();
                        formula = "Drow (6 + class Levels) " + " + 6 " + _sbCheckerBaseInput.CharacterClasses.FindAllClassLevels().ToString();
                        break;
                    case "Svirfneblin":
                        Computed_SR = 11 + _sbCheckerBaseInput.CharacterClasses.FindAllClassLevels();
                        formula = "Svirfneblin (11 + class Levels) " + " + 11 " + _sbCheckerBaseInput.CharacterClasses.FindAllClassLevels().ToString();
                        break;
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasTemplate("graveknight"))
            {
                Computed_SR += Convert.ToInt32(_sbCheckerBaseInput.MonsterSB.CR) + 11;
                formula += (Convert.ToInt32(_sbCheckerBaseInput.MonsterSB.CR) + 11).ToString() + "graveknight";
            }

            if (_sbCheckerBaseInput.IndvSB != null)
            {
                List<OnGoingStatBlockModifier> Mods = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockMods();
                foreach (OnGoingStatBlockModifier mod in Mods)
                {
                    if (mod.ModType == OnGoingStatBlockModifier.StatBlockModifierTypes.SR)
                    {
                        Computed_SR += mod.Modifier;
                        formula += " +" + mod.Modifier.ToString() + PathfinderConstants.SPACE + mod.Name;
                    }
                }
            }
           

            if (SB_SR == Computed_SR)          
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            else
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "SB SR = " + SB_SR.ToString() + " Computed SR = " + Computed_SR.ToString(),formula);
        }

        private void CheckResistanceValues()
        {
            ResistanceChecker resistanceChecker = new ResistanceChecker(_sbCheckerBaseInput, _equipmentData);
            resistanceChecker.CheckResistanceValues();
        }  

        private void CheckDamageResistance()
        {
            ResistanceChecker resistanceChecker = new ResistanceChecker(_sbCheckerBaseInput, _equipmentData);
            resistanceChecker.CheckDamageResistance();
        }

        private void CheckLanguages()
        {
            string CheckName = "Language Check";

            int intScore = _sbCheckerBaseInput.MonsterSBSearch.GetAbilityScoreValue(AbilityScores.AbilityScores.AbilityName.Intelligence);
            if (intScore > 4 && _sbCheckerBaseInput.MonsterSB.Languages.Length == 0)
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "No Languages with Int of " + intScore.ToString());
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("druid"))
            {
                if (!_sbCheckerBaseInput.MonsterSB.Languages.Contains("Druidic")) _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Druid missing Druidic language");
            }
        }      

        private void CheckImmune()
        {
            string immune =  _sbCheckerBaseInput.Race_Base.GetRacialImuunities();
            List<string> Immunities = immune.Split(',').ToList();
            Immunities.RemoveAll(x => x== string.Empty);
            foreach (string immunity in Immunities)
            {
                string tempImmunity = immunity.Trim();
                if (!_sbCheckerBaseInput.MonsterSB.Immune.Contains(tempImmunity))
                {
                    if (immunity.Contains("darkvision"))
                    {

                    }
                    else
                    {
                        _sbCheckerBaseInput.MessageXML.AddFail("Immune",  _sbCheckerBaseInput.Race_Base.Name() + " supposed to have " + tempImmunity + ", SB doesn't");
                    }
                }
            }
        }            

        private void CheckCombatManeuvers()
        {
            int OnGoing = 0;
            string formula = string.Empty;
            if (_sbCheckerBaseInput.IndvSB != null)
            {
                List<OnGoingStatBlockModifier> Mods = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockMods();
                foreach (OnGoingStatBlockModifier mod in Mods)
                {
                    if (mod.ModType == OnGoingStatBlockModifier.StatBlockModifierTypes.Attack && !mod.Name.Contains("Magic Weapon"))
                    {
                        OnGoing += mod.Modifier;
                        formula += " +" + mod.Modifier.ToString() + PathfinderConstants.SPACE + mod.Name;
                    }
                }             
            }
            ICombatManeuversCheckerInput combatManeuversCheckerInput = new CombatManeuversCheckerInput
            {
                DodgeBonus = _dodgeBonus,
                Formula = formula,
                OnGoing = OnGoing,
                SizeData = _sizeData,
                TotalHD = _hitDiceHitPointData.TotalHD
            };
            CombatManeuversChecker checker = new CombatManeuversChecker(_sbCheckerBaseInput, combatManeuversCheckerInput);

            if (_sbCheckerBaseInput.MonsterSB.CMB.Contains(","))
            {
                int commaPos = _sbCheckerBaseInput.MonsterSB.CMB.IndexOf(",");
                int start = _sbCheckerBaseInput.MonsterSB.CMB.IndexOf(PathfinderConstants.PAREN_LEFT);
                int stop = _sbCheckerBaseInput.MonsterSB.CMB.IndexOf(PathfinderConstants.PAREN_RIGHT,commaPos);
                if(!(commaPos > start && commaPos < stop)) _sbCheckerBaseInput.MessageXML.AddFail("CheckCombatManeuvers", "CMB has comma, missing?");
                throw new Exception("CheckCombatManeuvers: CMB has comma, missing?");
            }

            checker.CheckCMB();
            checker.CheckCMD();
        }

        private void CheckFeats()
        {
            if(_sbCheckerBaseInput.MonsterSB.BaseAtk.Contains("/"))  throw new Exception("CheckFeats: / in BAB");

            if (_featManager.FeatDatum == null) throw new Exception("CheckFeats: FeatDatum is null");
            if (!_featManager.FeatDatum.Any()) return;
            bool HasEnviroment = _sbCheckerBaseInput.MonsterSB.Environment.Length > 0 ? true : false;
            FeatCheckerInput featCheckerInput = new FeatCheckerInput
            {
                HasEnvirmonment = HasEnviroment,
                BAB = int.Parse(_sbCheckerBaseInput.MonsterSB.BaseAtk),
                Feats = _featManager.FeatDatum,
                HDValue = _sbCheckerBaseInput.MonsterSB.HDValue(),
                IntAbilityScoreValue = _sbCheckerBaseInput.MonsterSB.GetAbilityScoreValue(StatBlockInfo.INT),
                SkillCalculations = SkillCalculations
            };
           
            FeatChecker featChecker = new FeatChecker(_sbCheckerBaseInput, featCheckerInput, _featStatBlockBusiness);

            featChecker.CheckFeatCount(_sbCheckerBaseInput.MonsterSB.Feats);
            featChecker.CheckFeatPreReqs();
        }       

        private void CheckAC()
        {
            if ( _sbCheckerBaseInput.Race_Base.RaceSB != null)
            {
                ArmorClassChecker checker = new ArmorClassChecker(_sbCheckerBaseInput, _armorClassData, _sizeData.SizeMod, _dodgeBonus);

                checker.CheckACMath();
                checker.CheckACValue();
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail("CheckAC", " _sbCheckerBaseInput.Race_Base.RaceSB is null");
                return;
            }
        }

        private void CheckAura()
        {
         
        }      

        private void CheckSQ()
        {
          
        }   

        private void CheckSpecialAttacks()
        {
           
        }

        private void CheckResistance()
        {
            string CheckName = "Resistance";           

            string resistances =  _sbCheckerBaseInput.Race_Base.GetRacialResistance();
            if (resistances.Length > 0)
            {
                List<string> Resistances = resistances.Split(',').ToList();
                foreach (string resist in Resistances)
                {
                    string resistTemp = resist.Trim();
                    if (!_sbCheckerBaseInput.MonsterSB.Resist.Contains(resistTemp)) _sbCheckerBaseInput.MessageXML.AddFail(CheckName,  _sbCheckerBaseInput.Race_Base.Name() + " supposed to have " + resistTemp + ", SB doesn't");
                }
            }  
        }

        private void CheckSenses()
        {
            string CheckName = "Senses Perception";
            string temp = _sbCheckerBaseInput.MonsterSB.Senses;
            int Pos = temp.IndexOf(StatBlockInfo.SkillNames.PERCEPTION);
            temp = temp.Substring(Pos);
            temp = temp.Replace(StatBlockInfo.SkillNames.PERCEPTION, string.Empty);
            Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
            if (Pos >= 0) temp = temp.Substring(0, Pos);

            int valueSenses;
            int.TryParse(temp, out valueSenses);

            int valueSkills = 0;

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSkill(StatBlockInfo.SkillNames.PERCEPTION))
               valueSkills = _sbCheckerBaseInput.MonsterSBSearch.SkillValue(StatBlockInfo.SkillNames.PERCEPTION);


           if (valueSkills == valueSenses)
               _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
           else
               _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Senses Perception not equal to Skills Perception", "Senses: " + valueSenses.ToString() + "  Skill:" + valueSkills.ToString());
           

           string senses =  _sbCheckerBaseInput.Race_Base.GetRacialSenses();
           if (senses.Length > 0)
           {
               List<string> Senses = senses.Split(',').ToList();
               foreach (string sense in Senses)
               {
                   string senseTemp = sense.Trim();
                   if (!_sbCheckerBaseInput.MonsterSB.Senses.Contains(senseTemp)) _sbCheckerBaseInput.MessageXML.AddFail("Senses",  _sbCheckerBaseInput.Race_Base.Name() + " supposed to have " + senseTemp + ", SB doesn't");
               }
           }  
        }

        #region Basic

        private void CheckBAB()
        {     
            string CheckName = "BAB";
            int HD = _sbCheckerBaseInput.MonsterSB.HDValue();
            int BABValue = Convert.ToInt32(Utility.GetNonParenValue(_sbCheckerBaseInput.MonsterSB.BaseAtk));
            int Fast = StatBlockInfo.ComputeBAB(HD, StatBlockInfo.BABType.Fast);
            int Medium = StatBlockInfo.ComputeBAB(HD, StatBlockInfo.BABType.Medium);
            int Slow = StatBlockInfo.ComputeBAB(HD, StatBlockInfo.BABType.Slow);
            bool goodBAB = false;
            string formula = null;

            if (BABValue == Fast)
            {
                goodBAB = true;
                formula = "Fast";
            }
            else if (BABValue == Medium)
            {
                goodBAB = true;
                formula = "Medium";
            }
            else if (BABValue == Slow)
            {
                goodBAB = true;
                formula = "Slow";
            }

            if (goodBAB)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName, formula);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "No Match-- Fast:" + Fast.ToString() + " Medium:" + Medium.ToString() +
                      " Slow:" + Slow.ToString(), BABValue.ToString());
            }
        }

        private void FindFavoredClass()
        {
            FavoredClassParser favoredClassParser = new FavoredClassParser(_sbCheckerBaseInput, _hitDiceHitPointData.HDModifier, _hitDiceHitPointData.MaxHPMod);
            _favoredClassData = favoredClassParser.FindFavoredClass();
        }
      
        private void CheckTemplates()
        {
            string CheckName = "CheckTemplates";

            TemplateChecker templateChecker = new TemplateChecker(_sbCheckerBaseInput);

            try
            {
                templateChecker.CheckTemplates();
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, ex.Message);
            }
        }

        private void CheckXP_CR()
        {
            string CheckName = "XP CR";
            int XP_Comp = -100;
            try
            {
                XP_Comp = StatBlockInfo.GetXP(_sbCheckerBaseInput.MonsterSB.CR);
            }
            catch 
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Bad Parse with CR of " + _sbCheckerBaseInput.MonsterSB.CR);
                return;
            }

            string XPTemp = _sbCheckerBaseInput.MonsterSB.XP.Replace(",", string.Empty);

            if (XPTemp.Length > 0)
            {
                if (XPTemp == "-") XPTemp = "0";
                try
                {
                    int XP_SB = Convert.ToInt32(XPTemp);

                    if (XP_Comp == XP_SB)
                    {
                        _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
                    }
                    else
                    {
                        _sbCheckerBaseInput.MessageXML.AddFail(CheckName, XP_Comp.ToString(), XP_SB.ToString());
                    }
                }
                catch (Exception ex)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("CheckXP_CR", "Issue with XP Value");
                }
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "computed as " + XP_Comp.ToString(), "missing");
            }
        }

        private void CheckAbilityBaseScores()
        {
            BaseAbilityScoresChecker baseAbilityScoresChecker = new BaseAbilityScoresChecker(_sbCheckerBaseInput, _armorClassData);
            baseAbilityScoresChecker.CheckAbilityBaseScores( _hitDiceHitPointData.HDModifier);  
        }

        private void CheckInitModValue()
        {
            InitModValueChecker initModValueChecker = new InitModValueChecker(_sbCheckerBaseInput);
            initModValueChecker.CheckInitModValue();        
        }

        private void CheckPercpetionValue()
        {

        }       

        private void CheckSizeMod()
        {
            string CheckName = "Size Mod";
            if (_sizeData.SizeMod != _armorClassData.ACMods_SB.Size)
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, _sizeData.SizeMod.ToString(), _armorClassData.ACMods_SB.Size.ToString());
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
            }
        }

        #endregion

        #region Defense       

        private void CheckHDValue()
        {
            HitDiceChecker hitDiceChecker = new HitDiceChecker(_sbCheckerBaseInput);
            var output = hitDiceChecker.CheckHDValue();
            _hitDiceHitPointData.TotalHD = output.TotalHd;
            _hitDiceHitPointData.HDModifier = output.HDModifier;           
        }

        private void CheckHDModifier()
        {
            HDModifierChecker hdModifierChecker = new HDModifierChecker(_sbCheckerBaseInput, _favoredClassData, _hitDiceHitPointData);
            hdModifierChecker.CheckHDModifier();    
        }        

        private void CheckFavoredClassHP()
        {
            FavoredClassChecker favoredClassChecker = new FavoredClassChecker(_sbCheckerBaseInput, _favoredClassData, _hitDiceHitPointData);
            favoredClassChecker.CheckFavoredClassHP();
        }

        private void CheckSaves()
        {
            SavesChecker saveChecker = new SavesChecker(_sbCheckerBaseInput, _equipmentData);
            saveChecker.CheckFortValue();
            saveChecker.CheckRefValue();
            saveChecker.CheckWillValue();
        }
        #endregion

        #region Offense

        #region Ranged

        private void CheckRangedWeapons()
        {                      
            RangedWeaponChecker checker = new RangedWeaponChecker(_sbCheckerBaseInput,  
                                 _equipmentData,  _sizeData, _naturalWeaponBusiness, _weaponBusiness);

            try
            {
                checker.CheckRangedWeapons(_sbCheckerBaseInput.MonsterSB.Ranged);
            }
            catch(Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("CheckRangedWeapons", ex.Message);
            }
        }     

      
        #endregion Ranged

        #region MeleeNonNatural

        private void CheckMeleeWeapons()
        {             
            MeleeWeaponChecker checker = new MeleeWeaponChecker(_sbCheckerBaseInput,  _equipmentData,
                     _sizeData, _acDefendingMod, _naturalWeaponBusiness, _weaponBusiness);

            try
            {
                checker.CheckMeleeWeapons(_sbCheckerBaseInput.MonsterSB.Melee);
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("CheckMeleeWeapons" , ex.Message);
            }
        }
        #endregion MeleeNonNatural       

        private void ParseWeaponsTraining()
        {
            if (_sbCheckerBaseInput.MonsterSB.ClassArchetypes.Contains("weapon master")) return;
            if (_sbCheckerBaseInput.MonsterSB.SpecialAttacks.Contains("swashbuckler weapon training")) return;
            int Pos = _sbCheckerBaseInput.MonsterSB.SpecialAttacks.IndexOf("weapon training");
            if (Pos == -1) return;

            try
            {
                Pos = _sbCheckerBaseInput.MonsterSB.SpecialAttacks.IndexOf(PathfinderConstants.PAREN_LEFT, Pos);
                string temp = _sbCheckerBaseInput.MonsterSB.SpecialAttacks.Substring(Pos, _sbCheckerBaseInput.MonsterSB.SpecialAttacks.IndexOf(PathfinderConstants.PAREN_RIGHT, Pos) - Pos);
                temp = Utility.RemoveParentheses(temp);
                _equipmentData.WeaponsTrainingData = new Dictionary<string, int>();
                List<string> WTList = temp.Split(',').ToList();
                foreach (string train in WTList)
                {
                    temp = train;
                    Pos = temp.IndexOf("+");
                    if (Pos == -1) continue;
                    int Value = Convert.ToInt32(temp.Substring(Pos));
                    temp = temp.Substring(0, Pos).Trim();
                    string weapons = string.Empty;

                    //fighter weapon groups
                    weapons = GetWeaponsTrainingGroup(temp);
                    _equipmentData.WeaponsTrainingData.Add(weapons, Value);
                }
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("ParseWeaponsTraining", ex.Message);
            }
        }
        private static string GetWeaponsTrainingGroup(string temp)
        {
            string weapons = string.Empty;
            switch (temp)
            {
                case "axes":
                    weapons = "bardiche, battleaxe, dwarven waraxe, greataxe, handaxe, heavy pick, hooked axe, knuckle axe, light pick, mattock, orc double axe, pata, throwing axe, boarding axe, butchering axe";
                    break;
                case "heavy blades":
                    weapons = "bastard sword, chakram, double chicken saber], double walking stick katana, elven curve blade, falcata, falchion, greatsword, great terbutje, katana, khopesh, longsword, nine-ring broadsword, nodachi, scimitar, scythe, seven-branched sword, shotel, temple sword, terbutje, two-bladed sword, cutlass, sickle-sword, switchscythe";
                    break;
                case "light blades":
                    weapons = "bayonet, butterfly sword, dagger, gladius, kama, kerambit, kukri, pata, quadrens, rapier, short sword, sica, sickle, starknife, swordbreaker dagger, sword cane, wakizashi, drow razor, dueling dagger, sanpkhang, spiral rapier";
                    break;
                case "bows":
                    weapons = "composite longbow,composite shortbow,longbow,shortbow,orc hornbow";
                    break;
                case "close":
                    weapons = "bayonet, brass knuckles, cestus, dan bong, emei piercer, fighting fan, gauntlet, heavy shield, iron brush, light shield, madu, mere club, punching dagger, sap, scizore, spiked armor, spiked gauntlet, spiked shield, tekko-kagi, tonfa, unarmed strike, wooden stake, wushu dart, dwarven war-shield, tri-bladed katar, waveblade";
                    break;
                case "crossbows":
                    weapons = "double crossbow, hand crossbow, heavy crossbow, light crossbow, heavy repeating crossbow, light repeating crossbow, tube arrow shooter";
                    break;
                case "double":
                    weapons = "dire flail,dwarven urgrosh,gnome hooked hammer,orc double axe,quarterstaff,two-bladed sword, boarding gaff, chain-hammer, gnome battle ladder";
                    break;
                case "firearms":
                    weapons = "double-barreled pistol,blunderbuss,sword cane pistol,musket";
                    break;
                case "flails":
                    weapons = "chain spear, dire flail, double chained kama, flail, flying blade, heavy flail, kusarigama, kyoketsu shoge, meteor hammer, morningstar, nine-section whip, nunchaku, sansetsukon, scorpion whip, spiked chain, urumi, whip, cat-o’-nine-tails, dwarven dorn-dergar, flying talon";
                    break;
                case "hammers":
                    weapons = "aklys, battle aspergillum, club, greatclub, heavy mace, light hammer, light mace, mere club, taiaha, tetsubo, wahaika, warhammer, chain-hammer, gnome piston maul, lantern staff";
                    break;
                case "monk":
                    weapons = "bo staff, brass knuckles, butterfly sword, cestus, dan bong, double chained kama, double chicken saber, emei piercer, fighting fan, jutte, kama, kusarigama, kyoketsu shoge, lungshuan tamo, monk's spade, nine-ring broadsword, nine-section whip, nunchaku, quarterstaff, rope dart, sai, sansetsukon, seven-branched sword, shang gou, shuriken, siangham, tiger fork, tonfa, tri-point double-edged sword, unarmed strike, urumi, wushu dart, sanpkhang";
                    break;
                case "natural":
                    weapons = "unarmed strike and all natural weapons,such as bite,claw,gore,tail,wing";
                    break;
                case "pole arms":
                case "polearms":
                    weapons = "bardiche, bec de corbin, bill, glaive, glaive-guisarme, guisarme, halberd, hooked lance, lucerne hammer, mancatcher, monk's spade, naginata, nodachi, ranseur, rohomphaia, tepoztopili, tiger fork, boarding gaff, fauchard, gnome ripsaw glaive";
                    break;
                case "spears":
                    weapons = "amentum, boar spear, javelin, harpoon, lance, longspear, pilum, shortspear, sibat, spear, tiger fork, trident, stormshaft javelin";
                    break;
                case "thrown":
                    weapons = "aklys, amentum, atlatl, blowgun, bolas, boomerang, chakram, club, dagger, dart, halfling sling staff, harpoon, javelin, lasso, kestros, light hammer, net, poisoned sand tube, rope dart, shortspear, shuriken, sling, spear, starknife, throwing axe, throwing shield, trident, wushu dart, chain-hammer, dueling dagger, flask thrower, stormshaft javelin";
                    break;
            }
            return weapons;
        } 

        private void CheckDisease()
        {
            if (!_sbCheckerBaseInput.MonsterSB.SpecialAbilities.Contains("Disease (")) return;

            try
            {
                int StartPos = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.IndexOf("Disease (");
                string temp = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.Substring(StartPos);
                List<string> fields2 = new List<string> { "save", "frequency", "onset", "effect", "cure" };

                foreach (string field in fields2)
                {
                    if (!temp.Contains(field))
                    {
                        _sbCheckerBaseInput.MessageXML.AddFail("Disease", "Field Missing : " + field);
                    }
                }
                StartPos = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.IndexOf("DC ", StartPos);
                int EndPos = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.IndexOf(";", StartPos);

                string hold = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.Substring(StartPos, EndPos - StartPos);


                hold = hold.Replace("DC ", string.Empty);
                int Pos2 = hold.IndexOf(PathfinderConstants.PAREN_LEFT);
                if (Pos2 != -1)
                {
                    hold = hold.Substring(0, Pos2);
                }
                int SB_DC = int.Parse(hold);

                int abilityUsed = _sbCheckerBaseInput.AbilityScores.ConMod;
                string abilityUsedString = "ConMod";

                if (_sbCheckerBaseInput.MonsterSB.Type.Contains("undead"))
                {
                    abilityUsed = _sbCheckerBaseInput.AbilityScores.ChaMod;
                    abilityUsedString = "Cha Mod";
                }

                string calculation = "10 + (" + _sbCheckerBaseInput.MonsterSB.HDValue().ToString() + " / 2) " + " + " + abilityUsed.ToString();
                string formula = "10 + HD/2 + " + abilityUsedString;



                double ComputedDC = 10 + Math.Floor((double)_sbCheckerBaseInput.MonsterSB.HDValue() / 2) + abilityUsed;

                if (SB_DC == ComputedDC)
                {
                    _sbCheckerBaseInput.MessageXML.AddPass("Disease DC-" + SB_DC.ToString());
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("Disease DC-", ComputedDC.ToString(), SB_DC.ToString(), formula, calculation);
                }
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("Disease", " Issue with Formatting");
            }
        }

        private void CheckPoison()
        {
            if (!_sbCheckerBaseInput.MonsterSB.SpecialAbilities.Contains("Poison (")) return;

            try
            {
                int StartPos = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.IndexOf("Poison (");
                string temp = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.Substring(StartPos);
                List<string> fields2 = new List<string> {  "save", "frequency", "effect", "cure" };

                foreach (string field in fields2)
                {
                    if (temp.IndexOf(field) == -1)
                    {
                        _sbCheckerBaseInput.MessageXML.AddFail("Poison", "Field Missing : " + field);
                    }
                }
                StartPos = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.IndexOf("DC ", StartPos);
                int EndPos = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.IndexOf(";", StartPos);

                string hold = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.Substring(StartPos, EndPos - StartPos);

                hold = hold.Replace("DC ", string.Empty);
                int Pos2 = hold.IndexOf(PathfinderConstants.PAREN_LEFT);
                if (Pos2 != -1)
                {
                    hold = hold.Substring(0, Pos2);
                }

                int SB_DC = int.Parse(hold);
                int abilityUsed = _sbCheckerBaseInput.AbilityScores.ConMod;

                if (_sbCheckerBaseInput.MonsterSB.Type.Contains("undead")) abilityUsed = _sbCheckerBaseInput.AbilityScores.ChaMod;

                int HDValue = _sbCheckerBaseInput.MonsterSB.HDValue();
                string formula = "10 + (" + HDValue.ToString() + " / 2) " + " + " + abilityUsed.ToString();
              //  string formula = "10 + HD/2 + " + abilityUsedString;



                double ComputedDC = 10 + Math.Floor((double)HDValue / 2) + abilityUsed;
                if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Ability Focus(poison)"))
                {
                    ComputedDC += 2;
                    formula += "Ability Focus(poison)";
                  //  calculation += " + 2";
                }

                StartPos = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.IndexOf("Poison ");
                StartPos = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.IndexOf("racial bonus", StartPos);
                int Pos = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.IndexOf(" (");
                if (StartPos != -1)
                {
                    if ((Pos == -1 || (Pos >= 0 && Pos > StartPos)))
                    {
                        EndPos = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.LastIndexOf(PathfinderConstants.SPACE, StartPos);
                        EndPos = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.LastIndexOf(PathfinderConstants.SPACE, EndPos - 1);
                        hold = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.Substring(EndPos, StartPos - EndPos);
                      //  ComputedDC += int.Parse(hold);
                    }
                    else
                    {
                        StartPos = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.LastIndexOf(PathfinderConstants.SPACE, StartPos);
                        Pos = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.LastIndexOf(PathfinderConstants.SPACE, StartPos - 1);
                        hold = _sbCheckerBaseInput.MonsterSB.SpecialAbilities.Substring(Pos, StartPos - Pos);
                      //  ComputedDC += int.Parse(hold);
                    }
                }

                if (SB_DC == ComputedDC)
                {
                    _sbCheckerBaseInput.MessageXML.AddPass("Poison DC-" + SB_DC.ToString());
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("Poison DC-", ComputedDC.ToString(), SB_DC.ToString(), formula);
                }
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("Poison DC", "Parse Issue: " + ex.Message);
            }

        }

        #region Spells

        private void ParseSLA()
        {
            if (_sbCheckerBaseInput.MonsterSB.SpellLikeAbilities.Length == 0) return;

            _spellsData.SLA = new Dictionary<string, SpellList>();
            SpellList SL;
            string hold = _sbCheckerBaseInput.MonsterSB.SpellLikeAbilities;
            string stringSLA = "Spell-Like Abilities";
            int Count = (hold.Length - hold.Replace(stringSLA, string.Empty).Length) / stringSLA.Length;
            int Pos;
            string temp, SpellBlock;

            for (int a = 1; a <= Count; a++)
            {
                string name = "General";
                Pos = hold.IndexOf(stringSLA);
                name = hold.Substring(0, Pos).Trim();
                if (name.Length > 0)
                {
                    hold = hold.Substring(Pos);
                }
                Pos = hold.IndexOf(stringSLA);
                Pos = hold.IndexOf(stringSLA, Pos + 1);
                if (Pos > 0)
                {
                    Pos = hold.LastIndexOf(Environment.NewLine, Pos);
                    temp = hold.Substring(0, Pos);
                    hold = hold.Replace(temp, string.Empty);
                    Pos = temp.IndexOf(stringSLA);
                    //name = temp.Substring(0, Pos);
                    //if (name.Length > 0)
                    //{
                    //    temp = temp.Replace(name, string.Empty);
                    //}
                }
                else
                {
                    temp = hold;
                }

                Pos = temp.IndexOf(Environment.NewLine);
                SpellBlock = temp; //temp.Substring(Pos);
                SL = new SpellList();
                try
                {
                    SL.ParseSpellList(SpellBlock, _sbCheckerBaseInput.SourceSuperScripts);
                }
                catch (Exception ex)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("Pare SLA", ex.Message);
                }
                if (SL.Errors.Length > 0)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("Parse SLA", SL.Errors);
                }
                _spellsData.SLA.Add(name, SL);
            }
        }

        private void ParseClassSpells()
        {
            ClassSpellChecker classSpellChecker = new ClassSpellChecker(_sbCheckerBaseInput);
            _spellsData.ClassSpells = classSpellChecker.ParseClassSpells();         
        }          

        private void CheckSpells(bool CheckAll)
        {
            try
            {
                SpellChecker spellChecker = new SpellChecker(_sbCheckerBaseInput, _spellsData.ClassSpells, _spellsData.SLA, _spellStatBlockBusiness);
                spellChecker.CheckSpellsLevels();
                bool IsGnome =  _sbCheckerBaseInput.Race_Base.Name() == "Gnome" ? true : false;
                spellChecker.CheckSLA(IsGnome);
                if (CheckAll) spellChecker.CheckSpellDC(IsGnome);
            }
            catch (Exception ex)
            {
                throw new Exception("SBChecker-CheckSpells--" + ex.Message, ex);
            }
        }  
                
        #endregion Spells

        #endregion Offense

        #region Statistics

        private void CheckAbilityScoreMods()
        {
            if ( _sbCheckerBaseInput.Race_Base.RaceBaseType != RaceBase.RaceType.StatBlock) return;
            if (_sbCheckerBaseInput.MonsterSB.Class.Length == 0) return;
            string CheckName = "Ability Score Adjustments";

            int ModSum = 10; //+4, +4, +2, +2, +0, –2
            int diff = 0;
            List<int> Mods = new List<int> {+4, +4, +2, +2, +0, -2 };
            int RaceSum =  _sbCheckerBaseInput.Race_Base.AbilityScoreSum();
            string Fail = string.Empty;
            string Now = string.Empty;

            List<string> AbilityNames = new List<string> { StatBlockInfo.STR, StatBlockInfo.INT, StatBlockInfo.WIS, StatBlockInfo.DEX, StatBlockInfo.CON, StatBlockInfo.CHA };
            int Sum = 0;
            foreach (string name in AbilityNames)
            {
                Sum += _sbCheckerBaseInput.MonsterSB.GetAbilityScoreValue(name);
            }

            if (Sum == RaceSum + ModSum)
            {
                //have to check each score
                foreach (string name in AbilityNames)
                {
                    Now = name;
                    diff =  _sbCheckerBaseInput.MonsterSB.GetAbilityScoreValue(name) -  _sbCheckerBaseInput.Race_Base.GetAbilityScore(name);
                    for (int a = 0; a < Mods.Count; a++)
                    {
                        if (diff == Mods[a])
                        {
                            Mods.Remove(Mods[a]);
                            Now = string.Empty;
                            break;
                        }
                    }
                    if (Now.Length > 0)
                    {
                        Fail += Now + PathfinderConstants.SPACE;
                    }
                }
                if (!Mods.Any())
                {
                    _sbCheckerBaseInput.MessageXML.AddPass(CheckName);
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddInfo(CheckName + " wrong adjustment added." + Fail);
                }
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, Sum.ToString(), (RaceSum + ModSum).ToString());
            }
        }

        private void CheckBaseAttack()
        {
            string CheckName = "Base Atk";
            int BAB_SB = 0;
            try
            {
                BAB_SB = Convert.ToInt32(Utility.GetNonParenValue(_sbCheckerBaseInput.MonsterSB.BaseAtk));
            }
            catch
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName,"Issue with converting BAB to int");
                return;
            }
            string formula;
            int BAB = _sbCheckerBaseInput.CharacterClasses.GetBABValue(out formula) + (_sbCheckerBaseInput.CharacterClasses.HasClass("animal companion") ? 0 :  _sbCheckerBaseInput.Race_Base.RaceBAB());

            if (_sbCheckerBaseInput.IndvSB != null)
            {
               int attackOverride = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.Attack, OnGoingStatBlockModifier.StatBlockModifierSubTypes.Override);
               if (attackOverride > 0)
               {
                   BAB = attackOverride;
               }
            }

            if (BAB_SB == BAB)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName, BAB_SB.ToString());
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, BAB.ToString(), BAB_SB.ToString(), formula);
            }
        }     

        private int GetACModValue(string Mod)
        {
            string holdMods = _sbCheckerBaseInput.MonsterSB.AC_Mods;
            if (holdMods.Length == 0) return 0;

            int Pos = holdMods.IndexOf(PathfinderConstants.PAREN_RIGHT);
            holdMods = holdMods.Substring(0, Pos);
            Pos = holdMods.IndexOf("|");
            if (Pos != -1)
            {
                holdMods = holdMods.Substring(0, Pos).Trim();
            }

            if (holdMods.IndexOf(Mod) >= 0)
            {
                holdMods = holdMods.Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
                holdMods = holdMods.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                List<string> Mods = holdMods.Split(',').ToList();
                foreach (string mod in Mods)
                {
                    if (mod.Contains(Mod))
                    {
                        string hold = mod.Replace(Mod, string.Empty).Trim();
                        if (hold.IndexOf(PathfinderConstants.SPACE) > 0) hold = hold.Substring(0, hold.IndexOf(PathfinderConstants.SPACE));
                        return Convert.ToInt32(hold);
                    }
                }
            }
            return 0;
        }       

        private void CheckLanguageCount()
        {
            if ( _sbCheckerBaseInput.Race_Base == null) return;

            string CheckName = "Language Count";
            List<string> Langs = _sbCheckerBaseInput.MonsterSB.Languages.Split(',').ToList();
            Langs.RemoveAll(x => x== string.Empty);
            int SB_count = Langs.Count;
            int IntScore = _sbCheckerBaseInput.MonsterSB.GetAbilityScoreValue(StatBlockInfo.INT);
            int BonusLangs = StatBlockInfo.GetAbilityModifier(IntScore);
            

            if (IntScore > 3 && BonusLangs < 0)
            {
                BonusLangs = 0;
            }

            if (BonusLangs < 0)
            {
                BonusLangs = 0;
            }
            int RaceLangs =  _sbCheckerBaseInput.Race_Base.RaceLanguages().Count;
            int LangCount = RaceLangs + BonusLangs +1; //all seem to get 1 extra lang
            string formula = null;
            if (RaceLangs != 0) formula = "+" +  RaceLangs.ToString() + " RaceLangs ";
            if (BonusLangs != 0) formula += "+" + BonusLangs.ToString() + " BonusLangs ";

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSkill(StatBlockInfo.SkillNames.LINGUISTICS))
            {
                foreach (SkillsInfo.SkillInfo Info in _sbCheckerBaseInput.SkillsValues)
                {
                    if (Info.Skill.SkillName == SkillData.SkillNames.Linguistics)
                    {
                        int rank = Info.Rank;
                        if ( _sbCheckerBaseInput.Race_Base.Name() == "Tengu") rank *= 2;
                        LangCount += rank;
                        formula += "+" + rank.ToString() + " Linguistics";
                        break;
                    }
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Cosmopolitan"))
            {
                LangCount += 2;
                formula += "+2 Cosmopolitan";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("oracle's curse (tongues)"))
            {
                LangCount++;
                formula += "+1 tongues";
            }

            if(_sbCheckerBaseInput.CharacterClasses.HasClass("druid"))
            {
                LangCount++;
                formula += "+1 Druidic";
            }

            if (SB_count == LangCount)
            {
                _sbCheckerBaseInput.MessageXML.AddPass(CheckName, formula);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, LangCount.ToString(), SB_count.ToString(),formula);
            }
        }

        #endregion Statistics

        #region Common Methods

        private void ComputeSizeMod()
        {
            _sizeData.SizeCat = StatBlockInfo.GetSizeEnum(_sbCheckerBaseInput.MonsterSB.Size);
            _sizeData.SizeMod = StatBlockInfo.GetSizeModifier(_sbCheckerBaseInput.MonsterSB.Size);
        }

        private void ComputeDodgeBonus()
        {
            _dodgeBonus = 0;
            bool isDodgeFeat = false;
            bool isMythicDodgeFeat = false;

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Dodge"))
            {
                _dodgeBonus++;
                isDodgeFeat = true;
                if (_sbCheckerBaseInput.MonsterSBSearch.HasMythicFeat("Dodge"))
                {
                    _dodgeBonus++;
                    isMythicDodgeFeat = true;
                }
            }

            //if (_monSBSearch.HasFeat("Combat Expertise"))
            //{
            //    DodgeBonus++;
            //    DodgeBonus += int.Parse(_sbCheckerBaseInput.MonsterSB.BaseAtk) / 4;
            //}

            int Mod = GetACModValue("dodge");
            if (Mod != 0)
            {
                if (isDodgeFeat) Mod--;
                if (isMythicDodgeFeat) Mod--;
                _dodgeBonus += Mod;
            }
        }

        private void ComputeTotalArmorCheckPenalty()
        {
            TotalArmorCheckPenaltyComputer totalArmorCheckPenaltyComputer = new TotalArmorCheckPenaltyComputer(_sbCheckerBaseInput, _equipmentData.Armor, _spellsData, _armorClassData);
            totalArmorCheckPenaltyComputer.ComputeTotalArmorCheckPenalty();  
        }

        private void ParseEquipment()
        {
            EquipmentParser equipmentParser = new EquipmentParser(_sbCheckerBaseInput, _sizeData, _armorClassData, _equipmentData,
                _magicItemStatBlockBusiness, _weaponBusiness, _armorBusiness, _equipmentGoodsBusiness);
            equipmentParser.ParseEquipment(_sbCheckerBaseInput.MonsterSBSearch);
        }    

        private void ParseACMods()
        {
            try
            {
                _armorClassData.ACMods_SB = new StatBlockInfo.ACMods(_sbCheckerBaseInput.MonsterSB.AC_Mods);
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("ParseACMods", ex.Message);
                return;
            }

            ACModParser acModParser = new ACModParser(_sbCheckerBaseInput, _equipmentData,
                      _armorClassData, ref _acDefendingMod, _sizeData.SizeMod);
            acModParser.ParseACMods();
        }                     

        private void ParseCreatureType()
        {
            _sbCheckerBaseInput.CreatureType = CreatureTypeManager.CreatureTypeDetailsWrapper.GetRaceDetailClass(_sbCheckerBaseInput.MonsterSB.Type);
            if (_sbCheckerBaseInput.CreatureType.Name == "UnknownCreatureType")
            {
                _sbCheckerBaseInput.MessageXML.AddFail("ParseCreatureType()", "CreatureType is Unknown--" + _sbCheckerBaseInput.MonsterSB.Type);
                // throw new Exception("Creature type undefined -- " + _sbCheckerBaseInput.MonsterSB.Type);
            }
         
        }

        private void ParseRace()
        {
            RaceParser raceParser = new RaceParser(_sbCheckerBaseInput, _monsterStatBlockBusiness);
            raceParser.ParseRace();             
        }

        private void ParseSubTypes()
        {
            List<string> SubTypes = CommonMethods.GetCreatureSubTypes();
            string subTypes = _sbCheckerBaseInput.MonsterSB.SubType;
            subTypes = subTypes.Replace(";", string.Empty);
            subTypes = Utility.RemoveParentheses(subTypes);
            if(subTypes.Length == 0) return;

            if (subTypes.Contains("augmented"))
            {                
                int Pos = subTypes.IndexOf("augmented") + "augmented".Length + 1;
                //we need to remove orig subtype too
                int Pos2 = -1;
                if (Pos < subTypes.Length)
                {
                    Pos2 = subTypes.IndexOf(",", Pos);
                }
                else
                {
                    Pos = subTypes.Length;
                }
                string temp = string.Empty;
                if (Pos2 == -1)
                {
                    temp = subTypes.Substring(Pos);
                    if (!string.IsNullOrEmpty(temp))
                    {
                        subTypes = subTypes.Replace(temp, string.Empty);
                    }
                }
                else
                {
                    temp = subTypes.Substring(Pos, Pos2 - Pos);
                    subTypes = subTypes.Replace(temp, string.Empty);
                }
            }

            List<string> subTypesSBList = subTypes.Split(',').Select(s => s.Trim()).ToList();
            subTypesSBList.RemoveAll(x => x== string.Empty);

            for (int a = subTypesSBList.Count - 1; a >= 0; a--)
            {
                subTypesSBList[a] = Utility.RemoveSuperScripts(subTypesSBList[a]);
                if (SubTypes.Contains(subTypesSBList[a])) subTypesSBList.RemoveAt(a);
                
            }

            subTypesSBList.Remove("Great Old One"); //only capitalized subtype

            if (subTypesSBList.Any())
            {
                _sbCheckerBaseInput.MessageXML.AddFail("Unknown SubType", string.Join(",", subTypesSBList.ToArray())); 
            }
        }

        private void ParseConstantSpells()
        {
            ConstantSpellsParser constantSpellsParser = new ConstantSpellsParser(_sbCheckerBaseInput, _spellsData, _spellStatBlockBusiness);
            constantSpellsParser.ParseConstantSpells();
        }

        // Stat Block values reflect whatever spells, potions etc were cast/used in Before Combat text
        // parse out italiced bloacks for later use
        private void ParseBeforeCombat()
        {
            BeforeCombatParser beforeCombatParser = new BeforeCombatParser(_sbCheckerBaseInput, _spellsData);
            beforeCombatParser.ParseBeforeCombat();          
        }

        private void ApplyBeforeCombatMagic()
        {
            BeforeCombatMagicParser beforeCombatMagicParser = new BeforeCombatMagicParser(_sbCheckerBaseInput, _spellsData,
                    _equipmentData.EquipementRoster, ref _onGoingAttackMod, _armorClassData, _spellStatBlockBusiness, _magicItemStatBlockBusiness);

            beforeCombatMagicParser.ApplyBeforeCombatMagic();
        }       

        private void ParseFeats()
        {
            _featManager = new FeatMaster();
            FeatParser featParser = new FeatParser(_sbCheckerBaseInput, _featManager, _featStatBlockBusiness);
            featParser.ParseFeats();
        }

        private void ParseClasses()
        {
            try
            {
                if (_sbCheckerBaseInput.MonsterSB.Class.Length != 0)
                {
                    string mystery = _sbCheckerBaseInput.MonsterSB.Mystery;
                    if (_sbCheckerBaseInput.MonsterSB.Class.Contains("shaman")) mystery = _sbCheckerBaseInput.MonsterSB.Spirit;
                    //   if (_sbCheckerBaseInput.MonsterSB.Bloodline.Contains("Spells Prepared")) throw new Exception("Spells Prepared in Bloodline");
                    _sbCheckerBaseInput.CharacterClasses = new ClassMaster(new ClassMasterInput(
                        _sbCheckerBaseInput.MonsterSB.Class, _sbCheckerBaseInput.MonsterSB.SpellDomains, mystery,
                        _sbCheckerBaseInput.MonsterSB.Bloodline, _sbCheckerBaseInput.MonsterSB.Patron ));
                }
                else
                {
                    _sbCheckerBaseInput.CharacterClasses = new ClassMaster(new ClassMasterInput(
                        string.Empty, string.Empty, string.Empty, string.Empty, string.Empty));
                }
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("ParseClasses", ex.Message);
                _sbCheckerBaseInput.CharacterClasses = new ClassMaster(new ClassMasterInput(
                        string.Empty, string.Empty, string.Empty, string.Empty, string.Empty));
            }
        }                           

        #endregion
    }
}