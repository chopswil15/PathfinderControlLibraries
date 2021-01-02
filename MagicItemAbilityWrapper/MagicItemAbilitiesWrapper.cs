using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using CommonInterFacesDD;

namespace MagicItemAbilityWrapper
{
    public class MagicItemAbilitiesWrapper
    {
        public List<SpellAbility> SpellAbilities { get; set; }
        public List<IOnGoing> OnGoingStatBlockModifiers { get; set; }
        public MagicItemAbilityActivation Activation { get; set; }
        public IEquipment EquimentBase {get; set;}
        public string EquimentBaseString { get; set; }

        public MagicItemAbilitiesWrapper()
        {
            SpellAbilities = new List<SpellAbility>();
            OnGoingStatBlockModifiers = new List<IOnGoing>();
            EquimentBaseString = string.Empty;
        }

        public void AddSpellAbility(SpellAbility Ability)
        {
            SpellAbilities.Add(Ability);
        }

        public void AddOnGoingStatBlockModifier(IOnGoing SBMod)
        {
            OnGoingStatBlockModifiers.Add(SBMod);
        }

        public enum MagicItemAbilityActivation
        {
            Constant = 0,
            Activate = 1
        }
    }
}
