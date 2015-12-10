using DataTableMapper.Mapping;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;

namespace DataTableMapper.Tests.Mapping
{
    [TestFixture]
    public class TypeHelperTests
    {
        [Test]
        [TestCase(0, true)]
        [TestCase(1, true)]
        [TestCase(2.99f, true)]
        [TestCase(2.99d, true)]
        [TestCase("hello", true)]
        public void IsSimpleTypeTests(object obj, bool expectedIsSimpleType)
        {
            //Act
            var isSimpleType = TypeHelper.IsSimpleType(obj.GetType());

            //Assert
            Assert.AreEqual(expectedIsSimpleType, isSimpleType);
        }

        [Test]
        [TestCase(typeof(IEnumerable<string>))]
        [TestCase(typeof(List<int>))]
        [TestCase(typeof(double[]))]
        public void EnumerableTypesTest(Type type)
        {
            TypeHelper.IsEnumerable(type).ShouldBeTrue();
        }

        [Test]
        [TestCase(typeof(string))]
        [TestCase(typeof(int))]
        public void NonEnumerableTypesTest(Type type)
        {
            TypeHelper.IsEnumerable(type).ShouldBeFalse();
        }

        [Test]
        [TestCase(typeof(DateTime?))]
        [TestCase(typeof(int?))]
        public void IsNullableType(Type type)
        {
            TypeHelper.IsNullable(type).ShouldBeTrue();
        }

        [Test]
        [TestCase(typeof(int))]
        [TestCase(typeof(DateTime))]
        public void NonNullableTypes(Type type)
        {
            TypeHelper.IsNullable(type).ShouldBeFalse();
        }
    }
}