using NUnit.Framework;

namespace DataTableMapper.Tests.MapToDynamic
{
    [TestFixture]
    public class MapToDynamicObjectTest
    {
        //[Test]
        //public void PropertyNamesMatchColumnNames()
        //{
        //    var table = new DataTable();
        //    table.Columns.Add("FirstName");
        //    table.Columns.Add("LastName");

        //    table.Rows.Add("John", "Smith");

        //    //Act
        //    var person = table.MapTo<dynamic>().First();

        //    //Assert
        //    Assert.AreEqual("John", (string)person.FirstName);

        //}

        [Test]
        public void PropertyHasValueCorrespondingToColumnType()
        {
        }
    }
}