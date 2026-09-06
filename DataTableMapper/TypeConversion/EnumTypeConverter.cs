using System;

namespace DataTableMapper.TypeConversion
{
    /// <summary>
    /// Converts values to enum
    /// </summary>
    internal class EnumTypeConverter : ITypeConverter
    {
        public object Convert(object value, Type toType)
        {
            //Case-insensitive: a database column holding "horror" should bind the same as "Horror".
            if (value is string) return Enum.Parse(toType, (string)value, ignoreCase: true);
            else return Enum.ToObject(toType, value);
        }

        public bool IsMatch(Type fromType, Type toType)
        {
            return toType.IsEnum;
        }
    }
}