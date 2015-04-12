using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTableMapper.Attributes
{
    /// <summary>
    /// Tries to convert the object to an bool as per the C-like rules. e.g. zero is false, non-zero is true
    /// </summary>
    public class BoolValueConversionAttribute : PropertyMappingAttribute
    {
        /// <summary>
        /// Map column to bool with Bool.TryParse and C-like value conversion attempts.
        /// </summary>
        /// <param name="aliases"></param>
        public BoolValueConversionAttribute(params string[] aliases) : base(aliases) { }
        public override object Convert(object o)
        {

            var intValue = IntegerValue(o);

            var toBoolValue = ToBool(o);

            return toBoolValue || intValue > 0;
        }

        static bool ToBool(object o)
        {
            var b = false;
            bool.TryParse(o.ToString(), out b);

            return b;
        }

         static int IntegerValue( object o)
        {
            var x = 0;
            int.TryParse(o.ToString(), out x);
            return x;
        }
    }
}
