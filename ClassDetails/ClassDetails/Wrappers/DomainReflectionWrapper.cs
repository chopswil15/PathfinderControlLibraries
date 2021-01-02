using System;
using System.IO;
using System.Reflection;
using ClericDomains;

namespace ClassDetails
{
    public static class DomainReflectionWrapper
    {
        public static IDomain AddDomainClass(string Domian)
        {
            Assembly a = null;
            try
            {
                a = Assembly.Load("ClericDomains");
            }
            catch (FileNotFoundException ex)
            {

            }

            Type cond = a.GetType("ClericDomains." + Domian); //null if not found
            if (cond != null)
            {
                object obj = Activator.CreateInstance(cond);
                return (IDomain)obj;
            }
            return null;
        }
    }
}
