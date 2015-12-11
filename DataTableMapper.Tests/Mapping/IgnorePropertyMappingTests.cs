using DataTableMapper.Attributes;
using DataTableMapper.Mapping;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Reflection;

namespace DataTableMapper.Tests.Mapping
{
    [TestFixture]
    public class IgnorePropertyMappingTests
    {
        private const string enumerablePropertyName = "EnumerableProperty";
        private const string ignoredPropertyName = "IgnoredProperty";
        private const string otherPropertyName = "OtherProperty";
        private IgnorePropertyMapping _mapping;
        private PropertyInfo _enumerableProperty;
        private PropertyInfo _ignoredProperty;
        private PropertyInfo _otherProperty;

        [SetUp]
        public void Setup()
        {
            _mapping = new IgnorePropertyMapping();
            var testClassType = typeof(TestClass);
            _enumerableProperty = testClassType.GetProperty(enumerablePropertyName);
            _ignoredProperty = testClassType.GetProperty(ignoredPropertyName);
            _otherProperty = testClassType.GetProperty(otherPropertyName);
        }

        [Test]
        public void IsMatch_If_Property_Is_Enumerable()
        {
            _mapping.IsMatch(_enumerableProperty).ShouldBeTrue();
        }

        [Test]
        public void IsMatch_If_Property_Has_IgnoreMappingAttribute()
        {
            _mapping.IsMatch(_ignoredProperty).ShouldBeTrue();
        }

        [Test]
        public void IsMatch_False_For_Anything_Else()
        {
            _mapping.IsMatch(_otherProperty).ShouldBeFalse();
        }

        private class TestClass
        {
            public IEnumerable<string> EnumerableProperty { get; set; }

            [IgnoreMapping]
            public string IgnoredProperty { get; set; }

            public string OtherProperty { get; set; }
        }
    }
}