using PathfinderGlobals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Utilities;

namespace StatBlockCommon.Spell_SB
{
    public class SpellStatBlock_Combat : SpellStatBlock
    {
        private string ComputedDuration;
        private string ComputedRange;
        private string ComputedTarget;
        private string ComputedArea;
        private int ComputedDurationValue;

        public int CasterLevel { get; set; }

        public int GetDuration()
        {
            return ComputedDurationValue;
        }
        
        private enum StandardRanges
        {
            NonStandard = 0,
            Close = 1,
            Medium = 2,
            Long = 3
        }

        public enum RangeGroup
        {
            Proximate = 1, //Personal or Touch
            Distant = 2   // all others
        }

        public SpellStatBlock_Combat(SpellStatBlock SB)
        {
            Type type = SB.GetType();
            type = type.BaseType;
            UpdateForType(type, SB, this);
        }

        private static void UpdateForType(Type type, SpellStatBlock source, SpellStatBlock_Combat destination)
        {
            FieldInfo[] myObjectFields = type.GetFields(
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo fi in myObjectFields)
            {
                fi.SetValue(destination, fi.GetValue(source));
            }
        }

        private StandardRanges CheckStandardRanges(string Text)
        {
            string Lower = Text.ToLower();
            if (Lower.Contains("close ")) return StandardRanges.Close;
            if (Lower.Contains("medium ")) return StandardRanges.Medium;
            if (Lower.Contains("long ")) return StandardRanges.Long;
            return StandardRanges.NonStandard;
        }

        private string ComputeStandardRange(int CasterLevel, StandardRanges eStandardRange)
        {
            switch (eStandardRange)
            {
                case StandardRanges.Close:
                    return ((int)CasterLevel / (int)2 * 5 + 25).ToString() + " feet";
                case StandardRanges.Medium:
                    return ((int)10 * CasterLevel + 100).ToString() + " feet";
                case StandardRanges.Long:
                    return ((int)40 * CasterLevel + 400).ToString() + " feet";
                default :
                    return string.Empty;
            }            
        }

        public void ComputeRange(int CasterLevel)
        {
            if (range.Contains("/level"))
            {
                StandardRanges eStandardRange = CheckStandardRanges(range);
                if (eStandardRange != StandardRanges.NonStandard)
                {
                    ComputedRange = ComputeStandardRange(CasterLevel, eStandardRange);
                }
                else
                {
                    int Pos = range.IndexOf(PathfinderConstants.SPACE);
                    int Pos2 = range.IndexOf("/level");
                    int Number = Convert.ToInt32(range.Substring(0, Pos - 1));
                    Number = Number * CasterLevel;
                    ComputedRange = Number.ToString() + PathfinderConstants.SPACE + range.Substring(Pos + 1, Pos2 - Pos - 1);
                }
            }
            else
            {
                ComputedRange = range;
            }
        }

        public void ComputeTarget(int CasterLevel)
        {
            if (targets.Contains("/level"))
            {
                int Pos = targets.IndexOf(PathfinderConstants.SPACE);
                int Pos2 = targets.IndexOf("/level");
                string Temp = targets.Substring(0, Pos - 1);
                int Number = Convert.ToInt32(Temp);
                Number = Number * CasterLevel;
                ComputedTarget = Number + PathfinderConstants.SPACE + targets.Substring(Pos + 1, Pos2 - Pos - 1);
            }
            else
            {
                ComputedTarget = targets;
            }
        }

        public void ComputeArea(int CasterLevel)
        {
            if (area.Contains("/level"))
            {
                int Pos = area.IndexOf(PathfinderConstants.SPACE);
                int Pos2 = area.IndexOf("/level");
                int Number = Convert.ToInt32(area.Substring(0, Pos - 1));
                Number = Number * CasterLevel;
                ComputedArea = Number + PathfinderConstants.SPACE + area.Substring(Pos + 1, Pos2 - Pos - 1);
            }
            else
            {
                ComputedArea = area;
            }
        }

        public void ComputeDuration(int CasterLevel)
        {
            this.CasterLevel = CasterLevel;
            if (duration.Contains("/level"))
            {
                int Pos = duration.IndexOf(PathfinderConstants.SPACE);
                int Pos2 = duration.IndexOf("/level");
                string temp = duration.Substring(0, Pos);
                //int Number = Convert.ToInt32(duration.Substring(0, Pos));
                //ComputedDurationValue = Number * CasterLevel;
                //ComputedDuration = ComputedDurationValue + PathfinderConstants.SPACE + duration.Substring(Pos + 1, Pos2 - Pos - 1);
            }
            else
            {
                ComputedDuration = duration;
            }
        }

        public void ComputeAll(int CasterLevel)
        {
            ComputeDuration(CasterLevel);
            ComputeArea(CasterLevel);
            ComputeTarget(CasterLevel);
            ComputeRange(CasterLevel);
        }
    }
}
