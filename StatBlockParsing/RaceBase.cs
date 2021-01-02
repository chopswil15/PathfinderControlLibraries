using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CreatureTypeManager;
using CommonStatBlockInfo;
using OnGoing;
using StatBlockCommon.Monster_SB;
using StatBlockCommon.Individual_SB;
using PathfinderGlobals;
using CreatureTypeFoundational;
using StatBlockBusiness;
using RaceFoundational;

namespace StatBlockParsing
{
    public class RaceBase
    {
        private MonsterStatBlock Race_SB;
        private IndividualStatBlock_Combat _indvSB;
        private RaceType BaseRaceType;
        public bool UseRacialHD {get; private set; }
        private bool _useTemplateValues = false;
        private StatBlockInfo.BABType BaseBABType;
        private CreatureTypeMaster CreatureTypeMaster;
        private StatBlockInfo.SaveBonusType FortOverride = StatBlockInfo.SaveBonusType.None;
        private StatBlockInfo.SaveBonusType RefOverride = StatBlockInfo.SaveBonusType.None;
        private StatBlockInfo.SaveBonusType WillOverride = StatBlockInfo.SaveBonusType.None;
        private Dictionary<string, int> _skillValues = new Dictionary<string, int>();

        public enum RaceType
        {
            None = 0,
            Race = 1,
            StatBlock = 2,
            BestiaryStatBlock = 3
        }

        public RaceFoundation RaceFoundationType { get; }

        public RaceType RaceBaseType
        {
            get
            {
                return BaseRaceType;
            }
        }

        public MonsterStatBlock RaceSB
        {
            get
            {
                return Race_SB;
            }
        }

        public RaceBase(object RaceValue, CreatureTypeFoundation CreatureType, string searchName, IMonsterStatBlockBusiness monsterStatBlockBusiness) : this (RaceValue, CreatureType, false, null, searchName, monsterStatBlockBusiness)
        { }

        public RaceBase(object RaceValue, CreatureTypeFoundation creatureType, bool isBestiaryStatBlock, 
            IndividualStatBlock_Combat IndivSB, string searchName, IMonsterStatBlockBusiness monsterStatBlockBusiness)
        {
            BaseRaceType = RaceType.None;
            if (RaceValue == null) return;
            _indvSB = IndivSB;

            CreatureTypeMaster = new CreatureTypeMaster();
            CreatureTypeMaster.CreatureTypeInstance = creatureType;
            try
            { 
                RaceFoundationType = (RaceFoundation)RaceValue;
                BaseRaceType = RaceType.Race;
                Race_SB = monsterStatBlockBusiness.GetBestiaryMonsterByNamePathfinderDefault(RaceFoundationType.Name);
                if (Race_SB == null)
                {
                    if (!string.IsNullOrEmpty(searchName) && searchName != RaceFoundationType.Name)
                    {
                        Race_SB = monsterStatBlockBusiness.GetBestiaryMonsterByNamePathfinderDefault(searchName);
                        if (Race_SB == null)
                        {
                            throw new Exception(RaceFoundationType.Name + " has no Core Race entry");
                        }
                    }
                    else
                    {
                        throw new Exception(RaceFoundationType.Name + " has no Core Race entry");
                    }
                }
              //  IsHumanoid = true;
            }
            catch
            {
                try
                {
                    Race_SB = (MonsterStatBlock)RaceValue;
                    if (Race_SB.Environment.Length > 0 && isBestiaryStatBlock)
                    {
                        BaseRaceType = RaceType.BestiaryStatBlock;
                    }
                    else
                    {
                        BaseRaceType = RaceType.StatBlock;
                    }
                    if (Race_SB.Race.Contains("humanoid")|| Race_SB.Type.Contains("humanoid"))
                    {
                     //   IsHumanoid = true;                        
                    }
                    UseRacialHD = Race_SB.DontUseRacialHD ? false : true;

                    int HD = RacialHDValue();
                    if (CreatureTypeMaster.GetSaveType("will") == StatBlockInfo.SaveBonusType.Varies)
                    {
                        int Will = Convert.ToInt32(Race_SB.Will);
                        if (HasFeat("Iron Will"))
                        {
                            Will -= 2;
                        }
                        WillOverride = StatBlockInfo.ComputeSaveBonusType(HD, Race_SB.GetAbilityScoreValue(StatBlockInfo.WIS), Will);
                    }
                    if (CreatureTypeMaster.GetSaveType("ref") == StatBlockInfo.SaveBonusType.Varies)
                    {
                        int Ref = Convert.ToInt32(Race_SB.Ref);
                        if (HasFeat("Lightning Reflexes"))  Ref -= 2;
                        Ref -= GetRefOnGoingMods();
                        RefOverride = StatBlockInfo.ComputeSaveBonusType(HD, Race_SB.GetAbilityScoreValue(StatBlockInfo.DEX), Ref);
                    }
                    if (CreatureTypeMaster.GetSaveType("fort") == StatBlockInfo.SaveBonusType.Varies)
                    {
                        int Fort = Convert.ToInt32(Race_SB.Fort);
                        if (HasFeat("Great Fortitude"))  Fort -= 2;

                        FortOverride = StatBlockInfo.ComputeSaveBonusType(HD, Race_SB.GetAbilityScoreValue(StatBlockInfo.CON), Fort);
                    }
                    BaseBABType = StatBlockInfo.ComputeBABType(RacialHDValue(), Convert.ToInt32(Race_SB.BaseAtk));
                    ParseSkills();
                }
                catch
                { 
                  
                }                
            }
        }

        private int GetRefOnGoingMods()
        {
            int OnGoingMods = 0;
            if (_indvSB != null)
            {
                OnGoingMods = _indvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow, OnGoingStatBlockModifier.StatBlockModifierSubTypes.None);
                OnGoingMods += _indvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow, OnGoingStatBlockModifier.StatBlockModifierSubTypes.SavingThrow_Ref);
            }
            return OnGoingMods;
        }

        private void ParseSkills()
        {
            List<string> skillsList = Race_SB.Skills.Split(',').ToList();
            int Pos, value;
            string temp, temp2;

            foreach(string skill in skillsList)
            {
                temp = skill.Trim();
                Pos = temp.IndexOf("+");
                if (Pos == -1)
                {
                    Pos = temp.IndexOf("-");
                }
                temp = temp.Substring(0, Pos - 1).Trim();
                temp2 = skill.Replace(temp, string.Empty);
                value = Convert.ToInt32(temp2);
                _skillValues.Add(temp, value);
            }
        }

        private bool HasFeat(string FeatName)
        {
            return Race_SB.Feats.Contains(FeatName);            
        }

        public string RaceWeapons()
        {
            if(RaceBaseType != RaceType.Race) return string.Empty;
            return RaceFoundationType.RaceWeapons;
        }

        public string GetRacialResistance()
        {
            if (BaseRaceType == RaceType.Race)
                return RaceFoundationType.Resistance;

            return string.Empty;
        }


        public string GetRacialSenses()
        {
            if (BaseRaceType == RaceType.Race)
                return RaceFoundationType.Senses;

            return string.Empty;
        }

        public string GetRacialImuunities()
        {
            if (BaseRaceType == RaceType.Race)
                return RaceFoundationType.Immune;

            return string.Empty;
        }

        public void ApplyTemplatedRaceSB(MonsterStatBlock TemplatedRaceSB, bool use_template_values)
        {
            if (TemplatedRaceSB != null)
            {                
                Race_SB = TemplatedRaceSB;
                _useTemplateValues = use_template_values;
            }
        }

        public string GetTemplateRacialMods()
        {
            if (_useTemplateValues)
                return Race_SB.RacialMods;

            return string.Empty;
        }

        public Dictionary<string, int> RacialSkillMods()
        {
            if (BaseRaceType == RaceType.Race)
                return RaceFoundationType.SkillsRacialBonus();

            return new Dictionary<string, int>();
        }

        public List<OnGoingStatBlockModifier> RacialOnGoingMods()
        {
            if (BaseRaceType == RaceType.Race)
                return RaceFoundationType.GetRaceOnGoingModifiers();

            return new List<OnGoingStatBlockModifier>();
        }

        public List<string> RaceLanguages()
        {
            switch (BaseRaceType)
            {
                case RaceType.Race:
                    return RaceFoundationType.RaceLanguages;
                case RaceType.StatBlock:
                case RaceType.BestiaryStatBlock:
                case RaceType.None:
                    return new List<string>();
            }
            return null;
        }

        public string Name()
        {
            switch (BaseRaceType)
            {
                case RaceType.Race:
                    return RaceFoundationType.Name;
                case RaceType.StatBlock:
                case RaceType.None:
                    return string.Empty;             
            }
            return null;
        }

        public string Size()
        {
            switch (BaseRaceType)
            {
                case RaceType.Race:
                    return StatBlockInfo.GetSizeFromEnum(RaceFoundationType.Size);
                case RaceType.StatBlock:
                    return Race_SB.Size;
                case RaceType.None:
                    return string.Empty;
            }
            return null;
        }

        public int BaseSpeed()
        {
            switch (BaseRaceType)
            {
                case RaceType.Race:
                    return RaceFoundationType.BaseSpeed;                    
                case RaceType.StatBlock:
                case RaceType.None:
                    return -1;                    
            }
            return -1;
        }

        public int BonusFeatCount()
        {
            switch (BaseRaceType)
            {
                case RaceType.Race:
                    int temp = RaceFoundationType.BonusFeatCount();
                    if (Race_SB != null)
                        temp += ComputeBonusFeats();

                    return temp;                    
                case RaceType.StatBlock:                   
                    if (!UseRacialHD) return 0; // determined by class
                    if (Race_SB.Feats.Length > 0)
                        return ComputeBonusFeats(); 
                    else
                        return 0;
                case RaceType.None:
                    return 0;                    
            }
            return -100;
        }

        public int HDFeats()
        {
            switch (BaseRaceType)
            {
                case RaceType.Race:
                    return 0;
                case RaceType.StatBlock:
                    if (!UseRacialHD) return 0; // determined by class
                    if (Race_SB.Feats.Length > 0)
                        return StatBlockInfo.GetHDFeats(RacialHDValue());
                    else
                        return 0;
                case RaceType.None:
                    return 0;
            }
            return -100;
        }

        private int ComputeBonusFeats()
        {
            if(BaseRaceType == RaceType.StatBlock || (BaseRaceType == RaceType.Race && Race_SB != null))
            {
                if (Race_SB.Feats.Length == 0) return 0;
                string tempFeatsBlock = Race_SB.Feats;
                ParenCommaFix(ref tempFeatsBlock);
                List<string> Feats = tempFeatsBlock.Split(',').ToList();
                Feats.RemoveAll(x => x== string.Empty);
                string temp = string.Empty;
                int Count = 0;

                foreach (string feat in Feats)
                {
                    temp = feat.Trim();
                    if (temp.LastIndexOf("B") == temp.Length - 1)
                        Count++;
                }
                return Count;
            }
            return 0;
        }

        private void ParenCommaFix(ref string Block)
        {
            int LeftParenPos = Block.IndexOf(PathfinderConstants.PAREN_LEFT);
            int RightParenPos = Block.IndexOf(PathfinderConstants.PAREN_RIGHT);
            int CommaPos = Block.IndexOf(",");
            if (LeftParenPos == -1) return;

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

        public int RaceBAB()
        {
            switch (BaseRaceType)
            {
                case RaceType.Race:
                    return 0;
                case RaceType.StatBlock:
                case RaceType.BestiaryStatBlock:
                    if (!UseRacialHD) return 0; // determined by class
                    if (Race_SB.BaseAtk.Length > 0)
                        return StatBlockInfo.ComputeBAB(RacialHDValue(), BaseBABType);
                    else
                        return 0;              
                case RaceType.None:
                    return 0;
            }
            return -100;
        }

        public int RaceFort()
        {
            switch (BaseRaceType)
            {
                case RaceType.Race:
                    return 0;
                case RaceType.StatBlock:
                case RaceType.BestiaryStatBlock:
                   // if (IsHumanoid) return 0; // determined by class
                    if (!UseRacialHD) return 0; // determined by class
                    if (_useTemplateValues)
                    {
                        int modFort = StatBlockInfo.GetAbilityModifier(GetAbilityScore(FortMod()));
                        return Convert.ToInt32(Race_SB.Fort) - modFort;
                    }
                    else
                    {
                        if (FortOverride == StatBlockInfo.SaveBonusType.None)
                        {
                            StatBlockInfo.HDBlockInfo racialHDInfo = new StatBlockInfo.HDBlockInfo();
                            racialHDInfo.ParseHDBlock(RacialHD());
                            return CreatureTypeMaster.GetFortSaveValue(racialHDInfo.Multiplier);
                        }
                        else
                        {
                            return StatBlockInfo.ParseSaveBonues(RacialHDValue(), FortOverride);
                        }
                    }
                case RaceType.None:
                    return 0;
            }
            return -1;
        }

        public int RaceRef()
        {
            switch (BaseRaceType)
            {
                case RaceType.Race:
                    return 0;
                case RaceType.StatBlock:
                case RaceType.BestiaryStatBlock:
                   // if (IsHumanoid) return 0; // determined by class
                    if (!UseRacialHD) return 0; // determined by class
                    if (_useTemplateValues)
                    {
                        int modRef = StatBlockInfo.GetAbilityModifier(GetAbilityScore(StatBlockInfo.DEX));
                        return Convert.ToInt32(Race_SB.Ref) - modRef;
                    }
                    else
                    {
                        if (RefOverride == StatBlockInfo.SaveBonusType.None)
                        {
                            StatBlockInfo.HDBlockInfo racialHDInfo = new StatBlockInfo.HDBlockInfo();
                            racialHDInfo.ParseHDBlock(RacialHD());
                            return CreatureTypeMaster.GetRefSaveValue(racialHDInfo.Multiplier);
                        }
                        else
                        {
                            return StatBlockInfo.ParseSaveBonues(RacialHDValue(), RefOverride);
                        }
                    }
                case RaceType.None:
                    return 0;
            }
            return -1;
        }

        public int RaceWill()
        {
            switch (BaseRaceType)
            {
                case RaceType.Race:
                    return 0;
                case RaceType.StatBlock:
                case RaceType.BestiaryStatBlock:
                   // if (IsHumanoid) return 0; // determined by class
                    if (!UseRacialHD) return 0; // determined by class
                    if (_useTemplateValues)
                    {
                        int modWill = StatBlockInfo.GetAbilityModifier(GetAbilityScore(StatBlockInfo.WIS));
                        return Convert.ToInt32(Race_SB.Will) - modWill;
                    }
                    else
                    {
                        if (WillOverride == StatBlockInfo.SaveBonusType.None)
                        {
                            StatBlockInfo.HDBlockInfo racialHDInfo = new StatBlockInfo.HDBlockInfo();
                            racialHDInfo.ParseHDBlock(RacialHD());
                            return CreatureTypeMaster.GetWillSaveValue(racialHDInfo.Multiplier);
                        }
                        else
                        {
                            return StatBlockInfo.ParseSaveBonues(RacialHDValue(), WillOverride);
                        }
                    }
                case RaceType.None:
                    return 0;
            }
            return -1;
        }

        public int RaceNaturalArmor()
        {
            switch (BaseRaceType)
            {
                case RaceType.Race:
                    if (Race_SB != null)
                    {
                        return GetNaturalMod();
                    }
                    return 0;
                case RaceType.StatBlock:
                case RaceType.BestiaryStatBlock:                   
                    if (!UseRacialHD) return 0; // determined by class 
                    return GetNaturalMod();
                case RaceType.None:
                    return 0;
            }
            return -100;
        }

        private int GetNaturalMod()
        {            
            if (Race_SB.AC_Mods.Contains("natural"))
            {
                int Pos = Race_SB.AC_Mods.IndexOf(PathfinderConstants.PAREN_RIGHT);
                string temp = Race_SB.AC_Mods.Substring(0, Pos);
                Pos = temp.IndexOf("|");
                if (Pos >= 0) temp = temp.Substring(0, Pos);
                if (temp.Contains(" vs")) throw new Exception("Race Base:GetNaturalMod - vs in AC mods -" + temp);
                temp = temp.Replace(PathfinderConstants.PAREN_LEFT, string.Empty).Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                List<string> Mods = temp.Split(',').ToList();
                foreach (string mod in Mods)
                {
                    if (mod.Contains("natural"))
                    {
                        temp = mod.Replace("natural armor", string.Empty).Replace("natural", string.Empty).Trim();
                        return Convert.ToInt32(temp);
                    }
                }
            }
            return 0;
        }

        public int AbilityScoreSum()
        {
            List<string> AbilityNames = new List<string>{StatBlockInfo.STR,StatBlockInfo.INT,StatBlockInfo.WIS, StatBlockInfo.DEX,StatBlockInfo.CON, StatBlockInfo.CHA};
            int sum = 0;
            foreach(string name in AbilityNames)
            {
                sum += Race_SB.GetAbilityScoreValue(name);
            }
            return sum;
        }

        public int GetAbilityScore(string ScoreName)
        {
            return Race_SB.GetAbilityScoreValue(ScoreName);
        }

        public string RacialHD()
        {
            switch (BaseRaceType)
            {
                case RaceType.Race:
                    return string.Empty;
                case RaceType.StatBlock:
                case RaceType.BestiaryStatBlock:
                    if (!UseRacialHD) return string.Empty;

                    return Race_SB.HD;
                case RaceType.None:
                    return string.Empty;
            }
            return string.Empty;
        }

        public string FortMod()
        {
            string mod = StatBlockInfo.CON;

            if (CreatureTypeMaster != null && CreatureTypeMaster.CreatureTypeInstance != null)
            {
                if (CreatureTypeMaster.CreatureTypeInstance.FortMod().Length > 0)
                {
                    mod = CreatureTypeMaster.CreatureTypeInstance.FortMod();
                }
            }

            return mod;
        }

        public int RacialHDValue()
        {
            StatBlockInfo.HDBlockInfo racialHDInfo = new StatBlockInfo.HDBlockInfo();
            racialHDInfo.ParseHDBlock(RacialHD());
            return racialHDInfo.Multiplier;
        }

        public bool HasSpecialAbility(string SpecialAbility)
        {
            if (Race_SB != null)
            {
                if (Race_SB.SpecialAbilities.Contains(SpecialAbility)) return true;
            }
            return false;
        }
    }
}
