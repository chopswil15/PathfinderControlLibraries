using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RaceFoundational;

namespace RaceManager
{
    public class RaceMaster
    {
     //   private RaceFoundation Race;

        public RaceFoundation ParceRace(string RaceName)
        {
            return RaceDetailsWrapper.GetRaceDetailClass(RaceName);         
        }
    }
}
