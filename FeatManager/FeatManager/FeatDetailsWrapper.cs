using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using FeatFoundational;
using PathfinderGlobals;

namespace FeatManager
{
    public static class FeatDetailsWrapper
    {
        public static FeatFoundation GetFeatDetailClass(string Feat)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;


            ReflectionFoundation.ReflectionFoundation RF = new ReflectionFoundation.ReflectionFoundation();
            if (RF.LoadAssembly("FeatDetails"))
            {
                Feat = textInfo.ToTitleCase(Feat);
                Feat = Feat.Replace("-", string.Empty);
                Feat = Feat.Replace(PathfinderConstants.SPACE, string.Empty);
                if (RF.GetType("FeatDetails." + Feat))
                {
                    return (FeatFoundation)RF.Instance;
                }  
            }
            return null;
        }
    }
}
