using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using DataTableMapper.Attributes;
using System.Linq;
using DataTableMapper.Attributes.Core;

namespace DataTableMapper
{
    /// <summary>
    /// Contains extension methods for System.Data.DataTable
    /// </summary>
    public static class DataTableExtensions
    {


        //Property mapping attribute
        //Property name

        private static IEnumerable<T> MapTo<T>(this System.Data.DataTable table, IPreProcessor preProcessor) where T : new()
        {
            //Do the reflection magic!
            //Go through all the properties of T and find columns for it

            //http://stackoverflow.com/questions/1089123/setting-a-property-by-reflection-with-a-string-value

            foreach (DataRow row in table.Rows)
            {
                yield return Map<T>(row);
            }
        }

        //1) ColumnMappingAttributes to get a value for this property from a column (Column Mapping) return null or default value if none found
        //2) Loop th

        private static T Map2<T>(DataRow row) where T: new()
        {
            var x = new T();



            return x;
        }

        private static T Map<T>(DataRow row) where T : new()
        {
            var x = new T();

            var properties = x.GetType().GetProperties();

            foreach (var property in properties)
            {
                object value = null;
                var propertyType = property.PropertyType;

                if (PrimitiveCheck(propertyType))
                {
                    //1) Property attributes
                    var propertyAttributes = property.GetCustomAttributes(typeof(DataMapperAttribute), true).OrderBy(p => ((DataMapperAttribute)p).Priority);


                    foreach (var attribute in propertyAttributes)
                    {
                        //if (attribute.GetType() == typeof(PropertyMappingAttribute))
                        //if(attribute.GetType().IsAssignableFrom(typeof(PropertyMappingAttribute)))
                        if (attribute is IValueConversion)
                        {
                            var propertyMappingAttribute = (PropertyMappingAttribute)attribute;


                            foreach (var alias in propertyMappingAttribute.Aliases)
                            {
                                try
                                {
                                    value = propertyMappingAttribute.Convert(row[alias]);


                                    break;
                                }
                                catch (ArgumentException) { }
                            }

                            if (value == null) //If still null, revert to property name but still convert!
                            {
                                value = propertyMappingAttribute.Convert(GetValueByPropertyName(row, property));
                            }

                            if (value != null)
                            {
                                property.SetValue(x, Convert.ChangeType(value, property.PropertyType), null);
                                continue;
                            }

                        }


                    }

                    //2) Property name if the value is still null

                    if (value == null)
                    {
                        try
                        {
                            value = row[property.Name];

                            property.SetValue(x, Convert.ChangeType(value, property.PropertyType), null);
                        }
                        catch (ArgumentException) { }
                    }


                }

                else //Else for complex types, map to recursively - kind of! Need to use reflection to invoke a generic method. See: http://stackoverflow.com/questions/2107845/generics-in-c-using-type-of-a-variable-as-parameter
                {
                    MethodInfo method = typeof(DataTableExtensions).GetMethod("Map", BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(new Type[] { propertyType });

                    var complexPropertyInstance = method.Invoke(null, new object[] { row });

                    property.SetValue(x, Convert.ChangeType(complexPropertyInstance, property.PropertyType), null);
                }

                //Property custom assignment
            }

            return x;
        }

        private static object GetValueByPropertyName(DataRow row, PropertyInfo property)
        {
            return row[property.Name];
        }

        private static bool PrimitiveCheck(Type type)
        {
            return type.IsPrimitive || type == typeof(Decimal) || type == typeof(String) || type == typeof(DateTime);
        }

        /// <summary>
        /// Maps DataTable to type T's properties for each row in table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns>Enumerable of T</returns>
        public static IEnumerable<T> MapTo<T>(this System.Data.DataTable table) where T : new()
        {
            return MapTo<T>(table, new NullPreProcessor());
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
