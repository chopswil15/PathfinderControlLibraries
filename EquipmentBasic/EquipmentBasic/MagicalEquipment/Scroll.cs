using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonInterFacesDD;
using CommonStatBlockInfo;

using CommonStrings;

using Utilities;
using PathfinderGlobals;

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

        public Scroll(string scrollString)
        {
            Masterwork = true;
            ParseScroll(scrollString);         
        }

        private void ParseScroll(string scrollString)
        {
            int Pos = scrollString.IndexOf(" of ");
            if (Pos > 0)
            {               
                MetaMagicPowers = ParseMetaMagicPowers(ref scrollString);
                ParseCasterLevel(ref scrollString);

                Pos = scrollString.IndexOf(" of ");
                ScrollOf = scrollString.Substring(Pos + 4).Trim();
            }
        }

        private void ParseCasterLevel(ref string scroll)
        {
            CasterLevel = 0;

            if (!scroll.Contains(PathfinderConstants.PAREN_LEFT))
            {
                int Pos = scroll.IndexOf(PathfinderConstants.PAREN_LEFT);
                string temp = scroll.Substring(Pos);
                scroll = scroll.Replace(temp, string.Empty).Trim();

                List<string> OrdinalLevelList = CommonMethods.GetOrdinalLevelList();
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
            List<string> metaMagicList = Utility.GetMetaMagicPowers();

            foreach (string meta in metaMagicList)
            {
                if (scroll.Contains(meta))
                {
                    if ((meta == "silent" && scroll.Contains("silent image"))) continue;
                    if ((meta == "heightened" && scroll.Contains("heightened awareness"))) continue;

                    scroll = scroll.ReplaceFirst(meta, string.Empty);
                    string temp = meta;
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
