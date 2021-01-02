using CommonStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockCommon
{
    public class TableParse
    {
        const string TABLE = "<table>";
        const string TABLE_BORDER = "<table border ='1'>";
        const string ETABLE = "</table>";
        const string ROW = "<tr>";
        const string EROW = "</tr>";
        const string HEADING = "<th>";
        const string EHEADING = "</th>";
        const string CELL = "<td>";
        const string ECELL = "</td>";

        public string ParseTable(string tableText, bool setBorder)
        {
            StringBuilder tableOut = new StringBuilder(1000);
            char[] CRLF = Environment.NewLine.ToCharArray();

            if (setBorder)
            {
                tableOut.Append(TABLE_BORDER);
            }
            else
            {
                tableOut.Append(TABLE);
            }

            tableText = tableText.Replace("“", ((char)34).ToString()).Replace("”", ((char)34).ToString()).Replace("—", "-")
                .Replace("’", "'").Replace((char)(8211), char.Parse("-"));

            List<string> rows = tableText.Split(CRLF, 100, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> cells;
            int rowCount = 1;
            char[] TAB = StringExtenstions.AsciiToString(9).ToCharArray();

            foreach (string row in rows)
            {
                tableOut.Append(ROW);
                cells = row.Trim().Split(TAB, 100, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (string cell in cells)
                {
                    if (rowCount == 1)
                    {
                        tableOut.Append(HEADING + cell.Trim() + EHEADING);
                    }
                    else
                    {
                        tableOut.Append(CELL + cell.Trim() + ECELL);
                    }
                }
                tableOut.Append(EROW);
                rowCount++;
            }
            tableOut.Append(ETABLE);

            return tableOut.ToString();
        }
    }
}
