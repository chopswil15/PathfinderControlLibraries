using StatBlockCommon.Individual_SB;
using StatBlockCommon.Monster_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockParsing
{
    public class SBCommonBaseInput : ISBCommonBaseInput
    {
        public MonsterStatBlock MonsterSB { get; set; }
        public IndividualStatBlock IndvidSB { get; set; }
    }
}
