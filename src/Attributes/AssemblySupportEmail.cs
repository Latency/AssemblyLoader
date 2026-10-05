// 1****************************************************************************
// Project:  AssemblyLoader
// File:     AssemblySupportEmail.cs
// Author:   Latency McLaughlin
// Date:     1/22/2024
// ****************************************************************************

using System.Runtime.InteropServices;

#pragma warning disable IDE0130
namespace System.Reflection;
#pragma warning restore IDE0130

/// <summary>
///     Defines a support email address custom attribute for an assembly manifest.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
[ComVisible(true)]
public sealed class AssemblySupportEmailAttribute : Attribute
{
    /// <summary>
    ///     Defines a support email address custom attribute for an assembly manifest.
    /// </summary>
    public AssemblySupportEmailAttribute(string value) => SupportEmail = value;

    /// <summary>
    ///     Gets the support email address.
    /// </summary>
    /// <returns>
    ///     A string containing the support email.
    /// </returns>
    public string SupportEmail { get; }
}