using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTableMapper.Attributes;
using DataTableMapper.DataTable;

namespace DataTableMapper.Tests.DataTableRM
{
    [TestFixture]
    public class DataTableMapToClassWithAttributeAliases
    {

        [Test]
        public void MapToAttributeClass()
        {
            //Arrange

            var table = new System.Data.DataTable();

            table.Columns.Add("Identity");
            table.Columns.Add("FirstName");
            table.Columns.Add("DateOfBirth");


            table.Rows.Add(1, "Padraic Duffy", "1987-09-11");

            //Act
            var xList = table.MapTo<AttributeClass>();

            var c1 = xList.First();

            //Assert
            Assert.AreEqual(1, xList.Count());

            Assert.AreEqual(1, c1.Id);
            Assert.AreEqual("Padraic Duffy", c1.Name);
            Assert.AreEqual(new DateTime(1987, 09, 11), c1.DOB);

        }

        [Test]
        public void MapToAttributeClass2()
        {
            //Arrange

            var table = new System.Data.DataTable();

            table.Columns.Add("Id");
            table.Columns.Add("Name");
            table.Columns.Add("DateOfBirth");


            table.Rows.Add(1, "Padraic Duffy", "1987-09-11");

            //Act
            var xList = table.MapTo<AttributeClass>();

            var c1 = xList.First();

            //Assert
            Assert.AreEqual(1, xList.Count());

            Assert.AreEqual(1, c1.Id);
            Assert.AreEqual("Padraic Duffy", c1.Name);
            Assert.AreEqual(new DateTime(1987, 09, 11), c1.DOB);

        }

        [Test]
        public void MapToAttributeClass3()
        {
            //Arrange

            var table = new System.Data.DataTable();

            table.Columns.Add("ClassID");
            table.Columns.Add("Name");
            table.Columns.Add("DateOfBirth");


            table.Rows.Add(1, "Padraic Duffy", "1987-09-11");

            //Act
            var xList = table.MapTo<AttributeClass>();

            var c1 = xList.First();

            //Assert
            Assert.AreEqual(1, xList.Count());

            Assert.AreEqual(1, c1.Id);
            Assert.AreEqual("Padraic Duffy", c1.Name);
            Assert.AreEqual(new DateTime(1987, 09, 11), c1.DOB);

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
