// ****************************************************************************
// Project:  Unit Tests
// File:     TTest.cs
// Author:   Latency McLaughlin
// Date:     10/13/2026
// ****************************************************************************
// ReSharper disable InconsistentNaming

using AssemblyLoader;

namespace Unit_Tests;

public class Tests(ITestOutputHelper output)
{
    [Fact]
    public void TTest()
    {
        var asm = typeof(AssemblyInfo).Assembly;

        output.WriteLine($"ProductTitle:      {asm.Title()}");
        output.WriteLine($"AssemblyVersion:   {asm.Version()}");
        output.WriteLine($"Authors:           {asm.Authors()}");
        output.WriteLine($"Define Constants:  {asm.DefineConstants()}");
        output.WriteLine($"Description:       {asm.Description()}");
        output.WriteLine($"Product:           {asm.Product()}");
        output.WriteLine($"Copyright:         {asm.Copyright()}");
        output.WriteLine($"Company:           {asm.Company()}");
        output.WriteLine($"Trademark:         {asm.Trademark()}");
        output.WriteLine($"LangVersion:       {asm.LangVersion()}");
        output.WriteLine($"PackageProjectUrl: {asm.ProjectUrl()}");
        output.WriteLine($"SupportEmail:      {asm.SupportEmail()}");
    }
}
