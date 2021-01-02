using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DatabaseInterfaces
{
    public class FilterExpressionInfo
    {
        public string PropertyToFilter {get;set;}
        public object Value { get; set; }
        public FilterExpressionType FilterExpressionType { get; set; }

    }

    //allowed ExpressionTypes
    public enum FilterExpressionType
    {
        Equal = ExpressionType.Equal,
        NotEqual = ExpressionType.NotEqual,
        GreaterThan = ExpressionType.GreaterThan,
        GreaterThanOrEqual = ExpressionType.GreaterThanOrEqual,
        LessThan = ExpressionType.LessThan,
        LessThanOrEqual = ExpressionType.LessThanOrEqual
    }
}
