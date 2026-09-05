using System;
using System.Globalization;

namespace DataTableMapper.TypeConversion
{
    /// <summary>
    /// Basic type conversion
    /// </summary>
    internal class BaseTypeConverter : ITypeConverter
    {
        public object Convert(object value, Type toType)
        {
            //Invariant culture: the data is database-sourced, so parsing must not depend on the host locale.
            return System.Convert.ChangeType(value, toType, CultureInfo.InvariantCulture);
        }

        public bool IsMatch(Type fromType, Type toType)
        {
            return true;
        }
    }
}
