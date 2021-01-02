using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Reflection;
using StatBlockCommon.Monster_SB;
using PathfinderGlobals;
using TemplateFoundational;

namespace TemplateManager
{
    public class TemplateManager
    {
        private Assembly Assemb;
        private Type ClassInst;

        private bool LoadAssemlyInfo(string templateName)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            try
            {
                Assemb = Assembly.Load("TemplateDetails");
            }
            catch (FileNotFoundException ex)
            {
                //
            }

            templateName = templateName.Replace("@", PathfinderConstants.SPACE);
            templateName = textInfo.ToTitleCase(templateName);
            templateName = templateName.Replace(PathfinderConstants.SPACE, string.Empty).Replace("-", string.Empty)
                .Replace(PathfinderConstants.SPACE, string.Empty).Replace("Hd","HD");
            ClassInst = Assemb.GetType("TemplateDetails." + templateName);
            return ClassInst != null ? true : false;
        }

        public MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB, string templateName, out bool success)
        {
            success = false;            
            templateName = templateName.Replace("|", string.Empty);            
            if(LoadAssemlyInfo(templateName))            
            {
                object obj = Activator.CreateInstance(ClassInst);
                TemplateFoundation Template = (TemplateFoundation)obj;
                success = true;
                return Template.ApplyTemplate(MonSB);
            }

            return MonSB;
        }

        public MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB, string templateName, List<string> stackableTemplates, out List<string> failureList)
        {
            failureList = new List<string>();
            templateName = templateName.Replace("|", string.Empty);
            if (LoadAssemlyInfo(templateName))
            {
                object obj = Activator.CreateInstance(ClassInst);
                TemplateFoundation Template = (TemplateFoundation)obj;
                return Template.ApplyTemplate(MonSB, stackableTemplates);
            }

            return MonSB;
        }

        public MonsterStatBlock ApplyMultipleTemplates(MonsterStatBlock MonSB, List<string> templates, out List<string> failureList)
        {
            failureList = new List<string>();
            foreach (string template in templates)
            {
                bool success;
                MonSB = ApplyTemplate(MonSB, template, out success);
                if (!success) failureList.Add(template);
            }
            return MonSB;
        }

        public MonsterStatBlock ApplyAdvancedHDTemplate(MonsterStatBlock MonSB, string templateName, string NewSize, int HighCR, int LowCR)
        {
            if (LoadAssemlyInfo(templateName)) 
            {
                try
                {
                    object obj = Activator.CreateInstance(ClassInst);
                    TemplateFoundation Template = (TemplateFoundation)obj;
                    return Template.ApplyAdvancedHDTemplate(MonSB, NewSize, HighCR, LowCR);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }
    }
}
