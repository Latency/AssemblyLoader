// 1****************************************************************************
// Project:  UnitTests
// File:     Base.cs
// Author:   Latency McLaughlin
// Date:     1/22/2024
// ****************************************************************************

using System.Diagnostics;
using Xunit.Abstractions;

namespace AssemblyLoader_Unit_Tests
{
    /// <summary>
    ///     Primary Constructor
    /// </summary>
    /// <param name="console"></param>
    public abstract class Base(ITestOutputHelper console)
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected readonly ITestOutputHelper Console = console;
    }
}