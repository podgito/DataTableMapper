using System;
using System.Data;
using System.Reflection;

namespace DataTableMapper.Mapping
{
    internal class ComplexTypePropertyMapping : IPropertyMapping
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