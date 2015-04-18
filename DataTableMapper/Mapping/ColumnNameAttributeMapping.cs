using DataTableMapper.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataTableMapper.Extensions;

namespace DataTableMapper.Mapping
{
    class ColumnNameAttributeMapping : IMapping
    {
        public object Map(System.Reflection.PropertyInfo property, System.Data.DataRow row)
        {
            if (TypeHelper.IsSimpleType(property.PropertyType))
            {

                if (property.GetCustomAttributes(typeof(ColumnMappingAttribute), true).Any())
                {
                    var mappingAttribute = (ColumnMappingAttribute)property.GetCustomAttributes(typeof(ColumnMappingAttribute), true).First(); //Ordering may be redundant if we linit to only 1 ColumnMappingAttribute per property

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



            throw new NotImplementedException();
        }
    }
}
