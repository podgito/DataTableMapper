using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataTableMapper.Tests.DataTableMapTo
{
    /// <summary>
    /// byte[] was special-cased in 1.4.0. Any array whose element type is a simple type is now
    /// treated the same way - set straight from the column value rather than skipped as a
    /// child collection.
    /// </summary>
    [TestFixture]
    public class DataTableMapToClassWithSimpleArrayProperties
    {
        [Test]
        public void CharArrayColumnMapsToCharArrayProperty()
        {
            var data = new[] { 'a', 'b', 'c' };

            var table = new DataTable();
            table.Columns.Add("Code", typeof(char[]));
            table.Rows.Add(new object[] { data });

            var result = table.MapTo<Entity>().Single();

            CollectionAssert.AreEqual(data, result.Code);
        }

        [Test]
        public void IntArrayColumnMapsToIntArrayProperty()
        {
            var data = new[] { 4, 5, 6 };

            var table = new DataTable();
            table.Columns.Add("Scores", typeof(int[]));
            table.Rows.Add(new object[] { data });

            var result = table.MapTo<Entity>().Single();

            CollectionAssert.AreEqual(data, result.Scores);
        }

        [Test]
        public void DBNullMapsToSimpleArrayPropertyAsNull()
        {
            var table = new DataTable();
            table.Columns.Add("Code", typeof(char[]));
            table.Rows.Add(new object[] { DBNull.Value });

            var result = table.MapTo<Entity>().Single();

            Assert.IsNull(result.Code);
        }

        [Test]
        public void GenuineChildCollectionIsStillSkipped()
        {
            var table = new DataTable();
            table.Columns.Add("Scores", typeof(int[]));
            table.Rows.Add(new object[] { new[] { 1, 2 } });

            var result = table.MapTo<EntityWithCollection>().Single();

            CollectionAssert.AreEqual(new[] { 1, 2 }, result.Scores);
            Assert.IsNull(result.Tags);
        }

        private class Entity
        {
            public char[] Code { get; set; }
            public int[] Scores { get; set; }
        }

        private class EntityWithCollection
        {
            public int[] Scores { get; set; }
            public List<string> Tags { get; set; }
        }
    }
}
