using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnGoing
{
    public class OnGoingCondition : IOnGoing
    {
        public ConditionTypes ConditionType { get; set; }
        public int Duration { get; set; }
        public OnGoingType OnGoingType
        {
            get { return OnGoingType.Condition; }
        }

        public OnGoingCondition(ConditionTypes ConditionType, int Duration)
        {
            this.ConditionType = ConditionType;
            this.Duration = Duration;
        }
    }

    [Flags]
    public enum ConditionTypes : int //bitwise
    {
        None = 0,
        AbilityDamaged = 1 << 0,
        AbilityDrained = 1 << 1,
        Blinded = 1 << 2,
        BlownAway = 1 << 3,
        Confused = 1 << 4,
        Cowering = 1 << 5,
        Dazed = 1 << 6,
        Dazzled = 1 << 7,
        Deafened = 1 << 8,
        EnergyDrained = 1 << 9,
        Entangled = 1 << 10,
        Exhausted = 1 << 11,
        Fascinated = 1 << 12,
        Fatigued = 1 << 13,
        FlatFooted = 1 << 14,
        Frightened = 1 << 15,
        Grappled = 1 << 16,
        Helpless = 1 << 17,
        Incorporeal = 1 << 18,
        Invisible = 1 << 19,
        Nauseated = 1 << 20,
        Panicked = 1 << 21,
        Paralyzed = 1 << 22,
        Petrified = 1 << 23,
        Pinned = 1 << 24,
        Prone = 1 << 25,
        Shaken = 1 << 26,
        Sickened = 1 << 27,
        Stable = 1 << 28,
        Staggered = 1 << 29,
        Stunned = 1 << 30,
        Unconscious = 1 << 31
    }   
}
