using NUnit.Framework;
using System;
using System.Data;
using System.Linq;

namespace DataTableMapper.Tests.DataTableMapTo
{
    [TestFixture]
    public class DataTableMapToClassWithGuidProperty
    {
        [Test]
        public void GuidColumnMapsToGuidProperty()
        {
            var id = Guid.NewGuid();

            var table = new DataTable();
            table.Columns.Add("Id", typeof(Guid));
            table.Rows.Add(id);

            var result = table.MapTo<Entity>().Single();

            Assert.AreEqual(id, result.Id);
            Assert.AreNotEqual(Guid.Empty, result.Id);
        }

        [Test]
        public void StringColumnMapsToGuidProperty()
        {
            var id = Guid.NewGuid();

            var table = new DataTable();
            table.Columns.Add("Id", typeof(string));
            table.Rows.Add(id.ToString());

            var result = table.MapTo<Entity>().Single();

            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public void GuidColumnMapsToNullableGuidProperty()
        {
            var id = Guid.NewGuid();

            var table = new DataTable();
            table.Columns.Add("Id", typeof(Guid));
            table.Rows.Add(id);

            var result = table.MapTo<NullableEntity>().Single();

            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public void DBNullMapsToNullableGuidPropertyAsNull()
        {
            var table = new DataTable();
            table.Columns.Add("Id", typeof(Guid));
            table.Rows.Add(DBNull.Value);

            var result = table.MapTo<NullableEntity>().Single();

            Assert.IsNull(result.Id);
        }

        private class Entity
        {
            public Guid Id { get; set; }
        }

        private class NullableEntity
        {
            public Guid? Id { get; set; }
        }
    }
}
