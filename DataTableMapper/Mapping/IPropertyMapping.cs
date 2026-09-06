namespace DataTableMapper.Mapping
{
    internal interface IPropertyMapping
    {
        bool IsMatch(System.Reflection.PropertyInfo property);

        void PerformMapping(object obj, System.Reflection.PropertyInfo property, System.Data.DataRow row);
    }
}