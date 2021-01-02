using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using ReflectionFoundation;

namespace ClassDetails
{
    public static class ClassPowerReflectionWrapper 
    {
        public static List<OnGoingPower> AddClassPower(string Power)
        {
            List<OnGoingPower> temp = new List<OnGoingPower>();
            ReflectionFoundation.ReflectionFoundation RF = new ReflectionFoundation.ReflectionFoundation();
            if (RF.LoadAssembly("ClassPowers"))
            {
                if (RF.GetType("ClassPowers.GetClassPowers"))
                {
                    if (RF.GetMethod(Power))
                    {
                        int cnt = 0;
                        if (RF.ParamInfoCount() > 0)
                        {
                            foreach (string param_name in RF)
                            {
                                switch (param_name)
                                {
                                    //case "target":
                                    //    args[cnt] = TargetStatBlock;
                                    //    break;
                                    //case "Condition":
                                    //    args[cnt] = Condition;
                                    //    break;
                                }
                                cnt++;
                            }
                        }
                        return (List<OnGoingPower>)RF.Invoke();
                    }
                }
            }
            return temp;
        }
    }
}
