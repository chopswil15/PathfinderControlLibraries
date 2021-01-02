using System;
using System.IO;
using System.Reflection;
using OracleMysteries;

namespace ClassDetails
{
    public static class MysteryReflectionWrapper
    {
        public static IMystery AddMysteryClass(string Mystery)
        {
            bool loadFailed = false;
            Assembly a = null;
            try
            {
                a = Assembly.Load("OracleMysteries");
            }
            catch (FileNotFoundException ex)
            {
                loadFailed = true;
            }

            if(loadFailed)
            {
                try
                {
                    a = Assembly.Load(@"\Dynamic\OracleMysteries");
                    loadFailed = false;
                }
                catch (FileNotFoundException ex)
                {
                    
                }
            }

            Type cond = a.GetType("OracleMysteries." + Mystery); //null if not found
            if (cond != null)
            {
                object obj = Activator.CreateInstance(cond);
                return (IMystery)obj;
            }
            return null;
        }
    }
}
