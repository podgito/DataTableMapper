# Changelog

All notable changes to this project are documented here. The format is based on
[Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and the project follows
[Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.5.0] - 2026-09-06

Closes the four remaining "known gaps" — all cases where mapping silently produced a wrong
value with no exception.

### Fixed
- **Custom `struct` properties are now populated.** `Map<T>` operated on a boxed copy, so every
  reflective write landed on a throwaway box and the property came back as `default`. Nested
  structs and top-level `MapTo<SomeStruct>()` both work now.
- **`Nullable<TStruct>` properties map instead of throwing.** A `MyStruct?` property was routed
  to `Convert.ChangeType` and raised `InvalidCastException`; it is now mapped as its underlying
  struct.
- **`IValueConversion` is honoured on complex-typed properties.** Previously only simple-typed
  properties consulted a converting attribute; on a class- or struct-typed property it was
  silently inert. The converter now reads its column and produces the value, bypassing the
  recursive map.
- **Enum values parse case-insensitively from strings.** `"horror"` now binds the same as
  `"Horror"`. An unparseable string still throws (on enumeration), consistent with other
  conversions.
- **`char[]` and other simple-element array columns map.** `byte[]` was special-cased in 1.4.0;
  the rule is now general — any array whose element type is a simple type is set straight from
  the column value rather than skipped as a child collection.

### Changed
- The test suite now also runs on `net8.0`, which is what exercises the `netstandard2.0` library
  asset (and its conditional `System.Data.DataSetExtensions` dependency) in CI.

## [1.4.0] - 2026-09-05

### Fixed
- `byte[]` properties now map (were silently left null) — the type ADO.NET uses for `varbinary`
  and `rowversion`/`timestamp` columns.

## [1.3.0] - 2026-09-05

### Fixed
- `Guid` / `TimeSpan` / `DateTimeOffset` properties now map (were silently left empty).
- Nullable enums map from string columns.
- String-to-number and string-to-date parsing is now invariant-culture, not host-locale.
- `[DefaultValue]` applies alongside `[BoolValueConversion]` when there is no value to convert.
- Cyclic object graphs no longer overflow the stack — a re-entered type is left unset.
- `BoolValueConversion` treats any non-zero number as true.

## [1.2.0] - 2026-09-03

### Changed
- Modernised for .NET 10. The library now multi-targets `netstandard2.0;net10.0`; packaging and
  CI run from the SDK-style project. No public API changes.

## [1.1.1] - 2015-12-11

Last release of the original 2015 line.

[1.5.0]: https://github.com/podgito/DataTableMapper/releases/tag/v1.5.0
[1.4.0]: https://github.com/podgito/DataTableMapper/releases/tag/v1.4.0
[1.3.0]: https://github.com/podgito/DataTableMapper/releases/tag/v1.3.0
[1.2.0]: https://github.com/podgito/DataTableMapper/releases/tag/v1.2.0
[1.1.1]: https://github.com/podgito/DataTableMapper/releases/tag/v1.1.1
