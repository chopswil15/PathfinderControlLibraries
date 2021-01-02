using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonInterFacesDD;
using PathfinderGlobals;
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

        public Wand(string wandString)
        {
            Masterwork = true;            
            Charges = 0;
            ParseWand(wandString);
        }

        private void ParseWand(string wandString)
        {
            int Pos = wandString.IndexOf(" of ");
            if (Pos > 0)
            {
                string temp = wandString.Substring(Pos + 4).Trim();
                string temp2 = string.Empty;
                if (temp.IndexOf(PathfinderConstants.PAREN_LEFT) > 0)
                {
                    temp2 = temp.Substring(0, temp.IndexOf(PathfinderConstants.PAREN_LEFT));
                    temp = temp.Replace(temp2, string.Empty).Trim();
                    ParseChargesCL(temp);
                    temp2 = Utility.RemoveParentheses(temp2);
                }
                WandOf = wandString.Substring(Pos + 4).Trim();
            }
        }

        private void ParseChargesCL(string Parse)
        {
            Parse = Utility.RemoveParentheses(Parse);
            List<string> hold = Parse.Split('|').ToList();
            string temp2;

            foreach (string temp in hold)
            {
                if(temp.Contains("CL"))
                {
                    temp2 = temp.Replace("CL", string.Empty).Replace("st", string.Empty).Replace("nd", string.Empty).Replace("rd", string.Empty)
                        .Replace("th", string.Empty);
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
