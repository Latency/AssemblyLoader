//  *****************************************************************************
//  File:       Program.cs
//  Solution:   AssemblyLoader
//  Project:    Tests
//  Date:       09/10/2017
//  Author:     Latency McLaughlin
//  Copywrite:  Bio-Hazard Industries - 1998-2017
//  *****************************************************************************

using System;
using System.IO;
using System.Reflection;
using AssemblyLoader;
using NUnit.Framework;

namespace Tests {
  public class Program {
    [Test]
    public void Test1() {
      // Dynamically find the embedded resource assembly included within the project to test against.
      var assembly = Assembly.GetExecutingAssembly();
      foreach (var s in assembly.GetManifestResourceNames())
        using (var stream = assembly.GetManifestResourceStream(s)) {
          if (stream != null) {
            // Test overload +1 - Stream
            var asm = stream.Assembly();
            Console.WriteLine(asm.FullName);

            asm = asm.GetName().Assembly();
            Console.WriteLine(asm.FullName);

            var data = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(data, 0, data.Length);
            var fileName = Path.GetTempPath() + s.Substring(s.IndexOf('.') + 1);
            if (File.Exists(fileName)) // Overwrite if desired  (depending on your needs)
              File.Delete(fileName);
            File.WriteAllBytes(fileName, data);
            // Test overload +4 - string
            asm = fileName.Assembly();
            Console.WriteLine(asm.FullName);
          }
        }
    }


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