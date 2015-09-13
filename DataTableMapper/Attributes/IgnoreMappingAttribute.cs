using System;

namespace DataTableMapper.Attributes
{
    /// <summary>
    /// Have the mapping ignore setting this property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IgnoreMappingAttribute : Attribute
    {
    }
}