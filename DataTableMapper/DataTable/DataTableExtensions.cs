using DataTableMapper.Attributes;
using DataTableMapper.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DataTableMapper
{
    /// <summary>
    /// Contains extension methods for System.Data.DataTable
    /// </summary>
    public static class DataTableExtensions
    {
        //Applying open-closed principle - order is important!!
        private static IEnumerable<IMapping> _mappings = new List<IMapping>() { new ColumnNameAttributeMapping(), new PropertyNameMapping(), new DefaultValueAttributeMapping() };

        /// <summary>
        /// Maps DataTable to type T's properties for each row in table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns>Enumerable of T</returns>
        public static IEnumerable<T> MapTo<T>(this DataTable table) where T : new()
        {
            //Do the reflection magic!
            //Go through all the properties of T and find columns for it

            //http://stackoverflow.com/questions/1089123/setting-a-property-by-reflection-with-a-string-value

            foreach (DataRow row in table.Rows)
            {
                yield return Map<T>(row);
            }
        }

        private static T Map<T>(DataRow row) where T : new()
        {
            var x = new T();

            var properties = x.GetType().GetProperties().Where(p => p.CanWrite);

            foreach (var property in properties)
            {
                object mappedValue = null;

                if (TypeHelper.IsSimpleType(property.PropertyType))
                {
                    // 1) Mapping
                    foreach (var mapping in _mappings)
                    {
                        mappedValue = mapping.Map(property, row);
                        if (mappedValue != null) break; //Break if we have gotten a value
                    }

                    //2) conversion
                    object convertedMappedValue = AttributeConversion(property, mappedValue);

                    if (convertedMappedValue == null)
                    {
                        convertedMappedValue = TypeHelper.GetDefault(property.PropertyType);
                    }

                    property.SetValue(x, Convert.ChangeType(convertedMappedValue, property.PropertyType), null);
                }
                else //complex type
                {
                    MethodInfo method = typeof(DataTableExtensions).GetMethod("Map", BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(new Type[] { property.PropertyType });

                    var complexPropertyInstance = method.Invoke(null, new object[] { row });

                    property.SetValue(x, Convert.ChangeType(complexPropertyInstance, property.PropertyType), null);
                }
            }

            return x;
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
        /// Maps a column to an IEnumerable of T, where T is a primitive or string (simple types)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="columnName">The table column name to read</param>
        /// <returns>Enumerable of T</returns>
        public static IEnumerable<T> MapTo<T>(this System.Data.DataTable table, string columnName) where T : IComparable
        {
            foreach (DataRow row in table.Rows)
            {
                yield return row[columnName] == DBNull.Value ? default(T) : row.Field<T>(columnName);
            }
        }
    }
}