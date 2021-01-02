using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonStrings
{
    public static class StringExtenstions
    {
        public static int StringToAscii(this string temp)
        {
            char x;
            if (temp.Length == 1)
            {
                x = char.Parse(temp);                
            }
            else
            {
                x = char.Parse(temp.Substring(0, 1));
            }
            return (int)x;
        }

        public static string AsciiToString(this int ascii)
        {
            return Convert.ToString((char)(ascii));
        }
        
        public static string Right(this string param, int length)
        {
            return param.Substring(param.Length - length, length);
        }

        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos >= 0)
            {
                return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
            }
            return text;
        }

        public static string ReplaceFirst(this string text, string search, string replace, int start)
        {
            int pos = text.IndexOf(search, start);
            if (pos >= 0 && pos >= start)
            {
                return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
            }
            return text;
        }

        public static string ProperCase(this string stringInput)
        {
            StringBuilder sb = new StringBuilder();
            bool fEmptyBefore = true;
            foreach (char ch in stringInput)
            {
                char chThis = ch;
                if (char.IsWhiteSpace(chThis) || chThis == '-')
                    fEmptyBefore = true;
                else
                {
                    if (char.IsLetter(chThis) && fEmptyBefore)
                        chThis = char.ToUpper(chThis);
                    else
                        chThis = char.ToLower(chThis);
                    fEmptyBefore = false;
                }
                sb.Append(chThis);
            }
            return sb.ToString();
        }


        public static bool IsNumeric(this string test)
        {            
            double retNum;
            return double.TryParse(test, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);            
        }

        public static int PhraseCount(this string test, string phrase)
        {
           return (test.Length - test.Replace(phrase, string.Empty).Length) / phrase.Length;
        }
    }
}
