using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using CommonStatBlockInfo;
using TemplateFoundational;


namespace TemplateDetails
{
    public class PlaguedBeast : TemplateFoundation
    {
        public PlaguedBeast()
        {
            Name = "PlaguedBeast";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            TemplateCommon.ChangeHD(MonSB, StatBlockInfo.HitDiceCategories.d8);

            MonSB.Feats = StatBlockInfo.AddFeat(MonSB.Feats, "ToughnessB");

            TemplateCommon.AddDR(MonSB, "slashing", 5);

            return MonSB;
        }
    }
}
