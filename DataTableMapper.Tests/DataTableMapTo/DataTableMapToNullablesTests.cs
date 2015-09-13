using NUnit.Framework;
using System.Linq;
using System;
using DataTableMapper;
using DataTableMapper.Attributes;

namespace DataTableMapper.Tests.DataTableMapTo
{
    [TestFixture]
    public class DataTableMapToNullablesTests
    {
        //TODO Add support for nullable types!

        [Test]
        public void ReadAllNullRowToNullableTypes()
        {
            var table = CreateTable();
            table.Rows.Add(DBNull.Value, DBNull.Value);


            //Act
            var list = table.MapTo<NullableTypesClass>();
            var x = list.First();
            //Assert
            Assert.AreEqual(1, list.Count());


            Assert.AreEqual(null, x.NullableInt);
        }


        [Test]
        public void ReadValuesFromRowToNullableTypes()
        {
            var table = CreateTable();
            table.Rows.Add(123, 1);


            //Act
            var list = table.MapTo<NullableTypesClass>();
            var x = list.First();
            //Assert
            Assert.AreEqual(1, list.Count());
            Assert.AreEqual(123, x.NullableInt);
            Assert.AreEqual(true, x.NullableBool);
        }

        [Test]
        public void NullableDateTimeTest_IsNull()
        {
            //Arrange
            var table = new System.Data.DataTable();
            table.Columns.Add("Date", typeof(DateTime));
            table.Rows.Add(DBNull.Value);

            //Act
            var x = table.MapTo<NullableDateTimeClass>().Single();

            //Assert
            Assert.IsNull(x.Date);

        }

        [Test]
        public void NullableDateTimeTest_HasValue()
        {
            //Arrange
            var nowDate = DateTime.Now;
            var table = new System.Data.DataTable();
            table.Columns.Add("Date", typeof(DateTime));
            table.Rows.Add(nowDate);

            //Act
            var x = table.MapTo<NullableDateTimeClass>().Single();

            //Assert
            Assert.AreEqual(nowDate, x.Date);
        }


        public class NullableTypesClass
        {
            [ColumnMapping("Int")]
            public int? NullableInt { get; set; }

            [ColumnMapping("Bool")]
            public bool? NullableBool { get; set; }
        }

        public class NullableDateTimeClass
        {
            public DateTime? Date { get; set; }
        }

        private System.Data.DataTable CreateTable()
        {
            var table = new System.Data.DataTable();

            table.Columns.Add("Int", typeof(int));
            table.Columns.Add("Bool", typeof(bool));

            return table;
        }
    }
}
