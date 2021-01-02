using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
using CommonStatBlockInfo;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Utilities;
using PathfinderGlobals;

namespace StatBlockCommon.Monster_SB
{
    [Serializable]
    [XmlRootAttribute(ElementName = "Monster", IsNullable = false)]
    public class MonsterStatBlock : IMonsterStatBlock
    {       

        #region Properties
        public int id { get; set; }
        public string name { get; set; }
        public string CR { get; set; }
        public string XP { get; set; }
        public string Race { get; set; }
        public string Class { get; set; }
        public string MonsterSource { get; set; }
        public string Alignment { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string Init { get; set; }
        public string Senses { get; set; }
        public string Aura { get; set; }
        public string AC { get; set; }
        public string AC_Mods { get; set; }
        public string HP { get; set; }
        public string HD { get; set; }
        public string HP_Mods { get; set; }
        public string Saves { get; set; }
        public string Fort { get; set; }
        public string Ref { get; set; }
        public string Will { get; set; }
        public string Save_Mods { get; set; }
        public string DefensiveAbilities { get; set; }
        public string DR { get; set; }
        public string Immune { get; set; }
        public string Resist { get; set; }
        public string SR { get; set; }
        public string Weaknesses { get; set; }
        public string Speed { get; set; }
        public string Speed_Mod { get; set; }
        public string Melee { get; set; }
        public string Ranged { get; set; }
        public string Space { get; set; }
        public string Reach { get; set; }
        public string SpecialAttacks { get; set; }
        public string SpellLikeAbilities { get; set; }
        public string SpellsKnown { get; set; }
        public string SpellsPrepared { get; set; }
        public string SpellDomains { get; set; }
        public string AbilityScores { get; set; }
        public string AbilityScore_Mods { get; set; }
        public string BaseAtk { get; set; }
        public string CMB { get; set; }
        public string CMD { get; set; }
        public string Feats { get; set; }
        public string Skills { get; set; }
        public string RacialMods { get; set; }
        public string Languages { get; set; }
        public string SQ { get; set; }
        public string Environment { get; set; }
        public string Organization { get; set; }
        public string Treasure { get; set; }
        public string Description_Visual { get; set; }
        public string Group { get; set; }
        public string Source { get; set; }
        public bool? IsTemplate { get; set; }
        public string SpecialAbilities { get; set; }
        public string Description { get; set; }
        public string FullText { get; set; }
        public string Gender { get; set; }
        public string Bloodline { get; set; }
        public string ProhibitedSchools { get; set; }
        public string BeforeCombat { get; set; }
        public string DuringCombat { get; set; }
        public string Morale { get; set; }
        public string Gear { get; set; }
        public string OtherGear { get; set; }
        public string Vulnerability { get; set; }
        public string Note { get; set; }
        public bool CharacterFlag { get; set; }
        public bool CompanionFlag { get; set; }
        public bool Fly { get; set; }
        public bool Climb { get; set; }
        public bool Burrow { get; set; }
        public bool Swim { get; set; }
        public bool Land { get; set; }
        public string LinkText { get; set; }
        public string TemplatesApplied { get; set; }
        public string OffenseNote { get; set; }
        public string BaseStatistics { get; set; }
        public string ExtractsPrepared { get; set; }
        public string AgeCategory { get; set; }
        public bool DontUseRacialHD { get; set; }
        public string VariantParent { get; set; }
        public string Mystery { get; set; }
        public string ClassArchetypes { get; set; }
        public string Patron { get; set; }
        public int? CompanionFamiliarLink { get; set; }
        public string FocusedSchool { get; set; }
        public string Traits { get; set; }
        public string AlternateNameForm { get; set; }
        public bool UniqueMonster { get; set; }
        public string ThassilonianSpecialization { get; set; }
        public bool Variant { get; set; }
        public string AdditionalExtractsKnown { get; set; }
        public int MR { get; set; }
        public bool Mythic { get; set; }
        public int MT { get; set; }
        public string Spirit { get; set; }
        public string PsychicMagic { get; set; }
        public string KineticistWildTalents { get; set; }
        public string Implements { get; set; }
        public string PsychicDiscipline { get; set; }
        public bool IsBestiary { get; set; }

        #endregion Properties

        public MonsterStatBlock()
        {
            name = string.Empty;
            AlternateNameForm = string.Empty;
            CR = string.Empty;
            XP = "0";
            Race = string.Empty;
            Class = string.Empty;
            ClassArchetypes = string.Empty;
            MonsterSource = string.Empty;
            Alignment = string.Empty;
            Size = string.Empty;
            Type = string.Empty;
            SubType = string.Empty;
            Init = string.Empty;
            Senses = string.Empty;            
            Aura = string.Empty;
            //Defense
            AC = string.Empty;
            AC_Mods = string.Empty;
            HP = string.Empty;
            HD = string.Empty;
            HP_Mods = string.Empty;
            Saves = string.Empty;
            Fort = string.Empty;
            Ref = string.Empty;
            Will = string.Empty;
            Save_Mods = string.Empty;
            DefensiveAbilities = string.Empty;
            DR = string.Empty;
            Immune = string.Empty;
            Vulnerability = string.Empty;
            Resist = string.Empty;
            SR = string.Empty;
            Weaknesses = string.Empty;

            //Offense
            Speed = string.Empty;
            Speed_Mod = string.Empty;
            Melee = string.Empty;
            Ranged = string.Empty;
            Space = string.Empty;
            Reach = string.Empty;
            SpecialAttacks = string.Empty;
            SpellLikeAbilities = string.Empty;
            SpellsKnown = string.Empty;
            SpellsPrepared = string.Empty;
            SpellDomains = string.Empty;
            OffenseNote = string.Empty;
            Bloodline = string.Empty;
            ExtractsPrepared = string.Empty;
            Mystery = string.Empty;
            Patron = string.Empty;
            ProhibitedSchools = string.Empty;
            PsychicMagic = string.Empty;
            KineticistWildTalents = string.Empty;
            Implements = string.Empty;

            //Statistics
            AbilityScores = string.Empty;
            AbilityScore_Mods = string.Empty;
            BaseAtk = string.Empty;
            CMB = string.Empty;
            CMD = string.Empty;
            Feats = string.Empty;
            Skills = string.Empty;
            RacialMods = string.Empty;
            Languages = string.Empty;
            SQ = string.Empty;
            Gear = string.Empty;
            OtherGear = string.Empty;
            Traits = string.Empty;

            //Ecology
            Environment = string.Empty;
            Organization = string.Empty;
            Treasure = string.Empty;

            //Special Abilities
            SpecialAbilities = string.Empty;

            //Description
            Description = string.Empty;

            Note = string.Empty;
            Description_Visual = string.Empty;
            Group = string.Empty;
            Source = string.Empty;
            CharacterFlag = false;
            CompanionFlag = false;
            IsTemplate = false;
            FullText = string.Empty;
            LinkText = string.Empty;
            TemplatesApplied = string.Empty;
            DontUseRacialHD = false;
            VariantParent = string.Empty;
            CompanionFamiliarLink = null;
            ThassilonianSpecialization = string.Empty;
            Variant = false;
            AdditionalExtractsKnown = string.Empty;
            MR = 0;
            Mythic = false;
            MT = 0;
            Gender = string.Empty;
            Spirit = string.Empty;
            PsychicDiscipline = string.Empty;
            IsBestiary = false;       
        }

        #region Methods
        public int GetAbilityScoreValue(StatBlockInfo.AbilityName Ability)
        {
            string temp = Ability.ToString();
            temp = temp.Substring(0, 3);
            return GetAbilityScoreValue(temp);
        }

        public int GetAbilityScoreValue(string ScoreName)
        {
            if (ScoreName.Length == 0) return -100;
            AbilityScores = AbilityScores.Replace("*", string.Empty);
            List<string> Scores = AbilityScores.Split(',').ToList();
            string temp = string.Empty;

            foreach (string score in Scores)
            {
                if (score.Contains(ScoreName))
                {
                    temp = score.Replace(ScoreName, string.Empty).Trim();
                    if (temp == "-") temp = "0";
                    if (temp.IndexOf(PathfinderConstants.PAREN_LEFT) >= 0)
                    {
                        temp = temp.Substring(0, temp.IndexOf(PathfinderConstants.PAREN_LEFT));
                    }

                    return Convert.ToInt32(temp);
                }
            }

            return -100;
        }

        public int HDValue()
        {
            StatBlockInfo.HDBlockInfo racialHDInfo = new StatBlockInfo.HDBlockInfo();
            string hold = HD;
            hold = Utility.RemoveParentheses(hold);
            int Pos = hold.IndexOf(";");
            int Pos2 = hold.IndexOf("HD");
            if (Pos < Pos2)
            {               
                Pos = hold.IndexOf(";",Pos2);
            }
            int value = 0;

            if (Pos >= 0 && hold.Contains("HD"))
            {
                hold = hold.Substring(0, Pos);
                hold = hold.Replace("HD", string.Empty).Trim();
                return Convert.ToInt32(hold);
            }
            List<string> HDBlocks = hold.Split('+').ToList();
            foreach (string block in HDBlocks)
            {
                if (block.Contains("d") && !block.Contains(";"))
                {
                    racialHDInfo.ParseHDBlock(block);
                    value += racialHDInfo.Multiplier;
                }
            }

            return value;
        }

        public int HDMod()
        {
            string HoldHD = HD;
            HoldHD = HoldHD.Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
            HoldHD = HoldHD.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
            HoldHD = HoldHD.Replace(" plus ", "+");
            int Pos = HoldHD.IndexOf("temporary");
            if (Pos != -1)
            {
                Pos = HoldHD.LastIndexOf("+", Pos);
                HoldHD = HoldHD.Substring(0, Pos).Trim();                
            }

            Pos = HoldHD.LastIndexOf("+");
            int Pos3 = HoldHD.LastIndexOf("-");
            if (Pos3 > Pos) Pos = Pos3;
            if (Pos == -1) Pos = HoldHD.LastIndexOf("-");
            int Pos2 = HoldHD.LastIndexOf("d");
            int HDModifier = 0;            

            if (Pos >= 0 && Pos2 < Pos)
            {
                string temp = HoldHD.Substring(Pos);
                if(temp.Contains(";")) temp = temp.Substring(0, temp.IndexOf(";"));
                if (temp.Contains(PathfinderConstants.SPACE)) temp = temp.Substring(0, temp.IndexOf(PathfinderConstants.SPACE));
                HDModifier = Convert.ToInt32(temp);
            }
            if (HoldHD.Contains(" plus "))
            {
                Pos = HoldHD.IndexOf(" plus ");
                string temp = HoldHD.Substring(Pos);
                temp = temp.Replace(" plus ",string.Empty);
                HDModifier += Convert.ToInt32(temp);
            }

            return HDModifier;
        }

        public int RacialHD()
        {
            if (DontUseRacialHD) return 0;

            return HDValue();
        }

        #endregion Methods       

        public MonsterStatBlock Clone()
        {
            MemoryStream stream = SerializeToStream(this);
            return DeserializeFromStream(stream);
        }

        private MemoryStream SerializeToStream(MonsterStatBlock o)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, o);
            return stream;
        }

        private MonsterStatBlock DeserializeFromStream(MemoryStream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            return (MonsterStatBlock)formatter.Deserialize(stream);            
        }
        public static T DeepCopy<T>(T obj)
        {
            if (!typeof(T).IsSerializable)  throw new Exception("The source object must be serializable");
            if (Object.ReferenceEquals(obj, null))  throw new Exception("The source object must not be null");

            T result = default;

            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, obj);
                memoryStream.Seek(0, SeekOrigin.Begin);
                result = (T)formatter.Deserialize(memoryStream);
                memoryStream.Close();
            }

            return result;
        }
    }
}
