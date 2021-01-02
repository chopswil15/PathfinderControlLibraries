using PathfinderGlobals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockParsing
{
    public class IndividualStatisticsParser : IIndividualStatisticsParser
    {
        private ISBCommonBaseInput _sbcommonBaseInput;

        public IndividualStatisticsParser(ISBCommonBaseInput sbcommonBaseInput)
        {
            _sbcommonBaseInput = sbcommonBaseInput;
        }

        public string ParseIndividualStatistics(ref string Statistics, string CR)
        {
            //this is for IndividualStatBlock only fields
            //do this 1st before ParseStatistics() so it doesn't interfear with base class method

            //work your way back
            string sourceSuperScript = string.Empty;
            string temp, temp2, temp3;
            int Pos = Statistics.IndexOf(CR + "* ");
            if (Pos >= 0)
            {
                int CRPos = Statistics.LastIndexOf(CR);
                if (CRPos == Pos)
                {
                    temp3 = Statistics.Substring(Pos);
                }
                else
                {
                    temp3 = Statistics.Substring(Pos, CRPos - Pos);
                }
                Statistics = Statistics.Replace(temp3, string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Note = temp3.Trim();
            }

            Pos = Statistics.IndexOf(CR + "** ");
            if (Pos >= 0)
            {
                int CRPos = Statistics.LastIndexOf(CR);
                if (CRPos == Pos)
                {
                    temp3 = Statistics.Substring(Pos);
                }
                else
                {
                    temp3 = Statistics.Substring(Pos, CRPos - Pos);
                }
                Statistics = Statistics.Replace(temp3, string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Note += PathfinderConstants.BREAK + temp3.Trim();
            }

            Pos = Statistics.IndexOf(CR + "*** ");
            if (Pos >= 0)
            {
                int CRPos = Statistics.LastIndexOf(CR);
                if (CRPos == Pos)
                {
                    temp3 = Statistics.Substring(Pos);
                }
                else
                {
                    temp3 = Statistics.Substring(Pos, CRPos - Pos);
                }
                Statistics = Statistics.Replace(temp3, string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Note += PathfinderConstants.BREAK + temp3.Trim();
            }

            Statistics = Statistics.Trim();
            Pos = Statistics.IndexOf(" See ");
            if (Pos >= 0)
            {
                sourceSuperScript = FindSuperScriptNotes(ref Statistics, Pos, CR);
            }

            Statistics = Statistics.Replace(CR + "Other Gear", " Other Gear")
                .Replace("Other" + CR + "Gear", "Other Gear");

            Pos = Statistics.IndexOf("Other Gear");
            if (Pos >= 0)
            {
                Pos = Statistics.IndexOf("Other Gear");
                temp = Statistics.Substring(Pos);
                if (Statistics.IndexOf("; Other Gear") >= 0)
                {
                    temp2 = "; " + temp;
                }
                else
                {
                    temp2 = temp;
                }
                Statistics = Statistics.Replace(temp2, string.Empty).Trim();
                //temp = KeepCRs(temp);
                temp = temp.Replace(CR, PathfinderConstants.SPACE)
                    .Replace("  ", PathfinderConstants.SPACE)
                    .Replace("Other Gear", string.Empty).Trim();

                _sbcommonBaseInput.IndvidSB.OtherGear = temp;
            }

            temp2 = "Combat Gear ";
            Pos = Statistics.IndexOf(temp2);
            if (Pos == -1)
            {
                temp2 = "Gear ";
                Pos = Statistics.IndexOf(temp2);
            }
            if (Pos >= 0)
            {
                temp = Statistics.Substring(0, Pos);
                if (Statistics.Contains("; Gear "))
                {
                    Pos = Statistics.IndexOf("; Gear ");
                    temp = Statistics.Substring(Pos);
                    Statistics = Statistics.Replace(temp, string.Empty).Trim();
                    temp = temp.Replace("; Gear", string.Empty);
                    temp = CommonMethods.KeepCRsIndividual(temp, CR);
                    _sbcommonBaseInput.IndvidSB.OtherGear = temp.Trim();
                    Pos = Statistics.IndexOf("Combat Gear ");
                    temp = Statistics.Substring(0, Pos);
                    Pos = temp.LastIndexOf(CR);
                    temp = Statistics.Substring(Pos + CR.Length);
                }
                else
                {
                    Pos = temp.LastIndexOf(CR);
                    temp = Statistics.Substring(Pos + CR.Length);
                }


                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = CommonMethods.KeepCRsIndividual(temp, CR);
                temp = temp.Replace(temp2, string.Empty);
                _sbcommonBaseInput.IndvidSB.Gear = temp.Trim();
            }

            return sourceSuperScript;
        }

        private string FindSuperScriptNotes(ref string Monstr, int Pos, string CR)
        {
            string temp3 = Monstr.Substring(0, Pos);

            int LastCRPos = temp3.LastIndexOf(CR);
            string temp2 = Monstr.Substring(LastCRPos);
            Monstr = Monstr.Replace(temp2, string.Empty).Trim();
            _sbcommonBaseInput.MonsterSB.Note = temp2.Trim();
            List<string> superScripts = _sbcommonBaseInput.MonsterSB.Note.Split('.').ToList();
            superScripts.RemoveAll(x => x== string.Empty);
            List<string> values = new List<string>();
            foreach (string super in superScripts)
            {
                string temp6 = super.Trim();
                int CRPos = temp6.IndexOf(PathfinderConstants.SPACE);
                values.Add(temp6.Substring(0, CRPos).Trim());
            }
            return string.Join(",", values.ToArray());
        }       
    }
}
