using CommonStatBlockInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockChecker
{
    public class ArmorClassData : IArmorClassData
    {
        public StatBlockInfo.ACMods ACMods_SB { get; set; }
        public StatBlockInfo.ACMods ACMods_Computed { get; set; }
        public int TotalArmorCheckPenalty { get; set; }  //due to armor
        public int MaxDexMod { get; set; }  //due to armor
    }
}
