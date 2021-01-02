using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonInterFacesDD;
using Utilities;

namespace EquipmentBasic
{
    public class Potion : IEquipment
    {
        public bool Masterwork { get; set; }
        public string PotionOf { get; set; }
        public bool Broken { get; set; }
        public int ValueOverride { get; set; }
        public int CasterLevel { get; set; }

        public EquipmentType EquipmentType
        {
            get { return EquipmentType.Potion; }
        }

        public Potion(string PotionString) : this(PotionString, string.Empty) { }

        public Potion(string PotionString, string extraAbilities)
        {
            Masterwork = true;
            ParsePotion(PotionString);
            if (extraAbilities.Length > 0) ParseCasterLevel(ref extraAbilities);
        }

        private void ParsePotion(string PotionString)
        {
            int Pos = PotionString.IndexOf(" of ");
            if (Pos > 0)
            {
                PotionOf = PotionString.Substring(Pos + 4).Trim();
                Pos = PotionOf.IndexOf(Utility.PAREN_LEFT);
                if (Pos > 0)
                {
                    PotionOf = PotionOf.Substring(0, Pos).Trim();
                }

                Pos = PotionOf.IndexOf("+");
                if (Pos > 0)
                {
                    string temp = PotionOf.Substring(Pos);
                    PotionOf = PotionOf.Replace(temp,string.Empty).Trim();
                    ValueOverride = Convert.ToInt32(temp);
                }
            }
        }

        public int PotionCasterLevel(int SpellLevel)
        {
            if (CasterLevel != 0) return CasterLevel;
            switch (SpellLevel)
            {
                case 0:
                case 1:
                    return 1;
                case 2:
                    return 3;
                case 3:
                    return 5;
                default:
                    return -1;
            }
        }

        private void ParseCasterLevel(ref string potionCL)
        {
            CasterLevel = 0;

            if (!potionCL.Contains("CL")) return;

            potionCL = potionCL.Replace(Utility.PAREN_LEFT, string.Empty);
            potionCL = potionCL.Replace("CL", string.Empty);
            potionCL = potionCL.Replace(Utility.PAREN_RIGHT, string.Empty).Trim();
            potionCL = potionCL.Replace("st", string.Empty);
            potionCL = potionCL.Replace("nd", string.Empty);
            potionCL = potionCL.Replace("rd", string.Empty);
            potionCL = potionCL.Replace("th", string.Empty).Trim();
            CasterLevel = int.Parse(potionCL);      
        }

        public override string ToString()
        {
            return "potion of " + PotionOf;
        }
    }
}
