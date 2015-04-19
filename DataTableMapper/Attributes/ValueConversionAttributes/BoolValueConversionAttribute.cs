
using System;
namespace DataTableMapper.Attributes
{
    /// <summary>
    /// Tries to convert the object to an bool as per the C-like rules. e.g. zero is false, non-zero is true
    /// </summary>
    public class BoolValueConversionAttribute : ColumnMappingAttribute, IValueConversion
    {
        /// <summary>
        /// Map column to bool with Bool.TryParse and C-like value conversion attempts.
        /// </summary>
        /// <param name="aliases"></param>
        public BoolValueConversionAttribute(params string[] aliases) : base(aliases) { }

        /// <summary>
        /// Convert object to bool using C-like rules and Bool.TryParse
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public object Convert(object o)
        {
            if (o != null)
            {
                var intValue = IntegerValue(o);

                var toBoolValue = ToBool(o);

                return toBoolValue || intValue > 0;
            }
            else return null;

        }

        static bool ToBool(object o)
        {
            var b = false;
            Boolean.TryParse(o.ToString(), out b);

            return b;
        }

        static int IntegerValue(object o)
        {
            var x = 0;
            if (o != null)
                Int32.TryParse(o.ToString(), out x);
            return x;
        }
    }
}
