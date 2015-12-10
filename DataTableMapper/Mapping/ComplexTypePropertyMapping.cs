using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataTableMapper.Mapping
{
    class ComplexTypePropertyMapping : IPropertyMapping
    {
        public bool IsMatch(PropertyInfo property)
        {
            return !TypeHelper.IsSimpleType(property.PropertyType);
        }

        public void PerformMapping<T>(T obj, PropertyInfo property, DataRow row)
        {
            try
            {
                MethodInfo method =
                    typeof(DataTableExtensions).GetMethod("Map", BindingFlags.NonPublic | BindingFlags.Static)
                        .MakeGenericMethod(new Type[] { property.PropertyType });

                var complexPropertyInstance = method.Invoke(null, new object[] { row });

                property.SetValue(obj, Convert.ChangeType(complexPropertyInstance, property.PropertyType), null);
            }
            catch (ArgumentException) { } //Catch exceptions where in input does not meet the generic constraint (parameterless constructor)
        }
    }
}
