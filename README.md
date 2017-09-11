# AssemblyLoader
### Assembly Information & Loader via Reflection

---

* CREATED BY: Latency McLaughlin
* FRAMEWORK:  .NET 4.7
* SUPPORTS:   Visual Studio 2017, 2015, 2013, 2012, 2010, 2008
* UPDATED:    9/9/2017
* TAGS:       C# .NET AssemblyInfo AssemblyLoader API
* VERSION:    v1.1.6462

<hr>

## Navigation
* <a href="#introduction">Introduction</a>
* <a href="#history">History</a>
* <a href="#solution">Solution</a>
* <a href="#usage">Usage</a>
* <a href="#installation">Installation</a>
* <a href="#license">License</a>

<hr>

<h2><a name="introduction">Introduction</a></h2>

Gets assembly information at runtime commonly found in *Properties* within .NET assemblies.
Dynamically loads / unloads assemblies and their dependancies within a proxy for validation. 

<h2><a name=history">History</a></h2>

Trying to get information out of an assembly without loading it into the current application domain is not that simple.
There is no way to get custom assembly attributes without loading it into the current AppDomain.
There is a special assembly loading method, <i>Assembly.ReflectionOnlyLoad()</i>, which uses a "reflection-only" load context.
This lets you load assemblies that cannot be executed, but can have their metadata read.
You cannot get typed attributes from this kind of assembly, only CustomAttributeData.
That class doesn't provide any good way to filter for a specific attribute.

Even worse, a reflection-only load does not load any dependency assemblies, but it forces you to do it manually.
That makes it objectively worse than <i>Assembly.Load()</i> which at least gets the dependencies to load automatically.

MEF includes a whole ton of custom reflection code to make this work.

Ultimately, you cannot unload an assembly once it has been loaded.
You need to unload the entire app domain, as described in this [MSDN article].

<h2><a name="solution">Solution</a></h2>

Based on the following premises:

Creating an assembly proxy (or wrapper), derived from MarshalByRefObject, so that the CLR can marshal it by reference across AppDomain boundaries.
Loading the assembly within this proxy.
Performing the reflection inside this proxy and return the data you need.
Creating a temporary AppDomain and instantiating the assembly proxy in this AppDomain (AppDomain.CreateInstanceFrom).
Unloading the AppDomain as soon as you finished reflecting.

However, you have to keep in mind that reflection on the assembly loaded this way is only possible inside the proxy (the one derived from MarshalByRefObject).
It is not possible to return any "reflection object" (anything defined in the System.Reflection namespace, such as Type, MethodInfo, etc.).
Trying to access these from another AppDomain (the caller's domain) would result in exceptions.

<h2><a name="usage">Usage</a></h2>

- Types called across AppDomain boundaries must inherit MarshalByRefObject.
- Types called across AppDomain boundaries must be called via an interface.
- The property LoaderOptimization must be set to LoaderOptimization.MultiDomainHost.
- Construct a proxy manager that loads assemblies into AppDomains, performs queries, and unloads AppDomains.

<h2><a name="installation">Installation</a></h2>

This library can be installed with NuGet:  **Non-published**

Add a path to the .nupkg by going to Tools -> Options within Visual Studio.
Scroll down until you see the *Nuget Package Manager -> Package Sources* option.

A new dialog window will appear.
Click the (+) Add button to create a new entry.

Enter the details:
Name:    AssemblyInfo
Source:  <path to directory>

Click *Update* and check the option (enabled) for the new entry.

The package is now available in NuGet.
Select the reference from the combo-box *Package Source* to the new entry.

<h2><a name="license">License</a></h2>

[GNU LESSER GENERAL PUBLIC LICENSE] - Version 3, 29 June 2007


[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job.)

   [GNU LESSER GENERAL PUBLIC LICENSE]: <http://www.gnu.org/licenses/lgpl-3.0.en.html>
   [MSDN article]: <https://msdn.microsoft.com/en-us/library/c5b8a8f9(v=vs.100).aspx>
