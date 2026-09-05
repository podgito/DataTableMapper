using NUnit.Framework;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace DataTableMapper.Tests.DataTableMapTo
{
    [TestFixture]
    public class DataTableMapToCultureTests
    {
        private CultureInfo _originalCulture;

        [SetUp]
        public void Setup()
        {
            _originalCulture = Thread.CurrentThread.CurrentCulture;
            //de-DE uses ',' as the decimal separator - a machine-locale bug would parse "1.5" as 15.
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
        }

        [TearDown]
        public void TearDown()
        {
            Thread.CurrentThread.CurrentCulture = _originalCulture;
        }

        [Test]
        public void StringToDoubleParsesInvariantRegardlessOfCurrentCulture()
        {
            var table = new DataTable();
            table.Columns.Add("Value", typeof(string));
            table.Rows.Add("1.5");

            var result = table.MapTo<Entity>().Single();

            Assert.AreEqual(1.5d, result.Value);
        }

        [Test]
        public void StringToNullableDoubleParsesInvariantRegardlessOfCurrentCulture()
        {
            var table = new DataTable();
            table.Columns.Add("NullableValue", typeof(string));
            table.Rows.Add("1.5");

            var result = table.MapTo<Entity>().Single();

            Assert.AreEqual(1.5d, result.NullableValue);
        }

        private class Entity
        {
            public double Value { get; set; }
            public double? NullableValue { get; set; }
        }
    }
}
