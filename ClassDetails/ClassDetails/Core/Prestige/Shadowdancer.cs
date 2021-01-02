using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OnGoing;
using CommonStatBlockInfo;
using Skills;
using PathfinderGlobals;
using ClassFoundational;

namespace ClassDetails
{
    public class Shadowdancer : ClassFoundation
    {
        public Shadowdancer()
        {
            Name = "Shadowdancer";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 6;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            BABType = StatBlockInfo.BABType.Medium;
            IsPrestigeClass = true;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Extra;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.None;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
        }

        public override List<string> GetWeaponProficienciesExtra()
        {
            return new List<string> { "club", "hand crossbow", "light crossbow", "heavy crossbow", "dagger", "swordbreaker dagger", "punching dagger", "dart", 
                 "mace", "morningstar", "quarterstaff", "rapier", "sap", "shortbow" ,"composite shortbow", "short sword" };
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.DISGUISE, StatBlockInfo.SkillNames.ESCAPE_ARTIST, StatBlockInfo.SkillNames.PERCEPTION, 
                StatBlockInfo.SkillNames.PERFORM, StatBlockInfo.SkillNames.SLEIGHT_OF_HAND, StatBlockInfo.SkillNames.STEALTH };
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Dodge", "Combat Reflexes", "Mobility" };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Stealth, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Perform, SubType = "dance", Value = 2 });
            return temp;
        }
    }
}
