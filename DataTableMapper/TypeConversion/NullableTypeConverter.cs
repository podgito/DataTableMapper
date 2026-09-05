using DataTableMapper.Mapping;
using System;

namespace DataTableMapper.TypeConversion
{
    /// <summary>
    /// Convert a value to a nullable type by converting to its underlying type.
    /// </summary>
    internal class NullableTypeConverter : ITypeConverter
    {
        public object Convert(object value, Type toType)
        {
            var underlyingType = Nullable.GetUnderlyingType(toType);

            //Re-dispatch through the registry so the underlying type gets the right converter
            //(enum, Guid/TimeSpan/DateTimeOffset, invariant-culture base conversion, ...).
            return TypeConverterRegistry.Convert(value, underlyingType);
        }

        public bool IsMatch(Type fromType, Type toType)
        {
            return TypeHelper.IsNullable(toType);
        }
    }
}
