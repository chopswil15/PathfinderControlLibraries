using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonStrings
{
    public class clsStrings
    {        

        public static int StringToAscii(string temp)
        {
            char x;
            if (temp.Length == 1)
            {
                x = char.Parse(temp);
                return (int)x;
            }
            else
            {
                x = char.Parse(temp.Substring(0, 1));
                return (int)x;
            }
        }

        public static string AsciiToString(int ascii)
        {
            return Convert.ToString((char)(ascii));
        }


        public static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }

        public static string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos >= 0)
            {
                return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
            }
            return text;
        }

        public static string ReplaceFirst(string text, string search, string replace, int start)
        {
            int pos = text.IndexOf(search, start);
            if (pos >= 0 && pos >= start)
            {
                return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
            }
            return text;
        }

        public static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }        
    }
}
