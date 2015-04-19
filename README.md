# DataTableMapper
Map a DataTable to a class instance via `MapTo&lt;T>()` extension method. The tables columns will automatically map to the class' properties by name. The property names can be overriden with aliases via Attributes.

#Nuget

	Install-Package DataTableMapper

# Usage

The extension method interface is as follows

	public static IEnumerable<T> MapTo<T>(this System.Data.DataTable table) where T : new()


and call the extension method

	IEnumberable<MyClass> x = table.MapTo<MyClass>();
	
	
#Column Mapping

By default the MapTo function will attempt to map a property to the table's column with the same name.

Decorate properties with the ColumnMappingAttribute to map a property to a column with another name. The MapTo function will still fall back to property-name mapping if no match is found. e.g. The MapTo function will look for a column named "Id" to set the following property named "MyClassId"

	public class MyClass
	{
		[ColumnMapping("Id")]
		public int MyClassId { get; set; }
	}	
		
#Default Values

Decorate a property with the DefaultValueAttribute to assign a value to the property in the case where no mapping can be done OR the mapping yields a DBNull.	

