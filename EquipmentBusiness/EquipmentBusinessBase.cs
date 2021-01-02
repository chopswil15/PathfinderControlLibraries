using EquipmentBasic;
using PathfinderGlobals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentBusiness
{
    public abstract class EquipmentBusinessBase
    {
        public string ConnectionString { get; private set; }

        public EquipmentBusinessBase()
        {
            ConnectionString = PathfinderConstants.ConnectionString;
        }
    }
}
