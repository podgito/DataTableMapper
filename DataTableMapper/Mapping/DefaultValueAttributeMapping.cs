using DataTableMapper.Attributes;
using System;

namespace DataTableMapper.Mapping
{
    internal class DefaultValueAttributeMapping : IMapping
    {
        public object Map(System.Reflection.PropertyInfo property, System.Data.DataRow row)
        {
            if (TypeHelper.IsSimpleType(property.PropertyType))
            {
                var attributes = property.GetCustomAttributes(typeof(DefaultValueAttribute), true);
                if (attributes.Length > 0)
                {
                    return ((DefaultValueAttribute)attributes[0]).Value;
                }

                return null; //return null if we get this far
            }
            else
            {
                throw new ArgumentException("Property must be a simple type");
            }
        }
    }
}