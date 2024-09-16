// 1****************************************************************************
// Project:  AssemblyLoader
// File:     AssemblyAuthors.cs
// Author:   Latency McLaughlin
// Date:     1/22/2024
// ****************************************************************************

using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace System.Reflection;

/// <summary>
///     Defines an authors name custom attribute for an assembly manifest.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
[ComVisible(true)]
public sealed class AssemblyAuthorsAttribute : Attribute
{
    /// <summary>
    ///     Defines an authors name custom attribute for an assembly manifest.
    /// </summary>
    public AssemblyAuthorsAttribute(string authors)
    {
        Authors = authors;
    }

    /// <summary>
    ///     Gets authors name information.
    /// </summary>
    /// <returns>
    ///     A string containing the authors name.
    /// </returns>
    public string Authors { get; set; }
}