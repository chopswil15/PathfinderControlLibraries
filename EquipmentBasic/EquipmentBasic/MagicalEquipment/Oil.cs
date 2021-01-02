using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonInterFacesDD;
using PathfinderGlobals;
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

        public Oil(string oilString) : this(oilString, string.Empty) { }

        public Oil(string oilString, string extraAbilities)
        {
            Masterwork = true;
            ParseOil(oilString);
            if (extraAbilities.Length > 0) ParseCasterLevel(ref extraAbilities);
        }

        private void ParseOil(string oilString)
        {
            int Pos = oilString.IndexOf(" of ");
            if (Pos > 0)
            {
                OilOf = oilString.Substring(Pos + 4).Trim();
                Pos = OilOf.IndexOf(PathfinderConstants.PAREN_LEFT);
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

        private void ParseCasterLevel(ref string oilCL)
        {
            CasterLevel = 0;
            if (!oilCL.Contains("CL")) return;

            if (oilCL.Contains(";"))
            {
                int Pos = oilCL.IndexOf(";");
                oilCL = oilCL.Substring(Pos + 1).Trim();
            }

            oilCL = oilCL.Replace(PathfinderConstants.PAREN_LEFT, string.Empty).Replace("CL", string.Empty)
                .Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Replace("st", string.Empty).Replace("nd", string.Empty)
                .Replace("rd", string.Empty).Replace("th", string.Empty).Trim();
            CasterLevel = int.Parse(oilCL);      
        }

        public override string ToString()
        {
            return "oil of " + OilOf;
        }
    }    
}
