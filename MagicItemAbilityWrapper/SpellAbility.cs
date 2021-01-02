using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonInterFacesDD;

namespace MagicItemAbilityWrapper
{
    public class SpellAbility : ICasterLevel
    {
        public string SpellName { get; set;}
        public int CasterLevel { get; set; }

        public SpellAbility(string SpellName, int CasterLevel)
        {
            this.SpellName = SpellName;
            this.CasterLevel = CasterLevel;
        }
    }
}
