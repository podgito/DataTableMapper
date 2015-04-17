using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTableMapper.Mapping
{
    class TypeChecker
    {

        public static bool IsSimpleType(Type type)
        {
            return type.IsPrimitive || type == typeof(Decimal) || type == typeof(String) || type == typeof(DateTime);
        }

    }
}
