using DataTableMapper.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataTableMapper
{
    /// <summary>
    /// Contains extension methods for System.Data.DataTable
    /// </summary>
    public static class DataTableExtensions
    {
        //Order is important!
        private static IEnumerable<IPropertyMapping> _propertyMappings = new List<IPropertyMapping> { new IgnorePropertyMapping(), new SimpleTypePropertyMapping(), new ComplexTypePropertyMapping() };

        /// <summary>
        /// Maps DataTable to type T's properties for each row in table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns>Enumerable of T</returns>
        public static IEnumerable<T> MapTo<T>(this DataTable table) where T : new()
        {
            //Do the reflection magic!
            //Go through all the properties of T and find columns for it

            //http://stackoverflow.com/questions/1089123/setting-a-property-by-reflection-with-a-string-value

            foreach (DataRow row in table.Rows)
            {
                yield return Map<T>(row);
            }
        }

        private static T Map<T>(DataRow row) where T : new()
        {
            var x = new T();

            var properties = x.GetType().GetProperties().Where(p => p.CanWrite);

            foreach (var property in properties)
            {
                var mapping = _propertyMappings.First(m => m.IsMatch(property));
                mapping.PerformMapping<T>(x, property, row);
            }

            return x;
        }

        /// <summary>
        /// Maps a column to an IEnumerable of T, where T is a primitive or string (simple types)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="columnName">The table column name to read</param>
        /// <returns>Enumerable of T</returns>
        public static IEnumerable<T> MapTo<T>(this System.Data.DataTable table, string columnName) where T : IComparable
        {
            foreach (DataRow row in table.Rows)
            {
                yield return row[columnName] == DBNull.Value ? default(T) : row.Field<T>(columnName);
            }
        }
    }
}