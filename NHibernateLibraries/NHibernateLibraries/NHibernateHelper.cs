
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace NHibernateLibraries
{
    internal class NHibernateHelper
    {
        private readonly string _connectionString;
        private readonly string _assemblyNameWithClassMapper;
        private static ISessionFactory _sessionFactory;
        private static readonly object factorylock = new object();

        public ISessionFactory SessionFactory
        {
            get 
            {
                lock (factorylock)
                {
                    return _sessionFactory ?? (_sessionFactory = CreateSessionFactory(_assemblyNameWithClassMapper));
                }
            }
        }

        public NHibernateHelper(string connectionString,string AssemblyNameWithClassMapper)
        {
            _connectionString = connectionString;
            _assemblyNameWithClassMapper = AssemblyNameWithClassMapper;
        }

        private ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(_connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildSessionFactory();
        }

        private ISessionFactory CreateSessionFactory(string AssemblyNameWithClassMapper)
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(_connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.Load(AssemblyNameWithClassMapper)))
                .BuildSessionFactory();
        }
    }
}
