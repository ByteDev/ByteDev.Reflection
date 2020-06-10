# Release Notes

## 2.0.0 - 10 June 2020

Breaking changes:
- Removed ObjectExtensions.HasAttribute (use TypeExtensions.HasAttribute)
- ObjectExtensions.SetValue is now SetPropertyValue
- ObjectExtensions.SetReadOnlyProperty is now SetPropertyReadOnlyValue
- ObjectExtensions.SetPropertyValue now tries to handle type conversion to property type if required

New features:
- Added ObjectExtensions.GetPropertiesAsDictionary
- Added ObjectExtensions.SetPropertyValue overload for PropertyInfo
- Added TypeExtensions.IsNullable

Bug fixes:
- (None)

## 1.1.0 - 27 May 2020

Breaking changes:
- (None)

New features:
- Added TypeExtensions.GetPropertiesWithAttribute
- Added TypeExtensions.GetEnumProperties
- Added TypeExtensions.GetPropertiesOfType
- Added ObjectConstruction.ConstructNonPublic

Bug fixes:
- (None)

## 1.0.0 - 07 May 2020

Initial version.
