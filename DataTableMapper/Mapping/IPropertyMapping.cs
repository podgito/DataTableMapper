namespace DataTableMapper.Mapping
{
    interface IPropertyMapping
    {
        bool IsMatch(System.Reflection.PropertyInfo property);
        void PerformMapping<T>(T obj, System.Reflection.PropertyInfo property, System.Data.DataRow row);
    }
}