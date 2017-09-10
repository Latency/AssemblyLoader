//  *****************************************************************************
//  File:       Load.cs
//  Solution:   AssemblyInfo
//  Project:    AssemblyInfo
//  Date:       09/06/2017
//  Author:     Latency McLaughlin
//  Copywrite:  Bio-Hazard Industries - 1998-2017
//  *****************************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AssemblyInfo {
  public static class Load {
    /// <summary>
    ///   Load`
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static Assembly Assembly<T>(T assembly) {
      var assemblypath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
      // Each assemblypath (as it says..) is the full path to the assembly
      using (var isoDomain = new ProxyClass(assemblypath, Path.GetFileName(assemblypath) + ".domain", assemblypath + ".config")) {
        Assembly myAssembly = null;

        var type = typeof(T);
        if (type == typeof(string)) {
          var filePath = assembly as string;
          if (!string.IsNullOrEmpty(filePath)) {
            try {
              var data = File.ReadAllBytes(filePath);
              isoDomain.Dispose();
              myAssembly = Assembly(data);
            } catch (Exception) {
              // ignored
            }
          }
        } else if (type == typeof(byte[])) {
          var data = assembly as byte[];
          if (data != null && data.Length > 0) {
            try {
              myAssembly = System.Reflection.Assembly.Load(data);
            } catch (Exception) {
              // ignored
            }
          }
        } else if (type == typeof(AssemblyName)) {
          // This should always be null, since there are no embedded assemblies within AssemblyInfo or contains the name as that found in Tests. 
          // Since we are running these with VST in a proxy rather than assemblies loaded in the executing assembly, loaded assemblies can not be unloaded
          // without bringing down the domain.   There must be a host domain to call our methods.
          //
          // Leaving this one in for backwords compatability, which will mean that any local referenced assembly will need to be converted from <string>
          // to <AssemblyName> in order to be used.
          //
          // This should always fail as no dependancies exist.
          //
          // Therefore, I will overload the Assenbly.Load(<string>) to be an explicit file path rather than namespace display name assembly reference.
          var assemblyName = assembly as AssemblyName;
          if (assemblyName != null) {
            try {
              myAssembly = System.Reflection.Assembly.Load(assemblyName);
            } catch (Exception) {
              // ignored
            }
          }
        } else if (type == typeof(Stream)) {
          var asmStream = assembly as Stream;
          if (asmStream != null) {
            var data = new byte[asmStream.Length];
            asmStream.Read(data, 0, data.Length);
            isoDomain.Dispose();
            myAssembly = Assembly(data);
          }
        }
        return myAssembly;
      }
    }


    public static IEnumerable<Assembly> Assembly<T>(IEnumerable<T> obj) {
      // ReSharper disable once LoopCanBeConvertedToQuery
      foreach (var o in obj)
        yield return Assembly(o);
    }
  }
}