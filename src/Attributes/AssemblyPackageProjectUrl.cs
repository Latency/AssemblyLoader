// 1****************************************************************************
// Project:  AssemblyLoader
// File:     AssemblyPackageProjectUrl.cs
// Author:   Latency McLaughlin
// Date:     1/22/2024
// ****************************************************************************

using System.Runtime.InteropServices;

#pragma warning disable IDE0130
namespace System.Reflection;
#pragma warning restore IDE0130

/// <summary>
///     Defines a project URL custom attribute for an assembly manifest.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
[ComVisible(true)]
public sealed class AssemblyPackageProjectUrlAttribute : Attribute
{
    /// <summary>
    ///     Defines a project URL custom attribute for an assembly manifest.
    /// </summary>
    public AssemblyPackageProjectUrlAttribute(string value) => PackageProjectUrl = value;

    /// <summary>
    ///     Gets the project URL.
    /// </summary>
    /// <returns>
    ///     A string containing the project URL.
    /// </returns>
    public string PackageProjectUrl { get; }
}