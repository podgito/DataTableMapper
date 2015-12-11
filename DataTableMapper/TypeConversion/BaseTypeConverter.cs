using System;

namespace DataTableMapper.TypeConversion
{
    /// <summary>
    /// Basic type conversion
    /// </summary>
    internal class BaseTypeConverter : ITypeConverter
    {
        public object Convert(object value, Type toType)
        {
            return System.Convert.ChangeType(value, toType);
        }

        public bool IsMatch(Type fromType, Type toType)
        {
            return true;
        }
    }
}