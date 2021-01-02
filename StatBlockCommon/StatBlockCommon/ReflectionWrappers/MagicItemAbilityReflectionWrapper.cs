using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Globalization;
using System.Threading;

using MagicItemAbilityWrapper;
using Utilities;
using PathfinderGlobals;

namespace StatBlockCommon.ReflectionWrappers
{
    public static class MagicItemAbilityReflectionWrapper
    {
        public static MagicItemAbilitiesWrapper GetMagicItemAbility(string ItemName, string ExtraAbilities)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            string holdItem = ItemName;

            ItemName = textInfo.ToTitleCase(ItemName);
            ItemName = ItemName.Replace(PathfinderConstants.SPACE, string.Empty).Replace("-", string.Empty).Replace("'", string.Empty);
            int Pos = ItemName.IndexOf("+");
            int Bonus = 0;
            if (Pos >= 0)
            {
                Bonus = Convert.ToInt32(ItemName.Substring(Pos+1));
                ItemName = ItemName.Substring(0,Pos);
            }

            //Pos = ItemName.IndexOf(PathfinderConstants.PAREN_LEFT);
            //string Abilities = string.Empty;
            //if (Pos >= 0)
            //{
            //    Abilities = ItemName.Substring(Pos + 1);
            //    ItemName = ItemName.Substring(0, Pos);
            //}

            Assembly a = null;
            try
            {
                a = Assembly.Load("MagicItemAbilities");
            }
            catch (FileNotFoundException ex)
            {
                throw new Exception("Missing Assembly: MagicItemAbilities");
            }

            Type MIAbilities = a.GetType("MagicItemAbilities.GetAbilities"); //null if not found
            object obj = Activator.CreateInstance(MIAbilities);

            MethodInfo mi = MIAbilities.GetMethod(ItemName);

            if (mi == null)
            {
                throw new Exception("MagicItemAbilities, missing entry for " + holdItem);
            }
            else
            {
                ParameterInfo[] ParamInfos = mi.GetParameters();
                object[] args = new object[ParamInfos.Count()];
                int cnt = 0;
                foreach (ParameterInfo pi in ParamInfos)
                {
                    switch (pi.Name)
                    {
                        case "Bonus":
                            args[cnt] = Bonus;
                            break;  
                        case "Abilities":
                            args[cnt] = ExtraAbilities;
                            break;
                    }
                    cnt++;
                }
                if (ParamInfos.Any())
                {
                    return (MagicItemAbilitiesWrapper)mi.Invoke(obj, args);
                }
                else
                {
                    return (MagicItemAbilitiesWrapper)mi.Invoke(obj, null);
                }
            }
        }
    }
}
