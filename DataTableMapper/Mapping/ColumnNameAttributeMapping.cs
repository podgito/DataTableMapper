using DataTableMapper.Attributes;
using DataTableMapper.Extensions;
using System;

namespace DataTableMapper.Mapping
{
    internal class ColumnNameAttributeMapping : IMapping
    {
        public object Map(System.Reflection.PropertyInfo property, System.Data.DataRow row)
        {
            if (TypeHelper.IsSimpleType(property.PropertyType))
            {
                var attributes = property.GetCustomAttributes(typeof(ColumnMappingAttribute), true);
                if (attributes.Length > 0)
                {
                    var mappingAttribute = (ColumnMappingAttribute)attributes[0]; //Only one ColumnMappingAttribute per property is supported

                    foreach (var alias in mappingAttribute.Aliases)
                    {
                        try
                        {
                            object value = row.TryReadColumn(alias);

                            if (value != null)
                                return value;
                        }
                        catch (ArgumentException) { }
                    }
                }
                return null;
            }
            else
            {
                throw new ArgumentException("Property must be a simple type");
            }
        }
    }
}