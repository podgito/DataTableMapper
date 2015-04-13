using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataTableMapper;
using DataTableMapper.Attributes;

namespace DataTableMapper.Tests.Performance
{
    [TestFixture]
    public class MapToPerformanceTests
    {

        [Test]
        public void SinglePropertyTest_MatchingByPropertyName()
        {
            //Arrange
            var table = new DataTable();
            table.Columns.Add("IntegerValue");
            table.Rows.Add(1);

            //Act
            table.MapTo<SinglePropertyClass>();

        }

        [Test]
        public void SinglePropertyTest_MatchingByPropertyName_1000Rows()
        {
            //Arrange
            var table = new DataTable(); //100 Rows
            table.Columns.Add("IntegerValue");


            for (int i = 0; i < 1000; i++)
                table.Rows.Add(i);


            //Act
            table.MapTo<SinglePropertyClass>();

        }

        [Test]
        public void SinglePropertyTest_MatchingByPropertyAlias()
        {
            //Arrange
            var table = new DataTable();
            table.Columns.Add("IntegerAlias");
            table.Rows.Add(1);

            //Act
            table.MapTo<SinglePropertyClass>();

        }

        [Test]
        public void SinglePropertyTest_MatchingByPropertyAlias_1000Rows()
        {
            //Arrange
            var table = new DataTable();
            table.Columns.Add("IntegerAlias");
            for (int i = 0; i < 1000; i++)
                table.Rows.Add(i);

            //Act
            table.MapTo<SinglePropertyClass>();

        }

        class SinglePropertyClass
        {
            [PropertyMapping("IntegerAlias")]
            public int IntegerValue { get; set; }
        }

    }

}
