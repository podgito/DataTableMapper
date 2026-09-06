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

`MapTo` is a lazy iterator — mapping happens as you enumerate, so a conversion error surfaces
when you read the offending row, not when you call `MapTo`. `T` must have a public parameterless
constructor. Read-only properties are skipped.

### Mapping a single column

There is also an overload that projects one column to a sequence of scalars:

```csharp
public static IEnumerable<T> MapTo<T>(this System.Data.DataTable table, string columnName)
    where T : IComparable
```

```csharp
IEnumerable<int> ids = table.MapTo<int>("Id");   // DBNull becomes default(T)
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

`ColumnMappingAttribute` takes one or more aliases (`[ColumnMapping("Id", "user_id")]`); the first
column that exists and is non-null wins. It can be inherited for custom functionality — this is
how `BoolValueConversionAttribute` and any `IValueConversion` attribute also carry a column name.

`PropertyMappingAttribute` is a legacy synonym for `ColumnMappingAttribute` with the same
constructor. Prefer `ColumnMapping` in new code. (Its `virtual Convert` method is `[Obsolete]`
and never called by the mapper — implement `IValueConversion` for conversions.)

### Ignoring a property

Decorate a property with `IgnoreMappingAttribute` to skip it entirely:

```csharp
public class MyClass
{
    [IgnoreMapping]
    public string Computed { get; set; }
}
```

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
with the library) converts a column returning an integer to a boolean with C-like rules — zero is
`false`, any non-zero value is `true`:

```csharp
class MyClass
{
    // Looks for a column named "val"; if the row value is non-zero the property is set to true, else false
    [BoolValueConversion]
    public bool Val { get; set; }
}
```

## Extensibility

For a custom conversion (e.g. decryption, splitting a delimited column, parsing JSON), write an
attribute that implements `IValueConversion` **and** inherits `ColumnMappingAttribute`:

```csharp
public interface IValueConversion
{
    object Convert(object o);   // o is the raw column value; return the property value, or null
}
```

```csharp
public sealed class CsvToListAttribute : ColumnMappingAttribute, IValueConversion
{
    public CsvToListAttribute(params string[] aliases) : base(aliases) { }

    public object Convert(object o)
    {
        if (o == null || o == DBNull.Value) return null;               // let [DefaultValue] apply
        return ((string)o).Split(',').Select(s => s.Trim()).ToList();
    }
}

public class Order
{
    [CsvToList("Tags")]
    public List<string> Tags { get; set; }
}
```

Returning `null` from `Convert` leaves the door open for a `[DefaultValue]` (or the type default)
to apply, exactly as with the built-in `BoolValueConversion`. A converter works on both
simple-typed and complex-typed properties; on a complex-typed property it fully replaces the
recursive column-to-sub-property mapping.

## Supported property types

A property is mapped directly from its column ("simple") when its type is one of:

- any primitive (`bool`, `int`, `long`, `double`, `char`, …), `decimal`, `string`
- `DateTime`, `Guid`, `TimeSpan`, `DateTimeOffset`
- `byte[]`, `char[]`, or any array of the above
- `Nullable<T>` of any of the above
- any `enum` (and `Nullable<enum>`)

String-to-number and string-to-date parsing uses **invariant culture** — the data is
database-sourced, so it must not depend on the host locale. Enum strings parse
case-insensitively.

Any other class or struct type is treated as "complex": the mapper recurses and fills its
writable properties from the **same row** (a flat/denormalised model, not nested tables).

## Limitations

- `IEnumerable` / `List<T>` / arrays of complex types are treated as child collections and left
  unset — there is no row-grouping model.
- Reference cycles in the object graph are broken by leaving the re-entered property unset.
- Read-only properties, and properties without a public setter, are skipped.
- Mapping uses reflection per row with no caching.
- Cycle detection uses a `[ThreadStatic]` set, so a single `DataTable` must not be mapped
  concurrently across threads on the same `MapTo` enumeration.

## Building

Requires the [.NET SDK 10](https://dotnet.microsoft.com/download) (see `global.json`).

```
dotnet build -c Release
dotnet test  -c Release
dotnet pack  DataTableMapper/DataTableMapper.csproj -c Release -o artifacts
```

## Releasing

1. Bump `<VersionPrefix>` (and `AssemblyVersion`/`FileVersion`) in `Directory.Build.props`, the
   `<PackageReleaseNotes>` in the csproj, and add a `CHANGELOG.md` entry.
2. PR to `master`; once merged, tag: `git tag vX.Y.Z && git push --follow-tags`.
3. The `Release` workflow verifies the tag matches the project version, packs, and pushes to
   NuGet.org via [Trusted Publishing](https://learn.microsoft.com/nuget/nuget-org/trusted-publishing)
   (OIDC, no stored API key). It needs a `nuget-release` GitHub environment and a matching
   Trusted Publishing policy on nuget.org (see `.github/workflows/release.yml`).

## Changelog

See [CHANGELOG.md](CHANGELOG.md).

## Licence

[MIT](LICENSE.md)
