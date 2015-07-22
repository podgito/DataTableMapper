using System;

namespace DataTableMapper.Attributes.Core
{
    /// <summary>
    /// Base class for all DataMapper Attributes
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public abstract class DataMapperAttribute : Attribute
    {
    }

}