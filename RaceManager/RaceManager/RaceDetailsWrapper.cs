using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReflectionFoundation;
using System.Globalization;
using System.Threading;
using RaceFoundational;

namespace RaceManager
{
    public static class RaceDetailsWrapper
    {
        public static RaceFoundation GetRaceDetailClass(string Race)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;            
            TextInfo textInfo = cultureInfo.TextInfo;


            ReflectionFoundation.ReflectionFoundation RF = new ReflectionFoundation.ReflectionFoundation();
            if (RF.LoadAssembly("RaceDetails"))
            {
                Race = textInfo.ToTitleCase(Race);
                Race = Race.Replace("-", string.Empty);
                Race = Race.Replace(" ", string.Empty);
                Race = Race.Trim();
                if (RF.GetType("RaceDetails." + Race))
                {
                    return (RaceFoundation)RF.Instance;
                }

            }
            return null;
        }
    }
}
