# Release Notes

## 2.3.1 - 16 June 2021

Breaking changes:
- (None)

New features:
- (None)

Bug fixes / internal changes:
- `ObjectExtensions.SetPropertyValue` now handles enum type values as either name or number.

## 2.3.0 - 04 June 2021

Breaking changes:
- (None)

New features:
- Added `TypeExtensions.GetDefault`.

Bug fixes / internal changes:
- (None)

## 2.2.0 - 25 September 2020

Breaking changes:
- (None)

New features:
- Added `AssemblyExtensions.GetManifestResourceName` method.
- Added `AssemblyEmbeddedResource` type.

Bug fixes / internal changes:
- (None)

## 2.1.0 - 18 August 2020

Breaking changes:
- (None)

New features:
- Added `TypeExtensions.GetPropertiesWithAttribute` overload for `BindingFlags`.

Bug fixes:
- Package build changes.

## 2.0.0 - 10 June 2020

Breaking changes:
- Removed `ObjectExtensions.HasAttribute`  (use `TypeExtensions.HasAttribute` instead).
- Renamed `ObjectExtensions.SetValue` to `SetPropertyValue`.
- Renamed `ObjectExtensions.SetReadOnlyProperty` to `SetPropertyReadOnlyValue`.
- `ObjectExtensions.SetPropertyValue` now tries to handle type conversion to property type if required.

New features:
- Added `ObjectExtensions.GetPropertiesAsDictionary` method.
- Added `ObjectExtensions.SetPropertyValue` overload for `PropertyInfo`.
- Added `TypeExtensions.IsNullable` method.

Bug fixes:
- (None)

## 1.1.0 - 27 May 2020

Breaking changes:
- (None)

New features:
- Added `TypeExtensions.GetPropertiesWithAttribute` method.
- Added `TypeExtensions.GetEnumProperties` method.
- Added `TypeExtensions.GetPropertiesOfType` method.
- Added `ObjectConstruction.ConstructNonPublic` method.

Bug fixes:
- (None)

## 1.0.0 - 07 May 2020

Initial version.
