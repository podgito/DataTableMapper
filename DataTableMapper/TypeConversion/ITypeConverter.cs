using System;

namespace DataTableMapper.TypeConversion
{
    internal interface ITypeConverter
    {
        object Convert(object value, Type toType);

        bool IsMatch(Type fromType, Type toType);
    }
}