using DataTableMapper.Attributes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTableMapper.Attributes
{
    /// <summary>
    /// Use to set a default value for a property where no mapping can be done
    /// </summary>
    public class DefaultValueAttribute : DataMapperAttribute
    {
        /// <summary>
        /// The default value for the property 
        /// </summary>
        internal object Value { get; private set; }

        /// <summary>
        /// Setup a default value for the property after mapping if no mapping can be found or the value is DBNull
        /// </summary>
        /// <param name="value">The default value of the property</param>
        public DefaultValueAttribute(object value)
        {
            Value = value;
        }

    }
}
