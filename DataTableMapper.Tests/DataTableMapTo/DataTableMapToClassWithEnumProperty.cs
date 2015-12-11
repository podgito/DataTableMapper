using DataTableMapper.Attributes;
using NUnit.Framework;
using System.Collections;
using System.Data;
using System.Linq;

namespace DataTableMapper.Tests.DataTableMapTo
{
    [TestFixture]
    public class DataTableMapToClassWithEnumProperty
    {
        private class Person
        {
            [ColumnMapping("MyGender")]
            public Gender Gender { get; set; }
        }

        public enum Gender
        {
            Male = 1,
            Female
        }

        [Test, TestCaseSource(typeof(TestDataSource))]
        public Gender EnumByPropertyName(DataTable table)
        {
            var person = table.MapTo<Person>().Single();

            return person.Gender;
        }

        private class TestDataSource : IEnumerable
        {
            private static IEnumerable GetData()
            {
                var table1 = new DataTable();
                table1.Columns.Add("Gender", typeof(int));
                table1.Rows.Add(1);

                yield return new TestCaseData(table1).Returns(Gender.Male);

                var table2 = new DataTable();
                table2.Columns.Add("MyGender", typeof(int));
                table2.Rows.Add(2);

                yield return new TestCaseData(table2).Returns(Gender.Female);
            }

            public IEnumerator GetEnumerator()
            {
                return GetData().GetEnumerator();
            }
        }
    }
}