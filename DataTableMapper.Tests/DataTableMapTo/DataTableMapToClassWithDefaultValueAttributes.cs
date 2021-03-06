﻿using DataTableMapper.Attributes;
using NUnit.Framework;
using System;
using System.Data;
using System.Linq;

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
        public void PropertiesSetToDefaultValuesWhenThereAreNoMatchingColumns()
        {
            var table = new DataTable();

            table.Columns.Add("asgdsfsa");
            table.Columns.Add("sadfasdf");
            table.Columns.Add("asdfasdhhfgwe");

            table.Rows.Add(DBNull.Value, DBNull.Value, DBNull.Value);

            //Act
            var person = table.MapTo<Person>().First();

            Assert.AreEqual(99, person.Id);
            Assert.AreEqual("Johnny", person.Name);
            Assert.AreEqual(true, person.IsGreat);
        }

        //[Test]
        //public void PropertiesSetToDefaultValuesUsingColumnMappings()
        //{
        //}

        private class Person
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