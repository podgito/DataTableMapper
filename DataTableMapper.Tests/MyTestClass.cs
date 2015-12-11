using DataTableMapper.Attributes;
using System;

namespace DataTableMapper.Tests
{
    public class MyTestClass
    {
        [PropertyMapping("MyTestClassID", "MTCID")]
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public double Height { get; set; }
    }
}