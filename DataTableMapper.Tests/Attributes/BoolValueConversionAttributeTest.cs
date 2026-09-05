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
        [TestCase(-1)]
        [TestCase(-99)]
        public void Convert_Returns_True_For_Numbers_Less_Than_Zero(int number)
        {
            _boolAttribute.Convert(number).ShouldBe(true);
        }

        [Test]
        public void Convert_Returns_Null_For_Null()
        {
            //null in -> null out, so a [DefaultValue] (or the type default) can still apply downstream
            _boolAttribute.Convert(null).ShouldBe(null);
        }

        [Test]
        public void Convert_Returns_Null_For_DBNull()
        {
            _boolAttribute.Convert(DBNull.Value).ShouldBe(null);
        }
    }
}