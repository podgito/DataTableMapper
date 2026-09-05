using System;
using System.Globalization;

namespace DataTableMapper.TypeConversion
{
    /// <summary>
    /// Converts values to types that do not implement IConvertible (Guid, TimeSpan, DateTimeOffset)
    /// and therefore cannot go through Convert.ChangeType. Only handles the cases that genuinely
    /// need it: parsing from a string, and DateTime to DateTimeOffset. A value already of the
    /// target type falls through to BaseTypeConverter.
    /// </summary>
    internal class NonConvertibleTypeConverter : ITypeConverter
    {
        public object Convert(object value, Type toType)
        {
            if (value is DateTime && toType == typeof(DateTimeOffset))
            {
                return new DateTimeOffset((DateTime)value);
            }

            var text = value.ToString();

            if (toType == typeof(Guid))
            {
                return Guid.Parse(text);
            }
            if (toType == typeof(TimeSpan))
            {
                return TimeSpan.Parse(text, CultureInfo.InvariantCulture);
            }

            return DateTimeOffset.Parse(text, CultureInfo.InvariantCulture);
        }

        public bool IsMatch(Type fromType, Type toType)
        {
            if (toType != typeof(Guid) && toType != typeof(TimeSpan) && toType != typeof(DateTimeOffset))
            {
                return false;
            }

            if (fromType == typeof(string))
            {
                return true;
            }

            return toType == typeof(DateTimeOffset) && fromType == typeof(DateTime);
        }
    }
}
