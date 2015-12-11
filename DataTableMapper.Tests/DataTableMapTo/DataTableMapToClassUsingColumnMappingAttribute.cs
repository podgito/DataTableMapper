using DataTableMapper.Attributes;
using NUnit.Framework;
using System.Data;
using System.Linq;

namespace DataTableMapper.Tests.DataTableMapTo
{
    [TestFixture]
    public class DataTableMapToClassUsingColumnMappingAttribute
    {
        [Test]
        public void PropertyNameMappingTest()
        {
            //Arrange
            var table = new DataTable();
            table.Columns.Add("Name");
            table.Columns.Add("Age");

            var name = "John";
            var age = 99;

            table.Rows.Add(name, age);

            //Act
            var x = table.MapTo<ColumnMappingTestClass>().First(); ;

            //Assert
            Assert.AreEqual(name, x.Name);
            Assert.AreEqual(age, x.Age);
        }

        [Test]
        public void ColumnNameMappingTest()
        {
            //Arrange
            var table = new DataTable();
            table.Columns.Add("MyName");
            table.Columns.Add("MyAge");

            var name = "John";
            var age = 99;

            table.Rows.Add(name, age);

            //Act
            var x = table.MapTo<ColumnMappingTestClass>().First(); ;

            //Assert
            Assert.AreEqual(name, x.Name);
            Assert.AreEqual(age, x.Age);
        }

        [Test]
        public void ColumnNameMappingTestToSecondaryMapping()
        {
            //Arrange
            var table = new DataTable();
            table.Columns.Add("ThisName");
            table.Columns.Add("ThisAge");

            var name = "John";
            var age = 99;

            table.Rows.Add(name, age);

            //Act
            var x = table.MapTo<ColumnMappingTestClass2>().First(); ;

            //Assert
            Assert.AreEqual(name, x.Name);
            Assert.AreEqual(age, x.Age);
        }

        private class ColumnMappingTestClass
        {
            [ColumnMapping("MyName")]
            public string Name { get; set; }

            [ColumnMapping("MyAge")]
            public int Age { get; set; }
        }

        private class ColumnMappingTestClass2
        {
            [ColumnMapping("MyName", "ThisName")]
            public string Name { get; set; }

            [ColumnMapping("MyAge", "ThisAge")]
            public int Age { get; set; }
        }
    }
}