using DatabaseInterfaces;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateLibraries
{
    public class NHibernateLibrarySession : IDisposable, INHibernateLibrarySession
    {
        private readonly string LIBRARY_BASE_NAME = "";
        public ISessionFactory SessionFactory { get; private set; }
        public ISession Session { get; private set; }
        private readonly ITransaction _transaction;

        public NHibernateLibrarySession(string connectionString, string DomainAssemblyName)
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
                throw new DatabaseLibraryException(sqlEx.Message, LIBRARY_BASE_NAME, sqlEx);
            }
            catch (Exception ex)
            {
                throw new DatabaseLibraryException(ex.InnerException.Message, LIBRARY_BASE_NAME, ex);
            }
        }

        public NHibernateLibrarySession(ISessionFactory sessionFactory)
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
                throw new Exception("NHibernateLibrarySession-" + ex.InnerException, ex);
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
