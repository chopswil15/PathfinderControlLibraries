using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Patrons;

namespace ClassDetails
{
    public class PatronReflectionWrapper
    {
        public static IPatron AddPatronClass(string Patron)
        {
            Assembly a = null;
            try
            {
                a = Assembly.Load("Patrons");
            }
            catch (FileNotFoundException ex)
            {

            }

            Type cond = a.GetType("Patrons." + Patron); //null if not found
            if (cond != null)
            {
                object obj = Activator.CreateInstance(cond);
                return (IPatron)obj;
            }
            return null;
        }
    }
}
