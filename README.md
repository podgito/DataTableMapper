# DataTableMapper

[![CI](https://github.com/podgito/DataTableMapper/actions/workflows/ci.yml/badge.svg)](https://github.com/podgito/DataTableMapper/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/DataTableMapper.svg)](https://www.nuget.org/packages/DataTableMapper/)

Map a `DataTable` to a class instance via the `MapTo<T>()` extension method. The table's columns
automatically map to the class' properties by name. Attributes are available for mapping to a
column with a different name, for default values, and for converting one value to another before
setting the property.

Targets `netstandard2.0` (.NET Framework 4.6.1+, .NET Core 2.0+) and `net10.0`.

## Install

```
dotnet add package DataTableMapper
```

## Usage

The extension method signature is:

```csharp
public static IEnumerable<T> MapTo<T>(this System.Data.DataTable table) where T : new()
```

Call it:

```csharp
IEnumerable<MyClass> x = table.MapTo<MyClass>();
```

## Mapping

`MapTo` attempts to find a value for each property of the class in the following order:

1. **ColumnMappingAttribute** — map the property to a column with another name (not case sensitive)
2. **Property name** — search for a column with the property's name (not case sensitive)
3. **DefaultValueAttribute** — set a default value when steps 1 and 2 could not produce one

### Column mapping

By default `MapTo` maps a property to the table's column with the same name.

Decorate a property with `ColumnMappingAttribute` to map it to a column with another name. `MapTo`
still falls back to property-name mapping if no match is found. For example, `MapTo` will look for
a column named `Id` for the property below:

```csharp
public class MyClass
{
    [ColumnMapping("Id")]
    public int MyClassId { get; set; }
}
```

`ColumnMappingAttribute` can be inherited for custom functionality.

### Default values

Decorate a property with `DefaultValueAttribute` to assign a value when no mapping can be done, or
when the mapping yields a `DBNull`:

```csharp
class Person
{
    [DefaultValue(99)]
    public int Id { get; set; }

    [DefaultValue("Johnny")]
    public string Name { get; set; }

    [DefaultValue(true)]
    public bool IsGreat { get; set; }
}
```

## Conversion

For columns returning a different type to the property type, `BoolValueConversionAttribute` (shipped
with the library) converts a column returning an integer to a boolean with C-like rules:

```csharp
class MyClass
{
    // Looks for a column named "val"; if the row value is > 0 the property is set to true, else false
    [BoolValueConversion]
    public bool Val { get; set; }
}
```

## Extensibility

For a custom conversion (e.g. decryption), create an attribute implementing the `IValueConversion`
interface.

> Also inherit `ColumnMappingAttribute` to get column mapping with the attribute.

## Building

Requires the [.NET SDK 10](https://dotnet.microsoft.com/download) (see `global.json`).

```
dotnet build -c Release
dotnet test  -c Release
dotnet pack  DataTableMapper/DataTableMapper.csproj -c Release -o artifacts
```

## Releasing

1. Bump `<VersionPrefix>` in `Directory.Build.props`.
2. Commit, then tag: `git tag v1.2.0 && git push --follow-tags`.
3. The `Release` workflow verifies the tag matches the project version, packs, and pushes to
   NuGet.org. It requires a `NUGET_API_KEY` repository secret and a `nuget` environment.

## Licence

[MIT](LICENSE.md)
