using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ReflectionFoundation;

namespace ClassManager
{
    public static class ClassDetailsWrapper
    {
        public static void GetClassDetailClass(string ClassName, params string[] Paramaters)
        {
            ReflectionFoundation.ReflectionFoundation RF = new ReflectionFoundation.ReflectionFoundation();
            if (RF.LoadAssembly("ClassDetails"))
            {
                if (RF.GetType("ClassDetails." + ClassName))
                {
                    if (RF.GetMethod(Paramaters[0]))
                    {
                        int cnt = 0;
                        if (RF.ParamInfoCount() > 0)
                        {
                            foreach (string param_name in RF)
                            {
                                switch (param_name)
                                {
                                    case "Domains":
                                        RF.AddMethodArg(Paramaters[1], cnt);
                                        break;
                                }
                                cnt++;
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Class Detail doesn't exist: " + ClassName);
                }

            }
            //Assembly a = null;
            //try
            //{
            //    a = Assembly.Load("ClassDetails");
            //}
            //catch (FileNotFoundException ex)
            //{

            //}

            //Type cond = a.GetType("ClassDetails." + ClassName); //null if not found
            //object obj = Activator.CreateInstance(cond);
            //MethodInfo mi = cond.GetMethod(Paramaters[0]);

            //ParameterInfo[] ParamInfos = mi.GetParameters();
            //object[] args = new object[ParamInfos.Count()];
            //int cnt = 0;
            //foreach (ParameterInfo pi in ParamInfos)
            //{
            //    switch (pi.Name)
            //    {
            //        case "Domains":
            //            args[cnt] = Paramaters[1];
            //            break;                    
            //    }
            //    cnt++;
            //}
            //if (ParamInfos.Count() > 0)
            //{
            //    mi.Invoke(obj, args);
            //}
            //else
            //{
            //    mi.Invoke(obj, null);
            //}
        }
    }
}
