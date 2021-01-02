using DatabaseInterfaces;
using NHibernateLibraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderContext
{
    public class NHibernateServiceBase 
    {
        public string ConnectionString { get; }
        public string DomainAssemblyName { get; } 
        
        public NHibernateServiceBase(string connectionString, string domainAssemblyName)
        {
            ConnectionString = connectionString;
            DomainAssemblyName = domainAssemblyName;
        }

        public IRepository<T> CreateRepository<T>() where T : class, new()
        {
            return new NHibernateRepository<T>(ConnectionString, DomainAssemblyName);
        }

        public IEnumerable<string> Add<T>(T entity) where T : class
        {
            using (NHibernateRepository<T> repository = new NHibernateRepository<T>(ConnectionString, DomainAssemblyName))
            {
                IEnumerable<string> rules;
                repository.Add(entity, out rules);
                return rules;
            }
        }

        public IEnumerable<string> Update<T>(T entity) where T : class
        {
            using (NHibernateRepository<T> repository = new NHibernateRepository<T>(ConnectionString, DomainAssemblyName))
            {
                IEnumerable<string> rules;
                repository.Update(entity, out rules);
                return rules;
            }
        }

        public T FindBy<T>(int id) where T : class
        {
            using (IRepository<T> repository = new NHibernateRepository<T>(ConnectionString, DomainAssemblyName))
            {
                return repository.FindBy(id);
            }
        }

        public IEnumerable<T> FilterBy<T>(Expression<Func<T, bool>> expression) where T : class
        {
            using (IRepository<T> repository = new NHibernateRepository<T>(ConnectionString, DomainAssemblyName))
            {
                return repository.FilterBy(expression).ToList<T>();
            }
        }

        /// <summary>
        /// Dynamic Query Against a Table
        /// </summary>
        /// <typeparam name="T">Table Name</typeparam>
        /// <param name="propertyToFilter">Field Name in Table</param>
        /// <param name="value">Field Value To Compare</param>
        /// <returns></returns>
        private IEnumerable<T> GetByFilter<T>(FilterExpressionInfo filterExpressionInfo) where T : class
        {
            using (NHibernateRepository<T> repository = new NHibernateRepository<T>(ConnectionString, DomainAssemblyName))
            {
                var parameter = Expression.Parameter(typeof(T));
                var property = Expression.PropertyOrField(parameter, filterExpressionInfo.PropertyToFilter);
                var exprRight = Expression.Constant(filterExpressionInfo.Value);
                BinaryExpression binaryExpression = null;
                switch (filterExpressionInfo.FilterExpressionType)
                {
                    case FilterExpressionType.Equal:
                        binaryExpression = Expression.Equal(property, exprRight);
                        break;
                }
                Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);

                return (IEnumerable<T>)repository.FindBy(lambda);
            }
        }

        private IEnumerable<T> GetByFilter<T>(List<FilterExpressionInfo> filterExpressionInfos) where T : class
        {
            using (NHibernateRepository<T> repository = new NHibernateRepository<T>(ConnectionString, DomainAssemblyName))
            {
                var parameter = Expression.Parameter(typeof(T));
                BinaryExpression binaryExpressionBody = null;
                foreach (var filterExpressionInfo in filterExpressionInfos)
                {
                    var property = Expression.PropertyOrField(parameter, filterExpressionInfo.PropertyToFilter);
                    var exprRight = Expression.Constant(filterExpressionInfo.Value);
                    BinaryExpression binaryExpression = null;
                    switch (filterExpressionInfo.FilterExpressionType)
                    {
                        case FilterExpressionType.Equal:
                            binaryExpression = Expression.Equal(property, exprRight);
                            break;
                    }
                    if(binaryExpressionBody == null)
                    {
                        binaryExpressionBody = binaryExpression;
                    }
                    else
                    {
                        binaryExpressionBody = Expression.AndAlso(binaryExpressionBody, binaryExpression);
                    }
                }
                Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(binaryExpressionBody, parameter);

                return (IEnumerable<T>)repository.FindBy(lambda);
            }
        }
    }
}
