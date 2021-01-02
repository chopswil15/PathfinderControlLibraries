using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ClassDetails
{
    public class ClassMaster
    {
        private List<ClassWrapper> Classes { get; set;}
        private ClassWrapper CW;
        private Assembly Assemb;

        public ClassMaster()
        {
            Classes = new List<ClassWrapper>();
            try
            {
                Assemb = Assembly.Load("ClassDetails");
            }
            catch(FileNotFoundException ex)
            {
               //
            }
    }

        public void ParseClassList(string ClassList)
        {
            List<string> tempClasses = ClassList.Split('/').ToList<string>();
            string ClassName = string.Empty;
            string ClassLevel = string.Empty;
            int Pos = 0;

            foreach (string tempclass in tempClasses)
            {
                Pos = tempclass.LastIndexOf(" ");
                ClassName = tempclass.Substring(0, Pos).Trim();
                ClassLevel = tempclass.Replace(ClassName, string.Empty).Trim();
                ClassName = ProperCase(ClassName);
                Type ClassInst = Assemb.GetType("ClassDetails." + ClassName);
                if (ClassInst != null)
                {
                    object obj = Activator.CreateInstance(ClassInst);
                    CW = new ClassWrapper();
                    CW.ClassInstance = (ClassFoundation)obj;
                    CW.Level = Convert.ToInt32(ClassLevel);
                    Classes.Add(CW);
                }
            }
        }

        private string ProperCase(string stringInput)
        {
            StringBuilder sb = new StringBuilder();
            bool fEmptyBefore = true;
            stringInput = stringInput.ToLower();

            foreach (char ch in stringInput)
            {
                char chThis = ch;
                if (Char.IsWhiteSpace(chThis))
                    fEmptyBefore = true;
                else
                {
                    if (Char.IsLetter(chThis) && fEmptyBefore)
                        chThis = Char.ToUpper(chThis);
                    else
                        chThis = Char.ToLower(chThis);
                    fEmptyBefore = false;
                }
                sb.Append(chThis);
            }
            return sb.ToString();
        }
    }
}
