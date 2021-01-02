using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using StatBlockCommon;
using ClassManager;

namespace StatBlockChecker
{
    public class TemplateChecker
    {
        private StatBlockMessageWrapper _messageXML;
        private MonSBSearch _monSBSearch;
        private RaceBase Race_Base;
        private MonsterStatBlock MonSB;
        private ClassMaster CharacterClasses;


        public TemplateChecker(StatBlockMessageWrapper _messageXML, MonSBSearch _monSBSearch, RaceBase Race_Base, MonsterStatBlock MonSB, ClassMaster CharacterClasses)
        {
            this._messageXML = _messageXML;
            this._monSBSearch = _monSBSearch;
            this.Race_Base = Race_Base;
            this.MonSB = MonSB;
            this.CharacterClasses = CharacterClasses;
        }

        public void CheckTemplates()
        {
            if (Race_Base.RaceBaseType != RaceBase.RaceType.None)
            {
                TemplateManager.TemplateManager TM = new TemplateManager.TemplateManager();
                MonsterStatBlock TestMonSB = Race_Base.RaceSB;

                if (TestMonSB == null) return;
                if (MonSB.TemplatesApplied.Length != 0)
                    CheckAppliedTemplates(TM, ref TestMonSB);
                else
                {
                    if (Race_Base.Size() != MonSB.Size)
                    {
                        string temp = MonSB.CR;
                        try
                        {
                            if (temp == "1/2") return;
                            string temp2 = TestMonSB.CR;
                            if (temp2 == "1/2") return;
                            TestMonSB = TM.ApplyAdvancedHDTemplate(TestMonSB, "Advanced HD", MonSB.Size, Convert.ToInt32(temp), Convert.ToInt32(temp2));
                            Race_Base.ApplyTemplatedRaceSB(TestMonSB, false);
                        }
                        catch { }
                    }
                }
            }
        }

        private void CheckAppliedTemplates(TemplateManager.TemplateManager TM, ref MonsterStatBlock TestMonSB)
        {
            string CheckName = "CheckAppliedTemplates";
            bool stackedTemplates = false;
            if (MonSB.TemplatesApplied.Contains(",")) stackedTemplates = true; //|1,2,3…@sk|
            List<string> templates = MonSB.TemplatesApplied.Split('|').ToList<string>();
            templates.Remove("");
            List<string> stackedTemplateList = new List<string>();
            string stackedTemplateBase = string.Empty;
            if (stackedTemplates)
                FindStackTemplates(templates, ref stackedTemplateList, ref stackedTemplateBase);

           
            foreach (string appliedTemplate in templates)
            {
                string lowerAppliedTemplate = appliedTemplate.ToLower();
                if (lowerAppliedTemplate.Contains("advanced") || lowerAppliedTemplate.Contains("advanced hd"))
                {
                    if (MonSB.HDValue() - CharacterClasses.FindTotalClassLevels() != Race_Base.RacialHDValue())
                        TestMonSB = ApplyAdvancedHDTemplate(TM, TestMonSB);
                    else
                    {
                        if (TestMonSB != null)
                        {
                            List<string> Failures;
                            if (stackedTemplates)
                                TestMonSB = ApplyStackedTemplates(TM, TestMonSB, stackedTemplateList, stackedTemplateBase, out Failures);
                            else
                            {
                                if (templates.Count == 1)
                                    TestMonSB = ApplyOneTemplate(CheckName, TM, TestMonSB);
                                else
                                    TestMonSB = ApplyMultipleTemplates(TM, TestMonSB, templates, stackedTemplateList);
                            }
                            Race_Base.ApplyTemplatedRaceSB(TestMonSB, true);
                        }
                    }
                }
                else
                {
                    if (TestMonSB != null)
                    {
                        bool Success;
                        MonsterStatBlock tempmonSB = null;
                        List<string> Failures;

                        if (stackedTemplates)
                        {
                            TestMonSB = ApplyStackedTemplates(TM, TestMonSB, stackedTemplateList, stackedTemplateBase, out Failures);
                            Success = !Failures.Any();
                        }
                        else
                        {
                            TM.ApplyTemplate(TestMonSB, lowerAppliedTemplate.Trim(), out Success);
                        }
                        if (tempmonSB != null) TestMonSB = tempmonSB;
                        if (!Success)
                            _messageXML.AddFail("CheckTemplates", "Template not applied for " + lowerAppliedTemplate.Trim());
                        else
                        {
                            _messageXML.AddInfo("Template Applied - " + lowerAppliedTemplate.Trim());
                            Race_Base.ApplyTemplatedRaceSB(TestMonSB, true);
                        }
                    }
                }
            }
        }

        private MonsterStatBlock ApplyAdvancedHDTemplate(TemplateManager.TemplateManager TM, MonsterStatBlock TestMonSB)
        {
            try
            {
                TestMonSB = TM.ApplyAdvancedHDTemplate(TestMonSB, "Advanced HD", MonSB.Size, Convert.ToInt32(MonSB.CR), Convert.ToInt32(TestMonSB.CR));// MonSB.HD;
            }
            catch { }
            Race_Base.ApplyTemplatedRaceSB(TestMonSB, false);
            _messageXML.AddInfo("Template Applied - Advanced HD");
            return TestMonSB;
        }

        private static void FindStackTemplates(List<string> templates, ref List<string> stackedTemplateList, ref string stackedTemplateBase)
        {
            foreach (string template in templates)
            {
                if (template.Contains(","))
                {
                    int Pos = template.IndexOf("@");
                    stackedTemplateBase = template.Substring(Pos);
                    string templateHold = template.Replace(stackedTemplateBase, string.Empty).Trim();
                    stackedTemplateBase = stackedTemplateBase.Replace("@", string.Empty);
                    stackedTemplateList = templateHold.Split(',').ToList<string>();
                }
            }
        }

        private MonsterStatBlock ApplyMultipleTemplates(TemplateManager.TemplateManager TM, MonsterStatBlock TestMonSB, List<string> templates, List<string> stackedTemplateList)
        {
            List<string> Failures;
            TestMonSB = TM.ApplyMultipleTemplates(TestMonSB, templates, out Failures);
            if (Failures.Any())
                _messageXML.AddInfo("Failed Application of Template - " + string.Join(",", stackedTemplateList.ToArray()));

            return TestMonSB;
        }

        private MonsterStatBlock ApplyOneTemplate(string CheckName, TemplateManager.TemplateManager TM, MonsterStatBlock TestMonSB)
        {
            bool Success;
            TestMonSB = TM.ApplyTemplate(TestMonSB, MonSB.TemplatesApplied.Trim(), out Success);
            if (Success)           
                _messageXML.AddInfo("Template Applied - " + MonSB.TemplatesApplied.Trim());
            else
                _messageXML.AddFail(CheckName, "Failed Application of Template - " + MonSB.TemplatesApplied.Trim());

            return TestMonSB;
        }

        private MonsterStatBlock ApplyStackedTemplates(TemplateManager.TemplateManager TM, MonsterStatBlock TestMonSB, List<string> stackedTemplateList, string stackedTemplateBase, out  List<string> Failures)
        {           
            TestMonSB = TM.ApplyTemplate(MonSB, stackedTemplateBase, stackedTemplateList, out Failures);
            if (Failures.Any())
                _messageXML.AddInfo("Failed Application of Template - " + string.Join(",", Failures.ToArray()));

            return TestMonSB;
        }
    }
}
