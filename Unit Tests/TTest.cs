// ****************************************************************************
// Project:  UnitTests
// File:     TTests.cs
// Author:   Latency McLaughlin
// Date:     1/22/2024
// ****************************************************************************
// ReSharper disable InconsistentNaming

using System.Reflection;
using AssemblyLoader;

namespace Unit_Tests;

public partial class Tests
{
    [Fact]
    public void TTest()
    {
        var asm = Assembly.GetExecutingAssembly();

        Console.WriteLine($"ProductTitle:  {asm.Title()}");
        Console.WriteLine($"AssemblyVersion:  {asm.Version()}");
        Console.WriteLine($"Authors:  {asm.Authors()}");
        Console.WriteLine($"Description:  {asm.Description()}");
        Console.WriteLine($"Product:  {asm.Product()}");
        Console.WriteLine($"Copyright:  {asm.Copyright()}");
        Console.WriteLine($"Company:  {asm.Company()}");
        Console.WriteLine($"Trademark:  {asm.Trademark()}");
        Console.WriteLine($"LangVersion:  {asm.LangVersion()}");
        Console.WriteLine($"PackageProjectUrl:  {asm.ProjectUrl()}");
        Console.WriteLine($"SupportEmail:  {asm.SupportEmail()}");
    }
}