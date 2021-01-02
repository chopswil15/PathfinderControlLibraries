using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public interface IDomain
    {
        string Name { get; set; }
        List<string> Dieties { get; set; }
        string Description { get; set; }
        List<string> BonusFeats { get; set; }
        Dictionary<string, int> DomainSpells { get; set; }
        List<OnGoingSpecialAbility> GrantedAbilities { get; set; }
    }
}
