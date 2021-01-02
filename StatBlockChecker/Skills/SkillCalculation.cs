using PathfinderGlobals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace StatBlockChecker
{
    public class SkillCalculation : ISkillCalculation
    {
        public string Name { get; private set; }
        public int Mod { get; private set; }
        public int AbilityMod { get; private set; }
        public string Ability { get; private set; }
        public int ClassSkill { get; private set; }
        public int ExtraMod { get; private set; }
        public int ArmorCheckPenalty { get; private set; }
        public int Rank { get; private set; }
        public string Formula { get; private set; }
        public bool ExtraSkillUsed { get; private set; }

        public SkillCalculation()
        {

        }

        public SkillCalculation(string Name, int Mod, int AbilityMod, string Ability, int ClassSkill, int ExtraMod,
                                int ArmorCheckPenalty, int Rank, string Formula, bool ExtraSkillUsed)
        {
            this.Name = Name;
            this.Mod = Mod;
            this.AbilityMod = AbilityMod;
            this.Ability = Ability;
            this.ClassSkill = ClassSkill;
            this.ExtraMod = ExtraMod;
            this.ArmorCheckPenalty = ArmorCheckPenalty;
            this.Rank = Rank;
            this.Formula = Formula;
            this.ExtraSkillUsed = ExtraSkillUsed;
        }

        public override string ToString()
        {
            string temp = Name + " +" + Mod.ToString() + " = +" + Rank.ToString() + " ranks, +" + AbilityMod.ToString() + PathfinderConstants.SPACE + Ability;
            if (ClassSkill > 0)
            {
                temp += ", +" + ClassSkill.ToString() + " class skill";
                if (ExtraSkillUsed) temp += "*";
            }
            if (ExtraMod != 0)
            {
                //  temp += ", +" + ExtraMod.ToString() + " extra mods";               
            }
            if (ArmorCheckPenalty != 0)
            {
                temp += ", +" + ArmorCheckPenalty.ToString() + " Armor Check Penalty";
            }

            if (Formula.Length > 0)
            {
                temp += PathfinderConstants.SPACE + Formula;
            }

            temp = temp.Replace("+-", "-");

            return temp;
        }
    }
}
