using System;
using System.Collections.Generic;

namespace DataTableMapper.Mapping
{
    internal class TypeHelper
    {
        public static bool IsSimpleType(Type type)
        {
            return type.IsPrimitive || type == typeof(Decimal) || type == typeof(String) || type == typeof(DateTime) || IsNullable(type);
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

            foreach (Type intType in type.GetInterfaces())
            {
                if (intType.IsGenericType
                    && intType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsNullable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}