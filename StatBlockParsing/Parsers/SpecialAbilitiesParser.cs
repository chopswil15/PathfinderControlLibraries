using PathfinderGlobals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockParsing
{
    public class SpecialAbilitiesParser : ISpecialAbilitiesParser
    {
        private ISBCommonBaseInput _sbcommonBaseInput;

        public SpecialAbilitiesParser(ISBCommonBaseInput sbcommonBaseInput)
        {
            _sbcommonBaseInput = sbcommonBaseInput;
        }

        public void ParseSpecialAbilities(string SpecialAbilitiesString, string CR)
        {
            string temp;
            int Pos = SpecialAbilitiesString.IndexOf(CR + "* ");
            if (Pos >= 0)
            {
                int CRPos = SpecialAbilitiesString.LastIndexOf(CR);
                if (CRPos == Pos)
                {
                    temp = SpecialAbilitiesString.Substring(Pos);
                }
                else
                {
                    temp = SpecialAbilitiesString.Substring(Pos, CRPos - Pos);
                }
                SpecialAbilitiesString = SpecialAbilitiesString.Replace(temp, string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Note = temp.Trim();
            }

            Pos = SpecialAbilitiesString.IndexOf(CR + "** ");
            if (Pos >= 0)
            {
                int CRPos = SpecialAbilitiesString.LastIndexOf(CR);
                if (CRPos == Pos)
                {
                    temp = SpecialAbilitiesString.Substring(Pos);
                }
                else
                {
                    temp = SpecialAbilitiesString.Substring(Pos, CRPos - Pos);
                }
                SpecialAbilitiesString = SpecialAbilitiesString.Replace(temp, string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Note += PathfinderConstants.BREAK + temp.Trim();
            }

            Pos = SpecialAbilitiesString.IndexOf(CR + "*** ");
            if (Pos >= 0)
            {
                int CRPos = SpecialAbilitiesString.LastIndexOf(CR);
                if (CRPos == Pos)
                {
                    temp = SpecialAbilitiesString.Substring(Pos);
                }
                else
                {
                    temp = SpecialAbilitiesString.Substring(Pos, CRPos - Pos);
                }
                SpecialAbilitiesString = SpecialAbilitiesString.Replace(temp, string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Note += PathfinderConstants.BREAK + temp.Trim();
            }

            //Pos = SpecialAbilitiesString.LastIndexOf(" See ");
            //int Pos2 = SpecialAbilitiesString.IndexOf(". See ");
            //if (Pos >= 0 && Pos2 == -1 && SpecialAbilitiesString.IndexOf("(Ex) See ") == -1 &&  SpecialAbilitiesString.IndexOf("(Su) See ") == -1)
            //{                
            //    int CRPos = SpecialAbilitiesString.LastIndexOf(CR, Pos);
            //    string temp2 = SpecialAbilitiesString.Substring(CRPos);
            //    SpecialAbilitiesString = SpecialAbilitiesString.Replace(temp2, string.Empty).Trim(); //1st See
            //    Pos = SpecialAbilitiesString.LastIndexOf(" See ");

            //    while (Pos >= 0)
            //    {
            //        CRPos = SpecialAbilitiesString.LastIndexOf(CR, Pos);
            //        string hold = SpecialAbilitiesString.Substring(CRPos);
            //        if (hold.Contains(" See "))
            //        {
            //            temp2 += hold;
            //            SpecialAbilitiesString = SpecialAbilitiesString.Replace(hold, string.Empty).Trim();
            //        }
            //        else
            //        {
            //            break;
            //        }
            //        Pos = SpecialAbilitiesString.LastIndexOf(" See ");
            //    }              




            //    _sbcommonBaseInput.MonsterSB.Note = temp2.Trim();
            //    List<string> superScripts = _sbcommonBaseInput.MonsterSB.Note.Split((char)CR[0]).ToList();
            //   // List<string> superScripts = _sbcommonBaseInput.MonsterSB.Note.Split('.').ToList();
            //    List<string> values = new List<string>();
            //    foreach (string super in superScripts)
            //    {
            //        CRPos = super.IndexOf(PathfinderConstants.SPACE);
            //        values.Add(super.Substring(0, CRPos).Trim());
            //        SourceSuperScript = super.Substring(0, CRPos);
            //    }
            //    SourceSuperScript = String.Join(",", values.ToArray());
            //}

            SpecialAbilitiesString = SpecialAbilitiesString.Replace("Special Abilities", string.Empty).Trim();
            //mark the keeper CRs
            SpecialAbilitiesString = SpecialAbilitiesString.Replace("." + CR, ".<br>");

            //remove the unwanted CRs
            SpecialAbilitiesString = SpecialAbilitiesString.Replace(CR, PathfinderConstants.SPACE);

            //put back the keeper CRs
            SpecialAbilitiesString = SpecialAbilitiesString.Replace(PathfinderConstants.BREAK, CR);

            _sbcommonBaseInput.MonsterSB.SpecialAbilities = SpecialAbilitiesString;
        }
    }
}
