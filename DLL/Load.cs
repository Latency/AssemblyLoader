//  *****************************************************************************
//  File:      Load.cs
//  Solution:  AssemblyInfo
//  Project:   DLL
//  Date:      01/03/2016
//  Author:    Latency McLaughlin
//  Copywrite: Bio-Hazard Industries - 1998-2016
//  *****************************************************************************

using System;
using System.Reflection;

namespace AssemblyInfo {
  public static class Load {
    public static Assembly GetAssembly(string pAssemblyName) {
      if (string.IsNullOrEmpty(pAssemblyName))
        return null;
      var tMyAssembly = GetAssemblyEmbedded(pAssemblyName);
      if (tMyAssembly == null)
        GetAssemblyDLL(pAssemblyName);
      return tMyAssembly;
    }

    public static Assembly GetAssemblyEmbedded(string pAssemblyDisplayName) {
      Assembly tMyAssembly = null;
      if (string.IsNullOrEmpty(pAssemblyDisplayName))
        return null;
      try {
        tMyAssembly = Assembly.Load(pAssemblyDisplayName);
      }
      catch (Exception) {
        // ignored
      }
      return tMyAssembly;
    }

    public static Assembly GetAssemblyDLL(string pAssemblyNameDLL) {
      Assembly tMyAssembly = null;
      if (string.IsNullOrEmpty(pAssemblyNameDLL))
        return null;
      try {
        if (!pAssemblyNameDLL.ToLower().EndsWith(".dll"))
          pAssemblyNameDLL += ".dll";
        tMyAssembly = Assembly.LoadFrom(pAssemblyNameDLL);
      }
      catch (Exception) {
        // ignored
      }
      return tMyAssembly;
    }
  }
}