[![Build status](https://ci.appveyor.com/api/projects/status/github/bytedev/ByteDev.Reflection?branch=master&svg=true)](https://ci.appveyor.com/project/bytedev/ByteDev-Reflection/branch/master)
[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.Reflection.svg)](https://www.nuget.org/packages/ByteDev.Reflection)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ByteDev/ByteDev.Reflection/blob/master/LICENSE)

# ByteDev.Reflection

Library of reflection related functionality and extension methods.

## Installation

ByteDev.Reflection is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.Reflection`

Further details can be found on the [nuget page](https://www.nuget.org/packages/ByteDev.Reflection/).

## Release Notes

Releases follow semantic versioning.

Full details of the release notes can be viewed on [GitHub](https://github.com/ByteDev/ByteDev.Reflection/blob/master/docs/RELEASE-NOTES.md).

## Usage

### AssemblyEmbeddedResource

```csharp
// Retrieve an assembly's embedded resource and save it to disk

var assembly = Assembly.GetExecutingAssembly();
var embeddedFile = "EmbeddedResource1.txt";

AssemblyEmbeddedResource resource = AssemblyEmbeddedResource.CreateFromAssembly(assembly, embeddedFile);

resource.Save(Path.Combine(@"C:\Temp\", embeddedFile));
```

### Extension Methods

To use any extension methods or type simply reference the `ByteDev.Reflection` namespace.

Assembly extensions:
- GetAssemblyAttribute
- GetFileVersion
- GetManifestResourceName
- GetSubClasses
- GetVersion

Object extensions:
- GetPropertyValue<T>
- GetPropertyValue
- GetPropertiesAsDictionary
- SetPropertyReadOnlyValue
- SetPropertyValue

Type extensions:
- GetBaseTypes
- GetConstants
- GetConstantsValues
- GetEnumProperties
- GetPropertyOrThrow
- GetPropertiesWithAttribute
- GetPropertiesOfType
- GetImplementedInterfaces
- GetStaticPropertyOrThrow
- GetStaticPropertyValue
- HasAttribute
- IsInNamespace
- IsNullable
- IsTestClass

MemberInfo extensions:
- GetAttribute
- HasAttribute

Generic extensions:
- InvokeMethod

ObjectConstruction
- ConstructNonPublic