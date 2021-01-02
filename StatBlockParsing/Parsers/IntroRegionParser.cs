using CommonStrings;
using System;
using Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathfinderGlobals;

namespace StatBlockParsing
{
    public class IntroParser : IIntroParser
    {
        private ISBCommonBaseInput _sbcommonBaseInput;

        public IntroParser(ISBCommonBaseInput sbcommonBaseInput)
        {
            _sbcommonBaseInput = sbcommonBaseInput;
        }

        public void ParseIntro(string basic, bool groupInName, string CR)
        {
            string temp = basic;
            string temp2;
            int Pos = temp.IndexOf(",");

            int CRPos = temp.IndexOf(CR);

            if (CRPos == -1)
            {
                throw new Exception("ParseIntro-- Missing intro text");
            }

            if (Pos == -1 || Pos > CRPos)
            {
                temp2 = temp.Substring(0, CRPos);
                _sbcommonBaseInput.MonsterSB.Group = string.Empty;
                temp = temp.Replace(temp2, string.Empty).Trim();
                if (temp2.IndexOf(PathfinderConstants.PAREN_LEFT) > 0)
                {
                    string temp3 = temp2.Substring(temp2.IndexOf(PathfinderConstants.PAREN_LEFT));
                    temp2 = temp2.Replace(temp3, string.Empty).Trim();
                    temp3 = temp3.Replace(PathfinderConstants.PAREN_LEFT, string.Empty)
                        .Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                    _sbcommonBaseInput.MonsterSB.AlternateNameForm = temp3.Trim();
                }
                _sbcommonBaseInput.MonsterSB.name = temp2.Trim().ProperCase();

            }
            else
            {
                temp2 = temp.Substring(0, Pos);
                _sbcommonBaseInput.MonsterSB.Group = temp2.Trim().ProperCase();
                temp = temp.Replace(temp2 + ",", string.Empty).Trim();
                CRPos = temp.IndexOf(CR);
                temp2 = temp.Substring(0, CRPos);
                temp = temp.Remove(0, CRPos).Trim();
                temp2 = temp2.Trim() + PathfinderConstants.SPACE + _sbcommonBaseInput.MonsterSB.Group;
                if (!groupInName)
                {
                    temp2 = temp2.Replace(_sbcommonBaseInput.MonsterSB.Group, string.Empty).Trim();
                }
                _sbcommonBaseInput.MonsterSB.name = temp2.ProperCase();
            }

            //mark the keeper CRs
            temp = temp.Replace("." + CR, ".<br>");

            //remove the unwanted CRs
            temp = temp.Replace(CR, PathfinderConstants.SPACE);

            //put back the keeper CRs
            temp = temp.Replace(PathfinderConstants.BREAK, CR)
                .Replace(" f ", " f");
            _sbcommonBaseInput.MonsterSB.Description_Visual = temp.Trim();
        }
    }
}
