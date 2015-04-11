using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataTableMapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ValueConversionAttribute : PropertyMappingAttribute
    {
        public override abstract object Convert(object o);
    }

}
