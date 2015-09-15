using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTableMapper.TypeConversion
{
    interface ITypeConverter
    {

        object Convert(object value, Type toType);
        bool IsMatch(Type fromType, Type toType);

    }
}
