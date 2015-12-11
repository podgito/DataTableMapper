using DataTableMapper.Attributes;
using DataTableMapper.Mapping;
using NUnit.Framework;
using System.Collections.Generic;

namespace DataTableMapper.Tests.Mapping
{
    [TestFixture]
    public class PropertyMappingFactoryTests
    {
        private PropertyMappingFactory _factory;

        [SetUp]
        public void Setup()
        {
            _factory = new PropertyMappingFactory();
        }

        [Test]
        public void IgnoreMapping_Comes_First()
        {
            var testClassType = typeof(TestClass);
            var ignoredProperty = testClassType.GetProperty("IgnoredProperty");

            var mapping = _factory.Create(ignoredProperty);
        }

        private class TestClass
        {
            public IEnumerable<string> EnumerableProperty { get; set; }

            [DefaultValue("UnitTest")]
            [ColumnMapping("Value")]
            [IgnoreMapping]
            public string IgnoredProperty { get; set; }

            public string OtherProperty { get; set; }
        }
    }
}