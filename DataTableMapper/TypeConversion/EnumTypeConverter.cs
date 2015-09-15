using System;

namespace DataTableMapper.TypeConversion
{
    class EnumTypeConverter : ITypeConverter
    {

        public object Convert(object value, Type toType)
        {
            if (value is string) return Enum.Parse(toType, (string) value);
            else return Enum.ToObject(toType, value);
        }

        public bool IsMatch(Type fromType, Type toType)
        {
            return toType.IsEnum;
        }
    }
}