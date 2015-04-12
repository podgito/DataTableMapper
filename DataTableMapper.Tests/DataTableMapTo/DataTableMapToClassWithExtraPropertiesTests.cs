using NUnit.Framework;
using System;
using System.Linq;

namespace DataTableMapper.Tests.DataTableMapTo
{
    [TestFixture]
    public class DataTableMapToClassWithExtraPropertiesTests
    {

        [Test]
        public void MapToMyTestClass1()
        {
            //Arrange

            var table = CreateTable();
            table.Rows.Add(1, "Padraic Duffy", "1910-01-03");

            //Act
            var xList = table.MapTo<MyTestClass>();

            var c1 = xList.First();

            //Assert
            Assert.AreEqual(1, xList.Count());

            Assert.AreEqual(1, c1.Id);
            Assert.AreEqual("Padraic Duffy", c1.Name);
            Assert.AreEqual(0, c1.Height);
            Assert.AreEqual(new DateTime(1910, 01, 03), c1.DOB);

        }

        [Test]
        public void MapToTestClassWithSomeReadOnlyProperties()
        {
            //Arrange

            var table = new System.Data.DataTable();

            table.Columns.Add("Id");
            table.Columns.Add("FirstName");
            table.Columns.Add("LastName");
            table.Columns.Add("DOB");

            table.Rows.Add(1, "Padraic", "Duffy", "1910-01-03");

            //Act
            var xList = table.MapTo<TestUserWithName>();

            var u = xList.First();

            //Assert
            Assert.AreEqual(1, xList.Count());

            Assert.AreEqual("Padraic Duffy", u.Name);
        }




        private System.Data.DataTable CreateTable()
        {
            var table = new System.Data.DataTable();

            table.Columns.Add("Id");
            table.Columns.Add("Name");
            table.Columns.Add("DOB");




            return table;
        }
    }

    public class TestUserWithName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get { return FirstName + " " + LastName; } }
    }
}
