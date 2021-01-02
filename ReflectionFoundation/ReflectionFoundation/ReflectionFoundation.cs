using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ReflectionFoundation
{
    public class ReflectionFoundation : IEnumerable<string>
    {
        private Assembly assembly;
        private Type type;
        private object instance;
        private MethodInfo mi;
        private ParameterInfo[] ParamInfos;
        private object[] MethodArgs;

        public IEnumerator<string> GetEnumerator()
        {            
            foreach (ParameterInfo pi in ParamInfos)
            {                
                yield return pi.Name;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public object Instance
        {
            get
            {
                return instance;
            }
        }

        public int ParamInfoCount()
        {
            if (ParamInfos != null)
            {
                return ParamInfos.Count();
            }
            else
            {
                return 0;
            }
        }

        public  bool LoadAssembly(string AssemblyName)
        {
            assembly = null;
            try
            {
                assembly = Assembly.Load(AssemblyName);
            }
            catch (FileNotFoundException ex)
            {
                return false;
            }
            return true;
        }

        public  bool GetType(string TypeName)
        {
            type = assembly.GetType(TypeName);
            if (type != null)
            {
                instance = Activator.CreateInstance(type);
                return true;
            }
            else
            {
                return false;
            }
        }

        public  bool GetMethod(string MethodName)
        {
            try
            {
                mi = type.GetMethod(MethodName);

                ParamInfos = mi.GetParameters();
                MethodArgs = new object[ParamInfos.Count()];
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void AddMethodArg(object Arg, int index)
        {
            MethodArgs[index] = Arg;
        }

        public object Invoke()
        {
            if (ParamInfos.Any())
            {
                return (object)mi.Invoke(instance, MethodArgs);
            }
            else
            {
                return (object)mi.Invoke(instance, null);
            }
        }  
    }
}
