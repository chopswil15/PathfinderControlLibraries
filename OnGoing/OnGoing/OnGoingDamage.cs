using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnGoing
{
    public class OnGoingDamage : IOnGoing
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public OnGoingType OnGoingType
        {
            get { return OnGoingType.Damage; }
        }
        public string Damage { get; set; }
        // if string damage is re-computed each round, i.e 2d6
        // if int fixed damage

        public OnGoingDamage(string Name, int Duration, string Damage)
        {
            this.Name = Name;
            this.Duration = Duration;
            this.Damage = Damage;
        }
    }
}
