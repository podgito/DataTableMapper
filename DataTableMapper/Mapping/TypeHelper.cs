using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTableMapper.Mapping
{
    class TypeHelper
    {

        public static bool IsSimpleType(Type type)
        {
            return type.IsPrimitive || type == typeof(Decimal) || type == typeof(String) || type == typeof(DateTime);
        }

        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

    }
}
