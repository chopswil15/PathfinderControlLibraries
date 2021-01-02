﻿using StatBlockCommon.Monster_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateDetails
{
    public class Resolute : TemplateFoundation.TemplateFoundation
    {
        public Resolute()
        {
            Name = "Resolute";
            TemplateFoundationType = FoundationType.Acquired;
        }

        public override MonsterStatBlock ApplyTemplate(MonsterStatBlock MonSB)
        {            

            return MonSB;
        }
    }
}
