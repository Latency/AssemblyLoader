// 1****************************************************************************
// Project:  AssemblyLoader
// File:     AssemblySupportEmail.cs
// Author:   Latency McLaughlin
// Date:     1/22/2024
// ****************************************************************************

using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace System.Reflection
{
    /// <summary>
    ///     Defines a support email address custom attribute for an assembly manifest.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    [ComVisible(true)]
    public class AssemblySupportEmailAttribute : Attribute
    {
        /// <summary>
        ///     Defines a support email address custom attribute for an assembly manifest.
        /// </summary>
        public AssemblySupportEmailAttribute(string value)
        {
            SupportEmail = value;
        }

        /// <summary>
        ///     Gets the support email address.
        /// </summary>
        /// <returns>
        ///     A string containing the support email.
        /// </returns>
        public string SupportEmail { get; set; }
    }
}