using CommonStatBlockInfo;
using StatBlockCommon.Monster_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateDetails
{
    public class Ravenous : TemplateFoundation.TemplateFoundation
    {
        public Ravenous()
        {
            // Tome of Horrors Complete not green ronin version
            Name = "Ravenous";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            TemplateCommon.AddResistance(MonSB, "cold ", 5);
            TemplateCommon.AddResistance(MonSB, "electricity ", 5);
            TemplateCommon.ChangeHD(MonSB, StatBlockInfo.HitDiceCategories.d10);

            return MonSB;
        }
    }
}
