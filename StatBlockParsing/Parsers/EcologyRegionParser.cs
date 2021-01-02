using PathfinderGlobals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace StatBlockParsing
{
    public class EcologyRegionParser : IEcologyRegionParser
    {
        private ISBCommonBaseInput _sbcommonBaseInput;

        public EcologyRegionParser(ISBCommonBaseInput sbcommonBaseInput)
        {
            _sbcommonBaseInput = sbcommonBaseInput;
        }

        public void ParseEcology(string Ecology, string CR)
        {
            Ecology = Ecology.Replace("Ecology", string.Empty).Trim();


            int Pos = Ecology.LastIndexOf(CR + "* ");
            string temp;

            if (Pos >= 0)
            {
                temp = Ecology.Substring(Pos);
                Ecology = Ecology.Replace(temp, string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Note = temp.Trim();
            }

            //work your way back
            Pos = Ecology.IndexOf("Treasure");

            if (Pos >= 0)
            {
                temp = Ecology.Substring(Pos);
                Ecology = Ecology.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Treasure", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE).Trim();
                _sbcommonBaseInput.MonsterSB.Treasure = temp;
            }

            Pos = Ecology.IndexOf("Organization");

            if (Pos >= 0)
            {
                temp = Ecology.Substring(Pos);
                Ecology = Ecology.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Organization", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE).Trim();
                _sbcommonBaseInput.MonsterSB.Organization = temp;
            }

            Ecology = Ecology.Replace(CR, PathfinderConstants.SPACE)
                .Replace("Environment", string.Empty);
            _sbcommonBaseInput.MonsterSB.Environment = Ecology;
        }
    }
}
