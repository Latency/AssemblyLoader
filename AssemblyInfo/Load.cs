//  *****************************************************************************
//  File:      Load.cs
//  Solution:  AssemblyInfo
//  Project:   DLL
//  Date:      01/03/2016
//  Author:    Latency McLaughlin
//  Copywrite: Bio-Hazard Industries - 1998-2017
//  *****************************************************************************

using System;
using System.Reflection;

namespace AssemblyInfo {
  public static class Load {
    /// <summary>
    ///  GetAssembly
    /// </summary>
    /// <param name="pAssemblyName"></param>
    /// <returns></returns>
    public static Assembly GetAssembly(string pAssemblyName) {
      if (string.IsNullOrEmpty(pAssemblyName))
        return null;
      var tMyAssembly = GetAssemblyEmbedded(pAssemblyName);
      if (tMyAssembly == null)
        GetAssemblyFile(pAssemblyName);
      return tMyAssembly;
    }


    /// <summary>
    ///  GetAssemblyEmbedded
    /// </summary>
    /// <param name="pAssemblyDisplayName"></param>
    /// <returns></returns>
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


    /// <summary>
    ///  GetAssemblyFile
    /// </summary>
    /// <param name="pAssemblyFileName"></param>
    /// <returns></returns>
    public static Assembly GetAssemblyFile(string pAssemblyFileName) {
      Assembly tMyAssembly = null;
      if (string.IsNullOrEmpty(pAssemblyFileName))
        return null;
      try {
        if (!pAssemblyFileName.ToLower().EndsWith(".dll"))
          pAssemblyFileName += ".dll";
        tMyAssembly = Assembly.LoadFrom(pAssemblyFileName);
      }
      catch (Exception) {
        // ignored
      }
      return tMyAssembly;
    }
  }
}