
using System;
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

        public NHibernateHelper(string connectionString, string assemblyNameWithClassMapper)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new Exception("connectionString is empty");
            if (string.IsNullOrEmpty(assemblyNameWithClassMapper)) throw new Exception("assemblyNameWithClassMapper is empty");

            _connectionString = connectionString;
            _assemblyNameWithClassMapper = assemblyNameWithClassMapper;
        }

        private ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(_connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildSessionFactory();
        }

        private ISessionFactory CreateSessionFactory(string assemblyNameWithClassMapper)
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(_connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.Load(assemblyNameWithClassMapper)))
                .BuildSessionFactory();
        }
    }
}
