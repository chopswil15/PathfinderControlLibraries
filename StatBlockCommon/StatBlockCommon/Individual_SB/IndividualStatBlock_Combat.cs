using System;
using System.Collections.Generic;
using System.Linq;
using OnGoing;
using Extensions;
using ClassManager;
using CommonInterFacesDD;
using MagicItemAbilityWrapper;
using CommonStatBlockInfo;
using StatBlockCommon.Spell_SB;
using StatBlockCommon.MagicItem_SB;
using StatBlockCommon.ReflectionWrappers;
using Utilities;
using PathfinderGlobals;

namespace StatBlockCommon.Individual_SB
{
    public class IndividualStatBlock_Combat : IndividualStatBlock
    {
        private StatBlockInfo.Health meHealth;        
        private List<OnGoingCondition> OnGoingConditions = new List<OnGoingCondition>();
        private List<MagicItemAbilitiesWrapper> MagicItemAbilities = new List<MagicItemAbilitiesWrapper>();
        private List<OnGoingPower> OnGoingPowers = new List<OnGoingPower>();
       // private List<StatBlockGlobals.OnGoingStatBlockEffect> OnGoingStatBlockEffects = new List<StatBlockGlobals.OnGoingStatBlockEffect>();
        private List<OnGoingDamage> OnGoingDamages = new List<OnGoingDamage>();
        private Dictionary<string, StatBlockInfo.EffectModifierType> EffectModifiers = new Dictionary<string, StatBlockInfo.EffectModifierType>();
        private List<StatBlockGlobals.Ability> AbilityScores;
        private List<OnGoingStatBlockModifier> OnGoingStatBlockMods = new List<OnGoingStatBlockModifier>();
        private Dictionary<string, StatBlockGlobals.StatBlockModifierTotal> StatBlockModTotalList = new Dictionary<string,StatBlockGlobals.StatBlockModifierTotal>();
        private Dictionary<IEquipment, int> EquipementRoster = new Dictionary<IEquipment, int>();
        private ConditionTypes ConditionsInEffect;
        private StatBlockGlobals.AC_Values Indvid_AC_Values;
        private StatBlockGlobals.BodySlots Indvid_BodySlots;
        
        private ClassMaster CharacterClasses { get; set; }
        private int CurrentHP;
        private int TemporaryHP;
        public int AttackRollMod { get; set; }
        public int NonLeathalHP { get; set; }
        public string CombatGroup { get; set; }
        public float SaveModifer { get; set; }
        public  List<string> Warnings { get; set; }
        public StatBlockInfo.SizeCategories SizeCategory { get; set; }

        public IndividualStatBlock_Combat()
        {
            AttackRollMod = 0;
            Warnings = new List<string>();
            ConditionsInEffect = ConditionTypes.None;
        }

        public void ParseStatBlockData()
        {
            ParseAC_Types();
            ParseAbilityScores();
            int Pos = HP.IndexOf(PathfinderConstants.SPACE);
            if (Pos == -1)
            {
                Pos = HP.Length;
            }
            CurrentHP = Convert.ToInt32(HP.Substring(0, Pos));
            SaveModifer = 0;
            SizeCategory = (StatBlockInfo.SizeCategories)Enum.Parse(typeof(StatBlockInfo.SizeCategories), Size);            
            ParseClasses();
            OnGoingPowers.AddRange(CharacterClasses.GetAllClassPowers());
        }

        public int FindClassLevel(string ClassName)
        {
            return CharacterClasses.FindClassLevel(ClassName);
        }

        public int GetCurrentHP()
        {
            return CurrentHP;
        }

        public int GetCurrentHPPercent()
        {
            return (CurrentHP / GetMaxHP()) * 100;
        }

        public void AddTemporaryHP(int tempHP)
        {
            TemporaryHP += tempHP;
        }

        public int GetTemporaryHP()
        {
            return TemporaryHP;
        }

        public void TakeDamage(int Damage)
        {
            if (Damage > 0) //must be negative
            {
                Damage = Damage * -1;
            }

            if (TemporaryHP > 0)
            {
                if (Damage > TemporaryHP)
                {
                    Damage += TemporaryHP;
                }
                else
                {
                    TemporaryHP += Damage;
                }
            }

            if (Damage < 0)
            {
                CurrentHP += Damage;
            }
            UpdateHealth();
        }

        public void ReceiveHealing(int Healing)
        {
            if (Healing < 0)//must be positive
            {
                Healing = Healing * -1;
            }
            CurrentHP += Healing;
            UpdateHealth();
        }
        
        public int GetHD()
        {
            string temp = HD;
            int Pos = temp.IndexOf("d");
            temp = temp.Substring(0, Pos).Trim();
            return Convert.ToInt32(temp);
        }

        public bool IsUndead()
        {
            return Type.Contains("undead");            
        }

        //public static IndividualStatBlock_Combat GetByID(int ID)
        //{
        //    IndividualStatBlock SB = IndividualStatBlock_Select.GetByID(ID);
        //    Type type = SB.GetType();
        //    type = type.BaseType;
        //    type = type.BaseType;
        //    IndividualStatBlock_Combat SBC = new IndividualStatBlock_Combat();
        //    StatBlockGlobals.UpdateForType(type, SB, SBC);
        //    return SBC;
        //}       

        public void AddOnGoingStatBlockMod(OnGoingStatBlockModifier SBM)
        {
            StatBlockGlobals.StatBlockModifierTotal temp;

            OnGoingStatBlockMods.Add(SBM);

            if (StatBlockModTotalList.ContainsKey(SBM.Name))
            {
                temp = StatBlockModTotalList[SBM.Name];
                temp.Modifier = temp.Modifier + SBM.Modifier;
                temp.ModifierList = temp.ModifierList + ", " + SBM.Modifier.ToString() + PathfinderConstants.SPACE + SBM.Name;
                StatBlockModTotalList[SBM.Name] = temp;
            }
            else //new
            {
                temp = new StatBlockGlobals.StatBlockModifierTotal();
                temp.Modifier = SBM.Modifier;
                temp.ModifierList = SBM.Modifier.ToString() + PathfinderConstants.SPACE + SBM.Name;
                StatBlockModTotalList[SBM.Name] = temp;
            }
        }

        public void UpdateOnGoingItems()
        {
            if (OnGoingConditions.Any())
            {
                UpdateOnGoingConditions();
            }
            
            if (OnGoingStatBlockMods.Any())
            {
                UpdateStatBlockMods();
            }
        }

        private void UpdateOnGoingConditions()
        {
            OnGoingCondition conditionTemp;

            //count down in case one is removed
            for (int a = OnGoingConditions.Count() - 1; a >= 0; a--)
            {
                conditionTemp = OnGoingConditions[a];               
                if (conditionTemp.Duration > 0)
                {
                    conditionTemp.Duration--;
                    if (conditionTemp.Duration == 0)
                    {
                        SetCondition(conditionTemp.ConditionType, false);
                        OnGoingConditions.Remove(conditionTemp);
                    }
                    else
                    {
                        OnGoingConditions[a] = conditionTemp;
                    }
                }
            }
        }

        private void UpdateStatBlockMods()
        {
            OnGoingStatBlockModifier tempSBM;
            StatBlockGlobals.StatBlockModifierTotal temp;

            StatBlockModTotalList = new Dictionary<string, StatBlockGlobals.StatBlockModifierTotal>(); //re-do mods each round

            foreach (OnGoingStatBlockModifier SBM in OnGoingStatBlockMods)
            {
                tempSBM = SBM;

                if (tempSBM.Duration > 0) // -1 duration = on-going
                {
                    tempSBM.Duration--;
                }

                if (tempSBM.Duration == 0)
                {
                    OnGoingStatBlockMods.Remove(tempSBM);
                }
                else
                {
                    if (StatBlockModTotalList.ContainsKey(tempSBM.Name))
                    {
                        temp = StatBlockModTotalList[tempSBM.Name];
                        temp.Modifier = temp.Modifier + tempSBM.Modifier;
                        temp.ModifierList = temp.ModifierList + ", " + tempSBM.Modifier.ToString() + PathfinderConstants.SPACE + tempSBM.Name;
                        StatBlockModTotalList[tempSBM.Name] = temp;
                    }
                    else //new
                    {
                        temp = new StatBlockGlobals.StatBlockModifierTotal();
                        temp.Modifier = tempSBM.Modifier;
                        temp.ModifierList = tempSBM.Modifier.ToString() + PathfinderConstants.SPACE + tempSBM.Name;
                        StatBlockModTotalList[tempSBM.Name] = temp;
                    }
                }
            }
        }

        private void ParseAbilityScores()
        {
            AbilityScores = new List<StatBlockGlobals.Ability>();
            List<string> tempability = base.AbilityScores.Split(',').ToList();
            int cnt = 1;
            int Pos = 0;
            string temp2 = string.Empty;

            foreach (string temp in tempability)
            {
                temp2 = temp.Trim();
                Pos = temp2.IndexOf(PathfinderConstants.SPACE);
                temp2 = temp2.Remove(0, Pos).Trim();
                StatBlockGlobals.Ability Hold = new StatBlockGlobals.Ability((StatBlockInfo.AbilityName)cnt, Convert.ToInt32(temp2));
                AbilityScores.Add(Hold);
            }
        }

        public int FindAbilityScoreBonus(StatBlockInfo.AbilityName Name)
        {
            foreach (StatBlockGlobals.Ability ability in AbilityScores)
            {
                if (ability.Name == Name)
                {
                    return ability.AbilityModifier;
                }
            }
            return 0;
        }

        private void ParseAC_Types()
        {
            string TempAC = AC;
            TempAC = TempAC.Replace("AC", string.Empty);
            int lPos = TempAC.IndexOf(",");
            string temp = TempAC.Substring(0, lPos);
            Indvid_AC_Values.Base = Convert.ToInt32(temp.Trim());
            TempAC = TempAC.Remove(0, lPos + 1);

            lPos = TempAC.IndexOf(",");
            temp = TempAC.Substring(0, lPos);
            TempAC = TempAC.Remove(0, lPos + 1);
            temp = temp.Replace("touch", string.Empty);
            Indvid_AC_Values.Touch = Convert.ToInt32(temp.Trim());


            lPos = TempAC.IndexOf(";");
            if (lPos == -1)
            {
                lPos = TempAC.Length;
            }
            temp = TempAC.Substring(0, lPos);
            temp = temp.Replace("flat-footed", string.Empty);
            Indvid_AC_Values.FlatFooted = Convert.ToInt32(temp.Trim());
        }

        private void ParseClasses()
        {
            if (Class.Length != 0)
            {
                //string temp = Classes;
                CharacterClasses = new ClassMaster(Class, SpellDomains, Mystery,Bloodline, Patron);
            }
            else
            {
                CharacterClasses = new ClassMaster(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            }
        }
        

        public int SelectAC(StatBlockInfo.ACType eACType)
        {
            switch (eACType)
            {
                case StatBlockInfo.ACType.Base:
                    return Indvid_AC_Values.Base;
                case StatBlockInfo.ACType.Touch:
                    return Indvid_AC_Values.Touch;
                case StatBlockInfo.ACType.FlatFooted:
                    return Indvid_AC_Values.FlatFooted;
                default:
                    return -100;
            }
        }

        public int SelectAC(bool Touch)
        {
            if (Touch)
            {
                return SelectAC(StatBlockInfo.ACType.Touch);
            }
            else
            {
                if (CheckCondition(ConditionTypes.FlatFooted))
                {
                    return SelectAC(StatBlockInfo.ACType.FlatFooted);
                }
                else
                {
                    return SelectAC(StatBlockInfo.ACType.Base);
                }
            }
        }

        public int GetSizeModifier()
        {
            switch (Size)
            {
                case "Colossal":
                    return -8;
                case "Gargantuan":
                    return -4;
                case "Huge":
                    return -2;
                case "Large":
                    return -1;
                case "Medium":
                    return 0;
                case "Small":
                    return 1;
                case "Tiny":
                    return 2;
                case "Diminutive":
                    return 4;
                case "Fine":
                    return 8;
                default:
                    return -100;
            }
        }

        public int GetMaxHP()
        {
            string sTemp = string.Empty;

            int lPos = HP.IndexOf(char.Parse(PathfinderConstants.SPACE));
            if (lPos > 0)
            {
                sTemp = HP.Substring(0, lPos - 1);
            }
            else
            {
                sTemp = HP;
            }
            return Convert.ToInt32(sTemp);
        }

        public string GetHealth()
        {
            return Enum.GetName(typeof(StatBlockInfo.Health), meHealth);
        }

        public void UpdateHealth()
        {
            if (CurrentHP > 0)
            {
                meHealth = StatBlockInfo.Health.Alive;
                return;
            }

            if (CurrentHP < 0 && CurrentHP >= -9)
            {
                meHealth = StatBlockInfo.Health.Dying;
                return;
            }

            if (CurrentHP == 0)
            {
                meHealth = StatBlockInfo.Health.Disabled;
                return;
            }
            meHealth = StatBlockInfo.Health.Dead;
        }

        private void AddCondition(string Condition)
        {
            //use AddOnGoingCondition

            ConditionTypes Cond = (ConditionTypes)Enum.Parse(typeof(ConditionTypes), Condition);
            ConditionsInEffect = ConditionsInEffect.Set(Cond);
            // msConditions.Add(Cond);
        }

        private void RemoveCondition(string Condition)
        {
            //when AddOnGoingCondition duration expires Condtion is removed            
            ConditionTypes Cond = (ConditionTypes)Enum.Parse(typeof(ConditionTypes), Condition);
            ConditionsInEffect = ConditionsInEffect.Clear(Cond);
        }       

        public void StartCombatRound()
        {
            SetCondition(ConditionTypes.FlatFooted, true);
            UpdateOnGoingItems();
        }

        public void StartInitiative()
        {
            SetCondition(ConditionTypes.FlatFooted, false);
        }

        public bool CheckCondition(string Condition)
        {            
            ConditionTypes Cond = (ConditionTypes)Enum.Parse(typeof(ConditionTypes), Condition);
            return ConditionsInEffect.IsSet(Cond);
        }

        public bool CheckCondition(ConditionTypes Condition)
        {
            string ConditionName = Enum.GetName(typeof(ConditionTypes), Condition);
            return CheckCondition(ConditionName);
        }        

        public void SetCondition(ConditionTypes Condition, bool Status)
        {         
            if (Status == true)
            {
                ConditionsInEffect = ConditionsInEffect.Set(Condition);
            }
            else
            {
                ConditionsInEffect = ConditionsInEffect.Clear(Condition);
            }
        }

        public int GetOnGoingConditionCount()
        {
            return OnGoingConditions.Count;
        }

        public List<OnGoingCondition> GetOnGoingConditions()
        {
            return OnGoingConditions;
        }       

        public void AddOnGoingCondition(OnGoingCondition Condition)
        {
            OnGoingConditions.Add(Condition);
            SetCondition(Condition.ConditionType, true);
            ConditionReflectionWrapper.ApplyCondition(this, Condition);
        }

        public void AddOnGoingPower(OnGoingPower Power)
        {
            OnGoingPowers.Add(Power);
        }        

        public int PowerTypeCount(OnGoingPower.PowerBaseType PowerType)
        {
            int cnt = 0;
            foreach (OnGoingPower Power in OnGoingPowers)
            {
                if (Power.PowerBase == PowerType)
                {
                    cnt++;
                }
            }
            return cnt;
        }

        public List<string> PowerSourceList(OnGoingPower.PowerBaseType PowerType)
        {
            List<string> temp = new List<string>();
            string hold = string.Empty;

            foreach (OnGoingPower Power in OnGoingPowers)
            {
                if (Power.PowerBase == PowerType)
                {
                    hold = Power.PowerSource;
                    if (PowerType == OnGoingPower.PowerBaseType.Class)
                    {
                        hold = Power.Name;
                    }
                    temp.Add(hold);
                }
            }
            return temp;
        }       
       
        
        public List<OnGoingStatBlockModifier> GetOnGoingStatBlockMods()
        {
            return OnGoingStatBlockMods;
        }

        public int GetOnGoingStatBlockModsCount()
        {
            return OnGoingStatBlockMods.Count();
        }

        public int GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes Type,
                                               OnGoingStatBlockModifier.StatBlockModifierSubTypes SubType)
        {
            string fou = string.Empty;
            return GetOnGoingStatBlockModValue(Type, SubType, false, ref fou);

        }

        public int GetOnGoingStatBlockModValueStackable(OnGoingStatBlockModifier.StatBlockModifierTypes Type,
                                               OnGoingStatBlockModifier.StatBlockModifierSubTypes SubType, bool CheckStackable)
        {
            string fou = string.Empty;
            return GetOnGoingStatBlockModValue(Type, SubType, CheckStackable, ref fou);

        }

        public int GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes Type, 
                                               OnGoingStatBlockModifier.StatBlockModifierSubTypes SubType, bool CheckStackable ,ref string formula)
        {
            int modValue = 0;
            Dictionary<OnGoingStatBlockModifier.StatBlockModifierSubTypes, OnGoingStatBlockModifier> StackableValues = new Dictionary<OnGoingStatBlockModifier.StatBlockModifierSubTypes, OnGoingStatBlockModifier>();

            foreach (OnGoingStatBlockModifier mod in OnGoingStatBlockMods)
            {
                if (mod.ModType == Type && mod.SubType == SubType && mod.ConditionGroup == string.Empty)
                {
                    if (CheckStackable)
                    {
                        if (StackableValues.ContainsKey(SubType))
                        {
                            if (StackableValues[SubType].Modifier < mod.Modifier)
                            {
                                StackableValues[SubType].Modifier = mod.Modifier;
                            }
                        }
                        else
                        {
                            StackableValues.Add(SubType,mod);
                        }
                    }
                    else
                    {
                        modValue += mod.Modifier;
                        formula += " +" + mod.Modifier.ToString() + PathfinderConstants.SPACE + mod.Name;
                    }
                }
            }

            if (CheckStackable)
            {
                foreach (KeyValuePair<OnGoingStatBlockModifier.StatBlockModifierSubTypes, OnGoingStatBlockModifier> kvp in StackableValues)
                {
                    modValue += kvp.Value.Modifier;
                    formula += " +" + kvp.Value.Modifier.ToString() + PathfinderConstants.SPACE + kvp.Value.Name;
                }
            }

            return modValue;
        }

        public int GetOnGoingStatBlockSkillAbilityModValue(OnGoingStatBlockModifier.StatBlockModifierTypes Type, OnGoingStatBlockModifier.StatBlockModifierSubTypes SubType, ref string formula)
        {
            int modValue = 0;
            foreach (OnGoingStatBlockModifier mod in OnGoingStatBlockMods)
            {
                if (mod.ModType == Type && mod.SubType == SubType)
                {
                    modValue += mod.Modifier;
                    formula += " +" + mod.Modifier.ToString() + PathfinderConstants.SPACE + mod.Name;
                }
            }

            return modValue;
        }

        public int GetOnGoingStatBlockSkillModValue(OnGoingStatBlockModifier.StatBlockModifierTypes Type, string SkillName, ref string formula)
        {
            int modValue = 0;
            foreach (OnGoingStatBlockModifier mod in OnGoingStatBlockMods)
            {
                if (mod.ModType == Type && mod.SubType == OnGoingStatBlockModifier.StatBlockModifierSubTypes.Skill_Name && mod.ConditionGroup == SkillName)
                {
                    modValue += mod.Modifier;
                    formula += " +" + mod.Modifier.ToString() + PathfinderConstants.SPACE + mod.Name;
                }
            }

            return modValue;
        }

        public int GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes Type)
        {
            int modValue = 0;
            foreach (OnGoingStatBlockModifier mod in OnGoingStatBlockMods)
            {
                if (mod.ModType == Type)
                {
                    modValue += mod.Modifier;
                }
            }

            return modValue;
        }
   
        public List<OnGoingDamage> GetOnGoingDamages()
        {
            return OnGoingDamages;
        }

        public void AddOnGoingDamage(OnGoingDamage OnGoingDamage)
        {
            OnGoingDamages.Add(OnGoingDamage);
        }

        public string CastSpell(string SpellName, SpellStatBlock_Combat SpellData, List<IndividualStatBlock_Combat> Targets)
        {     
            return SpellReflectionWrapper.CastSpell(SpellName, SpellData, Targets);          
        }

        public string CastSpell(string SpellName, int CasterLevel, List<IndividualStatBlock_Combat> Targets)
        {          
            return SpellReflectionWrapper.CastSpell(SpellName, CasterLevel, Targets);
        }       

        public string ApplyMagicItem(IEquipment MagicItem)
        {
            MagicItemStatBlock MI = (MagicItemStatBlock) MagicItem;
            try
            {
                MagicItemAbilities.Add(MagicItemAbilityReflectionWrapper.GetMagicItemAbility(MI.name, MI.ExtraAbilities));
            }
            catch (Exception ex)
            {
                throw new Exception("IndividualStatBlock_Combat-ApplyMagicItem", ex);
            }
          
            return MI.name + " abilities added.";
        }


        public List<MagicItemAbilitiesWrapper> GetMagicItemAbilities()
        {
            return MagicItemAbilities;
        }
    }
}
