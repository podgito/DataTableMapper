using System;
using System.Data;

namespace DataTableMapper.Extensions
{
    internal static class DataRowExtensions
    {
        /// <summary>
        /// Try to get a value from the row for the specified column name
        /// </summary>
        /// <param name="row"></param>
        /// <param name="columnName"></param>
        /// <returns>Returns the object in the column or null if the value is a DBNull or the column doesn't exist</returns>
        public static object TryReadColumn(this DataRow row, string columnName)
        {
            object value = null;

            if (row.Table.Columns.Contains(columnName))
                value = row[columnName] == DBNull.Value ? null : row[columnName];

            return value;
        }
    }
}