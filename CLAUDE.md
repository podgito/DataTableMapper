# DataTableMapper

Small MIT library (published on NuGet since 2015) that maps `System.Data.DataTable` rows onto
POCOs via a `MapTo<T>()` extension method. Columns match properties by name; attributes override
the column name, supply defaults, and convert values.

## Commands

```
dotnet build -c Release                                            # both TFMs
dotnet test  -c Release                                            # NUnit, ~126 tests, 0 skipped
dotnet pack  DataTableMapper/DataTableMapper.csproj -c Release -o artifacts
```

Requires the .NET SDK pinned in `global.json` (10.x). The .NET Framework is not required — the
library multi-targets and CI builds on Ubuntu and Windows.

## Hard constraints — do not change without asking

- **Target frameworks: `netstandard2.0;net10.0`.** The `netstandard2.0` leg keeps .NET Framework
  4.6.1+ consumers working and is why `System.Data.DataSetExtensions` is a conditional
  `PackageReference` (in-box from .NET Core 3.0+, only the ns2.0 leg needs it).
- **NUnit stays on 3.14.** NUnit 4 removed the classic `Assert.AreEqual`/`Assert.IsNull` model
  the suite uses ~90 times. Dependabot is pinned (`.github/dependabot.yml`) to hold it at 3.x and
  `NUnit3TestAdapter` below 5.
- **`TreatWarningsAsErrors` is ON; `Nullable` is OFF.** Both set in `Directory.Build.props`.
  Supply-chain warnings (`NU19xx`) and the tests' one `CS0618` are demoted to warnings on purpose.
- **File encoding is UTF-8 without BOM** (`.editorconfig` `charset = utf-8`). Many legacy files
  still carry a BOM; new files should not.
- Shared MSBuild settings, package identity and version live in `Directory.Build.props`, not the
  csproj. There is no `AssemblyInfo.cs` — `InternalsVisibleTo` is an MSBuild item in the csproj.

## Architecture

`DataTable/DataTableExtensions.cs` — `MapTo<T>()` is a lazy iterator; `Map<T>(DataRow)` does
`new T()`, walks writable properties, and for each asks `PropertyMappingFactory` for a strategy.

Two strategy chains, both `internal`, order significant:

1. **`IPropertyMapping`** (`Mapping/`) — `IgnorePropertyMapping` → `SimpleTypePropertyMapping` →
   `ComplexTypePropertyMapping`. First match wins; the chain is total.
2. **`ITypeConverter`** (`TypeConversion/TypeConverterRegistry.cs`) — `Enum` → `Nullable`
   (unwraps + re-dispatches) → `NonConvertible` (Guid/TimeSpan/DateTimeOffset) → `Base`
   (`Convert.ChangeType` with `InvariantCulture`).

`TypeHelper.IsSimpleType` decides which `IPropertyMapping` handles a property, so adding a type
there is how you make it map as a scalar rather than recurse.

Cycle safety: `Map<T>` records the types on the current recursion path in a `[ThreadStatic]`
set; `ComplexTypePropertyMapping` leaves a re-entered property unset.

The only public extension point is `IValueConversion` (implement it *and* inherit
`ColumnMappingAttribute`). `PropertyMappingAttribute.Convert` is `[Obsolete]` — the engine never
calls it.

## Release

1. Bump `<VersionPrefix>` (and `AssemblyVersion`/`FileVersion`) in `Directory.Build.props` and
   `<PackageReleaseNotes>` in the csproj.
2. PR to `master` (CI is a required check: `build (ubuntu-latest)`, `build (windows-latest)`, `pack`).
3. After merge, tag `vX.Y.Z` and push the tag. `.github/workflows/release.yml` verifies the tag
   matches the project version, then packs and publishes to NuGet.org via **Trusted Publishing**
   (OIDC — no stored API key; `nuget-release` environment + a matching nuget.org policy).

Note: nuget.org has an **unlisted `2.0.0`** from a withdrawn earlier attempt. It can never be
re-published; a future 2.x line must start at `2.0.1`. Version resolution ignores it, so `1.x`
releases are what consumers get.

## Known gaps (not yet fixed — each its own change)

- `byte[]` properties are silently dropped (`IgnorePropertyMapping` matches any `IEnumerable`).
- Custom `struct` properties lose writes (`Map<T>` operates on a boxed copy).
- `IValueConversion` is ignored on complex-typed properties.
- `Enum.Parse` is case-sensitive (`"Horror"` works, `"horror"` throws).
