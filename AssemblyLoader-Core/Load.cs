//  *****************************************************************************
//  File:       Load.cs
//  Solution:   AssemblyLoader
//  Project:    AssemblyLoader
//  Date:       09/10/2017
//  Author:     Latency McLaughlin
//  Copywrite:  Bio-Hazard Industries - 1998-2017
//  *****************************************************************************

using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace AssemblyLoader {
  public static class Load {
    /// <summary>
    ///   Load`
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static Assembly Assembly<T>(this T assembly) {
      Assembly myAssembly = null;
      var type = typeof(T);
      if (type == typeof(string)) {
        var assemblyPath = assembly as string;
        if (!string.IsNullOrEmpty(assemblyPath))
          myAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
      } else if (type == typeof(AssemblyName)) {
        var assemblyName = assembly as AssemblyName;
        if (assemblyName != null)
          myAssembly = AssemblyLoadContext.Default.LoadFromAssemblyName(assemblyName);
      } else if (type == typeof(Stream)) {
        var asmStream = assembly as Stream;
        if (asmStream != null)
          myAssembly = AssemblyLoadContext.Default.LoadFromStream(asmStream);
      }
      return myAssembly;
    }
  }
}