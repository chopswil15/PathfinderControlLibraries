using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using CreatureTypeFoundational;

namespace CreatureTypeManager
{
    public class CreatureTypeMaster
    {
        public CreatureTypeFoundation CreatureTypeInstance { get; set; }

        public StatBlockInfo.SaveBonusType GetSaveType(string Save)
        {
            switch (Save.ToLower())
            {
                case "fort":
                    return CreatureTypeInstance.FortSaveType;
                case "ref":
                    return CreatureTypeInstance.RefSaveType;
                case "will":
                    return CreatureTypeInstance.WillSaveType;
            }
            return StatBlockInfo.SaveBonusType.None;
        }
        
        public int GetFortSaveValue(int HD)
        {
            if (CreatureTypeInstance != null)
            {
                return StatBlockInfo.ParseSaveBonues(HD, CreatureTypeInstance.FortSaveType);
            }
            return -1;
        }

        public int GetRefSaveValue(int HD)
        {
            if (CreatureTypeInstance != null)
            {
                return StatBlockInfo.ParseSaveBonues(HD, CreatureTypeInstance.RefSaveType);
            }
            return -1;
        }

        public int GetWillSaveValue(int HD)
        {
            if (CreatureTypeInstance != null)
            {
                return StatBlockInfo.ParseSaveBonues(HD, CreatureTypeInstance.WillSaveType);
            }
            return -1;
        }
    }
}
