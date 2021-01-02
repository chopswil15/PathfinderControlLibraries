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
    public class DragonDisciple : ClassFoundation
    {
        public DragonDisciple()
        {
            Name = "Dragon Disciple";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 2;
            HitDiceType = StatBlockInfo.HitDiceCategories.d12;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            BABType = StatBlockInfo.BABType.Medium;
            IsPrestigeClass = true;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.None;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.None;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.None;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.ESCAPE_ARTIST, StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.KNOWLEDGE_ALL, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.SPELLCRAFT };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeArcana, Value = 5 });
            return temp;
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            int temp = 0;
            if (ClassLevel >= 2) temp++;
            if (ClassLevel >= 5) temp++;
            if (ClassLevel >= 8) temp++;

            return temp;
        }

        public override void GetSpellOverLoads(ref int overloadLevel, ref int overloadClassLevel, int casterLevel, int classLevel)
        {
            overloadClassLevel += classLevel;
            overloadLevel += base.GetOverloadPrestige(classLevel, OverloadPrestigeType.BlocksOfThree);
        }
    }
}
