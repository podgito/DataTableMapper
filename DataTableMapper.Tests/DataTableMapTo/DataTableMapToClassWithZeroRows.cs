using DataTableMapper.Attributes;
using NUnit.Framework;
using System.Data;
using System.Linq;

namespace DataTableMapper.Tests.DataTableMapTo
{
    [TestFixture]
    public class DataTableMapToClassWithZeroRows
    {
        [Test]
        public void ReturnsEmptyEnumerableIfThereAreNoRows()
        {
            var table = new DataTable();

            table.Columns.Add("Id");
            table.Columns.Add("Name");

            //Act
            var list = table.MapTo<SimpleClass>();

            //Assert
            Assert.AreEqual(0, list.Count());
        }

        private class SimpleClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [Test]
        public void ReturnsEmptyEnumerableIfThereAreNoRows2()
        {
            var table = new DataTable();

            table.Columns.Add("ClassId");
            table.Columns.Add("Name");

            //Act
            var list = table.MapTo<SimpleClass2>();

            //Assert
            Assert.AreEqual(0, list.Count());
        }

        private class SimpleClass2
        {
            [ColumnMapping("ClassId")]
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}