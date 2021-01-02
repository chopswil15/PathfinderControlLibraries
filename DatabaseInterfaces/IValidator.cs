using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterfaces
{
    public interface IValidator<T>
    {
        bool IsValid(T entity);
        IEnumerable<string> BrokenRules(T entity);
    }
}
