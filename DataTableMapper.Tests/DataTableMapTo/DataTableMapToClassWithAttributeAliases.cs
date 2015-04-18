using NUnit.Framework;
using System;
using System.Linq;
using DataTableMapper.Attributes;

namespace DataTableMapper.Tests.DataTableMapTo
{
    [TestFixture]
    public class DataTableMapToClassWithAttributeAliases
    {

        [Test]
        public void MapWithColumnAttributeMapping()
        {
            //Arrange

            var table = new System.Data.DataTable();

            table.Columns.Add("Identity");
            table.Columns.Add("FirstName");
            table.Columns.Add("DateOfBirth");


            table.Rows.Add(1, "Padraic Duffy", "1900-03-04");

            //Act
            var xList = table.MapTo<AttributeClass>();

            var c1 = xList.First();

            //Assert
            Assert.AreEqual(1, xList.Count());

            Assert.AreEqual(1, c1.Id);
            Assert.AreEqual("Padraic Duffy", c1.Name);
            Assert.AreEqual(new DateTime(1900, 03, 04), c1.DOB);

        }

        [Test]
        public void MapWithColumnAttributeMapping2()
        {
            //Arrange

            var table = new System.Data.DataTable();

            table.Columns.Add("Id");
            table.Columns.Add("Name");
            table.Columns.Add("DateOfBirth");


            table.Rows.Add(1, "Padraic Duffy", "1900-03-04");

            //Act
            var xList = table.MapTo<AttributeClass>();

            var c1 = xList.First();

            //Assert
            Assert.AreEqual(1, xList.Count());

            Assert.AreEqual(1, c1.Id);
            Assert.AreEqual("Padraic Duffy", c1.Name);
            Assert.AreEqual(new DateTime(1900, 03, 04), c1.DOB);

        }

        [Test]
        public void MapWithColumnAttributeMapping3()
        {
            //Arrange

            var table = new System.Data.DataTable();

            table.Columns.Add("ClassID");
            table.Columns.Add("Name");
            table.Columns.Add("DateOfBirth");


            table.Rows.Add(1, "Padraic Duffy", "1900-03-04");

            //Act
            var xList = table.MapTo<AttributeClass>();

            var c1 = xList.First();

            //Assert
            Assert.AreEqual(1, xList.Count());

            Assert.AreEqual(1, c1.Id);
            Assert.AreEqual("Padraic Duffy", c1.Name);
            Assert.AreEqual(new DateTime(1900, 03, 04), c1.DOB);

        }
        



    }

    public class AttributeClass
    {
        [PropertyMapping("Identity", "ClassID")]
        public int Id {get;set;}

        [PropertyMapping("FirstName", "Name")]
        public string Name { get; set; }

        [PropertyMapping("DateOfBirth")]
        public DateTime DOB { get; set; }
    }
}
