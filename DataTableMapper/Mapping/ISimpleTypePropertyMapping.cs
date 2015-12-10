using DataTableMapper.Attributes;
using DataTableMapper.Mapping;
using DataTableMapper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataTableMapper.Mapping
{
    class SimpleTypePropertyMapping : IPropertyMapping
    {
        private static IEnumerable<IMapping> _mappings = new List<IMapping>() { new ColumnNameAttributeMapping(), new PropertyNameMapping() };
        private static IEnumerable<ITypeConverter> _typeConverters = new List<ITypeConverter> { new EnumTypeConverter(), new NullableTypeConverter(), new BaseTypeConverter() };
        private static DefaultValueAttributeMapping _defaultMapping = new DefaultValueAttributeMapping();

        public bool IsMatch(PropertyInfo property)
        {
            return TypeHelper.IsSimpleType(property.PropertyType);
        }

        public void PerformMapping<T>(T obj, PropertyInfo property, DataRow row)
        {
            object mappedValue = null;
            // 1) Mapping
            foreach (var mapping in _mappings)
            {
                mappedValue = mapping.Map(property, row);
                if (mappedValue != null) break; //Break if we have gotten a value
            }

            //2) conversion
            object convertedMappedValue = AttributeConversion(property, mappedValue);

            //3) Check for default values
            if (convertedMappedValue == null) convertedMappedValue = _defaultMapping.Map(property, row);

            if (convertedMappedValue == null)
            {
                convertedMappedValue = TypeHelper.GetDefault(property.PropertyType);
            }

            SetPropertyValue(obj, convertedMappedValue, property);
        }

        private static object AttributeConversion(PropertyInfo property, object value)
        {
            if (property.GetCustomAttributes(typeof(IValueConversion), true).Any())
            {
                var converter = (IValueConversion)property.GetCustomAttributes(typeof(IValueConversion), true).First();
                return converter.Convert(value);
            }
            else return value;
        }

        /// <summary>
        /// set the property value converting it to the right type. Checks for nullables also
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <param name="property"></param>
        private static void SetPropertyValue(object obj, object value, PropertyInfo property)
        {

            if (value != null)
            {
                var converter = _typeConverters.First(x => x.IsMatch(value.GetType(), property.PropertyType));
                property.SetValue(obj, converter.Convert(value, property.PropertyType), null);
            }
        }
    }
}
