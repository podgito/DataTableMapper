using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataTableMapper.Tests.MapToDynamic
{
    [TestFixture]
    public class MapToDynamicObjectTest
    {

        [Test]
        public void PropertyNamesMatchColumnNames()
        {
            var table = new DataTable();
            table.Columns.Add("FirstName");
            table.Columns.Add("LastName");

            table.Rows.Add("John", "Smith");

            //Act
            //dynamic person = table.MapTo().First();

            //Assert
            //Assert.AreEqual("John", (string)person.FirstName);

        }

        [Test]
        public void PropertyHasValueCorrespondingToColumnType()
        {

        }

    }
}
