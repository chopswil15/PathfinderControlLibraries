using System;
using System.Collections.Generic;
using System.Linq;
using CommonStatBlockInfo;
using System.Text;
using PathfinderGlobals;

namespace AbilityScores
{
    public class AbilityScores
    {      
        public int Int {get; set;}
        public int IntBase { get; private set; }
        public int IntBaseMod { get; private set; }
        public int IntMod { get; set; }
        public int Wis { get; set; }
        public int WisBase { get; private set; }
        public int WisBaseMod { get; private set; }
        public int WisMod { get; set; }
        public int Str { get; set; }
        public int StrBase { get; private set; }
        public int StrBaseMod { get; private set; }
        public int StrMod { get; set; }
        public int Dex { get; set; }
        public int DexBase { get; private set; }
        public int DexBaseMod { get; private set; }
        public int DexMod { get; set; }
        public int Cha { get; set; }
        public int ChaBase { get; private set; }
        public int ChaBaseMod { get; private set; }
        public int ChaMod { get; set; }
        public int Con { get; set; }
        public int ConBase { get; private set; }
        public int ConBaseMod { get; private set; }
        public int ConMod { get; set; }

        public enum AbilityName
        {
            Unknown = 0,
            Strength = 1,
            Dexterity = 2,
            Constitution = 3,
            Intelligence = 4,
            Wisdom = 5,
            Charisma = 6
        }

        public AbilityScores(string abilityScoresString)
        {
            abilityScoresString = abilityScoresString.Replace("*", string.Empty);
            List<string> Scores = abilityScoresString.Split(',').ToList();
            if (Scores.Count != 6) throw new Exception("AbilityScores: Count != 6 -- " + abilityScoresString);
            int Pos;

            foreach (string score in Scores)
            {
                string temp = score.Trim();
                Pos = temp.IndexOf(PathfinderConstants.SPACE);
                if (Pos == -1) throw new Exception("No Spaces in " + score);
                temp = temp.Substring(0,Pos);
                
                string temp2 = string.Empty;
                temp2 = score.Replace(temp, string.Empty).Trim();
                if (temp2.Contains(PathfinderConstants.PAREN_LEFT))
                {
                    Pos = temp2.IndexOf(PathfinderConstants.PAREN_LEFT);
                    temp2 = temp2.Substring(0, Pos).Trim();
                }               

                int Value;
                int.TryParse(temp2, out Value);               
               
                switch (temp)
                {
                    case StatBlockInfo.INT:
                        Int = Value;
                        IntBase = Int;
                        IntMod = GetAbilityModifier(Int);
                        IntBaseMod = IntBase;
                        break;
                    case StatBlockInfo.WIS:
                        Wis = Value;
                        WisBase = Wis;
                        WisMod = GetAbilityModifier(Wis);
                        WisBaseMod = WisMod;
                        break;
                    case StatBlockInfo.DEX:
                        Dex = Value;
                        DexBase = Dex;
                        DexMod = GetAbilityModifier(Dex);
                        DexBaseMod = DexMod;
                        break;
                    case StatBlockInfo.STR:
                        Str = Value;
                        StrBase = Str;
                        StrMod = GetAbilityModifier(Str);
                        StrBaseMod = StrMod;
                        break;
                    case StatBlockInfo.CON:
                        Con = Value;
                        ConBase = Con;
                        ConMod = GetAbilityModifier(Con);
                        ConBaseMod = ConMod;
                        break;
                    case StatBlockInfo.CHA:
                        Cha = Value;
                        ChaBase = Cha;
                        ChaMod = GetAbilityModifier(Cha);
                        ChaBaseMod = ChaMod;
                        break;
                }
            }
        }


        public int GetAbilityModifier(int score)
        {
            if (score <= 0) return 0;
            int mod_base;

            if (score % 2 == 0)
            {
                mod_base = 10;
            }
            else
            {
                mod_base = 11;
            }

            return (score - mod_base) / 2;
        }

        public int GetAbilityScoreValue(AbilityName abilityName)
        {
            switch (abilityName)
            {
                case AbilityName.Intelligence:
                    return Int;
                case AbilityName.Wisdom:
                    return Wis;
                case AbilityName.Strength:
                    return Str;
                case AbilityName.Dexterity:
                    return Dex;
                case AbilityName.Charisma:
                    return Cha;
                case AbilityName.Constitution:
                    return Con;
            }
            return -100;
        }

        public int GetAbilityScoreBaseValue(AbilityName abilityName)
        {
            switch (abilityName)
            {
                case AbilityName.Intelligence:
                    return IntBase;
                case AbilityName.Wisdom:
                    return WisBase;
                case AbilityName.Strength:
                    return StrBase;
                case AbilityName.Dexterity:
                    return DexBase;
                case AbilityName.Charisma:
                    return ChaBase;
                case AbilityName.Constitution:
                    return ConBase;
            }
            return -100;
        }

        public void UpdateAbilityScoreBaseValue(AbilityName abilityName, int NewValue)
        {
            switch (abilityName)
            {
                case AbilityName.Intelligence:
                    IntBase = NewValue;
                    IntBaseMod = GetAbilityModifier(IntBase);
                    return;
                case AbilityName.Wisdom:
                    WisBase = NewValue;
                    WisBaseMod = GetAbilityModifier(WisBase);
                    return; 
                case AbilityName.Strength:
                    StrBase = NewValue;
                    StrBaseMod = GetAbilityModifier(StrBase);
                    return;
                case AbilityName.Dexterity:
                    DexBase = NewValue;
                    DexBaseMod = GetAbilityModifier(DexBase);
                    return;
                case AbilityName.Charisma:
                    ChaBase = NewValue;
                    ChaBaseMod = GetAbilityModifier(ChaBase);
                    return;
                case AbilityName.Constitution:
                    ConBase = NewValue;
                    ConBaseMod = GetAbilityModifier(ConBase);
                    return;
            }
        }

        public int GetAbilityModValue(AbilityName abilityName)
        {
            switch (abilityName)
            {
                case AbilityName.Intelligence:
                    return IntMod;
                case AbilityName.Wisdom:
                    return WisMod;
                case AbilityName.Strength:
                    return StrMod;
                case AbilityName.Dexterity:
                    return DexMod;
                case AbilityName.Charisma:
                    return ChaMod;
                case AbilityName.Constitution:
                    return ConMod;
            }
            return -100;
        }

        public static AbilityName GetAbilityNameEnum(string abilityNameShort)
        {
            switch (abilityNameShort)
            {
                case StatBlockInfo.INT:
                    return AbilityName.Intelligence;
                case StatBlockInfo.WIS:
                    return AbilityName.Wisdom;
                case StatBlockInfo.DEX:
                    return AbilityName.Dexterity;
                case StatBlockInfo.STR:
                    return AbilityName.Strength;
                case StatBlockInfo.CON:
                    return AbilityName.Constitution;
                case StatBlockInfo.CHA:
                    return AbilityName.Charisma;
            }
            return AbilityName.Unknown;
        }
    }
}
