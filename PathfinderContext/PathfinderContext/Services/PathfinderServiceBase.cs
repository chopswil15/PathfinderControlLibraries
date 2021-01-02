using DatabaseInterfaces;

namespace PathfinderContext.Services
{  
    public class PathfinderServiceBase : NHibernateServiceBase, IServiceBase 
    {
        public PathfinderServiceBase(string connectionString) : base (connectionString, Globals.MapperContext)  {}      
    }
}
