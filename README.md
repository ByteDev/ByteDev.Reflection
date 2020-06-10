[![Build status](https://ci.appveyor.com/api/projects/status/github/bytedev/ByteDev.Reflection?branch=master&svg=true)](https://ci.appveyor.com/project/bytedev/ByteDev-Reflection/branch/master)
[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.Reflection.svg)](https://www.nuget.org/packages/ByteDev.Reflection)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ByteDev/ByteDev.Reflection/blob/master/LICENSE)

# ByteDev.Reflection

Reflection related extension methods.

## Installation

ByteDev.Reflection has been written as a .NET Standard 2.0 library, so you can consume it from a .NET Core or .NET Framework 4.6.1 (or greater) application.

ByteDev.Reflection is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.Reflection`

Further details can be found on the [nuget page](https://www.nuget.org/packages/ByteDev.Reflection/).

## Release Notes

Releases follow semantic versioning.

Full details of the release notes can be viewed on [GitHub](https://github.com/ByteDev/ByteDev.Reflection/blob/master/docs/RELEASE-NOTES.md).

## Code

The repo can be cloned from git bash:

`git clone https://github.com/ByteDev/ByteDev.Reflection`

## Usage

To use any extension methods or type simply reference the `ByteDev.Reflection` namespace.

Assembly extensions:
- GetVersion
- GetFileVersion
- GetSubClasses
- GetAssemblyAttribute

Object extensions:
- GetPropertyValue<T>
- GetPropertyValue
- GetPropertiesAsDictionary
- SetPropertyReadOnlyValue
- SetPropertyValue

Type extensions:
- HasAttribute
- GetPropertyOrThrow
- GetPropertiesWithAttribute
- GetPropertiesOfType
- GetEnumProperties
- GetStaticPropertyOrThrow
- GetStaticPropertyValue
- GetConstants
- GetConstantsValues
- GetBaseTypes
- GetImplementedInterfaces
- IsInNamespace
- IsNullable
- IsTestClass

MemberInfo extensions:
- HasAttribute
- GetAttribute

Generic extensions:
- InvokeMethod

ObjectConstruction
- ConstructNonPublic