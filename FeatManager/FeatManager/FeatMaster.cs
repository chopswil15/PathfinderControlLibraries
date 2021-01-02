using FeatFoundational;
using PathfinderGlobals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace FeatManager
{
    public class FeatMaster
    {
        private FeatFoundation Feat;

        public List<FeatFoundation> FeatsList { get; set; }
        public List<FeatData> FeatDatum { get; set; }

        public FeatMaster()
        {
            FeatDatum = new List<FeatData>();
        }

        public FeatMaster(List<string> FeatList)
        {
            FeatsList = new List<FeatFoundation>();
            ParseFeats(FeatList);
        }

        public void AddFeatData(FeatData FeatData)
        {
            FeatDatum.Add(FeatData);
        }

        public void ParseFeats(List<string> FeatList)
        {
            FeatList.RemoveAll(x => x == string.Empty);
            List<string> FailedFeats = new List<string>();
            string hold = string.Empty;
            string temp = string.Empty;
            bool bonusFeat = false;

            foreach (string feat in FeatList)
            {
                hold = feat.Trim();
                hold = hold.Replace("'", string.Empty);
                bonusFeat = false;
                if (hold.EndsWith("B"))
                {
                    hold = hold.Substring(0, hold.Length - 1);
                    bonusFeat = true;
                }
                temp = string.Empty;
                int Pos = hold.IndexOf(PathfinderConstants.PAREN_LEFT);
                if (Pos >= 0)
                {
                    temp = hold.Substring(Pos);
                    hold = hold.Replace(temp, string.Empty).Trim();
                    temp = Utility.RemoveParentheses(temp);
                }
                hold = hold.Replace("**", string.Empty);
                hold = hold.Replace("*", string.Empty);

                Feat = ParceFeat(hold);
                if (Feat != null)
                {
                    if (temp.Length > 0)
                    {
                        Feat.SelectedItem = temp;
                    }
                    Feat.BonusFeat = bonusFeat;
                    FeatsList.Add(Feat);
                }
                else
                {
                    FailedFeats.Add(hold);
                }
            }
            if (FailedFeats.Any()) throw new Exception("Feats " + string.Join(",", FailedFeats.ToArray()) + " not defined");
        }

        public FeatFoundation ParceFeat(string FeatName)
        {
            return FeatDetailsWrapper.GetFeatDetailClass(FeatName);
        }

    }
}
