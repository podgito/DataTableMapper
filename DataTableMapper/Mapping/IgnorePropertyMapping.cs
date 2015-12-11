using DataTableMapper.Attributes;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DataTableMapper.Mapping
{
    internal class IgnorePropertyMapping : IPropertyMapping
    {
        public bool IsMatch(PropertyInfo property)
        {
            return property.GetCustomAttributes(typeof(IgnoreMappingAttribute), true).Any() || TypeHelper.IsEnumerable(property.PropertyType);
        }

        public void PerformMapping<T>(T obj, PropertyInfo property, DataRow row)
        {
            //Do nothing - we're ignoring mapping!
        }
    }
}