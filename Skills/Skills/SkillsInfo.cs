using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace Skills
{
    public static class SkillsInfo  
    {
        public struct SkillInfo : IEnumerable 
        {
            public Skill Skill;
            public int Value;
            public int Rank;
            public string SubValue;
            public string SkillMod;

            public SkillInfo(Skill Skill, int Value, string SubValue, string SkillMod, int Rank)
            {
                this.Skill = Skill;
                this.Value = Value;
                this.SubValue = SubValue;
                this.SkillMod = SkillMod;
                this.Rank = Rank;
            }

            public SkillInfo(string Skill)
            {
                string skillRaw = Skill;
                SkillMod = string.Empty;
                SubValue = string.Empty;
                Rank = 1; //at least one
                int Pos = Skill.IndexOf("+");
                int Pos2 = Skill.IndexOf("-");
                if (Pos2 > 0)
                {
                    //hyphen in craft name
                    int Pos3 = Skill.IndexOf(PathfinderConstants.PAREN_RIGHT);
                    if (Pos2 < Pos3 && Pos3 > 0)
                    {
                        Pos3 = Skill.IndexOf(PathfinderConstants.PAREN_LEFT);
                        if (Pos2 > Pos3 && Pos3 > 0)
                        {
                            Pos2 = -1;
                        }
                    }
                }

                if (Pos == -1 || (Pos2 > 0 && Pos2 < Pos))
                {
                    Pos = Skill.IndexOf("-");
                }
                string hold = Skill.Substring(0, Pos);
                string value = Skill.Replace(hold, string.Empty);                

                if (value.Contains(PathfinderConstants.PAREN_LEFT))
                {
                    SkillMod = value.Substring(value.IndexOf(PathfinderConstants.PAREN_LEFT));
                    value = value.Replace(SkillMod, string.Empty).Trim();                    
                }

                if (Skill.Contains(StatBlockInfo.SkillNames.CRAFT))
                {
                    this.Skill = SkillData.GetSkill(StatBlockInfo.SkillNames.CRAFT);
                    hold = hold.Replace(StatBlockInfo.SkillNames.CRAFT, string.Empty);
                    hold = hold.Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
                    hold = hold.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                    this.SubValue = hold.Trim();
                    this.Value = Convert.ToInt32(value);

                }
                else if (Skill.Contains(StatBlockInfo.SkillNames.PERFORM))
                {
                    this.Skill = SkillData.GetSkill(StatBlockInfo.SkillNames.PERFORM);
                    hold = hold.Replace(StatBlockInfo.SkillNames.PERFORM, string.Empty);
                    hold = hold.Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
                    hold = hold.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                    this.SubValue = hold.Trim();
                    this.Value = Convert.ToInt32(value);

                }
                else if (Skill.Contains(StatBlockInfo.SkillNames.PROFESSION))
                {
                    this.Skill = SkillData.GetSkill(StatBlockInfo.SkillNames.PROFESSION);
                    hold = hold.Replace(StatBlockInfo.SkillNames.PROFESSION, string.Empty);
                    hold = hold.Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
                    hold = hold.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                    this.SubValue = hold.Trim();
                    this.Value = Convert.ToInt32(value);
                }
                else
                {
                    value = value.Replace(PathfinderConstants.SPACE, string.Empty);
                    value = value.Replace("*", string.Empty);
                    this.Skill = SkillData.GetSkill(hold.Trim());
                    this.Value = Convert.ToInt32(value);
                }               
                
            }

            public string FullName()
            {
                if (SubValue.Length > 0)
                {
                    return Skill.Name.Trim() + " [" + SubValue + "]";
                }
                if (Skill != null)
                {
                    return Skill.Name;
                }
                else
                {
                    return "Unknown";
                }
            }

            public string FullSkillName()
            {
                if (SubValue.Length > 0)
                {
                    return Skill.Name.Trim() + " (" + SubValue + PathfinderConstants.PAREN_RIGHT;
                }
                return Skill.Name;
            }



            #region IEnumerable Members

            public IEnumerator GetEnumerator()
            {
                yield return this;
            }

            #endregion
        }
    }
}
