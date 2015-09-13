using DataTableMapper.Extensions;
using System;

namespace DataTableMapper.Mapping
{
    internal class PropertyNameMapping : IMapping
    {
        public object Map(System.Reflection.PropertyInfo property, System.Data.DataRow row)
        {
            if (TypeHelper.IsSimpleType(property.PropertyType))
            {
                try
                {
                    return row.TryReadColumn(property.Name);
                }
                catch (ArgumentException) { return null; }
            }
            else
            {
                throw new ArgumentException("Property must be a simple type");
            }
        }
    }
}