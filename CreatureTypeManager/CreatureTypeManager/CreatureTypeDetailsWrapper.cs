using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReflectionFoundation;
using System.Globalization;
using System.Threading;
using CreatureTypeFoundational;
using Utilities;
using PathfinderGlobals;

namespace CreatureTypeManager
{
    public static class CreatureTypeDetailsWrapper
    {
        public static CreatureTypeFoundation GetRaceDetailClass(string CreatureType)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;


            ReflectionFoundation.ReflectionFoundation RF = new ReflectionFoundation.ReflectionFoundation();
            if (RF.LoadAssembly("CreatureTypeDetails"))
            {
                CreatureType = textInfo.ToTitleCase(CreatureType);
                CreatureType = CreatureType.Replace(PathfinderConstants.SPACE, string.Empty);
                if (RF.GetType("CreatureTypeDetails." + CreatureType))
                {
                    return (CreatureTypeFoundation)RF.Instance;
                }

            }
            RF.GetType("CreatureTypeDetails.UnknownCreatureType");
            return (CreatureTypeFoundation)RF.Instance;
        }
    }
}
