using System;

namespace DataTableMapper.Attributes.Core
{
    /// <summary>
    /// Base class for all DataMapper Attributes
    /// </summary>
    public abstract class DataMapperAttribute : Attribute
    {
        internal virtual AttributePriority Priority { get { return AttributePriority.Normal; } }
    }

    internal enum AttributePriority
    {
        Low = 0,
        Normal = 1,
        Hight = 2
    }
}