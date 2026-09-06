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
                || type == typeof(Guid)
                || type == typeof(TimeSpan)
                || type == typeof(DateTimeOffset)
                || (type.IsArray && IsSimpleType(type.GetElementType()))
                || (IsNullable(type) && IsSimpleType(Nullable.GetUnderlyingType(type)))
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
            //Anything we treat as a scalar (string, byte[], char[], and other simple-element
            //arrays) implements IEnumerable but is a value to set, not a child collection to skip.
            if (IsSimpleType(type)) return false;

            return type.GetInterfaces().Contains(typeof(System.Collections.IEnumerable));
        }

        public static bool IsNullable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}