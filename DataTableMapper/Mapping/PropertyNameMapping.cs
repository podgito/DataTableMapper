using DataTableMapper.Attributes;
using DataTableMapper.Attributes.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataTableMapper.Mapping
{
    class PropertyNameMapping : IMapping
    {

        public object Map(System.Reflection.PropertyInfo property, System.Data.DataRow row)
        {

            if (TypeChecker.IsSimpleType(property.PropertyType))
            {
                try
                {
                    return row[property.Name];
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
