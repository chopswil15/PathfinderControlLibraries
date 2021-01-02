using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using Skills;

namespace Bloodlines
{
    public interface IBloodline
    {
        string Name { get; set; }
        string Description { get; set; }
        SkillData.SkillNames ClassSkillName { get; set; }
        Dictionary<string, int> BonusSpells { get; set; }
        List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }
    }
}
