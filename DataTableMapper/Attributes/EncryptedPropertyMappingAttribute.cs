
namespace DataTableMapper.Attributes
{
    internal class EncryptedPropertyMappingAttribute : PropertyMappingAttribute
    {

        public EncryptedPropertyMappingAttribute() : base(new string[0]) { }

        public EncryptedPropertyMappingAttribute(params string[] aliases) : base(aliases) { }

    }
}
