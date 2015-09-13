namespace DataTableMapper
{
    /// <summary>
    /// Expose processing of object before setting property
    /// </summary>
    public interface IPreProcessor
    {
        /// <summary>
        /// Process an object before setting property
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        object Process(object value);
    }

    internal class NullPreProcessor : IPreProcessor
    {
        public object Process(object value)
        {
            return value;
        }
    }
}