using Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using CommonStatBlockInfo;

namespace StatBlockChecker.Skills
{
    public class SkillsParser : ISkillsParser
    {
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        public SkillsParser(ISBCheckerBaseInput sbCheckerBaseInput)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
        }
        public List<SkillsInfo.SkillInfo> ParseSkills(string skillsSB)
        {
            string hold = skillsSB;
            hold = hold.Replace("*", string.Empty);
            Utility.ParenCommaFix(ref hold);
            List<string> skills = hold.Split(',').ToList();
            skills.RemoveAll(x => x== string.Empty);

            for (int a = skills.Count - 1; a >= 0; a--)
            {
                if (skills[a].Contains("|") && skills[a].Contains("Knowledge"))
                {
                    string hold2 = skills[a];
                    skills.Remove(skills[a]);
                    int Pos = hold2.IndexOf("+");
                    string plus = hold2.Substring(Pos).Trim();
                    string temp = hold2.Replace(plus, string.Empty);
                    temp = temp.Replace("Knowledge", string.Empty);
                    temp = Utility.RemoveParentheses(temp);
                    List<string> Knowledge = temp.Split('|').ToList();
                    Knowledge.RemoveAll(x => x== string.Empty);
                    foreach (string knowledge in Knowledge)
                    {
                        skills.Add("Knowledge (" + knowledge.Trim() + ") " + plus);
                    }
                }

                if (skills[a].Contains(StatBlockInfo.SkillNames.KNOWLEDGE_ALL))
                {
                    string hold2 = skills[a];
                    skills.Remove(skills[a]);
                    int Pos = hold2.IndexOf("+");
                    string plus = hold2.Substring(Pos).Trim();
                    string temp = hold2.Replace(plus, string.Empty);
                    temp = temp.Replace("Knowledge", string.Empty);
                    temp = Utility.RemoveParentheses(temp);
                    List<string> Knowledge = new List<string> {StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA,StatBlockInfo.SkillNames.KNOWLEDGE_DUNGEONEERING, StatBlockInfo.SkillNames.KNOWLEDGE_ENGINEERING, StatBlockInfo.SkillNames.KNOWLEDGE_GEOGRAPHY,
                          StatBlockInfo.SkillNames.KNOWLEDGE_HISTORY,StatBlockInfo.SkillNames.KNOWLEDGE_LOCAL,StatBlockInfo.SkillNames.KNOWLEDGE_NATURE,StatBlockInfo.SkillNames.KNOWLEDGE_NOBILITY,StatBlockInfo.SkillNames.KNOWLEDGE_PLANES,StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION};
                    foreach (string knowledge in Knowledge)
                    {
                        skills.Add(knowledge.Trim() + plus);
                    }
                }

                if (skills[a].Contains("|") && skills[a].Contains(StatBlockInfo.SkillNames.PERFORM))
                {
                    string hold2 = skills[a];
                    skills.Remove(skills[a]);
                    int Pos = hold2.IndexOf("+");
                    string plus = hold2.Substring(Pos).Trim();
                    string temp = hold2.Replace(plus, string.Empty);
                    temp = temp.Replace(StatBlockInfo.SkillNames.PERFORM, string.Empty);
                    temp = Utility.RemoveParentheses(temp);
                    List<string> Perform = temp.Split('|').ToList();
                    Perform.RemoveAll(x => x== string.Empty);
                    foreach (string perform in Perform)
                    {
                        skills.Add("Perform (" + perform.Trim() + ") " + plus);
                    }
                }
            }

            List<SkillsInfo.SkillInfo> skillsValuesList = new List<SkillsInfo.SkillInfo>();
            SkillsInfo.SkillInfo tempSkillsValue;

            foreach (string skill in skills)
            {
                try
                {
                    tempSkillsValue = new SkillsInfo.SkillInfo(skill.Trim());
                    if (tempSkillsValue.Skill != null)
                    {
                        skillsValuesList.Add(tempSkillsValue);
                    }
                    else
                    {
                        _sbCheckerBaseInput.MessageXML.AddFail("Missing Skill", skill.Trim());
                    }
                }
                catch (Exception ex)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("ParseSkills", "Issue with " + skill + " -- " + ex.Message);
                }
            }

            return skillsValuesList;
        }
    }
}
