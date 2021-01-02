using CommonStatBlockInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockChecker
{
    public class SizeData : ISizeData
    {
        public int SizeMod { get; set; }
        public StatBlockInfo.SizeCategories SizeCat { get; set; }
    }
}
