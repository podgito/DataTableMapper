using DataTableMapper.Attributes;
using NUnit.Framework;
using Shouldly;
using System;

namespace DataTableMapper.Tests.Attributes
{
    [TestFixture]
    public class BoolValueConversionAttributeTest
    {
        private BoolValueConversionAttribute _boolAttribute;

        [SetUp]
        public void Setup()
        {
            _boolAttribute = new BoolValueConversionAttribute();
        }

        [Test]
        public void Convert_Returns_False_For_Number_Equal_To_Zero()
        {
            _boolAttribute.Convert(0).ShouldBe(false);
        }

        [Test]
        [TestCase(1)]
        [TestCase(100)]
        public void Convert_Returns_True_For_Numbers_Greater_Than_Zero(int number)
        {
            _boolAttribute.Convert(number).ShouldBe(true);
        }

        [Test]
        [Ignore("What should be the result of this test?")]
        [TestCase(-1)]
        [TestCase(-99)]
        public void Convert_Returns_True_For_Numbers_Less_Than_Zero(int number)
        {
            _boolAttribute.Convert(number).ShouldBe(true);
        }

        [Test]
        public void Convert_Returns_False_For_Null()
        {
            _boolAttribute.Convert(null).ShouldBe(false);
        }

        [Test]
        public void Convert_Returns_False_For_DBNull()
        {
            _boolAttribute.Convert(DBNull.Value).ShouldBe(false);
        }
    }
}