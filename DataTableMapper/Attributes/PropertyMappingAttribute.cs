using System;

namespace DataTableMapper.Attributes
{
    /// <summary>
    /// Allows DataTableMapper to map a property to a column name
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyMappingAttribute : System.Attribute, IValueConversion
    {
        public string[] Aliases { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aliases">An array of column names</param>
        public PropertyMappingAttribute(params string[] aliases)
        {
            Aliases = aliases;
        }

        /// <summary>
        /// The default convert just returns the input
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public virtual object Convert(object o)
        {
            return o;
        }


    }

    /// <summary>
    /// For converting a value to another value before setting a property
    /// </summary>
    public interface IValueConversion
    {
        /// <summary>
        /// Convert an object to another object before setting the property
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        object Convert(object o);
    }
}
