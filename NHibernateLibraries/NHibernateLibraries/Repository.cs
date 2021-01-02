using System.Collections.Generic;
using NHibernate;
using System.Linq;
using NHibernate.Metadata;
using NHibernate.Type;
using NHibernate.Linq;
using System.Linq.Expressions;
using System;

namespace NHibernateLibraries
{
    public class Repository<T> : IDisposable,  IRepository<T> where T : class
    {
        private readonly ISession _session;
        private readonly ISessionFactory _sessionFactory;
        private readonly Dictionary<string, int> _stringFieldLengths;
        private NHibernateUnitOfWork _uow;

        public Dictionary<string, int> StringFieldLengths
        {
            get { return _stringFieldLengths; }
        }

        
        public Repository(IUnitOfWork unitOfWork)
        {
            _uow = (NHibernateUnitOfWork)unitOfWork;
            _session = _uow.Session;
            _sessionFactory = _uow.SessionFactory;
            _stringFieldLengths = GetStringFieldLengths();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

        public Dictionary<string, int> GetStringFieldLengths()
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            IClassMetadata metadata = _sessionFactory.GetClassMetadata(typeof(T));

            foreach (string propertyName in metadata.PropertyNames)
            {
                IType propertyType = metadata.GetPropertyType(propertyName);
                StringType st = propertyType as StringType;
                if (st != null)
                {
                    if (st.SqlType.Length > 0)
                    {
                        temp.Add(propertyName, st.SqlType.Length);
                    }
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
            using (_uow)
            {
                if (!IsValid(entity))
                {
                    brokenRules = ValidateStringFieldLengths(entity);
                    _uow.Rollback();
                    return false;
                }
                brokenRules = new List<string>();
                _session.Save(entity);
                _uow.Commit();
                return true;
            }
        }

        public bool Add(System.Collections.Generic.IEnumerable<T> entities, out IEnumerable<string> brokenRules)
        {
            try
            {
                foreach (T item in entities)
                {
                    using (_uow)
                    {
                        if (!IsValid(item))
                        {
                            brokenRules = ValidateStringFieldLengths(item);
                            _uow.Rollback();
                            return false;
                        }                        
                        _session.Save(item);
                        _uow.Commit();
                    }
                }
                brokenRules = new List<string>();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        public bool Update(T entity, out IEnumerable<string> brokenRules)
        {
            using (_uow)
            {
                if (!IsValid(entity))
                {
                    brokenRules = ValidateStringFieldLengths(entity);
                    _uow.Rollback();
                    return false;
                }
                brokenRules = new List<string>();
                _session.Update(entity);
                _uow.Commit();
                return true;
            }
        }

        public bool Delete(T entity, out IEnumerable<string> brokenRules)
        {
            using (_uow)
            {
                if (!IsValid(entity))
                {
                    brokenRules = ValidateStringFieldLengths(entity);
                    _uow.Rollback();
                    return false;
                }
                brokenRules = new List<string>();
                _session.Delete(entity);
                _uow.Commit();
                return true;
            }
        }

        public bool Delete(System.Collections.Generic.IEnumerable<T> entities, out IEnumerable<string> brokenRules)
        {
            foreach (T item in entities)
            {
                if (!IsValid(item))
                {
                    brokenRules = ValidateStringFieldLengths(item);
                    return false;
                }
            }
            brokenRules = new List<string>();
            foreach (T entity in entities)
            {
                _session.Delete(entity);
            }
            return true;
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
