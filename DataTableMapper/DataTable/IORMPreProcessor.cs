using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTableMapper.DataTable
{
    public interface IORMPreProcessor
    {

        object Process(object value);

    }

    class NullPreProcessor : IORMPreProcessor
    {
        public object Process(object value)
        {
            return value;
        }
    }

}
