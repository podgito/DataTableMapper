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
        public void ManualControlTest()
        {
            //Arrange
            var table = new DataTable();
            table.Columns.Add("IntegerValue");


            for (int i = 0; i < 1000; i++)
                table.Rows.Add(i);

            //Act
            ControlTest(table, "IntegerValue");
        }

        [Test]
        public void ManualControlTest2()
        {
            //Arrange
            var table = new DataTable();
            table.Columns.Add("IntegerAlias");


            for (int i = 0; i < 1000; i++)
                table.Rows.Add(i);

            //Act
            ControlTest(table, "IntegerAlias");
        }

        [Test]
        public void SinglePropertyTest_MatchingByPropertyName()
        {
            //Arrange
            var table = new DataTable();
            table.Columns.Add("IntegerValue");
            table.Rows.Add(1);

            //Act
            Act(table);

        }

        [Test]
        public void SinglePropertyTest_MatchingByPropertyName_1000Rows()
        {
            //Arrange
            var table = new DataTable();
            table.Columns.Add("IntegerValue");


            for (int i = 0; i < 1000; i++)
                table.Rows.Add(i);


            //Act
            Act(table);

        }

        [Test]
        public void SinglePropertyTest_MatchingByPropertyAlias()
        {
            //Arrange
            var table = new DataTable();
            table.Columns.Add("IntegerAlias");
            table.Rows.Add(1);

            //Act
            Act(table);

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
            Act(table);

        }


        private void Act(DataTable table)
        {
            var c1 = table.MapTo<SinglePropertyClass>().First(); ;
        }

        private IEnumerable<SinglePropertyClass> ControlTest(DataTable table, string columnName)
        {
            foreach(var row in table.AsEnumerable())
            {
                var c = new SinglePropertyClass();

                c.IntegerValue = Convert.ToInt32(row[columnName].ToString());

                yield return c;
            }

        }

        class SinglePropertyClass
        {
            [PropertyMapping("IntegerAlias")]
            public int IntegerValue { get; set; }
        }

    }

}
