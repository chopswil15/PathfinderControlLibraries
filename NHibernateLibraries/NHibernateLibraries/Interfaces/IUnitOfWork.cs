using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateLibraries
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
