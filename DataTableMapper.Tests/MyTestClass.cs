using System;
using DataTableMapper.Attributes;

namespace DataTableMapper.Tests
{
    public class MyTestClass
    {


        [PropertyMapping("MyTestClassID", "MTCID")]
        public int Id { get; set; }

        //[EncryptedPropertyMapping]
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public double Height { get; set; }

    }
}
