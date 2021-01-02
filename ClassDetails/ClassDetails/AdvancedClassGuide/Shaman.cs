using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassFoundational;
using CommonStatBlockInfo;
using OracleMysteries;
using PathfinderGlobals;

namespace ClassDetails
{
    public class Shaman : ClassFoundation
    {
        public Shaman()
        {
            Name = "Shaman";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            CanCastSpells = true;
            HasSpellsPerDay = true;
            MysteryUse = true;
            OracleMysteries = new List<IMystery>();
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.Poor;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
            BABType = StatBlockInfo.BABType.Medium;
            ClassArchetypes = new List<string>() { "Speaker For The Past" };
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Medium;
            //SetFeatures();
        }

        public override List<string> ClassArchetypeSkills(string Archetype)
        {
            switch (Archetype)
            {
                case "Speaker For The Past":
                    List<string> skills = ClassSkills();
                    skills.AddRange(new List<string> { StatBlockInfo.SkillNames.LINGUISTICS, StatBlockInfo.SkillNames.KNOWLEDGE_HISTORY, StatBlockInfo.SkillNames.KNOWLEDGE_LOCAL, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.USE_MAGIC_DEVICE });
                    return skills;
            }
            return ClassSkills();
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.KNOWLEDGE_NATURE, StatBlockInfo.SkillNames.KNOWLEDGE_PLANES, 
                StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.SURVIVAL };
        }

        public override string GetSpellBonusAbility()
        {
            return StatBlockInfo.WIS;
        }

        public override void ProcessMysteries(string Mysteries)
        {
            Mysteries = Mysteries.Replace(";", ",");
            Mysteries = Mysteries.Replace("WanderingSpirit", string.Empty);
            List<string> MysteryList = Mysteries.Split(',').ToList();
            foreach (string mystery in MysteryList)
            {
                int Pos = mystery.IndexOf(PathfinderConstants.SPACE);
                string temp = mystery.Trim(); ;
                if (Pos >= 0)
                {
                    temp = mystery.Substring(0, Pos).Trim();
                }
                IMystery mysteryClass = MysteryReflectionWrapper.AddMysteryClass(temp);
                if (mysteryClass == null) throw new Exception("Mystery not defined - " + temp);
                OracleMysteries.Add(mysteryClass);
            }
            OracleMysteries.Remove(null);
        }

        public override int GetMysteriesPerLevel(int ClassLevel)
        {
            int Count = 0;
            foreach (IMystery mystery in OracleMysteries)
            {
                Dictionary<string, int> Mysteries = mystery.BonusSpells(ClassLevel);
                Count += Mysteries.Count;
            }

            return Count;
        }

        public override List<int> GetSpellsPerLevel(int ClassLevel)
        {
            //Bonus Spells handled in elsewhere
            List<int> Levels = new List<int>();
            switch (ClassLevel)
            {
                case 1:
                    Levels.Add(3); //0th
                    Levels.Add(1); //1st
                    break;
                case 2:
                    Levels.Add(4); //0th
                    Levels.Add(2); //1st
                    break;
                case 3:
                    Levels.Add(4); //0th
                    Levels.Add(2); //1st
                    Levels.Add(1); //2nd
                    break;
                case 4:
                    Levels.Add(4); //0th
                    Levels.Add(3); //1st
                    Levels.Add(2); //2nd
                    break;
                case 5:
                    Levels.Add(4); //0th
                    Levels.Add(3); //1st
                    Levels.Add(2); //2nd
                    Levels.Add(1); //3rd
                    break;
                case 6:
                    Levels.Add(4); //0th
                    Levels.Add(3); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(2); //3rd
                    break;
                case 7:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(2); //3rd
                    Levels.Add(1); //4th
                    break;
                case 8:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(2); //4th
                    break;
                case 9:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(2); //4th
                    Levels.Add(1); //5th
                    break;
                case 10:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(3); //4th
                    Levels.Add(2); //5th
                    break;
                case 11:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(3); //4th
                    Levels.Add(2); //5th
                    Levels.Add(1); //6th
                    break;
                case 12:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(3); //4th
                    Levels.Add(3); //5th
                    Levels.Add(2); //6th
                    break;
                case 13:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(3); //5th
                    Levels.Add(2); //6th
                    Levels.Add(1); //7th
                    break;
                case 14:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(3); //5th
                    Levels.Add(3); //6th
                    Levels.Add(2); //7th
                    break;
                case 15:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(3); //6th
                    Levels.Add(2); //7th
                    Levels.Add(1); //8th
                    break;
                case 16:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(3); //6th
                    Levels.Add(3); //7th
                    Levels.Add(2); //8th
                    break;
                case 17:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(4); //6th
                    Levels.Add(3); //7th
                    Levels.Add(2); //8th
                    Levels.Add(1); //9th
                    break;
                case 18:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(4); //6th
                    Levels.Add(3); //7th
                    Levels.Add(3); //8th
                    Levels.Add(2); //9th
                    break;
                case 19:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(4); //6th
                    Levels.Add(4); //7th
                    Levels.Add(3); //8th
                    Levels.Add(3); //9th
                    break;
                case 20:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(4); //6th
                    Levels.Add(4); //7th
                    Levels.Add(4); //8th
                    Levels.Add(4); //9th
                    break;
            }
            return Levels;
        }
    }
}
