using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using StatBlockCommon;
using StatBlockParsing;

namespace StatBlockChecker
{
    public class TemplateChecker : ITemplateChecker
    {
        private ISBCheckerBaseInput _sbCheckerBaseInput;

        public TemplateChecker(ISBCheckerBaseInput sbCheckerBaseInput)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
        }

        public void CheckTemplates()
        {
            if (_sbCheckerBaseInput.Race_Base.RaceBaseType != RaceBase.RaceType.None)
            {
                TemplateManager.TemplateManager TM = new TemplateManager.TemplateManager();
                MonsterStatBlock TestMonSB = _sbCheckerBaseInput.Race_Base.RaceSB;

                if (TestMonSB == null) return;
                if (_sbCheckerBaseInput.MonsterSB.TemplatesApplied.Length != 0)
                {
                    CheckAppliedTemplates(TM, ref TestMonSB);
                }
                else
                {
                    if (_sbCheckerBaseInput.Race_Base.Size() != _sbCheckerBaseInput.MonsterSB.Size)
                    {
                        string temp = _sbCheckerBaseInput.MonsterSB.CR;
                        try
                        {
                            if (temp == "1/2") return;
                            string temp2 = TestMonSB.CR;
                            if (temp2 == "1/2") return;
                            TestMonSB = TM.ApplyAdvancedHDTemplate(TestMonSB, "Advanced HD", _sbCheckerBaseInput.MonsterSB.Size, Convert.ToInt32(temp), Convert.ToInt32(temp2));
                            _sbCheckerBaseInput.Race_Base.ApplyTemplatedRaceSB(TestMonSB, false);
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
            if (_sbCheckerBaseInput.MonsterSB.TemplatesApplied.Contains(",")) stackedTemplates = true; //|1,2,3…@sk|
            List<string> templates = _sbCheckerBaseInput.MonsterSB.TemplatesApplied.Split('|').ToList();
            templates.RemoveAll(x => x == string.Empty);
            List<string> stackedTemplateList = new List<string>();
            string stackedTemplateBase = string.Empty;
            if (stackedTemplates) FindStackTemplates(templates, ref stackedTemplateList, ref stackedTemplateBase);

            foreach (string appliedTemplate in templates)
            {
                string lowerAppliedTemplate = appliedTemplate.ToLower();
                if (lowerAppliedTemplate.Contains("advanced") || lowerAppliedTemplate.Contains("advanced hd") || lowerAppliedTemplate.Contains("advanced-hd"))
                {
                    if (_sbCheckerBaseInput.MonsterSB.HDValue() - _sbCheckerBaseInput.CharacterClasses.FindTotalClassLevels() != _sbCheckerBaseInput.Race_Base.RacialHDValue())
                    {
                        TestMonSB = ApplyAdvancedHDTemplate(TM, TestMonSB);
                    }
                    else
                    {
                        if (TestMonSB != null)
                        {
                            List<string> Failures;
                            if (stackedTemplates)
                            {
                                TestMonSB = ApplyStackedTemplates(TM, TestMonSB, stackedTemplateList, stackedTemplateBase, out Failures);
                            }
                            else
                            {
                                if (templates.Count == 1)
                                {
                                    TestMonSB = ApplyOneTemplate(CheckName, TM, TestMonSB);
                                }
                                else
                                {
                                    TestMonSB = ApplyMultipleTemplates(TM, TestMonSB, templates, stackedTemplateList);
                                }
                            }
                            _sbCheckerBaseInput.Race_Base.ApplyTemplatedRaceSB(TestMonSB, true);
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
                        {
                            _sbCheckerBaseInput.MessageXML.AddFail("CheckTemplates", "Template not applied for " + lowerAppliedTemplate.Trim());
                        }
                        else
                        {
                            _sbCheckerBaseInput.MessageXML.AddInfo("Template Applied - " + lowerAppliedTemplate.Trim());
                            _sbCheckerBaseInput.Race_Base.ApplyTemplatedRaceSB(TestMonSB, true);
                        }
                    }
                }
            }
        }

        private MonsterStatBlock ApplyAdvancedHDTemplate(TemplateManager.TemplateManager TM, MonsterStatBlock TestMonSB)
        {
            try
            {
                TestMonSB = TM.ApplyAdvancedHDTemplate(TestMonSB, "Advanced HD", _sbCheckerBaseInput.MonsterSB.Size, Convert.ToInt32(_sbCheckerBaseInput.MonsterSB.CR), Convert.ToInt32(TestMonSB.CR));// _sbCheckerBaseInput.MonsterSB.HD;
            }
            catch { }
            _sbCheckerBaseInput.Race_Base.ApplyTemplatedRaceSB(TestMonSB, false);
            _sbCheckerBaseInput.MessageXML.AddInfo("Template Applied - Advanced HD");
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
                    stackedTemplateList = templateHold.Split(',').ToList();
                }
            }
        }

        private MonsterStatBlock ApplyMultipleTemplates(TemplateManager.TemplateManager TM, MonsterStatBlock TestMonSB, List<string> templates, List<string> stackedTemplateList)
        {
            List<string> Failures;
            TestMonSB = TM.ApplyMultipleTemplates(TestMonSB, templates, out Failures);
            if (Failures.Any())
                _sbCheckerBaseInput.MessageXML.AddInfo("Failed Application of Template - " + string.Join(",", stackedTemplateList.ToArray()));

            return TestMonSB;
        }

        private MonsterStatBlock ApplyOneTemplate(string CheckName, TemplateManager.TemplateManager TM, MonsterStatBlock TestMonSB)
        {
            bool success;
            TestMonSB = TM.ApplyTemplate(TestMonSB, _sbCheckerBaseInput.MonsterSB.TemplatesApplied.Trim(), out success);
            if (success)
            {
                _sbCheckerBaseInput.MessageXML.AddInfo("Template Applied - " + _sbCheckerBaseInput.MonsterSB.TemplatesApplied.Trim());
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail(CheckName, "Failed Application of Template - " + _sbCheckerBaseInput.MonsterSB.TemplatesApplied.Trim());
            }

            return TestMonSB;
        }

        private MonsterStatBlock ApplyStackedTemplates(TemplateManager.TemplateManager TM, MonsterStatBlock TestMonSB, List<string> stackedTemplateList, string stackedTemplateBase, out List<string> Failures)
        {
            TestMonSB = TM.ApplyTemplate(_sbCheckerBaseInput.MonsterSB, stackedTemplateBase, stackedTemplateList, out Failures);
            if (Failures.Any())
                _sbCheckerBaseInput.MessageXML.AddInfo("Failed Application of Template - " + string.Join(",", Failures.ToArray()));

            return TestMonSB;
        }
    }
}
