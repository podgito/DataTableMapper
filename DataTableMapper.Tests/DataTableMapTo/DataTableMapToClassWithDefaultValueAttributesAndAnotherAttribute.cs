﻿using DataTableMapper.Attributes;
using NUnit.Framework;
using System;
using System.Data;
using System.Linq;

namespace DataTableMapper.Tests.DataTableMapTo
{
    [TestFixture]
    public class DataTableMapToClassWithDefaultValueAttributesAndAnotherAttribute
    {
        [Test]
        public void DefaultValueIsSetAfterAllOtherMappingsOrValueConversions()
        {
            //Arrange
            var table = new DataTable();
            table.Columns.Add("ImageUrl");
            table.Rows.Add(DBNull.Value);

            //Act
            var person = table.MapTo<TestPerson>().Single();

            //Assert
            Assert.AreEqual("", person.ImageUrl);
        }
    }

    public class TestPerson
    {
        [DefaultValue("")]
        [NullConversionMapping]
        public string ImageUrl { get; set; }
    }

    public class NullConversionMapping : ColumnMappingAttribute, IValueConversion
    {
        public object Convert(object o)
        {
            return null;
        }
    }
}