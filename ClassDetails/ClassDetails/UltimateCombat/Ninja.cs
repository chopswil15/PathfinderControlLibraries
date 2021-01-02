using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using OnGoing;
using CommonStatBlockInfo;
using PathfinderGlobals;
using ClassFoundational;

namespace ClassDetails
{
    public class Ninja : ClassFoundation 
    {
        public Ninja()
        {
            Name = "Ninja";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 8;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.Poor;
            RefSaveType = StatBlockInfo.SaveBonusType.Good;
            WillSaveType = StatBlockInfo.SaveBonusType.Poor;
            BABType = StatBlockInfo.BABType.Medium;
            ClassArchetypes = new List<string>() { "Hunting Serpent" };
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Extra;
        }

        public override string AlternateName()
        {
            return "Rogue";
        }

        public override List<string> GetWeaponProficienciesExtra()
        {
            return new List<string> { "kama", "katana", "kusarigama", "nunchaku", "sai", "shortbow", "short sword", "shuriken", "siangham", "wakizashi" };
        }

        public override List<string> ClassArchetypeSkills(string Archetype)
        {
            List<string> tempSkills = ClassSkills();
            switch (Archetype)
            {
                case "Hunting Serpent":
                    tempSkills.Remove(StatBlockInfo.SkillNames.KNOWLEDGE_NOBILITY);
                    tempSkills.Add(StatBlockInfo.SkillNames.SURVIVAL);
                    break;
            }

            return tempSkills;
        }      


        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.APPRAISE, StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.DISABLE_DEVICE, StatBlockInfo.SkillNames.DISGUISE, 
                                     StatBlockInfo.SkillNames.ESCAPE_ARTIST, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_LOCAL, StatBlockInfo.SkillNames.KNOWLEDGE_NOBILITY, StatBlockInfo.SkillNames.LINGUISTICS,
                                     StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.PERFORM, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SLEIGHT_OF_HAND, StatBlockInfo.SkillNames.STEALTH, StatBlockInfo.SkillNames.SWIM, StatBlockInfo.SkillNames.USE_MAGIC_DEVICE };
        }
       
    }
}
