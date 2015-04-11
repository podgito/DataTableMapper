using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DataTableMapper.Attributes;

namespace DataTableMapper.DataTable
{
    public static class DataTableRM
    {


        //Property mapping attribute
        //Property name

        public static IEnumerable<T> MapTo<T>(this System.Data.DataTable table, IORMPreProcessor preProcessor) where T : new()
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

            var properties = x.GetType().GetProperties();

            foreach (var property in properties)
            {
                object value = null;
                var propertyType = property.PropertyType;

                if (PrimitiveCheck(propertyType))
                {
                    //1) Property attributes
                    var propertyAttributes = property.GetCustomAttributes(true);
                     
                    foreach (var attribute in propertyAttributes)
                    {
                        //if (attribute.GetType() == typeof(PropertyMappingAttribute))
                        //if(attribute.GetType().IsAssignableFrom(typeof(PropertyMappingAttribute)))
                        if(attribute is IValueConversion)
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

                            if(value == null) //If still null, revert to property name but still convert!
                            {
                                value = propertyMappingAttribute.Convert(GetValueByPropertyName(row, property));
                            }

                            if(value!=null)
                            {
                                property.SetValue(x, Convert.ChangeType(value, property.PropertyType), null);
                                continue;
                            }
                            
                        }

                       
                    }

                    //2) Property name if the value is still null

                    if(value == null)
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
                    MethodInfo method = typeof(DataTableRM).GetMethod("Map", BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(new Type[] { propertyType });

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

        public static IEnumerable<T> MapTo<T>(this System.Data.DataTable table) where T : new()
        {
            return MapTo<T>(table, new NullPreProcessor());
        }

        //Primitives
        public static IEnumerable<T> MapTo<T>(this System.Data.DataTable table, string columnName) where T : IComparable
        {
            foreach (DataRow row in table.Rows)
            {
                yield return row[columnName] == DBNull.Value ? default(T) : row.Field<T>(columnName);
            }
        }
    }
}
