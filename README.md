# DataTableMapper [![NuGet Version](https://img.shields.io/badge/nuget-v1.1.1-green.svg)](https://www.nuget.org/packages/DataTableMapper/)

Map a DataTable to a class instance via `MapTo<T>()` extension method. The table's columns will automatically map to the class' properties by name. There are some Attributes available for mapping to a column with a different name and for default values as well as converting one value to another before setting the property. 

#Nuget

	Install-Package DataTableMapper

# Usage

The extension method interface is as follows

	public static IEnumerable<T> MapTo<T>(this System.Data.DataTable table) where T : new()


and call the extension method

	IEnumerable<MyClass> x = table.MapTo<MyClass>();
	
#Mapping

The `MapTo` method attempts to find a value for each property of the class in the following steps in order:

1. ColumnMappingAttributes - Map the property to a column with another name (NOT case sensitive)
2. Property Name - Search for columns with the property's name (NOT case sensitive)
3. DefaultValueAttributes - Provide default value in the case where both steps above were able to get a value for the property

##Column Mapping

By default the `MapTo` function will attempt to map a property to the table's column with the same name.

Decorate properties with the `ColumnMappingAttribute` to map a property to a column with another name. The `MapTo` function will still fall back to property-name mapping if no match is found. e.g. The `MapTo` function will look for a column named "Id" to set the following property named "MyClassId"

	public class MyClass
	{
		[ColumnMapping("Id")]
		public int MyClassId { get; set; }
	}	
	
The `ColumnMappingAttribute` class can be inherited for custom functionality.
		
##Default Values

Decorate a property with the `DefaultValueAttribute` to assign a value to the property in the case where no mapping can be done OR the mapping yields a `DBNull`.	

		class Person
        {
            [DefaultValue(99)]
            public int Id { get; set; }

            [DefaultValue("Johnny")]
            public string Name { get; set; }

            [DefaultValue(true)]
            public bool IsGreat { get; set; }
        }

#Conversion

For columns returning a different type to the property type. The `BoolValueConversionAttribute` comes with the library for converting columns returning `Integer` and converts it to a `Boolean` with C-like rules.

		class MyClass
        {
            //Looks for column named "val" and if the value for that row is > 0 then the property will be set to true, else false
            [BoolValueConversion]
            public bool Val { get; set; } 
        }

#Extensibility

For your own custom conversion (e.g. Decryption) create an Attribute implementing the `IValueConversion` interface. 

N.B. Also inherit the `ColumnMappingAttribute` class to have column mapping with the attribute.


