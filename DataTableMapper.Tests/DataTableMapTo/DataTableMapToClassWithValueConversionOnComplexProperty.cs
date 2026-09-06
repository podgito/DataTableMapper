using DataTableMapper.Attributes;
using NUnit.Framework;
using System;
using System.Data;
using System.Globalization;
using System.Linq;

namespace DataTableMapper.Tests.DataTableMapTo
{
    /// <summary>
    /// A consumer-written <see cref="IValueConversion"/> attribute on a complex-typed property
    /// takes over from the recursive map: it reads its own column and produces the value.
    /// </summary>
    [TestFixture]
    public class DataTableMapToClassWithValueConversionOnComplexProperty
    {
        [Test]
        public void ConverterBuildsTheComplexValueFromItsColumn()
        {
            var table = new DataTable();
            table.Columns.Add("Location", typeof(string));
            table.Rows.Add("53.5,-6.2");

            var result = table.MapTo<Place>().Single();

            Assert.IsNotNull(result.Point);
            Assert.AreEqual(53.5, result.Point.Lat);
            Assert.AreEqual(-6.2, result.Point.Lng);
        }

        [Test]
        public void ConverterReturningNullLeavesTheComplexPropertyUnset()
        {
            var table = new DataTable();
            table.Columns.Add("Location", typeof(string));
            table.Rows.Add(DBNull.Value);

            var result = table.MapTo<Place>().Single();

            Assert.IsNull(result.Point);
        }

        [Test]
        public void ConverterOnNullableStructPropertyIsHonoured()
        {
            var table = new DataTable();
            table.Columns.Add("Location", typeof(string));
            table.Rows.Add("1,2");

            var result = table.MapTo<PlaceWithOptionalPoint>().Single();

            Assert.IsTrue(result.Point.HasValue);
            Assert.AreEqual(1.0, result.Point.Value.Lat);
            Assert.AreEqual(2.0, result.Point.Value.Lng);
        }

        private static (double lat, double lng) Parse(object o)
        {
            var parts = o.ToString().Split(',');
            return (double.Parse(parts[0], CultureInfo.InvariantCulture),
                    double.Parse(parts[1], CultureInfo.InvariantCulture));
        }

        private sealed class LatLngToGeoPointAttribute : ColumnMappingAttribute, IValueConversion
        {
            public LatLngToGeoPointAttribute(params string[] aliases) : base(aliases) { }

            public object Convert(object o)
            {
                if (o == null || o == DBNull.Value) return null;
                var (lat, lng) = Parse(o);
                return new GeoPoint { Lat = lat, Lng = lng };
            }
        }

        private sealed class LatLngToCoordinateAttribute : ColumnMappingAttribute, IValueConversion
        {
            public LatLngToCoordinateAttribute(params string[] aliases) : base(aliases) { }

            public object Convert(object o)
            {
                if (o == null || o == DBNull.Value) return null;
                var (lat, lng) = Parse(o);
                return new Coordinate { Lat = lat, Lng = lng };
            }
        }

        public class GeoPoint
        {
            public double Lat { get; set; }
            public double Lng { get; set; }
        }

        public struct Coordinate
        {
            public double Lat { get; set; }
            public double Lng { get; set; }
        }

        private class Place
        {
            [LatLngToGeoPoint("Location")]
            public GeoPoint Point { get; set; }
        }

        private class PlaceWithOptionalPoint
        {
            [LatLngToCoordinate("Location")]
            public Coordinate? Point { get; set; }
        }
    }
}
