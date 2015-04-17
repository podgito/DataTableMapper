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
        public object Value { get; protected set; }

        public DefaultValueAttribute(object value)
        {
            Value = value;
        }

    }
}
