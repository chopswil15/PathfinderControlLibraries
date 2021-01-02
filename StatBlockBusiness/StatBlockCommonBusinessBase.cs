using PathfinderGlobals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockBusiness
{
    public abstract class StatBlockCommonBusinessBase
    {        
        public string ConnectionString { get; private set; }

        public StatBlockCommonBusinessBase()
        {
            ConnectionString = PathfinderConstants.ConnectionString;
        }
    }
}
