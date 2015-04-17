using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTableMapper.Attributes
{
    //Same as PropertyMappingAttribute class but just a better name!

    /// <summary>
    /// Map a DataTable column to a property
    /// </summary>
    public class ColumnMappingAttribute : PropertyMappingAttribute
    {
        /// <summary>
        /// Indicates the column name(s) to map to this property
        /// </summary>
        /// <param name="mappings"></param>
        public ColumnMappingAttribute(params string[] mappings) : base(mappings) { }
    }
}
