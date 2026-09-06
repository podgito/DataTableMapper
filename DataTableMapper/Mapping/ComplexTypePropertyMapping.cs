using DataTableMapper.TypeConversion;
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

        public void PerformMapping(object obj, PropertyInfo property, DataRow row)
        {
            if (DataTableExtensions.IsTypeBeingMapped(property.PropertyType))
            {
                //A value of this type is already being mapped higher up the graph - mapping it again
                //from the same row would recurse forever. Leave the property unset.
                return;
            }

            //A Nullable<TStruct> property is mapped as its underlying struct: Map<Nullable<T>> would
            //box to a null reference and NRE. The boxed struct assigns straight into the nullable slot.
            var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

            //An IValueConversion attribute takes over completely: it reads the column itself and
            //produces the property value, so there is nothing to recurse into.
            var converter = ValueConversionHelper.Find(property);
            if (converter != null)
            {
                var converted = converter.Convert(ValueConversionHelper.ReadSourceValue(property, row));
                if (converted != null)
                {
                    var value = property.PropertyType.IsInstanceOfType(converted)
                        ? converted
                        : TypeConverterRegistry.Convert(converted, targetType);
                    property.SetValue(obj, value, null);
                }
                return;
            }

            try
            {
                MethodInfo method =
                    typeof(DataTableExtensions).GetMethod("Map", BindingFlags.NonPublic | BindingFlags.Static)
                        .MakeGenericMethod(new Type[] { targetType });

                //Map<targetType> already returns an instance of exactly targetType, so no further
                //conversion is needed - and Convert.ChangeType cannot target Nullable<T> anyway.
                var complexPropertyInstance = method.Invoke(null, new object[] { row });

                property.SetValue(obj, complexPropertyInstance, null);
            }
            catch (ArgumentException) { } //Catch exceptions where input does not meet the generic constraint (parameterless constructor)
        }
    }
}