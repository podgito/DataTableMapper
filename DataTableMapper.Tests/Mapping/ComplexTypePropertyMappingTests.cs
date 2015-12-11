using DataTableMapper.Mapping;
using NUnit.Framework;
using Shouldly;
using System;
using System.Linq;

namespace DataTableMapper.Tests.Mapping
{
    [TestFixture]
    public class ComplexTypePropertyMappingTests
    {
        private ComplexTypePropertyMapping _mapping;

        [SetUp]
        public void Setup()
        {
            _mapping = new ComplexTypePropertyMapping();
        }

        [Test]
        public void IsMatch_IsFalse_For_SimpleTypes()
        {
            var properties = typeof(ClassWithOnlySimplePropertyTypes).GetProperties();

            properties.Where(_mapping.IsMatch).ShouldBeEmpty(); //No matches
        }

        [Test]
        public void IsMatch_IsTrue_For_ComplexTypes()
        {
            var properties = typeof(ClassWithComplexPropertyTypes).GetProperties();

            properties.Where(_mapping.IsMatch).Count().ShouldBe(properties.Count()); //All matches
        }

        private class ClassWithOnlySimplePropertyTypes
        {
            public int IntegerProperty { get; set; }
            public string StringProperty { get; set; }
            public double DoubleProperty { get; set; }
            public DateTime DateTimeProperty { get; set; }
            public bool BoolProperty { get; set; }
        }

        private class ClassWithComplexPropertyTypes
        {
            public ClassWithOnlySimplePropertyTypes ComplexProp1 { get; set; }
        }
    }
}