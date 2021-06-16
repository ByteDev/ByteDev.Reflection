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

Represents an assembly's embedded resource.

```csharp
// Retrieve an assembly's embedded resource and save it to disk

var assembly = Assembly.GetExecutingAssembly();
var embeddedFile = "EmbeddedResource1.txt";

AssemblyEmbeddedResource resource = AssemblyEmbeddedResource.CreateFromAssembly(assembly, embeddedFile);

resource.Save(Path.Combine(@"C:\Temp\", embeddedFile));
```

### ObjectConstruction

Initialize objects by calling their constructors using reflection.

```csharp
// Initialize with no params

public class PrivateCtor
{
    private PrivateCtor()
    {
    }
}

// ...

var test = ObjectConstruction.ConstructNonPublic<PrivateCtor>();
```

```csharp
// Initialize with params

public class InternalCtorWithParam
{
    public int Value { get; set; }

    internal InternalCtorWithParam(int value)
    {
        Value = value;
    }
}

// ...

var parameters = new Dictionary<Type, object>
{
    {typeof(int), 10}
};

var test = ObjectConstruction.ConstructNonPublic<InternalCtorWithParam>(parameters);

// test.Value == 10
```

---

### Extension Methods

To use any extension methods simply reference the `ByteDev.Reflection` namespace.

Assembly:
- GetAssemblyAttribute
- GetFileVersion
- GetManifestResourceName
- GetSubClasses
- GetVersion

Generic:
- InvokeMethod

MemberInfo:
- GetAttribute
- HasAttribute

Object:
- GetPropertyValue<T>
- GetPropertyValue
- GetPropertiesAsDictionary
- SetPropertyReadOnlyValue
- SetPropertyValue

Type:
- GetBaseTypes
- GetConstants
- GetConstantsValues
- GetDefault
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
