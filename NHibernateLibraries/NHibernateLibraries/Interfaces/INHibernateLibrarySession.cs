using NHibernate;
using System;

namespace NHibernateLibraries
{
    public interface INHibernateLibrarySession : IDisposable
    {
        ISession Session { get; }
        ISessionFactory SessionFactory { get; }

        void Commit();
        void Rollback();
    }
}