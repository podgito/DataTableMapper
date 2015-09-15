using System;
using DataTableMapper.Mapping;

namespace DataTableMapper.TypeConversion
{
    class NullableTypeConverter : ITypeConverter
    {
        public object Convert(object value, Type toType)
        {
            var underlyingType = Nullable.GetUnderlyingType(toType);
            return System.Convert.ChangeType(value, underlyingType);
        }

        public bool IsMatch(Type fromType, Type toType)
        {
            return TypeHelper.IsNullable(toType);
        }
    }
}