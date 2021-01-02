using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnGoing
{
    public class OnGoingSpecialAbility : IOnGoing
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public string DisplayName { get; set; }
        public SpecialAbilityTypes SpecialAbilityType { get; set; }
        public SpecialAbilityActivities Activity { get; set; }
        public int StartLevel { get; set; }
        public OnGoingType OnGoingType
        {
            get { return OnGoingType.SpecialAbility; }
        }

        public OnGoingSpecialAbility(string Name, int Duration, string DisplayName,
                                     SpecialAbilityTypes SpecialAbilityType,
                                     SpecialAbilityActivities Activity,
                                     int StartLevel)
        {
            this.Name = Name;
            this.Duration = Duration;
            this.DisplayName = DisplayName;
            this.SpecialAbilityType = SpecialAbilityType;
            this.Activity = Activity;
            this.StartLevel = StartLevel;
        }

        public enum SpecialAbilityTypes
        {
            None = 0,
            Ex_ExtraordinaryAbilities = 1,
            Sp_SpellLikeAbilities = 2,
            Su_SupernaturalAbilities = 3
        }

        public enum SpecialAbilityActivities
        {
            Constant = 0,
            Activate = 1
        }
    }
}
