using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Data;
using System.Data.SqlClient;

namespace NHibernateLibraries
{
    public class NHibernateUnitOfWork : IUnitOfWork
    {
        public ISessionFactory SessionFactory { get; private set; }
        private readonly ITransaction _transaction;
        public ISession Session { get; private set; }

        public NHibernateUnitOfWork(string connectionString, string DomainAssemblyName)
        {
            try
            {
                NHibernateHelper helper = new NHibernateHelper(connectionString, DomainAssemblyName);
                SessionFactory = helper.SessionFactory;
                Session = SessionFactory.OpenSession();
                Session.FlushMode = FlushMode.Auto;
                _transaction = Session.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            catch (SqlException sqlEx)
            {
                throw new NHibernateLibraryException(sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                throw new NHibernateLibraryException(ex.InnerException.Message, ex);
            }
        }

        public NHibernateUnitOfWork(ISessionFactory sessionFactory)
        {
            try
            {
                SessionFactory = sessionFactory;
                Session = SessionFactory.OpenSession();
                Session.FlushMode = FlushMode.Auto;
                _transaction = Session.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            catch (Exception ex)
            {
                throw new Exception("NHibernateUnitOfWork-" + ex.InnerException, ex);
            }
        }
       

        public void Dispose()
        {
            if (Session.IsOpen)
            {
                Session.Close();
                Session.Dispose();
            }
        }

        public void Commit()
        {
            if (!_transaction.IsActive)
            {
                throw new InvalidOperationException("No active transation");
            }
            _transaction.Commit();
        }

        public void Rollback()
        {
            if (_transaction.IsActive)
            {
                _transaction.Rollback();
            }
        }
    }
}
