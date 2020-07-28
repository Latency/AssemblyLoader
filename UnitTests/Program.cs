// *****************************************************************************
// File:       Program.cs
// Solution:   AssemblyLoader
// Project:    Tests
// Date:       11/25/2018
// Author:     Latency McLaughlin
// Copywrite:  Bio-Hazard Industries - 1998-2018
// ***************************************************************************** 

using System;
using System.IO;
using System.Reflection;
using AssemblyLoader;
using NUnit.Framework;

namespace Tests {
  public class Program {
    [Test]
    public void Extensions() {
      var asm = Assembly.GetAssembly(typeof(Extensions));

      Console.WriteLine($"ProductTitle:  {asm.ProductTitle()}");
      Console.WriteLine($"AssemblyVersion:  {asm.AssemblyVersion()}");
      Console.WriteLine($"AssemblyFileVersion:  {asm.AssemblyFileVersion()}");
      Console.WriteLine($"Description:  {asm.Description()}");
      Console.WriteLine($"Product:  {asm.Product()}");
      Console.WriteLine($"Copyright:  {asm.Copyright()}");
      Console.WriteLine($"Company:  {asm.Company()}");
      Console.WriteLine($"Trademark:  {asm.Trademark()}");
    }
  }
}