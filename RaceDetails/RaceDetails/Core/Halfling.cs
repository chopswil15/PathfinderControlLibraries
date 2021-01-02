using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RaceFoundational;

using CommonStatBlockInfo;
using OnGoing;

namespace RaceDetails
{
    public class Halfling : RaceFoundation
    {
        public Halfling()
        {
            Name = "Halfling";
            BaseSpeed = 20;
            Size = StatBlockInfo.SizeCategories.Small;
            RaceLanguages = new List<string> {"Common", "Halfling" };
            RaceWeapons = "sling";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { 
                                                  {StatBlockInfo.SkillNames.PERCEPTION,2},
                                                  {StatBlockInfo.SkillNames.ACROBATICS,2},
                                                  {StatBlockInfo.SkillNames.CLIMB,2} 
                                               };
        }

        public override List<OnGoingStatBlockModifier> GetRaceOnGoingModifiers()
        {
            List<OnGoingStatBlockModifier> mods = new List<OnGoingStatBlockModifier>();
            OnGoingStatBlockModifier mod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow, OnGoingStatBlockModifier.StatBlockModifierSubTypes.SavingThrow_Fort, 
                    "Halfling Fort",1,string.Empty);
            mods.Add(mod);

            mod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow, OnGoingStatBlockModifier.StatBlockModifierSubTypes.SavingThrow_Ref,
                    "Halfling Ref", 1, string.Empty);
            mods.Add(mod);

            mod = new OnGoingStatBlockModifier(0, OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow, OnGoingStatBlockModifier.StatBlockModifierSubTypes.SavingThrow_Will,
                    "Halfling Will", 1, string.Empty);
            mods.Add(mod);
            return mods;
        }
    }
}
