// 1****************************************************************************
// Project:  AssemblyLoader
// File:     AssemblyDefineConstants.cs
// Author:   Latency McLaughlin
// Date:     1/22/2024
// ****************************************************************************

using System.Runtime.InteropServices;

#pragma warning disable IDE0130
namespace System.Reflection;
#pragma warning restore IDE0130

/// <summary>
///     Defines a compiler flags custom attribute for an assembly manifest.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
[ComVisible(true)]
public sealed class AssemblyDefineConstantsAttribute : Attribute
{
    /// <summary>
    ///     Defines a compiler flags custom attribute for an assembly manifest.
    /// </summary>
    public AssemblyDefineConstantsAttribute(string defineConstants) => DefineConstants = defineConstants;

    /// <summary>
    ///     Gets the define constants from the build process.
    /// </summary>
    /// <returns>
    ///     A string containing the define constants used during compilation.
    /// </returns>
    public string DefineConstants { get; }
}
