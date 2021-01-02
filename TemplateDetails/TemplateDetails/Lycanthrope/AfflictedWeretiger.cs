﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using TemplateFoundational;

namespace TemplateDetails
{
    public class AfflictedWeretiger : TemplateFoundation
    {
        public AfflictedWeretiger()
        {
            Name = "Afflicted Weretiger"; 
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {
            if (!MonSB.AlternateNameForm.Contains("Human Form"))
            {
                TemplateCommon.AddDR(MonSB, "silver", 5);
            }

            return MonSB;
        }
    }
}
