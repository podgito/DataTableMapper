using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTableMapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyMappingAttribute : System.Attribute, IValueConversion
    {
        public string[] Aliases { get; protected set; }
        public PropertyMappingAttribute(params string[] aliases)
        {
            Aliases = aliases;
        }


        public virtual object Convert(object o)
        {
            return o;
        }


    }

    public interface IValueConversion
    {
        object Convert(object o);
    }
}
