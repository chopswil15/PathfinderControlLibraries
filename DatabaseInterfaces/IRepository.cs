using System;
using System.Collections.Generic;

namespace DatabaseInterfaces
{
    public interface IRepository<TEntity> : IDisposable,  IReadOnlyRepository<TEntity> where TEntity : class
    {
        bool Add(TEntity entity, out IEnumerable<string> brokenRules);
        bool Delete(TEntity entity, out IEnumerable<string> brokenRules);
        bool Update(TEntity entity, out IEnumerable<string> brokenRules);
        Dictionary<string, int> GetStringFieldLengths();
    }
}
