using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassDetails
{
    static class GlobalClassInfo
    {
        public static List<string> Alignments = new List<string>{ "LG", "NG", "CG", "LN", "N", "CN", "LE", "NE", "CE"};
        public static List<string> GoodAlignments = new List<string> { "LG", "NG", "CG" };
        public static List<string> EvilAlignments = new List<string> { "LE", "NE", "CE" };
        public static List<string> NonGoodAlignments = new List<string> { "LN", "N", "CN", "LE", "NE", "CE" };
        public static List<string> NonEvilAlignments = new List<string> { "LG", "NG", "CG", "LN", "N", "CN" };
        public static List<string> NonLawfulAlignments = new List<string> { "NG", "CG", "N", "CN", "NE", "CE" };
    }
}
