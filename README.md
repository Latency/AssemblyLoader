# AssemblyLoader
 
Assembly Information & Loader via Reflection
 
| | Description |
|---|---|
| ASSEMBLY NAME: | AssemblyLoader |
| CREATED BY: | [Latency McLaughlin] |
| UPDATED: | 10/16/2026 |
| FRAMEWORKS: | net45, net451, net452, net46, net461, net462, net47, net471, net472, net48, net481, netstandard2.0, netstandard2.1, net8.0, net9.0, net10.0, net11.0 |
| LANGUAGE: | [C#] preview |
| OUTPUT TYPE: | [Library] [API] |
| SUPPORTS: | [Visual Studio] |
| GFX SUBSYS: | [None] |
| TAGS: | [AssemblyLoader], [C#], [AssemblyInfo] |
| STATUS: | [![workflow-badge](https://github.com/Latency/AssemblyLoader/actions/workflows/status.yml/badge.svg)](https://github.com/Latency/AssemblyLoader/actions/workflows/status.yml) |
| LICENSE: | [![license-badge](https://img.shields.io/github/license/Latency/AssemblyLoader?logo=GitHub&style=plastic&logoColor=black&label=License&color=yellowgreen)](https://github.com/Latency/AssemblyLoader/blob/master/MIT-LICENSE.txt) |
| VERSION: | 1.2.8 |
 
---

## Navigation

* [Introduction](#introduction)
* [History](#history)
* [Solution](#solution)
* [Usage](#usage)
* [Installation](#installation)
* [References](#references)
* [Report An Issue](#report-an-issue)
* [Donation](#donation)
* [License](#license)

---

## Introduction

Gets assembly information at runtime commonly found in **Properties** within .NET assemblies.
Dynamically loads / unloads assemblies and their dependencies within a proxy for validation.

## History

Trying to get information out of an assembly without loading it into the current application domain is not that simple.
There is no way to get custom assembly attributes without loading it into the current AppDomain.
There is a special assembly loading method, `Assembly.ReflectionOnlyLoad()`, which uses a "reflection-only" load context.
This lets you load assemblies that cannot be executed, but can have their metadata read.
You cannot get typed attributes from this kind of assembly, only CustomAttributeData.
That class doesn't provide any good way to filter for a specific attribute.

Even worse, a reflection-only load does not load any dependency assemblies, but it forces you to do it manually.
That makes it objectively worse than `Assembly.Load()` which at least gets the dependencies to load automatically.

MEF includes a whole ton of custom reflection code to make this work.

Ultimately, you cannot unload an assembly once it has been loaded.
You need to unload the entire app domain, as described in this [MSDN article].

## Solution

Based on the following premises:

Creating an assembly proxy (or wrapper), derived from MarshalByRefObject, so that the CLR can marshal it by reference across AppDomain boundaries.
Loading the assembly within this proxy.
Performing the reflection inside this proxy and return the data you need.
Creating a temporary AppDomain and instantiating the assembly proxy in this AppDomain (AppDomain.CreateInstanceFrom).
Unloading the AppDomain as soon as you finished reflecting.

However, you have to keep in mind that reflection on the assembly loaded this way is only possible inside the proxy (the one derived from `MarshalByRefObject`).
It is not possible to return any reflected object (e.g. anything defined in the `System.Reflection` namespace, such as `Type`, `MethodInfo`, etc.).
Trying to access these from another AppDomain (the caller's domain) would result in exceptions.

## Usage

• Types called across AppDomain boundaries must inherit `MarshalByRefObject`.
• Types called across AppDomain boundaries must be called via an interface.
• The property `LoaderOptimization` must be set to `LoaderOptimization.MultiDomainHost`.
• Construct a proxy manager that loads assemblies into AppDomains, performs queries, and unloads AppDomains.
 
## Installation

This library can be installed from [NuGet].

## References

[AssemblyLoader], [AssemblyInfo]

## Report An Issue

If you have a patch to contribute, a feature to request, or a bug to report, please post to the [Issue Tracker](https://github.com/Latency/AssemblyLoader/issues).

## Donation

Click the button to donate to my efforts:
[![Donate via Paypal](https://www.paypal.com/en_US/i/btn/btn_donateCC_LG.gif)](https://paypal.me/Latency2?country.x=US&locale.x=en_US)

## License

The source code for the site is licensed under the MIT license, which you can find in the [MIT-LICENSE].txt file.

---

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job.)

   [NuGet]: <https://github.com/Latency/AssemblyLoader/pkgs/nuget/AssemblyLoader/>
   [.NET]: <https://en.wikipedia.org/wiki/.NET_Framework/>
   [Visual Studio]: <https://en.wikipedia.org/wiki/Microsoft_Visual_Studio/>
   [Library]: <https://en.wikipedia.org/wiki/Software_library/>
   [Latency McLaughlin]: <https://www.linkedin.com/in/Latency/>
   [API]: <https://en.wikipedia.org/wiki/Application_programming_interface>
   [C#]: <https://en.wikipedia.org/wiki/C_Sharp_(programming_language)>
   [DLL]: <https://en.wikipedia.org/wiki/Dynamic-link_library>
   [MIT-License]: <http://choosealicense.com/licenses/mit/>
   [WPF]: <https://en.wikipedia.org/wiki/Windows_Presentation_Foundation/>
   [WinForms]: <https://en.wikipedia.org/wiki/Windows_Forms/>
   [Unit Tests]: <https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-csharp-with-xunit>
   [AssemblyLoader]: <https://github.com/Latency/AssemblyLoader>
   [AssemblyInfo]: <https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualbasic.applicationservices.assemblyinfo>
