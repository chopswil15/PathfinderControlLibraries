using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnGoing
{
    public class OnGoingGameEffect : IOnGoing
    {
        public int Duration { get; set; } //in rounds 0=indefinite
        public OnGoingType OnGoingType
        {
            get { return OnGoingType.GameEffect; }
        }
        public string Name { get; set; }      
        //either Damage or DamgeStr is populated
        public int Damage { get; set; }  //fixed damage 
        public string DamageStr { get; set; } // damage is re-computed, i.e 2d6
    }
}
