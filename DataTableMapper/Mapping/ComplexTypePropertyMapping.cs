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
            if (DataTableExtensions.IsTypeBeingMapped(property.PropertyType))
            {
                //A value of this type is already being mapped higher up the graph - mapping it again
                //from the same row would recurse forever. Leave the property unset.
                return;
            }

            try
            {
                MethodInfo method =
                    typeof(DataTableExtensions).GetMethod("Map", BindingFlags.NonPublic | BindingFlags.Static)
                        .MakeGenericMethod(new Type[] { property.PropertyType });

                var complexPropertyInstance = method.Invoke(null, new object[] { row });

                property.SetValue(obj, Convert.ChangeType(complexPropertyInstance, property.PropertyType), null);
            }
            catch (ArgumentException) { } //Catch exceptions where input does not meet the generic constraint (parameterless constructor)
        }
    }
}