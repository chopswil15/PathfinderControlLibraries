using PathfinderGlobals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockParsing
{
    public class TacticsRegionParser : ITacticsRegionParser
    {
        private ISBCommonBaseInput _sbcommonBaseInput;

        public TacticsRegionParser(ISBCommonBaseInput sbcommonBaseInput)
        {
            _sbcommonBaseInput = sbcommonBaseInput;
        }

        public void ParseTactics(string tactics, string CR)
        {
            tactics = tactics.Replace("Tactics", string.Empty).Trim();
            //work your way back
            string temp;
            int Pos = tactics.IndexOf("Base Statistics");
            if (Pos >= 0)
            {
                temp = tactics.Substring(Pos);
                tactics = tactics.Replace(temp, string.Empty).Trim();
                temp = CommonMethods.KeepCRs(temp, CR);
                temp = temp.Replace("Base Statistics", string.Empty);
                _sbcommonBaseInput.MonsterSB.BaseStatistics = temp.Trim();
            }

            if (tactics.Length == 0) return;

            Pos = tactics.IndexOf("see Morale ");
            Pos = tactics.IndexOf("Morale ", Pos + 10);

            if (Pos >= 0)
            {
                if (Pos == 0) //only Morale
                {
                    temp = tactics;
                }
                else
                {
                    temp = tactics.Substring(0, Pos);
                    Pos = temp.LastIndexOf(CR);
                    temp = tactics.Substring(Pos);
                }

                tactics = tactics.Replace(temp, string.Empty).Trim();
                temp = CommonMethods.KeepCRs(temp, CR);
                temp = temp.Replace("Morale", string.Empty);
                _sbcommonBaseInput.MonsterSB.Morale = temp.Trim();
            }


            Pos = tactics.IndexOf("During Combat ");
            if (Pos >= 0)
            {
                if (Pos == 0)  //only During Combat left
                {
                    temp = tactics;
                }
                else
                {
                    temp = tactics.Substring(0, Pos);
                    Pos = temp.LastIndexOf(CR);
                    temp = tactics.Substring(Pos);
                }
                tactics = tactics.Replace(temp, string.Empty).Trim();
                temp = CommonMethods.KeepCRs(temp, CR);
                temp = temp.Replace("During Combat", string.Empty);
                _sbcommonBaseInput.MonsterSB.DuringCombat = temp.Trim();
            }

            Pos = tactics.IndexOf("Before Combat ");
            if (Pos >= 0)
            {
                temp = tactics;
                tactics = tactics.Replace(temp, string.Empty).Trim();

                temp = CommonMethods.KeepCRs(temp, CR);
                temp = temp.Replace("Before Combat", string.Empty);
                _sbcommonBaseInput.MonsterSB.BeforeCombat = temp.Trim();
            }
        }
    }
}
