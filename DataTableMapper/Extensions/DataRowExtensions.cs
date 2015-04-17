using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataTableMapper.Extensions
{
    static class DataRowExtensions
    {

        public static object TryReadColumn(this DataRow row, string columnName)
        {
            object value = null;

            value = row[columnName] == DBNull.Value ? null : row[columnName];

            return value;
        }

    }
}
