using NUnit.Framework;
using System;
using System.Data;
using System.Linq;

namespace DataTableMapper.Tests.DataTableMapTo
{
    [TestFixture]
    public class DataTableMapToClassWithTimeSpanAndOffset
    {
        [Test]
        public void TimeSpanColumnMapsToTimeSpanProperty()
        {
            var span = TimeSpan.FromMinutes(90);

            var table = new DataTable();
            table.Columns.Add("Duration", typeof(TimeSpan));
            table.Rows.Add(span);

            var result = table.MapTo<Entity>().Single();

            Assert.AreEqual(span, result.Duration);
        }

        [Test]
        public void StringColumnMapsToTimeSpanProperty()
        {
            var table = new DataTable();
            table.Columns.Add("Duration", typeof(string));
            table.Rows.Add("01:30:00");

            var result = table.MapTo<Entity>().Single();

            Assert.AreEqual(TimeSpan.FromMinutes(90), result.Duration);
        }

        [Test]
        public void DateTimeOffsetColumnMapsToDateTimeOffsetProperty()
        {
            var when = new DateTimeOffset(2026, 9, 5, 13, 0, 0, TimeSpan.FromHours(1));

            var table = new DataTable();
            table.Columns.Add("When", typeof(DateTimeOffset));
            table.Rows.Add(when);

            var result = table.MapTo<Entity>().Single();

            Assert.AreEqual(when, result.When);
        }

        [Test]
        public void DateTimeColumnMapsToDateTimeOffsetProperty()
        {
            var when = new DateTime(2026, 9, 5, 13, 0, 0);

            var table = new DataTable();
            table.Columns.Add("When", typeof(DateTime));
            table.Rows.Add(when);

            var result = table.MapTo<Entity>().Single();

            Assert.AreEqual(new DateTimeOffset(when), result.When);
        }

        [Test]
        public void StringColumnMapsToDateTimeOffsetProperty()
        {
            var table = new DataTable();
            table.Columns.Add("When", typeof(string));
            table.Rows.Add("2026-09-05T13:00:00+01:00");

            var result = table.MapTo<Entity>().Single();

            Assert.AreEqual(new DateTimeOffset(2026, 9, 5, 13, 0, 0, TimeSpan.FromHours(1)), result.When);
        }

        [Test]
        public void DBNullMapsToNullableTimeSpanAsNull()
        {
            var table = new DataTable();
            table.Columns.Add("Duration", typeof(TimeSpan));
            table.Rows.Add(DBNull.Value);

            var result = table.MapTo<NullableEntity>().Single();

            Assert.IsNull(result.Duration);
        }

        [Test]
        public void TimeSpanColumnMapsToNullableTimeSpan()
        {
            var span = TimeSpan.FromMinutes(90);

            var table = new DataTable();
            table.Columns.Add("Duration", typeof(TimeSpan));
            table.Rows.Add(span);

            var result = table.MapTo<NullableEntity>().Single();

            Assert.AreEqual(span, result.Duration);
        }

        private class Entity
        {
            public TimeSpan Duration { get; set; }
            public DateTimeOffset When { get; set; }
        }

        private class NullableEntity
        {
            public TimeSpan? Duration { get; set; }
        }
    }
}
