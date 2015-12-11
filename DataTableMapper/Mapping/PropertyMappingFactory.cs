using System.Collections.Generic;
using System.Linq;

namespace DataTableMapper.Mapping
{
    internal class PropertyMappingFactory
    {
        //Order is important!
        private static IEnumerable<IPropertyMapping> _propertyMappings =
            new List<IPropertyMapping> {
                new IgnorePropertyMapping(),
                new SimpleTypePropertyMapping(),
                new ComplexTypePropertyMapping() };

        public IPropertyMapping Create(System.Reflection.PropertyInfo property)
        {
            return _propertyMappings.First(m => m.IsMatch(property));
        }
    }
}