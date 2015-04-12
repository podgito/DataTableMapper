using System;


namespace DataTableMapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ValueConversionAttribute : PropertyMappingAttribute
    {
        public override abstract object Convert(object o);
    }

}
