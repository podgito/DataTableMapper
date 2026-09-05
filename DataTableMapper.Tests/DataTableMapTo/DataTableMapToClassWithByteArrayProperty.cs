using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataTableMapper.Tests.DataTableMapTo
{
    [TestFixture]
    public class DataTableMapToClassWithByteArrayProperty
    {
        [Test]
        public void ByteArrayColumnMapsToByteArrayProperty()
        {
            var data = new byte[] { 1, 2, 3 };

            var table = new DataTable();
            table.Columns.Add("Data", typeof(byte[]));
            table.Rows.Add(new object[] { data });

            var result = table.MapTo<Entity>().Single();

            Assert.IsNotNull(result.Data);
            CollectionAssert.AreEqual(data, result.Data);
        }

        [Test]
        public void RowVersionStyleColumnMaps()
        {
            var rowVersion = new byte[] { 0, 0, 0, 0, 0, 0, 16, 42 };

            var table = new DataTable();
            table.Columns.Add("RowVersion", typeof(byte[]));
            table.Rows.Add(new object[] { rowVersion });

            var result = table.MapTo<Entity>().Single();

            CollectionAssert.AreEqual(rowVersion, result.RowVersion);
        }

        [Test]
        public void DBNullMapsToByteArrayPropertyAsNull()
        {
            var table = new DataTable();
            table.Columns.Add("Data", typeof(byte[]));
            table.Rows.Add(new object[] { DBNull.Value });

            var result = table.MapTo<Entity>().Single();

            Assert.IsNull(result.Data);
        }

        [Test]
        public void NoMatchingColumnLeavesByteArrayPropertyNull()
        {
            var table = new DataTable();
            table.Columns.Add("Unrelated", typeof(string));
            table.Rows.Add("x");

            var result = table.MapTo<Entity>().Single();

            Assert.IsNull(result.Data);
        }

        [Test]
        public void ByteArrayMapsWhileCollectionPropertyStillSkipped()
        {
            var data = new byte[] { 9, 8, 7 };

            var table = new DataTable();
            table.Columns.Add("Data", typeof(byte[]));
            table.Rows.Add(new object[] { data });

            var result = table.MapTo<EntityWithCollection>().Single();

            CollectionAssert.AreEqual(data, result.Data);
            Assert.IsNull(result.Tags);
        }

        private class Entity
        {
            public byte[] Data { get; set; }
            public byte[] RowVersion { get; set; }
        }

        private class EntityWithCollection
        {
            public byte[] Data { get; set; }
            public IEnumerable<string> Tags { get; set; }
        }
    }
}
