using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PathfinderGlobals;
using OnGoing;
using CommonStatBlockInfo;
using Skills;
using ClassFoundational;

namespace ClassDetails
{
    public class ArcaneTrickster : ClassFoundation 
    {
        public ArcaneTrickster()
        {
            Name = "Arcane Trickster";
            ClassAlignments = CommonMethods.GetNonLawfulAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d6;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            BABType = StatBlockInfo.BABType.Medium;
            IsPrestigeClass = true;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.APPRAISE, StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.DISABLE_DEVICE, StatBlockInfo.SkillNames.DISGUISE, 
                         StatBlockInfo.SkillNames.ESCAPE_ARTIST, StatBlockInfo.SkillNames.KNOWLEDGE_ALL, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SLEIGHT_OF_HAND, StatBlockInfo.SkillNames.SPELLCRAFT, 
                         StatBlockInfo.SkillNames.STEALTH, StatBlockInfo.SkillNames.SWIM };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeArcana, Value = 4 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.DisableDevice, Value = 4 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.EscapeArtist, Value = 4 });
            return temp;
        }

        public override void GetSpellOverLoads(ref int overloadLevel, ref int overloadClassLevel, int casterLevel, int classLevel)
        {
            overloadClassLevel += classLevel;
            overloadLevel += base.GetOverloadPrestige(classLevel, OverloadPrestigeType.Full);
        }
    }
}
