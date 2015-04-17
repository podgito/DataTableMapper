using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataTableMapper.Extensions;

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
            var table = new DataTable();
            table.Columns.Add(columnName);

            table.Rows.Add(DBNull.Value);

            var row = table.AsEnumerable().First();

            //Act
            var value = row.TryReadColumn(columnName);

            //Assert
            Assert.IsNull(value);

        }

    }
}
