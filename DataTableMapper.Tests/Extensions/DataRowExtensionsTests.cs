using DataTableMapper.Extensions;
using NUnit.Framework;
using Shouldly;
using System;
using System.Data;
using System.Linq;

namespace DataTableMapper.Tests.Extensions
{
    [TestFixture]
    public class DataRowExtensionsTests
    {
        [Test]
        public void ReturnNullForDBNull()
        {
            //Arrange

            var columnName = "Col1";
            DataTable table = CreateTable(columnName, DBNull.Value);

            var row = table.AsEnumerable().First();

            //Act
            var value = row.TryReadColumn(columnName);

            //Assert
            value.ShouldBeNull();
        }

        [Test]
        public void ReturnsNullWhenColumnDoesntExist()
        {
            //Arrange

            var columnName = "Col1";
            DataTable table = CreateTable(columnName, DBNull.Value);

            var row = table.AsEnumerable().First();

            //Act
            var value = row.TryReadColumn("sadfasdf");

            //Assert
            value.ShouldBeNull();
        }

        [Test]
        [TestCase("Unit test")]
        [TestCase(true)]
        [TestCase(123)]
        [TestCase(0.9)]
        public void ReturnsValueWhenColumnDoesExist(object tableValue)
        {
            //Arrange
            var columnName = "Column1";
            DataTable table = CreateTableTyped(columnName, tableValue);

            var row = table.AsEnumerable().First();

            //Act
            var returnedValue = row.TryReadColumn(columnName);

            //Assert
            returnedValue.ShouldBe(tableValue);
        }

        private static DataTable CreateTable(string columnName, object value)
        {
            var table = new DataTable();
            table.Columns.Add(columnName);

            table.Rows.Add(value);
            return table;
        }

        private static DataTable CreateTableTyped(string columnName, object value)
        {
            var table = new DataTable();
            table.Columns.Add(columnName, value.GetType());

            table.Rows.Add(value);
            return table;
        }
    }
}