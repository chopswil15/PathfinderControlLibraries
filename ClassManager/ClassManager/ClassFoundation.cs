using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClassManager
{
    public abstract class ClassFoundation
    {
        //public enum SpecialAbilityType
        //{
        //    None = 0,
        //    Ex_ExtraordinaryAbilities = 1,
        //    Sp_SpellLikeAbilities = 2,
        //    Su_SupernaturalAbilities = 3
        //}

        //public enum FeatureActivity
        //{
        //    Constant = 0,
        //    Activate = 1
        //}

        //public struct Feature
        //{
        //    public string Name;
        //    public string DisplayName;
        //    public SpecialAbilityType SpecialAbility;
        //    public FeatureActivity Activity;
        //    public int StartLevel;

        //}

        protected List<string> Alignment { get; set; }
        protected string Name { get; set; }
        protected OnGoingSpecialAbility ClassFeature { get; set; }
        protected List<OnGoingSpecialAbility> Features { get; set; }
        protected bool ExCleric { get; set; }

        public ClassFoundation()
        {
            Features = new List<OnGoingSpecialAbility>();
        }

        public List<OnGoingSpecialAbility> GetContantFeatures()
        {
            List<OnGoingSpecialAbility> temp = new List<OnGoingSpecialAbility>();
            foreach (OnGoingSpecialAbility F in Features)
            {
                if (F.Activity == OnGoingSpecialAbility.SpecialAbilityActivities.Constant)
                {
                    temp.Add(F);
                }
            }
            return temp.OrderBy(x => x.StartLevel).ToList();
        }
    }
}
