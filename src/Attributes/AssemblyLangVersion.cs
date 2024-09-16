// 1****************************************************************************
// Project:  AssemblyLoader
// File:     AssemblyLangVersion.cs
// Author:   Latency McLaughlin
// Date:     1/22/2024
// ****************************************************************************

using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace
namespace System.Reflection;

/// <summary>
///     Defines a language version custom attribute for an assembly manifest.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
[ComVisible(true)]
public sealed class AssemblyLangVersionAttribute : Attribute
{
    /// <summary>
    ///     Defines a language version custom attribute for an assembly manifest.
    /// </summary>
    public AssemblyLangVersionAttribute(string langVersion)
    {
        LangVersion = langVersion;
    }

    /// <summary>
    ///     Gets the language version type from the compiler.
    /// </summary>
    /// <returns>
    ///     A string containing the compilers language version.
    /// </returns>
    public string LangVersion { get; set; }
}