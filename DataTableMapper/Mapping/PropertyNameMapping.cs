using DataTableMapper.Attributes;
using DataTableMapper.Attributes.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataTableMapper.Extensions;

namespace DataTableMapper.Mapping
{
    class PropertyNameMapping : IMapping
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
