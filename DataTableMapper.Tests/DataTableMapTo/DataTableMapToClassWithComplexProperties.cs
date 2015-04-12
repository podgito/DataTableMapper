using NUnit.Framework;
using System;
using System.Linq;
using DataTableMapper.Attributes;

namespace DataTableMapper.Tests.DataTableMapTo
{
    [TestFixture]
    public class DataTableMapToClassWithComplexProperties
    {

        [Test]
        public void MapToClassWithComplexObject1()
        {
            var table = new System.Data.DataTable();
             
            var eventId = 123;
            var userId = 456;

            table.Columns.Add("EventId");
            table.Columns.Add("UserId");
            table.Columns.Add("FirstName");
            table.Columns.Add("Surname");
            table.Columns.Add("DateOfBirth");

            table.Rows.Add(eventId, userId, "Padraic", "Duffy", "1910-08-01");

            //Act
            var xList = table.MapTo<TestEvent>();

            var e = xList.First();

            //Assert
            Assert.AreEqual(1, xList.Count());

            Assert.AreEqual(eventId, e.Id);
            Assert.AreEqual(userId, e.User.UserId);
            Assert.AreEqual("Padraic", e.User.Name.FirstName);
            Assert.AreEqual("Duffy", e.User.Name.LastName);
            Assert.AreEqual(new DateTime(1910, 08, 01), e.User.DOB);
        }

    }

    public class TestEvent
    {
        [PropertyMapping("EventId")]
        public int Id { get; set; }
        public string Description { get; set; }
        public TestUser User { get; set; }

    }

    public class TestUser
    {
        public int UserId { get; set; }
        public TestName Name { get; set; }

        [PropertyMapping("DateOfBirth")]
        public DateTime DOB { get; set; }

    }

    public class TestName
    {
        public string FirstName { get; set; }

        [PropertyMapping("Surname")]
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
    }
}
