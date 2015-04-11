using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTableMapper.DataTable;

namespace DataTableMapper.Tests.DataTableRM
{
    [TestFixture]
    public class DataTablePrimitiveMappingsTests
    {

        [Test]
        public void MapSingleColumnTableToStrings()
        {
            var table = new System.Data.DataTable();

            table.Columns.Add("Val");
            table.Rows.Add("Val1");
            table.Rows.Add("Val2");

            //Act
            var values = table.MapTo<string>("Val");

            //Assert
            Assert.AreEqual(2, values.Count());
            Assert.AreEqual("Val1", values.First());
            Assert.AreEqual("Val2", values.Skip(1).First());



        }

        [Test]
        public void MapSingleColumnTableToIntegers()
        {
            var table = CreateTable();
            table.Rows.Add("", 1);
            table.Rows.Add("", 2);
            table.Rows.Add("", 3);

            //Act
            var ints = table.MapTo<int>("int");

            //Assert
            Assert.AreEqual(3, ints.Count());
            Assert.AreEqual(1, ints.First());
            Assert.AreEqual(3, ints.Last());
        }

        [Test]
        public void MapSingleColumnTableNullToIntegers()
        {
            var table = CreateTable();
            table.Rows.Add("");
            table.Rows.Add("");
            table.Rows.Add("");

            //Act
            var ints = table.MapTo<int>("int");

            //Assert
            Assert.AreEqual(3, ints.Count());
            Assert.AreEqual(0, ints.First());
            Assert.AreEqual(0, ints.Last());
        }

        private System.Data.DataTable CreateTable()
        {
            var table = new System.Data.DataTable();

            table.Columns.Add("string");
            table.Columns.Add("int", typeof(int));
            table.Columns.Add("decimal", typeof(decimal));
            table.Columns.Add("datetime");

            return table;

        }


    }
}
