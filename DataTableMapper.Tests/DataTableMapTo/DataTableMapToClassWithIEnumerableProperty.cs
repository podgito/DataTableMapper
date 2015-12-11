using NUnit.Framework;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataTableMapper.Tests.DataTableMapTo
{
    [TestFixture]
    public class DataTableMapToClassWithIEnumerableProperty
    {
        private class Person
        {
            public string Name { get; set; }
            public IEnumerable<string> Attributes { get; set; }
        }

        [Test]
        public void AttributesListPropertyShouldBeExcludedFromAnyMapping()
        {
            var table = CreateTable();
            var name = "John";
            table.Rows.Add(name);

            var person = table.MapTo<Person>().Single();

            //Assert
            Assert.AreEqual(name, person.Name);
            Assert.IsNull(person.Attributes);
        }

        private DataTable CreateTable()
        {
            var table = new DataTable();

            table.Columns.Add("Name");

            return table;
        }
    }
}