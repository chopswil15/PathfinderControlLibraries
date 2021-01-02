using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderGlobals
{
    public static class PathfinderConstants
    {
        public static string ConnectionString = @"Data Source=localhost\DESKTOP-208N885,1433;Initial Catalog=PFRPG;User ID=PF_Service;Password=pathfinder";


        public static readonly string LG = "LG";
        public static readonly string LE = "LE";
        public static readonly string CE = "CE";
        public static readonly string NE = "NE";
        public static readonly string NG = "NG";
        public static readonly string CG = "CG";
        public static readonly string LN = "LN";
        public static readonly string CN = "CN";
        public static readonly string N = "N";

        public static string ASCII_10 = Convert.ToString((char)(10));
        public static string ASCII_13 = Convert.ToString((char)(13));
        public static string QUOTE = Convert.ToString((char)(34));
        public static string TAB = ((char)9).ToString();
        public static string SPACE = " ";
        public static string PAREN_LEFT = "(";
        public static string PAREN_RIGHT = ")";

        public static string COM_SP = ", ";
        public static string SC_SP = "; ";
        public static string BOLD = "<b>";
        public static string EBOLD = "</b>";
        public static string PARA = "<p>";
        public static string EPARA = "</p>";
        public static string LINE = "<hr/>";
        public static string H5 = "<h5>";
        public static string EH5 = "</h5>";
        public static string H6 = "<h6>";
        public static string EH6 = "</h6>";
        public static string H2 = "<h2>";
        public static string EH2 = "</h2>";
        public static string H3 = "<h3>";
        public static string EH3 = "</h3>";
        public static string H4 = "<h4>";
        public static string EH4 = "</h4>";
        public static string ITACLIC = "<i>";
        public static string EITACLIC = "</i>";
        public static string DIV = "<div>";
        public static string EDIV = "</div>";
        public static string LIST = "<ul>";
        public static string ELIST = "</ul>";
        public static string LIST_ITEM = "<li>";
        public static string BREAK = "<br>";
        public static string SUPER = "<sup>";
        public static string ESUPER = "</sup>";

        //time in rounds, 1 round = 6 seconds
        public static int ROUNDS_PER_MINUTE = 10; //60 seconds /6 seconds
        public static int ROUNDS_PER_HOUR = 60; //(60 seconds /6 seconds) * 60 minutes       

        public static List<string> CRVaules = new List<string> { Environment.NewLine, "\n\r", "\n" };
    }
}
