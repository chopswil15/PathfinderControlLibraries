using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Bloodlines;
using Utilities;
using PathfinderGlobals;

namespace ClassDetails
{
    public class BloodlineReflectionWrapper
    {
        public static IBloodline AddBloodline(string Bloodline, string Class)
        {
            Assembly a = null;
            try
            {
                a = Assembly.Load("Bloodlines");
            }
            catch (FileNotFoundException ex)
            {

            }

            Bloodline = Bloodline.Replace("*", string.Empty);
            if(Bloodline.Contains(PathfinderConstants.PAREN_LEFT))
            {
                int Pos = Bloodline.IndexOf(PathfinderConstants.PAREN_LEFT);
                Bloodline = Bloodline.Substring(0, Pos);
                Bloodline = Bloodline.Replace(PathfinderConstants.PAREN_LEFT, string.Empty).Trim();
            }

            if (Class != "Sorcerer") Bloodline += Class;

             Type cond = a.GetType("Bloodlines." + Bloodline); //null if not found
            if (cond != null)
            {
                object obj = Activator.CreateInstance(cond);
                return (IBloodline)obj;
            }
            return null;
        }
    }
}
