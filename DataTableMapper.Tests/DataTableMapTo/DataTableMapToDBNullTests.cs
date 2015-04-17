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
    public class DataTableMapToDBNullTests
    {

        [Test]
        public void MapToDefaultWhenNoMapping()
        {
            var table = new DataTable();
            table.Columns.Add("Id");
            table.Rows.Add(1);

            //Act
            var c = table.MapTo<MyUnitTestClass>().First();

            //Assert
            Assert.AreEqual(null, c.Name);
            
        }

        [Test]
        public void MapToNullWhenColumnHasDBNullForPropertyNameMapping()
        {
            var table = new DataTable();
            table.Columns.Add("Name");
            table.Rows.Add(DBNull.Value);

            //Act
            var c = table.MapTo<MyUnitTestClass>().First();

            //Assert
            Assert.AreEqual(null, c.Name);
        }

        [Test]
        public void MapToNullWhenColumnHasDBNullForColumnMapping()
        {
            var table = new DataTable();
            table.Columns.Add("MyName");
            table.Rows.Add(DBNull.Value);

            //Act
            var c = table.MapTo<MyUnitTestClass>().First();

            //Assert
            Assert.AreEqual(null, c.Name);
        }

        class MyUnitTestClass
        {
            [ColumnMapping("MyName")]
            public string Name { get; set; }
        }

    }
}
