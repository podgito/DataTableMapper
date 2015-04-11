# DataTableMapper
Map a DataTable to a class instance via MapTo&lt;T>() extension method. The tables columns will automatically map to the class' properties by name. The property names can be overriden with aliases via Attributes.

# Usage

The extension method interface is as follows

	public static IEnumerable<T> MapTo<T>(this System.Data.DataTable table) where T : new()

Add using to your class file

	using DataTableMapper.DataTable;

and call the extension method

	IEnumberable<MyClass> x = table.MapTo<MyClass>();
	
	
#Property Mapping

Use the PropertyMappingAttribute to override property name mapping to alias mapping. The MapTo function will still fall back to property name mapping if aliases fail to find a match.

	public class MyClass
	{
		[PropertyMapping("Id")]
		public int MyClassId { get; set; }
	}	
		
	
