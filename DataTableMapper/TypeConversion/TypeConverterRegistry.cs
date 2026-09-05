using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTableMapper.TypeConversion
{
    /// <summary>
    /// Holds the ordered set of type converters and applies the first one that matches.
    /// </summary>
    internal static class TypeConverterRegistry
    {
        //Order is important!
        private static readonly IEnumerable<ITypeConverter> _converters = new List<ITypeConverter>
        {
            new EnumTypeConverter(),
            new NullableTypeConverter(),
            new NonConvertibleTypeConverter(),
            new BaseTypeConverter()
        };

        public static object Convert(object value, Type toType)
        {
            var converter = _converters.First(c => c.IsMatch(value.GetType(), toType));
            return converter.Convert(value, toType);
        }
    }
}
