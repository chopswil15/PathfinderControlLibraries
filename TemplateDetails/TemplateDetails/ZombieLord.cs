using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using CommonStatBlockInfo;

namespace TemplateDetails
{
    public class ZombieLord : TemplateFoundation.TemplateFoundation
    {
        public ZombieLord()
        {
            Name = "Zombie Lord";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            TemplateCommon.ChangeHD(MonSB, StatBlockInfo.HitDiceCategories.d8);

            StatBlockInfo.HDBlockInfo tempHDInfo = new StatBlockInfo.HDBlockInfo();
            tempHDInfo.ParseHDBlock(MonSB.HD);
            double HD = Convert.ToInt32(tempHDInfo.Multiplier);
            double temp = HD / 3;
            MonSB.BaseAtk = "+" + Math.Floor(temp).ToString();

            temp = HD / 3; //Fort
            MonSB.Fort = "+" + Math.Floor(temp).ToString();

            temp = HD / 3; ///Ref
            MonSB.Ref = "+" + Math.Floor(temp).ToString();

            temp = HD / 2; ///Will
            MonSB.Will = "+" + (Math.Floor(temp) + 2).ToString();

            TemplateCommon.AddResistance(MonSB, "slashing ", 5);

            return MonSB;
        }
    }
}
