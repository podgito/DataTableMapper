using DataTableMapper.Attributes;
using DataTableMapper.Extensions;
using System.Data;
using System.Reflection;

namespace DataTableMapper.Mapping
{
    /// <summary>
    /// Shared handling for <see cref="IValueConversion"/> attributes, used by both the simple-type
    /// and complex-type property mappings so a converting attribute behaves the same on either.
    /// </summary>
    internal static class ValueConversionHelper
    {
        /// <summary>
        /// The <see cref="IValueConversion"/> attribute on <paramref name="property"/>, or null.
        /// </summary>
        public static IValueConversion Find(PropertyInfo property)
        {
            var attributes = property.GetCustomAttributes(typeof(IValueConversion), true);
            return attributes.Length > 0 ? (IValueConversion)attributes[0] : null;
        }

        /// <summary>
        /// The raw column value feeding the converter: the first non-null
        /// <see cref="ColumnMappingAttribute"/> alias, else the property name.
        /// </summary>
        public static object ReadSourceValue(PropertyInfo property, DataRow row)
        {
            var columnAttributes = property.GetCustomAttributes(typeof(ColumnMappingAttribute), true);
            if (columnAttributes.Length > 0)
            {
                foreach (var alias in ((ColumnMappingAttribute)columnAttributes[0]).Aliases)
                {
                    var value = row.TryReadColumn(alias);
                    if (value != null) return value;
                }
            }

            return row.TryReadColumn(property.Name);
        }
    }
}
