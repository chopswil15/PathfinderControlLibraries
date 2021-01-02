using DatabaseInterfaces;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Metadata;
using NHibernate.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NHibernateLibraries
{
    public class NHibernateRepository<T> : IDisposable, IRepository<T> where T : class
    {
        private readonly ISession _session;
        private readonly ISessionFactory _sessionFactory;
        private readonly Dictionary<string, int> _stringFieldLengths;
        private INHibernateLibrarySession _nHibernateLibrarySession;

        public NHibernateRepository(string connectionString, string domainAssemblyName)
        {
            _nHibernateLibrarySession = new NHibernateLibrarySession(connectionString, domainAssemblyName);
            _session = _nHibernateLibrarySession.Session;
            _sessionFactory = _nHibernateLibrarySession.SessionFactory;
            _stringFieldLengths = GetStringFieldLengths();
        }

        public void Dispose()
        {
            _nHibernateLibrarySession.Dispose();
        }

        public Dictionary<string, int> GetStringFieldLengths()
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            IClassMetadata metadata = _sessionFactory.GetClassMetadata(typeof(T));

            foreach (string propertyName in metadata.PropertyNames)
            {
                IType propertyType = metadata.GetPropertyType(propertyName);
                StringType st = propertyType as StringType;
                if (st != null && st.SqlType.Length > 0)
                {
                   temp.Add(propertyName, st.SqlType.Length);
                }
            }
            return temp;
        }


        public bool IsValid(object entity)
        {
            return ValidateStringFieldLengths(entity).Count() == 0;
        }

        public IEnumerable<string> ValidateStringFieldLengths(object entity)
        {
            foreach (KeyValuePair<string, int> kvp in _stringFieldLengths)
            {
                object hold = entity.GetType().GetProperty(kvp.Key).GetValue(entity, null);
                if (hold != null)
                {
                    string temp = hold.ToString();
                    if (temp.Length > kvp.Value)
                    {
                        yield return kvp.Key + " new length " + temp.Length.ToString();
                    }
                }
            }
        }


        #region IRepository<T> Members

        public bool Add(T entity, out IEnumerable<string> brokenRules)
        {
            using (_nHibernateLibrarySession)
            {
                if (!IsValid(entity))
                {
                    brokenRules = ValidateStringFieldLengths(entity);
                    _nHibernateLibrarySession.Rollback();
                    return false;
                }
                brokenRules = new List<string>();
                _session.Save(entity);
                _nHibernateLibrarySession.Commit();
                return true;
            }
        }     

        public bool Update(T entity, out IEnumerable<string> brokenRules)
        {
            using (_nHibernateLibrarySession)
            {
                if (!IsValid(entity))
                {
                    brokenRules = ValidateStringFieldLengths(entity);
                    _nHibernateLibrarySession.Rollback();
                    return false;
                }
                brokenRules = new List<string>();
                _session.Update(entity);
                _nHibernateLibrarySession.Commit();
                return true;
            }
        }

        public bool Delete(T entity, out IEnumerable<string> brokenRules)
        {
            using (_nHibernateLibrarySession)
            {
                if (!IsValid(entity))
                {
                    brokenRules = ValidateStringFieldLengths(entity);
                    _nHibernateLibrarySession.Rollback();
                    return false;
                }
                brokenRules = new List<string>();
                _session.Delete(entity);
                _nHibernateLibrarySession.Commit();
                return true;
            }
        }        

        #endregion

        #region IReadOnlyRepository<T> Members

        public IQueryable<T> All()
        {
            return _session.Query<T>();
        }

        public T FindBy(Expression<Func<T, bool>> expression)
        {
            return FilterBy(expression).Single();
        }

        public IQueryable<T> FilterBy(Expression<Func<T, bool>> expression)
        {
            return _session.Query<T>().Where(expression).AsQueryable();
        }

        public T FindBy(int id)
        {
            return _session.Get<T>(id);
        }

        #endregion
    }
}
