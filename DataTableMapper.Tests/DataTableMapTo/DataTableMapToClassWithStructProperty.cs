using DataTableMapper.Attributes;
using NUnit.Framework;
using System;
using System.Data;
using System.Linq;

namespace DataTableMapper.Tests.DataTableMapTo
{
    /// <summary>
    /// A user-defined struct property is not a simple type, so it is mapped by recursing into
    /// its own properties from the same row - the same model as a nested class.
    /// </summary>
    [TestFixture]
    public class DataTableMapToClassWithStructProperty
    {
        [Test]
        public void StructPropertyIsPopulatedFromTheRow()
        {
            var table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Latitude", typeof(double));
            table.Columns.Add("Longitude", typeof(double));
            table.Rows.Add("HQ", 53.349805, -6.26031);

            var result = table.MapTo<Place>().Single();

            Assert.AreEqual("HQ", result.Name);
            Assert.AreEqual(53.349805, result.Location.Latitude);
            Assert.AreEqual(-6.26031, result.Location.Longitude);
        }

        [Test]
        public void NullableStructPropertyIsPopulatedFromTheRow()
        {
            var table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Latitude", typeof(double));
            table.Columns.Add("Longitude", typeof(double));
            table.Rows.Add("HQ", 1.0, 2.0);

            var result = table.MapTo<PlaceWithOptionalLocation>().Single();

            Assert.IsTrue(result.Location.HasValue);
            Assert.AreEqual(1.0, result.Location.Value.Latitude);
            Assert.AreEqual(2.0, result.Location.Value.Longitude);
        }

        [Test]
        public void StructPropertyHonoursColumnMappingAttributesOnItsMembers()
        {
            var table = new DataTable();
            table.Columns.Add("lat", typeof(double));
            table.Columns.Add("lng", typeof(double));
            table.Rows.Add(10.0, 20.0);

            var result = table.MapTo<PlaceWithAliasedLocation>().Single();

            Assert.AreEqual(10.0, result.Location.Latitude);
            Assert.AreEqual(20.0, result.Location.Longitude);
        }

        public struct Coordinate
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

        public struct AliasedCoordinate
        {
            [ColumnMapping("lat")]
            public double Latitude { get; set; }

            [ColumnMapping("lng")]
            public double Longitude { get; set; }
        }

        public class Place
        {
            public string Name { get; set; }
            public Coordinate Location { get; set; }
        }

        public class PlaceWithOptionalLocation
        {
            public string Name { get; set; }
            public Coordinate? Location { get; set; }
        }

        public class PlaceWithAliasedLocation
        {
            public AliasedCoordinate Location { get; set; }
        }
    }
}
