using NUnit.Framework;
using System.Data;
using System.Linq;

namespace DataTableMapper.Tests.DataTableMapTo
{
    [TestFixture]
    public class DataTableMapToCyclicGraphTests
    {
        [Test]
        public void SelfReferencingTypeDoesNotRecurseForever()
        {
            var table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Rows.Add("root");

            var node = table.MapTo<Node>().Single();

            Assert.AreEqual("root", node.Name);
            Assert.IsNull(node.Next);
        }

        [Test]
        public void MutuallyReferencingTypesDoNotRecurseForever()
        {
            var table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Rows.Add("a");

            var a = table.MapTo<A>().Single();

            Assert.AreEqual("a", a.Name);
            Assert.IsNotNull(a.B);
            Assert.IsNull(a.B.A);
        }

        private class Node
        {
            public string Name { get; set; }
            public Node Next { get; set; }
        }

        private class A
        {
            public string Name { get; set; }
            public B B { get; set; }
        }

        private class B
        {
            public A A { get; set; }
        }
    }
}
