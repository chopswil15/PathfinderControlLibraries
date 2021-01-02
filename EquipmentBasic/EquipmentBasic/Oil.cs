using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonInterFacesDD;
using Utilities;

namespace EquipmentBasic
{
    public class Oil : IEquipment
    {
        public bool Masterwork { get; set; }
        public string OilOf { get; set; }
        public bool Broken { get; set; }
        public int ValueOverride { get; set; }
        public int CasterLevel { get; set; }

        public EquipmentType EquipmentType
        {
            get { return EquipmentType.Oil; }
        }

        public Oil(string OilString) : this(OilString, string.Empty) { }

        public Oil(string OilString, string extraAbilities)
        {
            Masterwork = true;
            ParseOil(OilString);
            if (extraAbilities.Length > 0) ParseCasterLevel(ref extraAbilities);
        }

        private void ParseOil(string OilString)
        {
            int Pos = OilString.IndexOf(" of ");
            if (Pos > 0)
            {
                OilOf = OilString.Substring(Pos + 4).Trim();
                Pos = OilOf.IndexOf(Utility.PAREN_LEFT);
                if (Pos > 0)
                {
                    OilOf = OilOf.Substring(0, Pos).Trim();
                }

                Pos = OilOf.IndexOf("+");
                if (Pos > 0)
                {
                    string temp = OilOf.Substring(Pos);
                    OilOf = OilOf.Replace(temp,string.Empty).Trim();
                    ValueOverride = Convert.ToInt32(temp);
                }
            }
        }

        public int OilCasterLevel(int SpellLevel)
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

            if (potionCL.Contains(";"))
            {
                int Pos = potionCL.IndexOf(";");
                potionCL = potionCL.Substring(Pos + 1).Trim();
            }

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
            return "oil of " + OilOf;
        }
    }    
}
