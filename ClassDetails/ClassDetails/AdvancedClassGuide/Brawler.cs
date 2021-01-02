using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassFoundational;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace ClassDetails
{
    public class Brawler : ClassFoundation 
    {
        public Brawler()
        {
            Name = "Brawler";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.Good;
            RefSaveType = StatBlockInfo.SaveBonusType.Good;
            WillSaveType = StatBlockInfo.SaveBonusType.Poor;
            BABType = StatBlockInfo.BABType.Fast;
            ClassArchetypes = new List<string>() {"Exemplar","Mutagenic Mauler","Shield Champion","Snakebite Striker","Steel-Breaker",
                                                   "Strangler","Wild Child"};
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Extra;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Shield;
            //SetFeatures();
        }

        public override List<string> GetWeaponProficienciesExtra()
        {
            return new List<string> { "handaxe", "short sword", "bayonet", "brass knuckles", "cestus", "dan bong", "emei piercer", "fighting fan", 
                "gauntlet", "heavy shield", "iron brush", "light shield", "madu", "mere club", "punching dagger", "sap", "scizore", "spiked armor", 
               "spiked gauntlet", "spiked shield", "tekko-kagi", "tonfa", "unarmed strike", "wooden stake", "wushu dart" };
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.ESCAPE_ARTIST, StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.INTIMIDATE,
                    StatBlockInfo.SkillNames.KNOWLEDGE_DUNGEONEERING, StatBlockInfo.SkillNames.KNOWLEDGE_LOCAL, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SWIM };
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            int Count = 1; //Improved Unarmed Strike

            if (ClassLevel >= 2) Count++;
            if (ClassLevel >= 5) Count++;
            if (ClassLevel >= 8) Count++;
            if (ClassLevel >= 11) Count++;
            if (ClassLevel >= 14) Count++;
            if (ClassLevel >= 17) Count++;
            if (ClassLevel >= 20) Count++;

            return Count;
        }
    }
}
