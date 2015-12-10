using System;
using System.Linq;

namespace DataTableMapper.Mapping
{
    /// <summary>
    /// Helping to categorise types!
    /// </summary>
    internal class TypeHelper
    {
        public static bool IsSimpleType(Type type)
        {
            return type.IsPrimitive
                || type == typeof(Decimal)
                || type == typeof(String)
                || type == typeof(DateTime)
                || IsNullable(type)
                || type.IsEnum;
        }

        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        public static bool IsEnumerable(Type type)
        {
            if (type == typeof(String)) return false;

            return type.GetInterfaces().Contains(typeof(System.Collections.IEnumerable));
        }

        public static bool IsNullable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}