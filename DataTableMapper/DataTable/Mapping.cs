using DataTableMapper.Attributes;
using DataTableMapper.Attributes.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataTableMapper
{
    internal class AttributeMapping
    {


        public object PropertyMapping(PropertyInfo propertyInfo, DataRow row)
        {
            object value = null;

            if (IsSimpleType(propertyInfo.PropertyType))
            {
                var propertyAttributes = propertyInfo.GetCustomAttributes(typeof(ColumnMappingAttributeBase), true).OrderBy(p => ((ColumnMappingAttributeBase)p).Priority);

                value = ColumnMapping(propertyAttributes, row);

                if (value == null)
                {
                    value = PropertyNameMapping(propertyInfo.Name, row);
                }
                if (value == null)
                {
                    DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)propertyInfo.GetCustomAttributes(typeof(DefaultValueAttribute), true).FirstOrDefault(); //Should only be a max of one due to attrinute usage constraint

                    if (defaultValueAttribute != null)
                    {
                        value = defaultValueAttribute.Value;
                    }
                }
            }
            else
            {
                throw new ArgumentException("Property must be a simple type");
            }

            return value;
        }

        public object ColumnMapping(IEnumerable<object> mappingAttributes, DataRow row)
        {
            object value = null;
            foreach (ColumnMappingAttribute a in mappingAttributes)
            {
                foreach (var alias in a.Aliases)
                {
                    try
                    {
                        value = row[alias]; //Maybe use row.ItemArray
                        break; //Mapping found
                    }
                    catch (ArgumentException) { }
                }
            }

            return value;
        }


        private object PropertyNameMapping(string propertyName, DataRow row)
        {
            try
            {
                return row[propertyName];
            }
            catch (ArgumentException) { return null; }
        }

        /// <summary>
        /// Converts the input value with the value converts (if any)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="valueConverters"></param>
        /// <returns></returns>
        public object ValueConversion(object value, IValueConversion valueConverter)
        {
            if (valueConverter != null) return valueConverter.Convert(value);
            else return value;
        }



        private static bool IsSimpleType(Type type)
        {
            return type.IsPrimitive || type == typeof(Decimal) || type == typeof(String) || type == typeof(DateTime);
        }

    }
}
