using DataTableMapper.Attributes.Core;
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
    public class ColumnMappingAttribute : ColumnMappingAttributeBase
    {
        /// <summary>
        /// DataTable column name aliases
        /// </summary>
        public string[] Aliases { get; protected set; }

        /// <summary>
        /// Indicates the column name(s) to map to this property
        /// </summary>
        /// <param name="aliases"></param>
        public ColumnMappingAttribute(params string[] aliases)
        {
            Aliases = aliases;
        }
    }
}
