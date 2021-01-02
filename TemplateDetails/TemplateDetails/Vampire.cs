using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TemplateFoundation;
using StatBlockCommon;
using CommonStatBlockInfo;
using CreatureTypeDetails;
using CreatureTypeFoundation;
using CreatureTypeManager;
using StatBlockCommon.Monster_SB;


namespace TemplateDetails
{
    public class Vampire : TemplateFoundation.TemplateFoundation
    {
        public Vampire()
        {
            Name = "Vampire";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            if (!MonSB.DontUseRacialHD)
            {
                TemplateCommon.ChangeHD(MonSB, StatBlockInfo.HitDiceCategories.d8);

                StatBlockInfo.HDBlockInfo tempHDInfo = new StatBlockInfo.HDBlockInfo();
                tempHDInfo.ParseHDBlock(MonSB.HD);
                //tempHDInfo.HDType = StatBlockInfo.HitDiceCategories.d8; //keeps HD, change to d8
                //tempHDInfo.Modifier = 0;
                //MonSB.HD = tempHDInfo.ToString();
                CreatureTypeFoundation.CreatureTypeFoundation CreatureType = CreatureTypeManager.CreatureTypeDetailsWrapper.GetRaceDetailClass("undead");
                int fort = StatBlockInfo.ParseSaveBonues(tempHDInfo.Multiplier, CreatureType.FortSaveType);
                CreatureTypeManager.CreatureTypeMaster CreatureTypeMaster = new CreatureTypeMaster();
                CreatureTypeMaster.CreatureTypeInstance = CreatureType;
                fort += StatBlockInfo.GetAbilityModifier(MonSB.GetAbilityScoreValue(CreatureTypeMaster.CreatureTypeInstance.FortMod()));
                MonSB.Fort = fort.ToString();
                int refValue = StatBlockInfo.ParseSaveBonues(tempHDInfo.Multiplier, CreatureType.RefSaveType);
                refValue += StatBlockInfo.GetAbilityModifier(MonSB.GetAbilityScoreValue(StatBlockInfo.DEX));
                MonSB.Ref = refValue.ToString();
                int will = StatBlockInfo.ParseSaveBonues(tempHDInfo.Multiplier, CreatureType.WillSaveType);
                will += StatBlockInfo.GetAbilityModifier(MonSB.GetAbilityScoreValue(StatBlockInfo.WIS));
                MonSB.Will = will.ToString();
            }


            MonSB.AC_Mods = StatBlockInfo.ChangeAC_Mod(MonSB.AC_Mods, "natural", 6, true);
            
            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "AlertnessB");
            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "Combat ReflexesB");
            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "DodgeB");
            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "Improved InitiativeB");
            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "Lightning ReflexesB");
            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "ToughnessB");

            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Bluff");   
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Perception");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Sense Motive");
            MonSB.RacialMods = StatBlockInfo.AddRacialMod(MonSB.RacialMods, "+8 Stealth");

            TemplateCommon.AddDR(MonSB, "magic and silver", 10);

            TemplateCommon.AddResistance(MonSB, "cold ", 10);
            TemplateCommon.AddResistance(MonSB, "electricity ", 10);

            return MonSB;
        }
    }
}
