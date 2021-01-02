using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using OnGoing;
using CommonStatBlockInfo;
using Skills;
using ClassFoundational;

namespace ClassDetails
{
    public class RedMantisAssassin : ClassFoundation 
    {
        public RedMantisAssassin()
        {
            Name = "Red Mantis Assassin";
            ClassAlignments = new List<string>() { "LE" };
            SkillRanksPerLevel = 6;
            CanCastSpells = true;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            BABType = StatBlockInfo.BABType.Medium;
            IsPrestigeClass = true;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.APPRAISE, StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.DISGUISE, StatBlockInfo.SkillNames.ESCAPE_ARTIST, StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.INTIMIDATE, 
                   StatBlockInfo.SkillNames.KNOWLEDGE_LOCAL, StatBlockInfo.SkillNames.KNOWLEDGE_NATURE, StatBlockInfo.SkillNames.KNOWLEDGE_NOBILITY, StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.STEALTH };
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Alertness", "Exotic Weapon Proficiency (sawtooth sabre)", "Two-Weapon Fighting", "Weapon Focus (sawtooth sabre)" };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Intimidate, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Perception, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Stealth, Value = 5 });
            return temp;
        }

        public override string GetSpellBonusAbility()
        {
            return StatBlockInfo.CHA;
        }

        public override List<int> GetSpellsPerDay(int ClassLevel)
        {
            //Bonus Spells handled in elsewhere
            List<int> Levels = new List<int>();
            Levels.Add(0); //no 0 level spells

            switch (ClassLevel)
            {
                case 1:
                    Levels.Add(1); //1st
                    break;
                case 2:
                    Levels.Add(2); //1st
                    break;
                case 3:
                    Levels.Add(3); //1st
                    break;
                case 4:
                    Levels.Add(3); //1st
                    Levels.Add(1); //2nd
                    break;
                case 5:
                    Levels.Add(4); //1st
                    Levels.Add(2); //2nd
                    break;
                case 6:
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    break;
                case 7:
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(1); //3rd
                    break;
                case 8:
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(2); //3rd
                    break;
                case 9:
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd
                    break;
                case 10:
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(1); //4th
                    break;              
            }

            return Levels;
        }

        public override List<int> GetSpellsPerLevel(int ClassLevel)
        {
            List<int> temp = new List<int>();
            temp.Add(0); //no 0 level spells

            switch (ClassLevel)
            {
                case 1:
                    temp.Add(2); //1st
                    break;
                case 2:
                    temp.Add(3); //1st
                    break;
                case 3:
                    temp.Add(4); //1st
                    break;
                case 4:
                    temp.Add(4); //1st
                    temp.Add(2); //2nd
                    break;
                case 5:
                    temp.Add(4); //1st
                    temp.Add(3); //2nd
                    break;
                case 6:
                    temp.Add(4); //1st
                    temp.Add(4); //2nd
                    break;
                case 7:
                    temp.Add(5); //1st
                    temp.Add(4); //2nd
                    temp.Add(2); //3rd
                    break;
                case 8:
                    temp.Add(5); //1st
                    temp.Add(4); //2nd
                    temp.Add(3); //3rd
                    break;
                case 9:
                    temp.Add(5); //1st
                    temp.Add(4); //2nd
                    temp.Add(4); //3rd
                    break;
                case 10:
                    temp.Add(5); //1st
                    temp.Add(5); //2nd
                    temp.Add(4); //3rd
                    temp.Add(2); //4th
                    break;        
            }

            return temp;
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {            
            int Count = 0;

            //Sabre Fighting
            if (ClassLevel >= 1) Count++;
            if (ClassLevel >= 5) Count++;
            if (ClassLevel >= 7) Count++;
            return Count;
        }
    }
}
