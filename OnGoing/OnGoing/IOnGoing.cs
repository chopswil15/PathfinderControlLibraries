using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnGoing
{
    public interface IOnGoing
    {
        int Duration { get; set; } //in rounds 0=indefinite
        OnGoingType OnGoingType { get;}
    }

    public enum OnGoingType
    {        
        Damage = 1,
        Condition = 2,
        StatBlock = 3,
        SpellEffect = 4,
        Health = 5,
        Power = 6,
        SpecialAbility = 7,
        GameEffect
    }
}
