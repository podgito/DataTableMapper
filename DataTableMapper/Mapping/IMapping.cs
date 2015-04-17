using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataTableMapper.Mapping
{
    interface IMapping
    {

        object Map(PropertyInfo property, DataRow row);

    }

}
