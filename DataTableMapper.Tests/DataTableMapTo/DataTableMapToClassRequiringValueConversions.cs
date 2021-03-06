﻿using DataTableMapper.Attributes;
using NUnit.Framework;
using System.Linq;

namespace DataTableMapper.Tests.DataTableMapTo
{
    [TestFixture]
    public class DataTableMapToClassRequiringValueConversions
    {
        [Test]
        [TestCase(true, true)]
        [TestCase("true", true)]
        [TestCase("1", true)]
        [TestCase(1, true)]
        [TestCase(12312, true)]
        [TestCase(false, false)]
        [TestCase("asdfasdf", false)]
        [TestCase("false", false)]
        [TestCase(null, false)]
        [TestCase(0, false)]
        public void BoolValueConversionTest1(object val, bool expectedOutcome)
        {
            var table = new System.Data.DataTable();

            table.Columns.Add("Val");
            table.Rows.Add(val);

            //Act
            var c = table.MapTo<TestClass1>().First();

            //Assert
            Assert.AreEqual(expectedOutcome, c.Val);
        }

        [Test]
        [TestCase("true", true)]
        [TestCase("1", true)]
        [TestCase(1, true)]
        [TestCase(12312, true)]
        [TestCase("asdfasdf", false)]
        [TestCase("false", false)]
        [TestCase(null, false)]
        [TestCase(0, false)]
        public void BoolValueConversionTestWithAlias(object val, bool expectedOutcome)
        {
            var table = new System.Data.DataTable();

            table.Columns.Add("SomeNonBool");
            table.Rows.Add(val);

            //Act
            var c = table.MapTo<TestClass1>().First();

            //Assert
            Assert.AreEqual(expectedOutcome, c.Val);
        }

        private class TestClass1
        {
            [BoolValueConversion("SomeNonBool")]
            public bool Val { get; set; }
        }

        [Test]
        [TestCase("true", true)]
        [TestCase("1", true)]
        [TestCase(1, true)]
        [TestCase(12312, true)]
        [TestCase("asdfasdf", false)]
        [TestCase("false", false)]
        [TestCase(null, false)]
        [TestCase(0, false)]
        public void ConversionOfChildInstance(object val, bool expectedOutcome)
        {
            var table = new System.Data.DataTable();

            table.Columns.Add("SomeNonBool");
            table.Rows.Add(val);

            //Act
            var c = table.MapTo<TestClass1>().First();

            //Assert
            Assert.AreEqual(expectedOutcome, c.Val);
        }

        private class TestClass2
        {
            public TestClass1 P1 { get; set; }
        }
    }
}