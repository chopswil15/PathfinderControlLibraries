using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using OnGoing;
using StatBlockCommon.Individual_SB;

namespace StatBlockCommon.ReflectionWrappers
{
    public static class ConditionReflectionWrapper
    {
        public static string ApplyCondition(IndividualStatBlock_Combat TargetStatBlock, OnGoingCondition Condition)
        { 
            Assembly a = null;
            try
            {
                a = Assembly.Load("Conditions");
            }
            catch (FileNotFoundException ex)
            {

            }

            Type cond = a.GetType("Conditions.ApplyCondition"); //null if not found
            object obj = Activator.CreateInstance(cond);
            MethodInfo mi = cond.GetMethod(Condition.ConditionType.ToString());

            ParameterInfo[] ParamInfos = mi.GetParameters();
            object[] args = new object[ParamInfos.Count()];
            int cnt = 0;
            foreach (ParameterInfo pi in ParamInfos)
            {
                switch (pi.Name)
                {                    
                    case "target":
                        args[cnt] = TargetStatBlock;
                        break;
                    case "Condition":
                        args[cnt] = Condition;
                        break;
                }
                cnt++;
            }
            if (ParamInfos.Any())
            {
                return (string)mi.Invoke(obj, args);
            }
            else
            {
                return (string)mi.Invoke(obj, null);
            }
        }
    }
}
