using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonInterFacesDD;
using CommonStatBlockInfo;
using D_D_Common;
using CommonStrings;

using Utilities;


namespace EquipmentBasic
{
    public class Scroll : IEquipment
    {
        public bool Masterwork { get; set; }
        public string ScrollOf { get; set; }
        public bool Broken { get; set; }
        public StatBlockInfo.MetaMagicPowers MetaMagicPowers { get; set; }
        public int CasterLevel { get; set; }

        public EquipmentType EquipmentType
        {
            get { return EquipmentType.Scroll; }
        }

        public Scroll(string ScrollString)
        {
            Masterwork = true;
            ParseScroll(ScrollString);         
        }

        private void ParseScroll(string ScrollString)
        {
            int Pos = ScrollString.IndexOf(" of ");
            if (Pos > 0)
            {
                string temp = ScrollString.Substring(Pos + 4).Trim();
                string temp2 = string.Empty;

                MetaMagicPowers = ParseMetaMagicPowers(ref ScrollString);
                ParseCasterLevel(ref ScrollString);

                Pos = ScrollString.IndexOf(" of ");
                ScrollOf = ScrollString.Substring(Pos + 4).Trim();
            }
        }

        private void ParseCasterLevel(ref string scroll)
        {
            CasterLevel = 0;

            if (scroll.IndexOf(Utility.PAREN_LEFT) != -1)
            {
                int Pos = scroll.IndexOf(Utility.PAREN_LEFT);
                string temp = scroll.Substring(Pos);
                scroll = scroll.Replace(temp, string.Empty).Trim();

                List<string> OrdinalLevelList = CommonInfo.GetOrdinalLevelList();
                int count = 1;

                foreach (string ord in OrdinalLevelList)
                {
                    if (temp.Contains(ord))
                    {
                        CasterLevel = count;
                        return;
                    }
                    count++;
                }
            }
        }

        private StatBlockInfo.MetaMagicPowers ParseMetaMagicPowers(ref string scroll)
        {
            StatBlockInfo.MetaMagicPowers powers = StatBlockInfo.MetaMagicPowers.None;
            List<string> MetaMagicList = CommonInfo.GetMetaMagicPowers();

            foreach (string Meta in MetaMagicList)
            {
                if (scroll.Contains(Meta))
                {
                    if ((Meta == "silent" && scroll.Contains("silent image"))) continue;
                    if ((Meta == "heightened" && scroll.Contains("heightened awareness"))) continue;

                    scroll = scroll.ReplaceFirst(Meta, string.Empty);
                    string temp = Meta;
                    if (temp == "still") temp = "stilled";
                    powers |= (StatBlockInfo.MetaMagicPowers)Enum.Parse(typeof(StatBlockInfo.MetaMagicPowers), temp);
                }
            }
            return powers;
        }

        public override string ToString()
        {
            return "scroll of " + ScrollOf;
        }
    }
}
