using System;

namespace DataTableMapper.TypeConversion
{
    /// <summary>
    /// For converting types
    /// </summary>
    internal interface ITypeConverter
    {
        object Convert(object value, Type toType);

        bool IsMatch(Type fromType, Type toType);
    }
}