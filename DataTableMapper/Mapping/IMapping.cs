using System.Data;
using System.Reflection;

namespace DataTableMapper.Mapping
{
    internal interface IMapping
    {
        object Map(PropertyInfo property, DataRow row);
    }
}