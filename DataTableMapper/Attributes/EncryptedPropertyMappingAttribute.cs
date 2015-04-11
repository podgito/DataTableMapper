using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTableMapper.Attributes
{
    public class EncryptedPropertyMappingAttribute : PropertyMappingAttribute
    {

        public EncryptedPropertyMappingAttribute() : base(new string[0]) { }

        public EncryptedPropertyMappingAttribute(params string[] aliases) : base(aliases) { }

    }
}
