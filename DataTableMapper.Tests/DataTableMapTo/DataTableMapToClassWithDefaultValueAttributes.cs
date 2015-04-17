using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataTableMapper.Attributes;
using System.Data;

namespace DataTableMapper.Tests.DataTableMapTo
{
    [TestFixture]
    public class DataTableMapToClassWithDefaultValueAttributes
    {

        [Test]
        public void PropertiesValuesSetToDefaultValuesForDBNull()
        {
            var table = new DataTable();

            table.Columns.Add("Id");
            table.Columns.Add("Name");
            table.Columns.Add("IsGreat");

            table.Rows.Add(DBNull.Value, DBNull.Value, DBNull.Value);

            //Act
            var person = table.MapTo<Person>().First();

            Assert.AreEqual(99, person.Id);
            Assert.AreEqual("Johnny", person.Name);
            Assert.AreEqual(true, person.IsGreat);
        }

        [Test]
        public void PropertiesSetToDefaultValuesForMissingColumns()
        {

        }

        [Test]
        public void PropertiesSetToDefaultValuesUsingColumnMappings()
        {

        }


        class Person
        {
            [DefaultValue(99)]
            public int Id { get; set; }

            [DefaultValue("Johnny")]
            public string Name { get; set; }

             [DefaultValue(true)]
            public bool IsGreat { get; set; }
        }

    }
}
