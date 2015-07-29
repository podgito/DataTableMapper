using DataTableMapper.Mapping;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTableMapper.Tests.Mapping
{
    [TestFixture]
    public class TypeCheckerTests
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
        public void IsEnumerableTest()
        {
            var list = new List<string>();
            var array = new string[] { "Hello!" };

            var @string = "MyString";

            Assert.IsTrue(TypeHelper.IsEnumerable(list.GetType()));
            Assert.IsTrue(TypeHelper.IsEnumerable(array.GetType()));
            Assert.IsFalse(TypeHelper.IsEnumerable(@string.GetType()));
        }

    }
}
