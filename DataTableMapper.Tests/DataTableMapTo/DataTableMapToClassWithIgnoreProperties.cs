using DataTableMapper.Attributes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataTableMapper.Tests.DataTableMapTo
{
    [TestFixture]
    public class DataTableMapToClassWithIgnoreProperties
    {

        [Test]
        public void IgnorePrimitive()
        {
            //Arrange
            var table = new DataTable();
            table.Columns.Add("Name");
            table.Columns.Add("Age");

            var name = "John";
            var age = 99;

            table.Rows.Add(name, age);

            //Act
            var x = table.MapTo<SimplePersonIgnoreAge>().First();

            //Assert
            Assert.AreEqual(name, x.Name);
            Assert.AreEqual(0, x.Age);
        }

        [Test]
        public void IgnoreClass()
        {
            //Arrange
            var table = new DataTable();
            table.Columns.Add("Title");
            table.Columns.Add("Name");
            table.Columns.Add("Age");

            var title = "Manager";
            var name = "John";
            var age = 99;

            table.Rows.Add(title, name, age);

            //Act
            var x = table.MapTo<SimpleJobIgnorePerson>().First();

            //Assert
            Assert.AreEqual(title, x.JobTitle);
            Assert.IsNull(x.Person);


        }

        class SimplePersonIgnoreAge
        {
            public string Name { get; set; }

            [IgnoreMapping]
            public int Age { get; set; }
        }


        class SimpleJobIgnorePerson
        {
            [ColumnMapping("Title")]
            public string JobTitle { get; set; }

            [IgnoreMapping]
            public SimplePersonIgnoreAge Person { get; set; }
        }

    }
}
