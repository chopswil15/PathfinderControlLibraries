using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonInterFacesDD;
using Utilities;

namespace EquipmentBasic
{
    public class Wand : IEquipment, ICharges, ICasterLevel
    {
        public bool Masterwork { get; set; }
        public bool Broken { get; set; }
        public string WandOf { get; set; }
        public int Charges { get; set; }
        public int CasterLevel { get; set; }

        public EquipmentType EquipmentType
        {
            get { return EquipmentType.Wand; }
        }

        public Wand(string WandString)
        {
            Masterwork = true;
            
            Charges = 0;
            ParseWand(WandString);
        }

        private void ParseWand(string WandString)
        {
            int Pos = WandString.IndexOf(" of ");
            if (Pos > 0)
            {
                string temp = WandString.Substring(Pos + 4).Trim();
                string temp2 = string.Empty;
                if (temp.IndexOf(Utility.PAREN_LEFT) > 0)
                {
                    temp2 = temp.Substring(0, temp.IndexOf(Utility.PAREN_LEFT));
                    temp = temp.Replace(temp2, string.Empty).Trim();
                    ParseChargesCL(temp);
                    temp2 = Utility.RemoveParentheses(temp2);
                }
                WandOf = WandString.Substring(Pos + 4).Trim();
            }
        }

        private void ParseChargesCL(string Parse)
        {
            Parse = Utility.RemoveParentheses(Parse);
            List<string> hold = Parse.Split('|').ToList();
            string temp2 = string.Empty;

            foreach (string temp in hold)
            {
                if(temp.Contains("CL"))
                {
                    temp2 = temp.Replace("CL", string.Empty);
                    temp2 = temp2.Replace("st", string.Empty);
                    temp2 = temp2.Replace("nd", string.Empty);
                    temp2 = temp2.Replace("rd", string.Empty);
                    temp2 = temp2.Replace("th", string.Empty);
                    CasterLevel = Convert.ToInt32(temp2);
                }

                if (temp.Contains("charges"))
                {
                    temp2 = temp.Replace("charges", string.Empty);
                    Charges = Convert.ToInt32(temp2);
                }
            }
        }

        public override string ToString()
        {
            return "wand of " + WandOf;
        }
    }
}
